namespace CourseProject.Common.Models;

public class Employee : BaseEntity
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; }= default!;
    public Address Address { get; set; }= default!;
    public Job Job { get; set; }= default!;
    public List<Team> Team { get; set; }= default!;
}