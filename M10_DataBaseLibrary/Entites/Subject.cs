using System.ComponentModel.DataAnnotations.Schema;

namespace StudyJournal.DataBase.Entities;

public class Subject
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SubjectID { get; set; }
    public string SubjectName { get; set; }
    public List<Lecture> Lectures { get; set; }
}
