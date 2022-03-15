using StudyJournal.DataBase.Entities;

namespace StudyJournal.DataBase
{
    public interface IStudyGroupRepository
    {
        IEnumerable<StudyGroup> GetAllStudyGroups();
        StudyGroup GetStudyGroupByID(int studyGroupId);
        void InsertStudyGroup(StudyGroup studyGroup);
        void DeleteStudyGroup(int studyGroupId);
        void UpdateStudyGroup(StudyGroup studyGroup);
    }
}
