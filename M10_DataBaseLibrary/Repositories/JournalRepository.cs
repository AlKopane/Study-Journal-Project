using StudyJournal.DataBase.Entities;
using StudyJournal.DataBase.Data;
using Microsoft.EntityFrameworkCore;

namespace StudyJournal.DataBase
{
    public class JournalRepository : IJournalRepository
    {
        private readonly IDbContextFactory<DataBaseContext> _contextFactory;

        public JournalRepository(IDbContextFactory<DataBaseContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void CreateDefaultMarks(int lectureId, int studyGroupId)
        {
            using var _context = _contextFactory.CreateDbContext();
            List<Student> students;
            students = _context.Students.Where(s => s.StudyGroupID == studyGroupId).ToList();
            List<int> studentIds = new List<int>();
            for (int i = 0; i < students.Count; i++)
            {
                studentIds.Add(students[i].StudentID);
            }
            for (int i = 0; i < studentIds.Count; i++)
            {
                Mark mark = new Mark();
                mark.StudentID = studentIds[i];
                mark.LectureID = lectureId;
                _context.Marks.Add(mark);
                _context.SaveChanges();
            }
        }

        public void UpdateMark(Mark mark)
        {
            using var _context = _contextFactory.CreateDbContext();
            _context.Entry(mark).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public double CheckAverageMark(int studentId)
        {
            using var _context = _contextFactory.CreateDbContext();
            List<Mark> marks = _context.Marks.Where(s => s.StudentID == studentId).ToList();
            List<int> markValues = new List<int>();
            foreach (Mark mark in marks)
            {
                markValues.Add(mark.MarkValue);
            }
            double result = markValues.Average();

            return result;
        }
        public bool ChekAttending(int studentId)
        {
            using var _context = _contextFactory.CreateDbContext();
            var chekStudentAttending = _context.Marks.FirstOrDefault(s=>s.StudentID == studentId);
            if (chekStudentAttending != null)
            {
                int count = 0;
                const int missedLectures = 3;

                List<Mark> marks = _context.Marks.Where(s => s.StudentID == studentId).ToList();
                List<bool> attending = new List<bool>();
                foreach (Mark mark in marks)
                {
                    attending.Add(mark.WasAtLecture);
                }

                foreach (bool a in attending)
                {
                    if (!a)
                    {
                        count++;
                    }
                }

                if (count >= missedLectures && !attending.Last())
                {
                    return true;
                }
                               
            }
            return false;
        }
        
        public Mark GetMarkByID(int id)
        {
            using var _context = _contextFactory.CreateDbContext();
            return _context.Marks.Find(id);
        }

    }
}
