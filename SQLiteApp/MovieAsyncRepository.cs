using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

namespace SQLiteApp
{
    public class MovieAsyncRepository
    {
        SQLiteAsyncConnection database;

        public MovieAsyncRepository(string databasePath)
        {
            database = new SQLiteAsyncConnection(databasePath);
        }

        public async Task CreateTable()
        {
            await database.CreateTableAsync<Movie>();
        }
        public async Task<List<Movie>> GetItemsAsync()
        {
            return await database.Table<Movie>().ToListAsync();

        }
        public async Task<Movie> GetItemAsync(int id)
        {
            return await database.GetAsync<Movie>(id);
        }
        public async Task<int> DeleteItemAsync(Movie item)
        {
            return await database.DeleteAsync(item);
        }
        public async Task<int> SaveItemAsync(Movie item)
        {
            if (item.Id != 0)
            {
                await database.UpdateAsync(item);
                return item.Id;
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }
    }
}