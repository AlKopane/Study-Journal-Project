using Microsoft.AspNetCore.Mvc;
using StudyJournal.DataBase;
using StudyJournal.DataBase.Data;
using StudyJournal.University;
using StudyJournal.University.Models;
using Microsoft.EntityFrameworkCore;

namespace StudyJournal.WebAPI.Controllers;

[ApiController]
[Route("[Student]")]
public class StudentController : ControllerBase
{
    private readonly ILogger<StudentController> _logger;
    private readonly IDbContextFactory<DataBaseContext> _contextFactory;
    private IStudentService _studentService;
    public StudentController(ILogger<StudentController> logger, IDbContextFactory<DataBaseContext> contextFactory, IStudentService studentService)

    {
        _logger = logger;
        _contextFactory = contextFactory;
        _studentService = studentService;
    }

    [HttpGet]
    [Route("/GetAllStudents")]
    public IEnumerable<Student> GetAllStudents()
    {
        var students = _studentService.GetAllStudents().ToList();
        List<Student> result = new List<Student>();
        for (int i = 0; i < students.Count; i++)
        {
            result.Add(Map.StudentEntityToModel(students[i]));
        }

        return result;
    }

    [HttpGet]
    [Route("/Student/{id}")]
    public ActionResult<Student> GetStudent(int id)
    {
        var student = _studentService.GetStudentByID(id);
        if (student != null)
        {
            Student result = Map.StudentEntityToModel(student);
            return Ok(result);
        }

        return NotFound();
    }


    [HttpGet]
    [Route("/StudentString/{id}")]
    public ActionResult<string> GetStudentString(int id)
    {
        var student = _studentService.GetStudentByID(id);
        if (student != null)
        {
            Student result = Map.StudentEntityToModel(student);
            return Ok(System.Text.Json.JsonSerializer.Serialize(student));
        }

        return NotFound();
    }

    [HttpPut]
    [Route("/Student/{id}")]
    public ActionResult UpdateStudent(int id, Student studentPut)
    {

        var student = _studentService.GetStudentByID(id);
        if (student != null)
        {
            if (!string.IsNullOrEmpty(studentPut.Name) && studentPut.Name != "string")
            {
                student.StudentName = studentPut.Name;
            }
            if (!string.IsNullOrEmpty(studentPut.Surname) && studentPut.Surname != "string")
            {
                student.StudentSurname = studentPut.Surname;
            }
            if (!string.IsNullOrEmpty(studentPut.Email) && studentPut.Email != "string")
            {
                student.StudentEmail = studentPut.Email;
            }
            if (!string.IsNullOrEmpty(studentPut.Phone) && studentPut.Phone != "string")
            {
                student.StudentPhone = studentPut.Phone;
            }
            if (studentPut.GroupId > 0 && studentPut.GroupId < 15)
            {
                student.StudyGroupID = studentPut.GroupId;
            }

            if(!_studentService.UpdateStudent(student))
            {
                return BadRequest("Something went wrong: incorrect input SudyGroupID");
            }
            return Ok($"Studen updated (Student Id: {student.StudentID})");
        }
        return NotFound("Student not found");

    }


    [HttpPost]
    [Route("/Student")]
    public ActionResult CreateStudent(Student student)
    {
        StudyJournal.DataBase.Entities.Student st = Map.StudentModelToEntity(student);
        if (!_studentService.InsertStudent(st))
        {
            return BadRequest("Something went wrong: incorrect input SudyGroupID");
        }
        else
        {
            return Ok("Student created");
        }

    }


    [HttpDelete]
    [Route("/Student/{id}")]
    public ActionResult DeleteStudent(int id)
    {
        var student = _studentService.GetStudentByID(id);
        if (student != null)
        {
            _studentService.DeleteStudent(id);
            return Ok();
        }
        return Ok("Already deleted");

    }


}
