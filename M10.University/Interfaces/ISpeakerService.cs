using StudyJournal.DataBase.Entities;

namespace StudyJournal.University
{
    public interface ISpeakerService
    {
        IEnumerable<Speaker> GetAllSpeakers();
        Speaker GetSpeakerByID(int speakerId);
        void InsertSpeaker(Speaker speaker);
        void DeleteSpeaker(int speakerId);
        void UpdateSpeaker(Speaker speaker);
    }
}
