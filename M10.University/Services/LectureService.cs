using StudyJournal.DataBase.Entities;
using StudyJournal.DataBase;
using Microsoft.Extensions.Logging;
using StudyJournal.University.Exceptions;
using Microsoft.EntityFrameworkCore;
using StudyJournal.DataBase.Data;

namespace StudyJournal.University
{
    public class LectureService : ILectureService
    {
        ILectureRepository _lectureRepository;
        ISpeakerRepository _speakerRepository;
        IStudyGroupRepository _studyGroupRepository;
        private readonly ILogger _logger;


        public LectureService(ILectureRepository lectureRepository, ISpeakerRepository speakerRepository, IStudyGroupRepository studyGroupRepository, ILogger<LectureService> logger)
        {
            _lectureRepository = lectureRepository;
            _logger = logger;
            _speakerRepository = speakerRepository;
            _studyGroupRepository = studyGroupRepository;
        }

        public IEnumerable<Lecture> GetAllLectures()
        {
            _logger.LogInformation("All lectures returned");
            return _lectureRepository.GetAllLectures();
        }
        public Lecture GetLectureByID(int lectureId)
        {
            _logger.LogInformation("Lecture returned");
            return _lectureRepository.GetLectureByID(lectureId);
        }
        public bool InsertLecture(Lecture lecture)
        {

            try
            {
                Speaker speaker = _speakerRepository.GetSpeakerByID((int)lecture.SpeakerID);
                StudyGroup studyGroup = _studyGroupRepository.GetStudyGroupByID((int)lecture.StudyGroupID);
                if (speaker == null || studyGroup == null || (lecture.SubjectID > 0 && lecture.SubjectID < 4))
                {
                    throw new LectureException("Incorrect input data: nonexistent SpeakerID, StudyGroupID or SubjectID");
                }
                else
                {
                    _lectureRepository.InsertLecture(lecture);
                }
            }
            catch (CustomException ex)
            {
                _logger.LogWarning("Lecture exception: incorrect input data: nonexistent SpeakerID, StudyGroupID or SubjectID");
                return false;

            }
            catch (Exception ex)
            {
                _logger.LogWarning("Something went wrong");
                return false;
            }
            _logger.LogInformation("Lecture created");
            return true;

        }
        public void DeleteLecture(int lectureId)
        {
            _logger.LogInformation("Lecture deleted (or already deleted)");
            _lectureRepository.DeleteLecture(lectureId);
        }
        public bool UpdateLecture(Lecture lecture)
        {

            try
            {
                Speaker speaker = _speakerRepository.GetSpeakerByID((int)lecture.SpeakerID);
                StudyGroup studyGroup = _studyGroupRepository.GetStudyGroupByID((int)lecture.StudyGroupID);
                if (speaker == null || studyGroup == null || (lecture.SubjectID > 0 && lecture.SubjectID < 4))
                {
                    throw new LectureException("Incorrect input data: nonexistent SpeakerID, StudyGroupID or SubjectID");
                }
                else
                {
                    _lectureRepository.UpdateLecture(lecture);
                }
            }
            catch (CustomException ex)
            {
                _logger.LogWarning("Lecture exception: incorrect input data: nonexistent SpeakerID, StudyGroupID or SubjectID");
                return false;

            }
            catch (Exception ex)
            {
                _logger.LogWarning("Something went wrong");
                return false;
            }
            _logger.LogInformation("Lecture updated");
            return true;
        }
    }
}
