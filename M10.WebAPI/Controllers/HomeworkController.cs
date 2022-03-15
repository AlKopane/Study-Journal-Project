using Microsoft.AspNetCore.Mvc;
using StudyJournal.DataBase;
using StudyJournal.DataBase.Data;
using StudyJournal.University;
using StudyJournal.University.Models;
using Microsoft.EntityFrameworkCore;

namespace StudyJournal.WebAPI.Controllers;

[ApiController]
[Route("HomeworkController]")]
public class HomeworkController : ControllerBase
{
    private readonly ILogger<HomeworkController> _logger;
    private IHomeworkService _homeworkService;
    private readonly IDbContextFactory<DataBaseContext> _contextFactory;

    public HomeworkController(ILogger<HomeworkController> logger, IDbContextFactory<DataBaseContext> contextFactory, IHomeworkService homeworkService)
    {
        _logger = logger;
        _homeworkService = homeworkService;
        _contextFactory = contextFactory;
    }

    [HttpGet]
    [Route("/GetAllHomeworks")]
    public IEnumerable<Homework> GetAllHomeworks()
    {
        List<StudyJournal.DataBase.Entities.Homework> homeworks = _homeworkService.GetAllHomeworks().ToList();
        List<Homework> result = new List<Homework>();
        for (int i = 0; i < homeworks.Count; i++)
        {
            result.Add(Map.HomeworkEntityToModel(homeworks[i]));
        }
        return result;
    }

    [HttpGet]
    [Route("/Homework/{id}")]
    public ActionResult<Homework> GetHomework(int id)
    {
        var homework = _homeworkService.GetHomeworkByID(id);
        if (homework != null)
        {
            Homework result = Map.HomeworkEntityToModel(homework);
            return Ok(result);
        }
        return NotFound("Homework not found");
    }

    [HttpPut]
    [Route("/Homework/{id}")]
    public ActionResult<Homework> UpdateHomework(int id, Homework homeworkPut)
    {
        var homework = _homeworkService.GetHomeworkByID(id);
        if (homework != null)
        {
            if (!string.IsNullOrEmpty(homeworkPut.Name) && homeworkPut.Name != "string")
            {
                homework.HomeworkName = homeworkPut.Name;
            }
            if (!string.IsNullOrEmpty(homeworkPut.Description) && homeworkPut.Description != "string")
            {
                homework.HomeworkDescription = homeworkPut.Description;
            }
            if (homeworkPut.LectureID > 0 && homeworkPut.LectureID <= 500)
            {
                homework.LectureID = homeworkPut.LectureID;
            }

            if (!_homeworkService.UpdateHomework(homework))
            {
                return BadRequest("Something went wrong: incorrect input LectureID");
            }
            else
            {
                return Ok($"Homework updated (Homework Id: {homework.HomeworkID})");
            }
        }
        return NotFound("Homework not found");
    }

    [HttpPost]
    [Route("/Homework")]
    public ActionResult<Homework> CreateHomework(Homework homework)
    {
        StudyJournal.DataBase.Entities.Homework hm = Map.HomeworkModelToEntity(homework);
        if (!_homeworkService.InsertHomework(hm))
        {
            return BadRequest("Something went wrong: incorrect input LectureID");
        }
        else
        {
            return Ok("Homework created");
        }
    }

    [HttpDelete]
    [Route("/Homework/{id}")]
    public ActionResult<Homework> DeleteHomework(int id)
    {
        var homework = _homeworkService.GetHomeworkByID(id);
        if (homework != null)
        {
            _homeworkService.DeleteHomework(id);
            return Ok();
        }
        return Ok("Already deleted");
    }

}
