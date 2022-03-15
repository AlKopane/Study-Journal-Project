namespace StudyJournal.University.Models
{
    public class Lecture
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int? GroupId { get; set; }
        public int? SpeakerId { get; set; }
        public int? SubjectId { get; set; }
    }
}
