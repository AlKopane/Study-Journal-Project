using Microsoft.AspNetCore.Mvc;
using StudyJournal.DataBase;
using StudyJournal.DataBase.Data;
using StudyJournal.University;
using StudyJournal.University.Models;
using Microsoft.EntityFrameworkCore;

namespace StudyJournal.WebAPI.Controllers;

[ApiController]
[Route("[SpeakerController]")]
public class SpeakerController : ControllerBase
{
    private readonly ILogger<SpeakerController> _logger;

    private readonly IDbContextFactory<DataBaseContext> _contextFactory;
    private ISpeakerService _speakerService;


    public SpeakerController(ILogger<SpeakerController> logger, IDbContextFactory<DataBaseContext> contextFactory, ISpeakerService speakerService)
    {
        _logger = logger;

        _contextFactory = contextFactory;
        _speakerService = speakerService;
    }

    [HttpGet]
    [Route("/GetAllSpeakers")]
    public IEnumerable<Speaker> GetAllSpeakers()
    {
        List<StudyJournal.DataBase.Entities.Speaker> speakers = _speakerService.GetAllSpeakers().ToList();
        List<Speaker> result = new List<Speaker>();
        for (int i = 0; i < speakers.Count; i++)
        {
            result.Add(Map.SpeakerEntityToModel(speakers[i]));
        }

        return result;
    }

    [HttpGet]
    [Route("/Speaker/{id}")]
    public ActionResult<Speaker> GetSpeaker(int id)
    {
        var speaker = _speakerService.GetSpeakerByID(id);

        if (speaker != null)
        {
            Speaker result = Map.SpeakerEntityToModel(speaker);
            return Ok(result);
        }

        return NotFound();
    }

    [HttpPut]
    [Route("/Speaker/{id}")]
    public ActionResult UpdateSpeaker(int id, Speaker speakerPut)
    {
        var speaker = _speakerService.GetSpeakerByID(id);
        if (speaker != null)
        {
            if (!string.IsNullOrEmpty(speakerPut.Name) && speakerPut.Name != "string")
            {
                speaker.SpeakerName = speakerPut.Name;
            }
            if (!string.IsNullOrEmpty(speakerPut.Surname) && speakerPut.Surname != "string")
            {
                speaker.SpeakerSurname = speakerPut.Surname;
            }
            if (!string.IsNullOrEmpty(speakerPut.Email) && speakerPut.Email != "string")
            {
                speaker.SpeakerEmail = speakerPut.Email;
            }

            _speakerService.UpdateSpeaker(speaker);
            return Ok($"Speaker updated (Speaker Id: {speaker.SpeakerID})");
        }
        return NotFound("Speaker not found");
    }

    [HttpPost]
    [Route("/Speaker")]
    public ActionResult CreateSpeaker(Speaker speaker)
    {
        StudyJournal.DataBase.Entities.Speaker sp = Map.SpeakerModelToEntity(speaker);
        _speakerService.InsertSpeaker(sp);
        return Ok("Speaker created");
    }

    [HttpDelete]
    [Route("/Speaker/{id}")]
    public ActionResult DeleteSpeaker(int id)
    {

        var speaker = _speakerService.GetSpeakerByID(id);
        if (speaker != null)
        {
            _speakerService.DeleteSpeaker(id);
            return Ok();
        }
        return Ok("Already deleted");

    }


}
