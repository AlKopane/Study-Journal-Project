using StudyJournal.DataBase.Entities;
using StudyJournal.DataBase.Data;
using StudyJournal.DataBase;
using System.Xml.Serialization;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using StudyJournal.University.Exceptions;

namespace StudyJournal.University
{
    public class JournalService : IJournalService
    {
        private readonly IDbContextFactory<DataBaseContext> _contextFactory;
        IJournalRepository _journalRepository;
        ILectureRepository _lectureRepository;
        IStudentRepository _studentRepository;
        private readonly Models.MailSettings _mailSettings;
        private readonly ILogger<JournalService> _logger;

        public JournalService(IDbContextFactory<DataBaseContext> contextFactory, IJournalRepository journalRepository, ILectureRepository lectureRepository, IStudentRepository studentRepository, IOptions<Models.MailSettings> options, ILogger<JournalService> logger)
        {

            _contextFactory = contextFactory;
            _journalRepository = journalRepository;
            _lectureRepository = lectureRepository;
            _studentRepository = studentRepository;
            _logger = logger;
            _mailSettings = options.Value;
        }

        public bool CreateDefaultMarks(int lectureId, int studyGroupId)
        {

            try
            {
                using var _context = _contextFactory.CreateDbContext();
                Mark mark = _context.Marks.FirstOrDefault(s => s.LectureID == lectureId);
                Lecture lecture = _context.Lectures.FirstOrDefault(s => s.LectureID == lectureId);
                StudyGroup studyGroup = _context.StudyGroups.FirstOrDefault(s => s.StudyGroupID == lecture.StudyGroupID);

                if (mark != null || studyGroup.StudyGroupID != studyGroupId)
                {
                    throw new JournalException("Incorrect LectureID or StudyGroupID");
                }
                else
                {
                    _journalRepository.CreateDefaultMarks(lectureId, studyGroupId);
                }
            }
            catch (CustomException ex)
            {
                _logger.LogWarning("Journal exception: incorrect LectureID or StudyGroupID");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Something went wrong");
                return false;
            }
            _logger.LogInformation("Default marks created");
            return true;
        }

        public bool UpdateMark(Mark mark)
        {
            using var _context = _contextFactory.CreateDbContext();
            Student student = _context.Students.FirstOrDefault(s => s.StudentID == mark.StudentID);
            Lecture lecture = _context.Lectures.FirstOrDefault(s => s.LectureID == mark.LectureID);
            try
            {
                if (student == null || lecture == null)
                {
                    throw new JournalException("Incorrect StudentID or LectureID");
                }
                else
                {
                    _journalRepository.UpdateMark(mark);
                }

            }
            catch (CustomException ex)
            {
                _logger.LogWarning("Journal exception: incorrect StudentID or LectureID");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Something went wrong");
                return false;
            }
            _logger.LogInformation("Mark updated");
            return true;
        }


        public double CheckAverageMark(int studentId)
        {
            _logger.LogInformation("Average mark checked");
            return _journalRepository.CheckAverageMark(studentId);
        }
        public bool ChekAttending(int studentId)
        {
            _logger.LogInformation("Attending checked");
            return _journalRepository.ChekAttending(studentId);
        }
        public void SendSms(int studentId)
        {
            _logger.LogInformation("SMS sent");
            //Some actions for SMS sending
        }

        public Mark GetMarkById(int id)
        {
            _logger.LogInformation("Mark returned");
            return _journalRepository.GetMarkByID(id);
        }

        public Models.LectureReport GenerateLectureAttendingReport(int lectureId)
        {
            var lectureReport = new Models.LectureReport();
            var lecture = _lectureRepository.GetLectureByID(lectureId);

            lectureReport.Name = $"{lecture.LectureName}";
            lectureReport.Date = lecture.LectureDate;
            int? studyGroup = lecture.StudyGroupID;
            using var _context = _contextFactory.CreateDbContext();
            List<Student> studentsAtLecture = _context.Students.Where(s => s.StudyGroupID == studyGroup).ToList();
            List<Models.ShortStudent> shortStudents = new List<Models.ShortStudent>();

            foreach (Student student in studentsAtLecture)
            {
                Mark mark = _context.Marks.FirstOrDefault(s => s.StudentID == student.StudentID && s.LectureID == lectureId);
                var stud = new Models.ShortStudent();

                stud.StudentID = student.StudentID;
                stud.StudentNameSurname = $"{student.StudentName} {student.StudentSurname}";
                stud.WasAtLecture = mark.WasAtLecture;

                lectureReport.students.Add(stud);
            }
            _logger.LogInformation("Lecture attending report generated");
            return lectureReport;
        }

