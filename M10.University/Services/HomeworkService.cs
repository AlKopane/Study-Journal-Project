using StudyJournal.DataBase.Entities;
using StudyJournal.DataBase;
using Microsoft.Extensions.Logging;
using StudyJournal.University.Exceptions;
using Microsoft.EntityFrameworkCore;
using StudyJournal.DataBase.Data;

namespace StudyJournal.University
{
    public class HomeworkService : IHomeworkService
    {
        private readonly IHomeworkRepository _homeworkRepository;
        private readonly ILogger _logger;
        private readonly IDbContextFactory<DataBaseContext> _contextFactory;

        public HomeworkService(IHomeworkRepository homeworkRepository, ILogger<HomeworkService> logger, IDbContextFactory<DataBaseContext> contextFactory)
        {
            _homeworkRepository = homeworkRepository;
            _logger = logger;
            _contextFactory = contextFactory;
        }


        public IEnumerable<Homework> GetAllHomeworks()
        {
            _logger.LogInformation("All homeworks returned");
            return _homeworkRepository.GetAllHomeworks();
        }
        public Homework GetHomeworkByID(int homeworkId)
        {
            _logger.LogInformation("Homework returned");
            return _homeworkRepository.GetHomeworkByID(homeworkId);
        }
        public bool InsertHomework(Homework homework)
        {
            try
            {
                using var _context = _contextFactory.CreateDbContext();
                Lecture lect = _context.Lectures.FirstOrDefault(s => s.Homework.LectureID == homework.LectureID);

                if (lect != null)
                {
                    throw new HomeworkException("Incorrect LectureID");
                }
                else
                {
                    _homeworkRepository.InsertHomework(homework);
                }
            }
            catch (CustomException ex)
            {
                _logger.LogWarning("Homework exception: incorrect LectureID");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Something went wrong");
                return false;
            }
            _logger.LogInformation("Homework created");
            return true;
        }
        public void DeleteHomework(int homeworkId)
        {
            _logger.LogInformation("Homework deleted (or already deleted)");
            _homeworkRepository.DeleteHomework(homeworkId);
        }
        public bool UpdateHomework(Homework homework)
        {
            try
            {
                using var _context = _contextFactory.CreateDbContext();
                Lecture lect = _context.Lectures.FirstOrDefault(s => s.Homework.LectureID == homework.LectureID);

                if (lect != null)
                {
                    throw new HomeworkException("Incorrect LectureID");
                }
                else
                {
                    _homeworkRepository.UpdateHomework(homework);
                }
            }
            catch (CustomException ex)
            {
                _logger.LogWarning("Homework exception: incorrect LectureID");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Something went wrong");
                return false;
            }
            _logger.LogInformation("Homework updated");
            return true;
        }
    }
}
