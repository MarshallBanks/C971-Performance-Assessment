using System;
using System.Collections.Generic;
using System.Text;

namespace C971_Performance_Assessment.Data
{
    class Course
    { 
        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string InstructorName { get; set; }
        public string InstructorNumber { get; set; }
        public string InstructorEmail { get; set; }
        public string Notes { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Assessment Objective { get; set; }
        public Assessment Performance { get; set; }
        public int TermId { get; set; }
    }
}
