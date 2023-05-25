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

        public Task<Course> GetCourseAsync(int id)
        {
            return _database.Table<Course>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveCourseAsync(Course course)
        {
            Debug.WriteLine($"SaveCourseAsync Reached. Course {course.Id}");
            if (course.Id != 0)
            {
                Debug.WriteLine($"UpdateAsync reached. Course {course.Id}");
                return _database.UpdateAsync(course);
            }
            else
            {
                Debug.WriteLine($"InsertAsync Reached. Course {course.Id}");
                return _database.InsertAsync(course);
            }

        }

        public Task<int> DeleteCourseAsync(Course course)
        {
            return _database.DeleteAsync(course);
        }
    }
}

