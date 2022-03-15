using StudyJournal.DataBase.Entities;
using StudyJournal.DataBase.Data;
using StudyJournal.DataBase;
using Microsoft.Extensions.Logging;

namespace StudyJournal.University
{
    public class SpeakerService : ISpeakerService
    {
        ISpeakerRepository _speakerRepository;
        private readonly ILogger _logger;

        public SpeakerService(ISpeakerRepository speakerRepository, ILogger<SpeakerService> logger)
        {
           _speakerRepository = speakerRepository;
            _logger = logger;
        }

        public IEnumerable<Speaker> GetAllSpeakers()
        {
            _logger.LogInformation("All speakers returned");
            return _speakerRepository.GetAllSpeakers();
        }
        public Speaker GetSpeakerByID(int speakerId)
        {
            _logger.LogInformation("Speaker returned");
            return _speakerRepository.GetSpeakerByID(speakerId);
        }
        public void InsertSpeaker(Speaker speaker)
        {
            _logger.LogInformation("Speaker created");
            _speakerRepository.InsertSpeaker(speaker);
        }
        public void DeleteSpeaker(int speakerId)
        {
            _logger.LogInformation("Speaker deleted (or already deleted)");
            _speakerRepository.DeleteSpeaker(speakerId);
        }
        public void UpdateSpeaker(Speaker speaker)
        {
            _logger.LogInformation("Speaker updated");
            _speakerRepository.UpdateSpeaker(speaker);
        }
    }
}
