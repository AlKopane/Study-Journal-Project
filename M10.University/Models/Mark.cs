namespace StudyJournal.University.Models
{
    public class Mark
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int LectureId { get; set; }
        public bool WasAtLecture { get; set; } = false;
        public int MarkValue { get; set; } = 0;
    }
}
