using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolMvc.Data;
using SchoolMvc.ViewModels;

namespace SchoolMvc.Controllers;

public class StudentController : Controller
{
    private readonly SchoolContext _context;

    public StudentController(SchoolContext context)
    {
        _context = context;
    }

    // GET /Student/ShowAll
    public async Task<IActionResult> ShowAll()
    {
        var students = await _context.Students
            .AsNoTracking()
            .Include(s => s.Department)
            .OrderBy(s => s.Id)
            .ToListAsync();

        return View(students);
    }

    // GET /Student/ShowDetails?id=3
    public async Task<IActionResult> ShowDetails(int id)
    {
        var student = await _context.Students
            .AsNoTracking()
            .Include(s => s.Department)
            .Include(s => s.StuCrsRes).ThenInclude(r => r.Course)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (student is null)
            return NotFound($"No student found with id {id}.");

        var vm = new StudentDetailsViewModel
        {
            Id = student.Id,
            Name = student.Name,
            Age = student.Age,
            DepartmentName = student.Department.Name,
            Courses = student.StuCrsRes.Select(r => new CourseResultViewModel
            {
                CourseName = r.Course.Name,
                Grade = r.Grade,
                MaxDegree = r.Course.Degree,
                MinDegree = r.Course.MinDegree
            }).ToList()
        };

        return View(vm);
    }
}
