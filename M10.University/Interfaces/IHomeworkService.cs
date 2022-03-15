using StudyJournal.DataBase.Entities;

namespace StudyJournal.University
{
    public interface IHomeworkService
    {
        IEnumerable<Homework> GetAllHomeworks();
        Homework GetHomeworkByID(int homeworkId);
        bool InsertHomework(Homework homework);
        void DeleteHomework(int homeworkId);
        bool UpdateHomework(Homework homework);
    }
}
