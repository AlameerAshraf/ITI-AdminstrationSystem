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
        public virtual DbSet<Instructors> Instructors { get; set; }
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<Courses> Courses { get; set; }
        public virtual DbSet<Instructor_Corse_InDepartment> InstCrsDep { get; set; }
        public virtual DbSet<Course_Student_Instructor> InstCrsStudent { get; set; }
        public virtual DbSet<Student_Exam> StdEx { get; set; }
        public virtual DbSet<QuestionAnswers> QuestionAnswers { get; set; }
        public virtual DbSet<Attendance> Attendance { get; set; }
        public virtual DbSet<Vacation> Vacation { get; set; }

       // public System.Data.Entity.DbSet<AdminstrationSysytem_v1.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}