using Microsoft.AspNetCore.Mvc;
using StudyJournal.DataBase;
using StudyJournal.DataBase.Data;
using StudyJournal.University;
using StudyJournal.University.Models;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;

namespace StudyJournal.WebAPI.Controllers;


[ApiController]
[Route("[JournalController]")]
public class JournalController : ControllerBase
{
    public readonly double TresholdValue = 4;
    private readonly ILogger<JournalController> _logger;
    private readonly IDbContextFactory<DataBaseContext> _contextFactory;
    private IJournalService _journalService;
    private IStudentService _studentService;
    private ILectureService _lectureService;
    private readonly MailSettings _mailSettings;

    public JournalController(ILogger<JournalController> logger, IDbContextFactory<DataBaseContext> contextFactory, IJournalService journalService, IStudentService studentService, ILectureService lectureService, IOptions<MailSettings> options)
    {
        _mailSettings = options.Value;
        _logger = logger;
        _contextFactory = contextFactory;
        _journalService = journalService;
        _studentService = studentService;
        _lectureService = lectureService;
    }

    [HttpPost]
    [Route("/CreateDefaultMarksForLecture")]
    public ActionResult CreateDefaultMarks(int lectureId, int studyGroupId)
    {
        if (!_journalService.CreateDefaultMarks(lectureId, studyGroupId))
        {
            return BadRequest("Something went wrong: incorrect input LectureID or StudyGroupID");
        }
        else
        {
            return Ok();
        }
    }

    [HttpPut]
    [Route("/UpdateMark/{id}")]
    public ActionResult UpdateMark(int id, Mark markPut)
    {
        var mark = _journalService.GetMarkById(id);
        if (mark != null)
        {
            if (mark.LectureID > 0)
            {
                mark.LectureID = markPut.LectureId;
            }
            if (mark.StudentID > 0)
            {
                mark.StudentID = markPut.StudentId;
            }
            mark.WasAtLecture = markPut.WasAtLecture;
            if (markPut.MarkValue >= 0 && markPut.MarkValue <= 5)
            {
                mark.MarkValue = markPut.MarkValue;
            }

            if (!_journalService.UpdateMark(mark))
            {
                return BadRequest("Something went wrong: incorrect input StudentID or LectureID");
            }
            else
            {
                double averageMarkValue = _journalService.CheckAverageMark(mark.StudentID);
                bool IsNeedToSendEmail = _journalService.ChekAttending(mark.StudentID);

                if (averageMarkValue < TresholdValue && IsNeedToSendEmail == true)
                {
                    return Ok($"Average mark = {averageMarkValue}. Student missed the lecture more than 3 times and missed the last lecture. Need to send Email and SMS!");
                }
                else if (averageMarkValue < TresholdValue && IsNeedToSendEmail != true)
                {
                    return Ok($"Average mark = {averageMarkValue}. Need to send SMS!");
                }
                else if (averageMarkValue >= TresholdValue && IsNeedToSendEmail == true)
                {
                    return Ok("Student missed the lecture more than 3 times and missed the last lecture. Need to send Email!");
                }

                return Ok();
            }
        }

        return NotFound("Mark not found");
    }


    [HttpGet]
    [Route("/GetAverageMark")]
    public ActionResult<double> GetStudentAverageMark(int studentId)
    {
        var student = _studentService.GetStudentByID(studentId);
        if (student != null)
        {
            double averageMarkValue = _journalService.CheckAverageMark(studentId);
            return Ok(averageMarkValue);
        }

        return NotFound();
    }

    [HttpGet]
    [Route("/CheckAttending")]
    public ActionResult<double> CheckAttending(int studentId)
    {
        var student = _studentService.GetStudentByID(studentId);
        if (student != null)
        {
            if (_journalService.ChekAttending(studentId) == false)
            {
                return Ok("Student regularly attends lectures");
            }
            else if (_journalService.ChekAttending(studentId) == true)
            {
                return Ok("Student missed the lecture more than 3 times and missed the last lecture. Need to send Email!");
            }
        }

        return NotFound();
    }

    [HttpPost]
    [Route("/SendEmail")]
    public async Task<ActionResult> SendEmail(int studentId)
    {
        var student = _studentService.GetStudentByID(studentId);
        if (student != null)
        {
            if (_journalService.ChekAttending(studentId) == true)
            {
                _journalService.SendEmail(studentId);
                return Ok(System.Text.Json.JsonSerializer.Serialize(new { EmailStatus = "Email sent", StudentName = student.StudentName, student.StudentSurname, StudentEmail = student.StudentEmail }));
            }
            else
            {
                return Ok(System.Text.Json.JsonSerializer.Serialize(new { EmailStatus = "No need to send Email", StudentName = student.StudentName, student.StudentSurname }));
            }
        }

        return NotFound();
    }


    [HttpPost]
    [Route("/SendSMS")]
    public ActionResult SendSMS(int studentId)
    {
        var student = _studentService.GetStudentByID(studentId);
        if (student != null)
        {
            double averageMarkValue = _journalService.CheckAverageMark(studentId);
            if (averageMarkValue < TresholdValue)
            {
                _journalService.SendSms(studentId);
                return Ok(System.Text.Json.JsonSerializer.Serialize(new { averageMark = averageMarkValue, Phone = student.StudentPhone, SmsStatus = "SMS sent" }));
            }
            else
            {
                return Ok(System.Text.Json.JsonSerializer.Serialize(new { averageMark = averageMarkValue, Phone = student.StudentPhone, SmsStatus = "No need to send SMS" }));
            }
        }

        return NotFound();
    }

    [HttpGet]
    [Route("/LectureReportJson")]
    public ActionResult LectureReportJson(int lectureId)
    {
        var lecture = _lectureService.GetLectureByID(lectureId);
        if (lecture != null)
        {
            string result = _journalService.GetLectureAttendingReportJson(lectureId);
            return Ok(result);
        }

        return NotFound();
    }


    [HttpGet]
    [Route("/LectureReportXml")]
    public ActionResult LectureReportXml(int lectureId)
    {
        var lecture = _lectureService.GetLectureByID(lectureId);
        if (lecture != null)
        {
            string result = _journalService.GetLectureAttendingReportXml(lectureId);
            return Ok(result);
        }

        return NotFound();
    }

    [HttpGet]
    [Route("/StudentReportJson")]
    public ActionResult StudentReportJson(int studentId)
    {
        var student = _studentService.GetStudentByID(studentId);
        if (student != null)
        {
            string result = _journalService.GetStudentAttendingReportJson(studentId);
            return Ok(result);
        }

        return NotFound();
    }

    [HttpGet]
    [Route("/StudentReportXml")]
    public ActionResult StudentReportXml(int studentId)
    {
        var student = _studentService.GetStudentByID(studentId);
        if (student != null)
        {
            string result = _journalService.GetLectureAttendingReportXml(studentId);
            return Ok(result);
        }

        return NotFound();
    }
}
