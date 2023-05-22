using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using SQLite;
namespace C971_Performance_Assessment.Data
{
    class CourseRepository
    {
        readonly SQLiteAsyncConnection _database;

        public CourseRepository(SQLiteAsyncConnection connection)
        {
            _database = connection;
        }

        public Task<List<Course>> GetCoursesAsync()
        {
            Debug.WriteLine($"GetCoursesAsync Reached");
            return _database.Table<Course>().ToListAsync();
        }

        public Task<Term> GetTermAsync(int id)
        {
            return _database.Table<Term>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveTermAsync(Course course)
        {
            Debug.WriteLine($"SaveTermAsync Reached. Term {course.Id}");
            if (course.Id != 0)
            {
                Debug.WriteLine($"UpdateAsync reached. Term {course.Id}");
                return _database.UpdateAsync(course);
            }
            else
            {
                Debug.WriteLine($"InsertAsync Reached. Term {course.Id}");
                return _database.InsertAsync(course);
            }

        }

        public Task<int> DeleteCourseAsync(Course course)
        {
            return _database.DeleteAsync(course);
        }
    }
}

