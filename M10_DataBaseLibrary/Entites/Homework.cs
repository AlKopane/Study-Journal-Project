using System.ComponentModel.DataAnnotations.Schema;

namespace StudyJournal.DataBase.Entities;

public class Homework
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int HomeworkID { get; set; }
    public string HomeworkName { get; set; }
    public string HomeworkDescription { get; set; }
    public int LectureID { get; set; }
    public Lecture Lecture { get; set; }
    
}
