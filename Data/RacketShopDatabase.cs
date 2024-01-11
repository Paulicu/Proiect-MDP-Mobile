﻿using System;
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
            _database.CreateTableAsync<ServiceList>().Wait();
            _database.CreateTableAsync<ServiceType>().Wait();
            _database.CreateTableAsync<ListService>().Wait();
            _database.CreateTableAsync<Racket>().Wait();
            _database.CreateTableAsync<Review>().Wait();
        }

        // Service List:
        public Task<List<ServiceList>> GetServiceListsAsync()
        {
            return _database.Table<ServiceList>().ToListAsync();
        }

        public Task<ServiceList> GetServiceListAsync(int id)
        {
            return _database.Table<ServiceList>()
                .Where(i => i.ID == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveServiceListAsync(ServiceList slist)
        {
            if (slist.ID != 0)
            {
                return _database.UpdateAsync(slist);
            }
            else
            {
                return _database.InsertAsync(slist);
            }
        }

        public Task<int> DeleteServiceListAsync(ServiceList slist)
        {
            return _database.DeleteAsync(slist);
        }

        // -----------------------------------------------------------------------------------------------------------
        // CRUD Operations for Service Types:
        public Task<int> SaveServiceAsync(ServiceType serviceType)
        {
            if (serviceType.ID != 0)
            {
                return _database.UpdateAsync(serviceType);
            }
            else
            {
                return _database.InsertAsync(serviceType);
            }
        }

        public Task<int> DeleteServiceAsync(ServiceType serviceType)
        {
            return _database.DeleteAsync(serviceType);
        }

        public Task<List<ServiceType>> GetServicesAsync()
        {
            return _database.Table<ServiceType>().ToListAsync();
        }

        public Task<int> SaveListServiceTypeAsync(ListService listS)
        {
            if (listS.ID != 0)
            {
                return _database.UpdateAsync(listS);
            }
            else
            {
                return _database.InsertAsync(listS);
            }
        }

        public Task<List<ServiceType>> GetListServicesAsync(int servicelistid)
        {
            return _database.QueryAsync<ServiceType>(
            "select S.ID, S.Description from ServiceType S"
            + " inner join ListService LS"
            + " on S.ID = LS.ServiceTypeID where LS.ServiceListID = ?",
            servicelistid);
        }

        public Task<int> DeleteServiceTypeFromServiceListAsync(int serviceTypeId, int serviceListId)
        {
            return _database.Table<ListService>()
                .Where(ls => ls.ServiceTypeID == serviceTypeId && ls.ServiceListID == serviceListId)
                .DeleteAsync();
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
    }
}
