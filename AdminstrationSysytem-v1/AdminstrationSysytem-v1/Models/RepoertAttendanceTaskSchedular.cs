using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz; 
using Quartz.Impl;

namespace AdminstrationSysytem_v1.Models
{
    public class RepoertAttendanceTaskSchedular : IJob
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public void Execute(IJobExecutionContext context)
        {
            AttendanceReporter(DateTime.Now);

        }

        public void AttendanceReporter (DateTime dateOfDay)
        {
            //The day to calc the attendance 
            DateTime todayAttendingSheet = dateOfDay.Date;
            //Ids of doesn't come students 
            var doesntCome = db.Attendance.Where(m => m.IsAttended == false).Where(s => s.Date == todayAttendingSheet).Select(e => e.StudentId).ToList();


            foreach (string item in doesntCome)
            {
                var targetStudent = db.Students.SingleOrDefault(m => m.Id == item);
                if (targetStudent == null) throw new ArgumentNullException(nameof(targetStudent));
                bool isTheyPermitted = db.Attendance.Where(m => m.StudentId == item).Select(e => e.IsPermitted).SingleOrDefault();
                if (isTheyPermitted)
                {
                    targetStudent.NoOfPermissions += 1;
                    db.Entry(targetStudent).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    int? absenceDays = targetStudent.NoOfAbsenceDay;
                    int? degrees = targetStudent.GradeOfAbsence;
                    //1 - no , 2-5 - 5 , 6-9 - 10 , 10 - # - 25 ;
                    if (absenceDays == 0 || absenceDays == 1 || absenceDays == null)
                    {
                        if (absenceDays == null)
                        {
                            targetStudent.NoOfAbsenceDay = 1;
                        }
                        else
                        {
                            targetStudent.NoOfAbsenceDay = absenceDays + 1;
                        }
                        db.Entry(targetStudent).State = System.Data.Entity.EntityState.Modified;
                    }
                    else if (absenceDays >= 2 && absenceDays <= 3)
                    {
                        int punshment = db.AttendanceRules.Where(m => m.RuleCase == "2-3").Select(e => e.PunshimentNumber).SingleOrDefault();
                        targetStudent.GradeOfAbsence = degrees - punshment;
                        db.Entry(targetStudent).State = System.Data.Entity.EntityState.Modified;
                    }
                    else if (absenceDays >= 6 && absenceDays <= 9)
                    {
                        int punshment = db.AttendanceRules.Where(m => m.RuleCase == "6-9").Select(e => e.PunshimentNumber).SingleOrDefault();
                        targetStudent.GradeOfAbsence = degrees - punshment;
                        db.Entry(targetStudent).State = System.Data.Entity.EntityState.Modified;
                    }
                    else if (absenceDays >= 10)
                    {
                        int punshment = db.AttendanceRules.Where(m => m.RuleCase == "10-#").Select(e => e.PunshimentNumber).SingleOrDefault();
                        targetStudent.GradeOfAbsence = degrees - punshment;
                        db.Entry(targetStudent).State = System.Data.Entity.EntityState.Modified;
                    }
                    db.SaveChanges();
                }
            }

        }
    }


    public class JobScheduler
    {
        public static void Start()
        {
            IScheduler sch = StdSchedulerFactory.GetDefaultScheduler();
            sch.Start();
        }
    }
}