using System.ComponentModel.DataAnnotations.Schema;

namespace StudyJournal.DataBase.Entities;

public class Speaker
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SpeakerID { get; set; }
    public string SpeakerName { get; set; }
    public string SpeakerSurname { get; set; }
    public string SpeakerEmail { get; set; }
    public List<Lecture> Lectures { get; set;}
}
