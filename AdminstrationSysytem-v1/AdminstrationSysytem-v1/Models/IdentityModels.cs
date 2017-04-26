using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AdminstrationSysytem_v1.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public string UserAccessType { get; set; }
        public bool IsActivated { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
       
        public ApplicationDbContext()
            : base("ITI", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Qualifications> Qualifications { get; set; }
        public DbSet<Instructors> Instructors { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<Instructor_Corse_InDepartment> InstCrsDep { get; set; }
        public DbSet<Course_Student_Instructor> InstCrsStudent { get; set; }
        public DbSet<Student_Exam> StdEx { get; set; }
        public DbSet<QuestionAnswers> QuestionAnswers { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<Vacation> Vacation { get; set; }
        public DbSet<AttendanceRules> AttendanceRules { get; set; }

        // public System.Data.Entity.DbSet<AdminstrationSysytem_v1.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}