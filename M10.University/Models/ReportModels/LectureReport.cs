namespace StudyJournal.University.Models
{
    public class LectureReport
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public List<ShortStudent> students { get; set; } = new List<ShortStudent>();
    }
}
