using Microsoft.EntityFrameworkCore;
using SchoolMvc.Models;

namespace SchoolMvc.Data;

public class SchoolContext : DbContext
{
    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }

    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Teacher> Teachers => Set<Teacher>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<StuCrsRes> StuCrsRes => Set<StuCrsRes>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Table names match the diagram
        modelBuilder.Entity<Department>().ToTable("Departments");
        modelBuilder.Entity<Student>().ToTable("Students");
        modelBuilder.Entity<Teacher>().ToTable("Teachers");
        modelBuilder.Entity<Course>().ToTable("Courses");

        // Composite primary key on the join table
        modelBuilder.Entity<StuCrsRes>(e =>
        {
            e.ToTable("StuCrsRes");
            e.HasKey(x => new { x.StudentId, x.CourseId });

            e.HasOne(x => x.Student)
             .WithMany(s => s.StuCrsRes)
             .HasForeignKey(x => x.StudentId)
             .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(x => x.Course)
             .WithMany(c => c.StuCrsRes)
             .HasForeignKey(x => x.CourseId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        // Department 1 -> * Students / Teachers / Courses.
        // Restrict avoids SQL Server "multiple cascade paths" errors.
        modelBuilder.Entity<Student>()
            .HasOne(s => s.Department).WithMany(d => d.Students)
            .HasForeignKey(s => s.DepartmentId).OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Teacher>()
            .HasOne(t => t.Department).WithMany(d => d.Teachers)
            .HasForeignKey(t => t.DepartmentId).OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Course>()
            .HasOne(c => c.Department).WithMany(d => d.Courses)
            .HasForeignKey(c => c.DepartmentId).OnDelete(DeleteBehavior.Restrict);

        // Course 1 -> * Teachers
        modelBuilder.Entity<Teacher>()
            .HasOne(t => t.Course).WithMany(c => c.Teachers)
            .HasForeignKey(t => t.CourseId).OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Teacher>().Property(t => t.Salary).HasColumnType("decimal(18,2)");

        // Seed data so the pages have something to show
        modelBuilder.Entity<Department>().HasData(
            new Department { Id = 1, Name = "Computer Science", MgrName = "Dr. Ahmed Hassan" },
            new Department { Id = 2, Name = "Mathematics", MgrName = "Dr. Mona Ali" },
            new Department { Id = 3, Name = "Physics", MgrName = "Dr. Omar Khaled" });

        modelBuilder.Entity<Course>().HasData(
            new Course { Id = 1, Name = "C# Programming", Degree = 100, MinDegree = 50, DepartmentId = 1 },
            new Course { Id = 2, Name = "Databases", Degree = 100, MinDegree = 50, DepartmentId = 1 },
            new Course { Id = 3, Name = "Calculus", Degree = 100, MinDegree = 40, DepartmentId = 2 },
            new Course { Id = 4, Name = "Mechanics", Degree = 100, MinDegree = 45, DepartmentId = 3 });

        modelBuilder.Entity<Teacher>().HasData(
            new Teacher { Id = 1, Name = "Sara Mahmoud", Salary = 12000m, Address = "Cairo", CourseId = 1, DepartmentId = 1 },
            new Teacher { Id = 2, Name = "Youssef Nabil", Salary = 11000m, Address = "Giza", CourseId = 3, DepartmentId = 2 },
            new Teacher { Id = 3, Name = "Laila Samir", Salary = 11500m, Address = "Alexandria", CourseId = 4, DepartmentId = 3 });

        modelBuilder.Entity<Student>().HasData(
            new Student { Id = 1, Name = "Ali Mostafa", Age = 20, DepartmentId = 1 },
            new Student { Id = 2, Name = "Nour Adel", Age = 21, DepartmentId = 1 },
            new Student { Id = 3, Name = "Hana Tarek", Age = 22, DepartmentId = 2 },
            new Student { Id = 4, Name = "Karim Fathy", Age = 19, DepartmentId = 3 },
            new Student { Id = 5, Name = "Mariam Wael", Age = 20, DepartmentId = 2 });

        modelBuilder.Entity<StuCrsRes>().HasData(
            new StuCrsRes { StudentId = 1, CourseId = 1, Grade = 88 },
            new StuCrsRes { StudentId = 1, CourseId = 2, Grade = 92 },
            new StuCrsRes { StudentId = 2, CourseId = 1, Grade = 75 },
            new StuCrsRes { StudentId = 3, CourseId = 3, Grade = 81 },
            new StuCrsRes { StudentId = 3, CourseId = 1, Grade = 67 },
            new StuCrsRes { StudentId = 4, CourseId = 4, Grade = 59 },
            new StuCrsRes { StudentId = 5, CourseId = 3, Grade = 95 });
    }
}
