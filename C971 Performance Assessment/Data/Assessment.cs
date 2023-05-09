using System;
using System.Collections.Generic;
using System.Text;

namespace C971_Performance_Assessment.Data
{
    class Assessment
    {
        public int Id { get; set; }

        public string Code { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public int CourseId { get; set; }
    }
}
