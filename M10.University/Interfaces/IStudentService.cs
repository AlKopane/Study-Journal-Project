using StudyJournal.DataBase.Entities;


namespace StudyJournal.University
{
    public interface IStudentService
    {
        IEnumerable<Student> GetAllStudents();
        Student GetStudentByID(int studentId);
        bool InsertStudent(Student student);
        void DeleteStudent(int studentId);
        bool UpdateStudent(Student student);
    }
}
