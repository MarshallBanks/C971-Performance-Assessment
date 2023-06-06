using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace C971_Performance_Assessment.Data
{
    class TermRepository
    {
        readonly SQLiteAsyncConnection _database;

        public TermRepository(SQLiteAsyncConnection connection)
        {
            _database = connection;
        }

        public Task<List<Term>> GetTermsAsync()
        {
            Debug.WriteLine($"GetTermsAsync Reached");
            return _database.Table<Term>().ToListAsync();
        }

        public Task<Term> GetTermAsync(int id)
        {
            return _database.Table<Term>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        public async Task<int> SaveTermAsync(Term term)
        {
            Debug.WriteLine($"SaveTermAsync Reached. Term {term.Id}");
            if (term.Id != 0)
            {
                Debug.WriteLine($"UpdateAsync reached. Term {term.Id}");
                await _database.UpdateAsync(term);
            }
            else
            {
                Debug.WriteLine($"InsertAsync Reached. Term {term.Id}");
                await _database.InsertAsync(term);
            }

            int lastInsertedId = await _database.ExecuteScalarAsync<int>("SELECT last_insert_rowid();");
            return lastInsertedId;
        }

        public Task<int> DeleteTermAsync(Term term)
        {
            return _database.DeleteAsync(term);
        }
    }
}
