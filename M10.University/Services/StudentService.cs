using StudyJournal.DataBase.Entities;
using StudyJournal.DataBase;
using Microsoft.Extensions.Logging;
using StudyJournal.University.Exceptions;
using Microsoft.EntityFrameworkCore;
using StudyJournal.DataBase.Data;


namespace StudyJournal.University
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IStudyGroupRepository _studyGroupRepository;
        private readonly ILogger _logger;

        public StudentService(IStudentRepository studentRepository, IStudyGroupRepository studyGroupRepository, ILogger<StudentService> logger)
        {
            _studentRepository = studentRepository;
            _logger = logger;

            _studyGroupRepository = studyGroupRepository;
        }


        public IEnumerable<Student> GetAllStudents()
        {
            _logger.LogInformation("All students returned");
            return _studentRepository.GetAllStudents();
        }

        public Student GetStudentByID(int studentId)
        {
            _logger.LogInformation("Student returned");
            return _studentRepository.GetStudentByID(studentId);
        }
        public bool InsertStudent(Student student)
        {


            try
            {
                StudyGroup studyGroup = _studyGroupRepository.GetStudyGroupByID((int)student.StudyGroupID);
                if (studyGroup == null)
                {
                    throw new StudentException("Incorrect SudyGroupID");
                }
                else
                {
                    _studentRepository.InsertStudent(student);
                }
            }
            catch (CustomException ex)
            {
                _logger.LogWarning("SudyGroup exception: incorrect SudyGroupID");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Something went wrong");
                return false;
            }
            _logger.LogInformation("Student created");
            return true;

        }
        public void DeleteStudent(int studentId)
        {
            _logger.LogInformation("Student deleted (or already deleted)");
            _studentRepository.DeleteStudent(studentId);
        }
        public bool UpdateStudent(Student student)
        {
            try
            {
                StudyGroup studyGroup = _studyGroupRepository.GetStudyGroupByID((int)student.StudyGroupID);
                if (studyGroup == null)
                {
                    throw new StudentException("Incorrect SudyGroupID");
                }
                else
                {
                    _studentRepository.UpdateStudent(student);
                }
            }
            catch (CustomException ex)
            {
                _logger.LogWarning("SudyGroup exception: incorrect SudyGroupID");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Something went wrong");
                return false;
            }
            _logger.LogInformation("Student updated");
            return true;

        }
    }
}
