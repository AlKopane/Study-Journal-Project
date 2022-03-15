using StudyJournal.DataBase.Entities;
using StudyJournal.DataBase.Data;
using Microsoft.EntityFrameworkCore;

namespace StudyJournal.DataBase
{
    public class SpeakerRepository : ISpeakerRepository
    {
        private readonly IDbContextFactory<DataBaseContext> _contextFactory;

        public SpeakerRepository(IDbContextFactory<DataBaseContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public IEnumerable<Speaker> GetAllSpeakers()
        {
            using var _context = _contextFactory.CreateDbContext();
            return _context.Speakers.ToList();
        }
        public Speaker GetSpeakerByID(int speakerId)
        {
            using var _context = _contextFactory.CreateDbContext();
            return _context.Speakers.Find(speakerId);
        }
        public void InsertSpeaker(Speaker speaker)
        {
            using var _context = _contextFactory.CreateDbContext();
            _context.Speakers.Add(speaker);
            _context.SaveChanges();
        }
        public void DeleteSpeaker(int speakerId)
        {
            using var _context = _contextFactory.CreateDbContext();
            Speaker speaker = _context.Speakers.Find(speakerId);
            _context.Speakers.Remove(speaker);
            _context.SaveChanges();
        }
        public void UpdateSpeaker(Speaker speaker)
        {
            using var _context = _contextFactory.CreateDbContext();
            _context.Entry(speaker).State = EntityState.Modified;
            _context.SaveChanges();
        }

    }
}
