using StudyJournal.DataBase.Entities;

namespace StudyJournal.University
{
    public interface IStudyGroupService
    {
        IEnumerable<StudyGroup> GetAllStudyGroups();
        StudyGroup GetStudyGroupByID(int studyGroupId);
        void InsertStudyGroup(StudyGroup studyGroup);
        void DeleteStudyGroup(int studyGroupId);
        void UpdateStudyGroup(StudyGroup studyGroup);
    }
}
