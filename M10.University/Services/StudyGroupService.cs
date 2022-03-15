using StudyJournal.DataBase.Entities;
using StudyJournal.DataBase.Data;
using StudyJournal.DataBase;
using Microsoft.Extensions.Logging;

namespace StudyJournal.University
{
    public class StudyGroupService : IStudyGroupService
    {
        IStudyGroupRepository _studyGroupRepository;
        private readonly ILogger _logger;

        public StudyGroupService(IStudyGroupRepository studyGroupRepository, ILogger<StudyGroupService> logger)
        {
            _studyGroupRepository = studyGroupRepository;
            _logger = logger;
        }

        public IEnumerable<StudyGroup> GetAllStudyGroups()
        {
            _logger.LogInformation("All study groups returned");
            return _studyGroupRepository.GetAllStudyGroups();
        }
        public StudyGroup GetStudyGroupByID(int studyGroupId)
        {
            _logger.LogInformation("Study group returned");
            return _studyGroupRepository.GetStudyGroupByID(studyGroupId);
        }
        public void InsertStudyGroup(StudyGroup studyGroup)
        {
            _logger.LogInformation("Study group created");
            _studyGroupRepository.InsertStudyGroup(studyGroup);
        }
        public void DeleteStudyGroup(int studyGroupId)
        {
            _logger.LogInformation("Study group deleted (or already deleted)");
            _studyGroupRepository.DeleteStudyGroup(studyGroupId);
        }
        public void UpdateStudyGroup(StudyGroup studyGroup)
        {
            _logger.LogInformation("Study group updated");
            _studyGroupRepository.UpdateStudyGroup(studyGroup);
        }
    }
}
