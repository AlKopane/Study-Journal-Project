using System.ComponentModel.DataAnnotations.Schema;

namespace StudyJournal.DataBase.Entities;

public class Student
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int StudentID { get; set; }
    public string StudentName { get; set; }
    public string StudentSurname { get; set; }
    public string StudentEmail { get; set; }
    public string StudentPhone { get; set; }
    public int? StudyGroupID { get; set; }
    public StudyGroup? StudyGroup { get; set; }
    public List<Mark> Marks { get; set; }
    

}
