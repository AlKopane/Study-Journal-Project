using System.ComponentModel.DataAnnotations.Schema;

namespace StudyJournal.DataBase.Entities;

public class Lecture
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LectureID { get; set; }
    public string LectureName { get; set; }
    public string LectureDescription { get; set; }
    public DateTime LectureDate { get; set; }

    public int? StudyGroupID { get; set; }
    public StudyGroup? StudyGroup { get; set; }
    public int? SpeakerID { get; set; }
    public Speaker? Speaker { get; set; }
    public int? SubjectID { get; set; }
    public Subject? Subject { get; set; }
   
    public Homework? Homework { get; set; }
    public List<Mark>? Marks { get; set; }
}
