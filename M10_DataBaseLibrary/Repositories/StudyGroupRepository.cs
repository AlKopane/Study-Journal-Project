using StudyJournal.DataBase.Entities;
using StudyJournal.DataBase.Data;
using Microsoft.EntityFrameworkCore;

namespace StudyJournal.DataBase
{
    public class StudyGroupRepository : IStudyGroupRepository
    {
        private readonly IDbContextFactory<DataBaseContext> _contextFactory;
        public StudyGroupRepository(IDbContextFactory<DataBaseContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<StudyGroup> GetAllStudyGroups()
        {
            using var _context = _contextFactory.CreateDbContext();
            return _context.StudyGroups.ToList();
        }
        public StudyGroup GetStudyGroupByID(int studyGroupId)
        {
            using var _context = _contextFactory.CreateDbContext();
            return _context.StudyGroups.Find(studyGroupId);
        }
        public void InsertStudyGroup(StudyGroup studyGroup)
        {
            using var _context = _contextFactory.CreateDbContext();
            _context.StudyGroups.Add(studyGroup);
            _context.SaveChanges();
        }
        public void DeleteStudyGroup(int studyGroupId)
        {
            using var _context = _contextFactory.CreateDbContext();
            StudyGroup studyGroup = _context.StudyGroups.Find(studyGroupId);
            _context.StudyGroups.Remove(studyGroup);
            _context.SaveChanges();
        }
        public void UpdateStudyGroup(StudyGroup studyGroup)
        {
            using var _context = _contextFactory.CreateDbContext();
            _context.Entry(studyGroup).State = EntityState.Modified;
            _context.SaveChanges();
        }
        
    }
}
