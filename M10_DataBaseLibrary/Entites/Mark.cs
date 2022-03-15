using System.ComponentModel.DataAnnotations.Schema;

namespace StudyJournal.DataBase.Entities;

public class Mark
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MarkID { get; set; }
    public int StudentID { get; set; }
    public Student Student { get; set; }
    public int LectureID { get; set; }
    public Lecture Lecture { get; set; }
    public bool WasAtLecture { get; set; }
    public int MarkValue { get; set; }
    
   
}
