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

        public async Task<int> SaveCourseAsync(Course course)
        {
            Debug.WriteLine($"SaveCourseAsync Reached. Course {course.Title} {course.Id}");

            if (course.Id != 0)
            {
                Debug.WriteLine($"UpdateAsync reached. Course {course.Title} {course.Id} ");
                await _database.UpdateAsync(course);
            }
            else
            {
                Debug.WriteLine($"InsertAsync Reached. Course {course.Id}");
                await _database.InsertAsync(course);
            }

            int lastInsertedId = await _database.ExecuteScalarAsync<int>("SELECT last_insert_rowid();");
            return lastInsertedId;
        }

        public Task<int> DeleteCourseAsync(Course course)
        {
            return _database.DeleteAsync(course);
        }
    }
}

