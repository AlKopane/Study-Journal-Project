using StudyJournal.DataBase.Entities;
using StudyJournal.DataBase.Data;
using Microsoft.EntityFrameworkCore;

namespace StudyJournal.DataBase
{
    public class HomeworkRepository : IHomeworkRepository
    {
        private readonly IDbContextFactory<DataBaseContext> _contextFactory;

        public HomeworkRepository(IDbContextFactory<DataBaseContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<Homework> GetAllHomeworks()
        {
            using var _context = _contextFactory.CreateDbContext();
            return _context.Homeworks.ToList();
        }
        public Homework GetHomeworkByID(int homeworkId)
        {
            using var _context = _contextFactory.CreateDbContext();
            return _context.Homeworks.Find(homeworkId);
        }
        public void InsertHomework(Homework homework)
        {
            using var _context = _contextFactory.CreateDbContext();
            _context.Homeworks.Add(homework);
            _context.SaveChanges();
        }
        public void DeleteHomework(int homeworkId)
        {
            using var _context = _contextFactory.CreateDbContext();
            Homework homework = _context.Homeworks.Find(homeworkId);
            _context.Homeworks.Remove(homework);
            _context.SaveChanges();

        }
        public void UpdateHomework(Homework homework)
        {
            using var _context = _contextFactory.CreateDbContext();
            _context.Entry(homework).State = EntityState.Modified;
            _context.SaveChanges();
        }





    }
}
