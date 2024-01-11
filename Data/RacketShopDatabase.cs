using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Proiect_MDP_Mobile.Models;

namespace Proiect_MDP_Mobile.Data
{
    public class RacketShopDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public RacketShopDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Racket>().Wait();
            _database.CreateTableAsync<Review>().Wait();
            _database.CreateTableAsync<Service>().Wait();
        }

        // -----------------------------------------------------------------------------------------------------------
        // CRUD Operations for Rackets:
        public Task<List<Racket>> GetRacketsAsync()
        {
            return _database.Table<Racket>().ToListAsync();
        }

        public Task<Racket> GetRacketAsync(int id)
        {
            return _database.Table<Racket>()
                .Where(i => i.ID == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveRacketAsync(Racket racket)
        {
            if (racket.ID != 0)
            {
                return _database.UpdateAsync(racket);
            }
            else
            {
                return _database.InsertAsync(racket);
            }
        }

        public Task<int> DeleteRacketAsync(Racket racket)
        {
            return _database.DeleteAsync(racket);
        }

        public Task<int> UpdateRacketAsync(Racket racket)
        {
            return _database.UpdateAsync(racket);
        }


        // -----------------------------------------------------------------------------------------------------------
        // CRUD Operations for Reviews:

        public Task<int> SaveReviewAsync(Review review)
        {
            if (review.ID != 0)
            {
                return _database.UpdateAsync(review);
            }
            else
            {
                return _database.InsertAsync(review);
            }
        }

        public Task<List<Review>> GetReviewsAsync()
        {
            return _database.Table<Review>().ToListAsync();
        }

        public Task<List<Review>> GetReviewAsync(int racketId)
        {
            return _database.Table<Review>()
                .Where(r => r.RacketID == racketId)
                .ToListAsync();
        }

        public Task<int> DeleteReviewAsync(Review review)
        {
            return _database.DeleteAsync(review);
        }

        // -----------------------------------------------------------------------------------------------------------
        // CRUD Operations for Services:

        public Task<List<Service>> GetServicesAsync()
        {
            return _database.Table<Service>().ToListAsync();
        }

        public Task<Service> GetServiceAsync(int id)
        {
            return _database.Table<Service>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveServiceAsync(Service service)
        {
            if (service.ID != 0)
            {
                return _database.UpdateAsync(service);
            }
            else
            {
                return _database.InsertAsync(service);
            }
        }

        public Task<int> DeleteServiceAsync(Service service)
        {
            return _database.DeleteAsync(service);
        }
    }
}
