using CourseProject.Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Infrastructure;

public class ApplicationDbContext : IdentityDbContext<IdentityUser , IdentityRole , string>
{
   public DbSet<Address> Addresses { get; set; } 
   public DbSet<Employee> Employees { get; set; }
   public DbSet<Team> Teams { get; set; }
   public DbSet<Job> Jobs { get; set; }

   public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
   {
      
   }

   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   {
      optionsBuilder.UseSqlite("Filename=Coursesproject.db");
      base.OnConfiguring(optionsBuilder);
   }     

   protected override void OnModelCreating(ModelBuilder builder)
   {
      builder.Entity<Address>().HasKey(e => e.Id);
      builder.Entity<Employee>().HasKey(e => e.Id);
      builder.Entity<Job>().HasKey(e => e.Id);
      builder.Entity<Team>().HasKey(e => e.Id);
      
      builder.Entity<Employee>().HasOne(e => e.Address);
      builder.Entity<Employee>().HasOne(e => e.Job);
      
      //  HasMany(e => e.Employees) => ทีม (Team) มีพนักงาน (Employees) หลายคน
      //  WithMany(e => e.Team) => พนักงาน (Employee) อยู่ในทีม (Team) หลายทีม
      // คำสั่งนี้กำหนดความสัมพันธ์ many-to-many ระหว่าง Team และ Employee.
      builder.Entity<Team>().HasMany(e => e.Employees).WithMany(e => e.Team);
      base.OnModelCreating(builder);
   }
}