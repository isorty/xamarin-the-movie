using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using TheMovie.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(TheMovie.SQLite.MovieAsyncRepository))]
namespace TheMovie.SQLite
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
            await database.CreateTableAsync<ShortMovie>();
        }
        public async Task<List<ShortMovie>> GetItemsAsync()
        {
            return await database.Table<ShortMovie>().ToListAsync();

        }
        public async Task<ShortMovie> GetItemAsync(int id)
        {
            return await database.GetAsync<ShortMovie>(id); 
        }
        public async Task<int> DeleteItemAsync(ShortMovie item)
        {
            return await database.DeleteAsync(item);
        }
        public async Task<int> SaveItemAsync(ShortMovie item)
        {
            return await database.InsertAsync(item);
        }
    }
}