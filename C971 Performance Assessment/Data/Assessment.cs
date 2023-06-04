using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace C971_Performance_Assessment.Data
{
    public class Assessment
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Code { get; set; } = "XXXX";
        public string Title { get; set; }
        public DateTime DueDate { get; set; } = DateTime.Now.AddMonths(1);
        public int CourseId { get; set; }
        public bool DateAlertIsOn { get; set; } = true;
        public AssessmentType Type { get; set; } // e.g., "Objective" or "Performance"
    }

    public enum AssessmentType
    {
        Objective,
        Performance
    }
}
