using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using SQLite;
namespace C971_Performance_Assessment.Data
{
    class AssessmentRepository
    {
        readonly SQLiteAsyncConnection _database;

        public AssessmentRepository(SQLiteAsyncConnection connection)
        {
            _database = connection;
        }

        public Task<List<Assessment>> GetAssessmentsAsync()
        {
            Debug.WriteLine($"GetAssessmentsAsync Reached");
            return _database.Table<Assessment>().ToListAsync();
        }

        public Task<Assessment> GetAssessmentAsync(int id)
        {
            return _database.Table<Assessment>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveAssessmentAsync(Assessment assessment)
        {
            Debug.WriteLine($"SaveAssessmentAsync Reached. Assessment {assessment.Title} {assessment.Id}");
            if (assessment.Id != 0)
            {
                Debug.WriteLine($"UpdateAsync reached. Assessment{assessment.Title} {assessment.Id}");
                return _database.UpdateAsync(assessment);
            }
            else
            {
                Debug.WriteLine($"InsertAsync Reached. Assessment{assessment.Title} {assessment.Id}");
                return _database.InsertAsync(assessment);
            }

        }

        public Task<int> DeleteAssessmentAsync(Assessment assessment)
        {
            Debug.WriteLine($"Delete Assessment Async Reached. Assessment {assessment.Title}");
            return _database.DeleteAsync(assessment);
        }
    }
}

