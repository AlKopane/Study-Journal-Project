using System;
using System.Collections.Generic;
using StudyJournal.DataBase.Entities;

namespace StudyJournal.DataBase
{
    public interface ISpeakerRepository
    {
        IEnumerable<Speaker> GetAllSpeakers();
        Speaker GetSpeakerByID(int speakerId);
        void InsertSpeaker(Speaker speaker);
        void DeleteSpeaker(int speakerId);
        void UpdateSpeaker(Speaker speaker);
    }
}
