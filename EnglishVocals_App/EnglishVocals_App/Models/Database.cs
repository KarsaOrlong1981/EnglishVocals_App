using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace EnglishVocals_App.Models
{
   
    public class Database
    {
        readonly SQLiteAsyncConnection _database;
        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Words>().Wait();
        }
        public Task<List<Words>> GetAcalStatusAsync()
        {
            return _database.Table<Words>().ToListAsync();
        }
        public Task<int> SaveAcalStatusAsync(Words words)
        {
            return _database.InsertAsync(words);
        }
        public async Task DeleteItemAsync(int id)
        {
            var item = await _database.Table<Words>().Where(x => x.Id == id).FirstOrDefaultAsync();
            if (item != null)
            {
                await _database.DeleteAsync(item);
            }
        }
        public Task<int> DeleteAllItems<T>()
        {
            return _database.DeleteAllAsync<Words>();
        }
        public Task<int> GetLayoutCount()
        {
            return _database.Table<Words>().CountAsync();
        }
    }
}
