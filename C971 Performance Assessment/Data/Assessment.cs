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

        public string Code { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public int CourseId { get; set; }
    }
}
