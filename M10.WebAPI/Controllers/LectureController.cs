using Microsoft.AspNetCore.Mvc;
using StudyJournal.DataBase;
using StudyJournal.DataBase.Data;
using StudyJournal.University;
using StudyJournal.University.Models;
using Microsoft.EntityFrameworkCore;

namespace StudyJournal.WebAPI.Controllers;

[ApiController]
[Route("[LectureController]")]
public class LectureController : ControllerBase
{


    private readonly ILogger<LectureController> _logger;
    private ILectureService _lectureService;
    private readonly IDbContextFactory<DataBaseContext> _contextFactory;

    public LectureController(ILogger<LectureController> logger, IDbContextFactory<DataBaseContext> contextFactory, ILectureService lectureService)
    {
        _logger = logger;

        _contextFactory = contextFactory;
        _lectureService = lectureService;
    }

    [HttpGet]
    [Route("/GetAllLectures")]
    public IEnumerable<Lecture> GetAllLectures()
    {
        List<StudyJournal.DataBase.Entities.Lecture> lectures = _lectureService.GetAllLectures().ToList();
        List<Lecture> result = new List<Lecture>();
        for (int i = 0; i < lectures.Count; i++)
        {
            result.Add(Map.LectureEntityToModel(lectures[i]));
        }
        return result;
    }

    [HttpGet]
    [Route("/Lecture/{id}")]
    public ActionResult<Lecture> GetLecture(int id)
    {
        var lecture = _lectureService.GetLectureByID(id);
        if (lecture != null)
        {
            Lecture result = Map.LectureEntityToModel(lecture);
            return Ok(result);
        }
        return NotFound("Lecture not found");
    }

    [HttpPut]
    [Route("/Lecture/{id}")]
    public ActionResult<Lecture> LectureUpdate(int id, Lecture lecturePut)
    {
        var lecture = _lectureService.GetLectureByID(id);
        if (lecture != null)
        {
            if (!string.IsNullOrEmpty(lecturePut.Name) && lecturePut.Name != "string")
            {
                lecture.LectureName = lecturePut.Name;
            }
            if (lecturePut.Date != default)
            {
                lecture.LectureDate = lecturePut.Date;
            }
            if (string.IsNullOrEmpty(lecturePut.Description) && lecturePut.Description != "string")
            {
                lecture.LectureDescription = lecturePut.Description;
            }
            if (lecturePut.GroupId > 0 && lecturePut.GroupId < 15)
            {
                lecture.StudyGroupID = lecturePut.GroupId;
            }
            if (lecturePut.SpeakerId > 0 && lecturePut.SpeakerId < 15)
            {
                lecture.SpeakerID = lecturePut.SpeakerId;
            }
            if (lecturePut.SubjectId > 0)
            {
                lecture.SubjectID = lecturePut.SubjectId;
            }

            if (!_lectureService.UpdateLecture(lecture))
            {
                return BadRequest("Something went wrong: incorrect input data (nonexistent SpeakerID, StudyGroupID or SubjectID)");
            }
            else
            {
                return Ok($"Lecture updated (Lecture Id = {lecture.LectureID}");
            }
        }
        return NotFound("Lecture not found");
    }

    [HttpPost]
    [Route("/Lecture")]
    public ActionResult<Lecture> CreateNewLecture(Lecture lecture)
    {

        StudyJournal.DataBase.Entities.Lecture lect = Map.LectureModelToEntity(lecture);

        if (!_lectureService.InsertLecture(lect))
        {
            return BadRequest("Something went wrong: incorrect input data (nonexistent SpeakerID, StudyGroupID or SubjectID)");
        }
        else
        {
            return Ok("Lecture created");
        }
    }


    [HttpDelete]
    [Route("/Lecture/{id}")]
    public ActionResult DeleteLecture(int id)
    {

        var lecture = _lectureService.GetLectureByID(id);
        if (lecture != null)
        {
            _lectureService.DeleteLecture(id);
            return Ok();
        }
        return Ok("Already deleted");
    }


}
