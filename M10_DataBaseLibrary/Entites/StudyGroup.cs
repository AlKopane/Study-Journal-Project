using System.ComponentModel.DataAnnotations.Schema;

namespace StudyJournal.DataBase.Entities;

public class StudyGroup
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int StudyGroupID { get; set; }
    public string StudyGroupName { get; set; }
    public List<Student> Students { get; set; }
    public List<Lecture> Lectures { get; set; }
}
