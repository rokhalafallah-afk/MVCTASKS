namespace SchoolMvc.ViewModels;

public class StudentDetailsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public List<CourseResultViewModel> Courses { get; set; } = new();
}

public class CourseResultViewModel
{
    public string CourseName { get; set; } = string.Empty;
    public int Grade { get; set; }
    public int MaxDegree { get; set; }
    public int MinDegree { get; set; }
    public bool Passed => Grade >= MinDegree;
}
