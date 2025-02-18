namespace CourseProject.Common.Models;

public class Team : BaseEntity
{
   public string Name { get; set; } = default!;
   public List<Employee> Employees { get; set; }= default!;
}