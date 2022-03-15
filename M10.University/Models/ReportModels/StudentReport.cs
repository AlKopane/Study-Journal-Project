namespace StudyJournal.University.Models
{
    public class StudentReport
    {
        public int StudentId { get; set; }
        public string StudentNameSurname { get; set; }
        public List<ShortLecture> lectures { get; set; } = new List<ShortLecture>();
    }
}
