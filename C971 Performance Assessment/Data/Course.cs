using SQLite;
using System;

namespace C971_Performance_Assessment.Data
{
    public class Course //: INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Code { get; set; } = "Course Number";
        public string Title { get; set; } = "Course Title";
        public string InstructorName { get; set; } = "Instructor Name";
        public string InstructorNumber { get; set; } = "555.555.5555";
        public string InstructorEmail { get; set; } = "www.example@wgu.edu";
        public string Notes { get; set; }
        public CourseStatus Status { get; set; } = CourseStatus.InProgress;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(7);
        public bool IsDateAlertsActive { get; set; } = true;
        public int TermId { get; set; }


        public string GetFormattedStatus()
        {
            switch (Status)
            {
                case CourseStatus.InProgress:
                    return "In Progress";
                case CourseStatus.Completed:
                    return "Completed";
                case CourseStatus.Dropped:
                    return "Dropped";
                case CourseStatus.PlanToTake:
                    return "Plan to Take";
                default:
                    return string.Empty;
            }
        }
    }

    public enum CourseStatus
    {
        InProgress,
        Completed,
        Dropped,
        PlanToTake
    }

}
