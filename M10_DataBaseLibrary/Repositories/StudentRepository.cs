using StudyJournal.DataBase.Entities;
using StudyJournal.DataBase.Data;
using Microsoft.EntityFrameworkCore;

namespace StudyJournal.DataBase
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IDbContextFactory<DataBaseContext> _contextFactory;

        public StudentRepository(IDbContextFactory<DataBaseContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public IEnumerable<Student> GetAllStudents()
        {
            using var _context = _contextFactory.CreateDbContext();
            return _context.Students.ToList();
        }
        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            using var _context = _contextFactory.CreateDbContext();
            return _context.Students.ToList();
        }
        public Student GetStudentByID(int studentId)
        {
            using var _context = _contextFactory.CreateDbContext();
            return _context.Students.Find(studentId);
        }
        public void InsertStudent(Student student)
        {
            using var _context = _contextFactory.CreateDbContext();
            _context.Students.Add(student);
            _context.SaveChanges();
        }
        public void DeleteStudent(int studentId)
        {
            using var _context = _contextFactory.CreateDbContext();
            Student student = _context.Students.Find(studentId);
            _context.Students.Remove(student);
            _context.SaveChanges();
        }
        public void UpdateStudent(Student student)
        {
            using var _context = _contextFactory.CreateDbContext();
            _context.Entry(student).State = EntityState.Modified;
            _context.SaveChanges();
        }

        
        
    }
}
