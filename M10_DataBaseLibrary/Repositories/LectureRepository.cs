using StudyJournal.DataBase.Entities;
using StudyJournal.DataBase.Data;
using Microsoft.EntityFrameworkCore;

namespace StudyJournal.DataBase
{
    public class LectureRepository : ILectureRepository
    {
        private readonly IDbContextFactory<DataBaseContext> _contextFactory;

        public LectureRepository(IDbContextFactory<DataBaseContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

       public IEnumerable<Lecture> GetAllLectures()
        {
            using var _context = _contextFactory.CreateDbContext();
            return _context.Lectures.ToList();
        }
       public Lecture GetLectureByID(int lectureId)
        {
            using var _context = _contextFactory.CreateDbContext();
            return _context.Lectures.Find(lectureId);
        }
       public void InsertLecture(Lecture lecture)
        {
            using var _context = _contextFactory.CreateDbContext();
            _context.Lectures.Add(lecture);
            _context.SaveChanges();
        }
       public void DeleteLecture(int lectureId)
        {
            using var _context = _contextFactory.CreateDbContext();
            Lecture lecture = _context.Lectures.Find(lectureId);
            _context.Lectures.Remove(lecture);
            _context.SaveChanges();
        }
       public void UpdateLecture(Lecture lecture)
        {
            using var _context = _contextFactory.CreateDbContext();
            _context.Entry(lecture).State = EntityState.Modified;
            _context.SaveChanges();
        }

    }
}