        public string GetLectureAttendingReportXml(int lectureId)
        {
            Models.LectureReport lectureReport = GenerateLectureAttendingReport(lectureId);

            string serializedXml;
            XmlSerializer serializer = new XmlSerializer(typeof(Models.LectureReport));
            using (StringWriter sw = new StringWriter())
            {
                serializer.Serialize(sw, lectureReport);
                serializedXml = sw.ToString();
            }
            _logger.LogInformation("Report type: XML");
            return serializedXml;
        }

        public string GetLectureAttendingReportJson(int lectureId)
        {
            Models.LectureReport lectureReport = GenerateLectureAttendingReport(lectureId);
            _logger.LogInformation("Report type: JSON");
            return System.Text.Json.JsonSerializer.Serialize(lectureReport);
        }

        public Models.StudentReport GenerateStudentAttendingReport(int studentId)
        {
            var studentReport = new Models.StudentReport();
            var student = _studentRepository.GetStudentByID(studentId);

            studentReport.StudentNameSurname = $"{student.StudentName} {student.StudentSurname}";
            studentReport.StudentId = studentId;
            using var _context = _contextFactory.CreateDbContext();
            List<Mark> marks = _context.Marks.Where(s => s.StudentID == studentId).ToList();

            foreach (var mark in marks)
            {
                var lect = new Models.ShortLecture();
                var lecture = _lectureRepository.GetLectureByID(mark.LectureID);
                lect.Name = lecture.LectureName;
                lect.Date = lecture.LectureDate;

                studentReport.lectures.Add(lect);
            }
            _logger.LogInformation("Student attending report generated");
            return studentReport;
        }
        public string GetStudentAttendingReportXml(int studentId)
        {
            Models.StudentReport studentReport = GenerateStudentAttendingReport(studentId);

            string serializedXml;
            XmlSerializer serializer = new XmlSerializer(typeof(Models.StudentReport));
            using (StringWriter sw = new StringWriter())
            {
                serializer.Serialize(sw, studentReport);
                serializedXml = sw.ToString();
            }
            _logger.LogInformation("Report type: XML");
            return serializedXml;
        }
        public string GetStudentAttendingReportJson(int studentId)
        {
            Models.StudentReport studentReport = GenerateStudentAttendingReport(studentId);
            _logger.LogInformation("Report type: JSON");
            return System.Text.Json.JsonSerializer.Serialize(studentReport);
        }

        public void SendEmail(int studentId)
        {
            var student = _studentRepository.GetStudentByID(studentId);
            if (student != null)
            {
                if (_journalRepository.ChekAttending(studentId))
                {
                    var mail = new Models.MailRequest();
                    mail.NameTo = $"{student.StudentName} {student.StudentSurname}";
                    mail.EmailTo = student.StudentEmail;
                    mail.Message = $"Dear {mail.NameTo}, you have a lot of missed lectures. You can be expelled from the course! University administration.";

                    EmailSender(mail);
                    _logger.LogInformation("Email sent");
                }
            }
        }

        public async Task EmailSender(Models.MailRequest mail)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));
            message.To.Add(new MailboxAddress(mail.NameTo, mail.EmailTo));
            message.Subject = mail.Subject;
            message.Body = new TextPart("plain")
            {
                Text = mail.Message
            };
            using var client = new SmtpClient();
            await client.ConnectAsync(_mailSettings.Host, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            _logger.LogInformation("Successfully connected to smtp host");

            await client.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
            _logger.LogInformation("Email sender returned");
        }
    }
}
