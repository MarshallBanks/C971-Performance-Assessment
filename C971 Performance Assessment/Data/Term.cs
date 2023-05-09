using System;
using System.Collections.Generic;
using System.Text;

namespace C971_Performance_Assessment.Data
{
    class Term
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Course> Courses { get; set; }
    }
}
