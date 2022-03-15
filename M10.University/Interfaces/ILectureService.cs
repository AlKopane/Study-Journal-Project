using StudyJournal.DataBase.Entities;

namespace StudyJournal.University
{
    public interface ILectureService
    {
        IEnumerable<Lecture> GetAllLectures();
        Lecture GetLectureByID(int lectureId);
        bool InsertLecture(Lecture lecture);
        void DeleteLecture(int lectureId);
        bool UpdateLecture(Lecture lecture);
    }
}
