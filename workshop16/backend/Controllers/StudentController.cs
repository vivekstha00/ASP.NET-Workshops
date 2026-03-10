using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Models;

namespace WeatherAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private static readonly List<Student> students = new()
    {
        new Student { Id = "NP01MS7A240036", Name = "Aarav Sharma", Age = 20, Course = "BSc CSIT" },
        new Student { Id = "NP01MS7A240037", Name = "Sita Rai", Age = 21, Course = "BCA" },
        new Student { Id = "NP01MS7A240038", Name = "Rohan Thapa", Age = 19, Course = "BIT" },
        new Student { Id = "NP01MS7A240039", Name = "Anisha Gurung", Age = 22, Course = "BIM" }
    };

    [HttpGet("getall")]
    public ActionResult<List<Student>> GetAll()
    {
        return Ok(students);
    }

    [HttpGet("{id}")]
    public ActionResult<Student> GetById(string id)
    {
        var student = students.FirstOrDefault(s => s.Id == id);

        if (student == null)
        {
            return NotFound(new { message = "Student not found" });
        }

        return Ok(student);
    }

    [HttpPost("add")]
    public ActionResult<Student> Add([FromBody] Student student)
    {
        if (string.IsNullOrWhiteSpace(student.Id) ||
            string.IsNullOrWhiteSpace(student.Name) ||
            string.IsNullOrWhiteSpace(student.Course) ||
            student.Age <= 0)
        {
            return BadRequest(new { message = "Invalid student data" });
        }

        var existingStudent = students.FirstOrDefault(s => s.Id == student.Id);
        if (existingStudent != null)
        {
            return BadRequest(new { message = "Student ID already exists" });
        }

        students.Add(student);
        return Ok(student);
    }

    [HttpPut("update")]
    public ActionResult<Student> Update([FromBody] Student student)
    {
        var existingStudent = students.FirstOrDefault(s => s.Id == student.Id);

        if (existingStudent == null)
        {
            return NotFound(new { message = "Student not found" });
        }

        existingStudent.Name = student.Name;
        existingStudent.Age = student.Age;
        existingStudent.Course = student.Course;

        return Ok(existingStudent);
    }

    [HttpDelete("delete/{id}")]
    public ActionResult Delete(string id)
    {
        var student = students.FirstOrDefault(s => s.Id == id);

        if (student == null)
        {
            return NotFound(new { message = "Student not found" });
        }

        students.Remove(student);
        return NoContent();
    }
}