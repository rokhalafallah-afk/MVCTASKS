namespace SchoolMvc.Models;

// Join entity between Students and Courses (composite key: StudentId + CourseId)
public class StuCrsRes
{
    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;

    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;

    public int Grade { get; set; }
}
