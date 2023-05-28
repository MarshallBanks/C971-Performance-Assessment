using C971_Performance_Assessment.Pages;
using C971_Performance_Assessment.Views;
using SQLite;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace C971_Performance_Assessment.Data
{
    public class Course //: INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Code { get; set; } = "Course Number";
        public string Title { get; set; } = "Course Title";
        public string InstructorName { get; set; } = "Instructor Name";
        public string InstructorNumber { get; set; } = "Instructor Number";
        public string InstructorEmail { get; set; } = "www.example@wgu.edu";
        public string Notes { get; set; } = "Enter notes here";
        public CourseStatus Status { get; set; } = CourseStatus.InProgress;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(7);
        public int TermId { get; set; }
    }

    public enum CourseStatus
    {
        InProgress,
        Completed,
        Dropped,
        PlanToTake
    }
}
