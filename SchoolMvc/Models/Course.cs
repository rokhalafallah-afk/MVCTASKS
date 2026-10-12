namespace SchoolMvc.Models;

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Degree { get; set; }
    public int MinDegree { get; set; }

    public int DepartmentId { get; set; }
    public Department Department { get; set; } = null!;

    public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    public ICollection<StuCrsRes> StuCrsRes { get; set; } = new List<StuCrsRes>();
}
