using Microsoft.AspNetCore.Mvc;
using StudyJournal.DataBase;
using StudyJournal.DataBase.Data;
using StudyJournal.University;
using StudyJournal.University.Models;
using Microsoft.EntityFrameworkCore;

namespace StudyJournal.WebAPI.Controllers;

[ApiController]
[Route("[StudyGroupController]")]
public class StudyGroupController : ControllerBase
{
    private readonly ILogger<StudyGroupController> _logger;

    private readonly IDbContextFactory<DataBaseContext> _contextFactory;
    private IStudyGroupService _studyGroupService;

    public StudyGroupController(ILogger<StudyGroupController> logger, IDbContextFactory<DataBaseContext> contextFactory, IStudyGroupService studyGroupService)
    {
        _logger = logger;
        _contextFactory = contextFactory;
        _studyGroupService = studyGroupService;
    }

    [HttpGet]
    [Route("/GetAllStudyGroups")]
    public IEnumerable<StudyGroup> GetAllStudyGroups()
    {
        List<StudyJournal.DataBase.Entities.StudyGroup> studyGroups = _studyGroupService.GetAllStudyGroups().ToList();
        List<StudyGroup> result = new List<StudyGroup>();
        for (int i = 0; i < studyGroups.Count; i++)
        {
            result.Add(Map.StudyGroupEntityToModel(studyGroups[i]));
        }

        return result;
    }

    [HttpGet]
    [Route("/StudyGroup/{id}")]
    public ActionResult<StudyGroup> GetStudyGroup(int id)
    {
        var studyGroup = _studyGroupService.GetStudyGroupByID(id);
        if (studyGroup != null)
        {
            StudyGroup result = Map.StudyGroupEntityToModel(studyGroup);
            return Ok(result);
        }

        return NotFound();
    }

    [HttpPut]
    [Route("/StudyGroup/{id}")]
    public ActionResult<StudyGroup> UpdateStudyGroup(int id, StudyGroup studyGroupPut)
    {
        var studyGroup = _studyGroupService.GetStudyGroupByID(id);
        if (studyGroup != null)
        {
            if (!string.IsNullOrEmpty(studyGroupPut.Name) && studyGroupPut.Name!="string")
            {
                studyGroup.StudyGroupName = studyGroupPut.Name;
            }
            _studyGroupService.UpdateStudyGroup(studyGroup);

            return Ok($"Study Group updated (Study Group Id: {studyGroup.StudyGroupID})");
        }
        return NotFound("Study Group not found");

    }


    [HttpPost]
    [Route("/StudyGroup")]
    public ActionResult<StudyGroup> CreateStudyGroup(StudyGroup studyGroup)
    {
        StudyJournal.DataBase.Entities.StudyGroup st = Map.StudyGroupModelToEntity(studyGroup);
        _studyGroupService.InsertStudyGroup(st);
        return Ok("Study Group created");

    }

    [HttpDelete]
    [Route("/StudyGroup/{id}")]
    public ActionResult DeleteStudyGroup(int id)
    {

        var studyGroup = _studyGroupService.GetStudyGroupByID(id);
        if (studyGroup != null)
        {
            _studyGroupService.DeleteStudyGroup(id);
            return Ok();
        }
        return Ok("Already deleted");

    }


}
