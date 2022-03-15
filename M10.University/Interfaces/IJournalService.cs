using StudyJournal.DataBase.Entities;

namespace StudyJournal.University
{
    public interface IJournalService
    {
        bool CreateDefaultMarks(int lectureId, int studyGroupId);
        bool UpdateMark(Mark mark);
        public double CheckAverageMark(int studentId);
        public bool ChekAttending(int studentId);
        public void SendSms(int studentId);
        public Mark GetMarkById(int id);
        public Models.LectureReport GenerateLectureAttendingReport(int lectureId);
        public string GetLectureAttendingReportXml(int lectureId);
        public string GetLectureAttendingReportJson(int lectureId);
        public Models.StudentReport GenerateStudentAttendingReport(int studentId);
        public string GetStudentAttendingReportXml(int studentId);
        public string GetStudentAttendingReportJson(int studentId);
        public Task EmailSender(Models.MailRequest mail);
        public void SendEmail(int studentId);
    }
}
