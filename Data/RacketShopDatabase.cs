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
            _database.CreateTableAsync<ServiceList>().Wait();
            _database.CreateTableAsync<ServiceType>().Wait();
            _database.CreateTableAsync<ListService>().Wait();
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

        // Service:

        public Task<int> SaveServiceAsync(ServiceType product)
        {
            if (product.ID != 0)
            {
                return _database.UpdateAsync(product);
            }
            else
            {
                return _database.InsertAsync(product);
            }
        }

        public Task<int> DeleteServiceAsync(ServiceType product)
        {
            return _database.DeleteAsync(product);
        }

        public Task<List<ServiceType>> GetServicesAsync()
        {
            return _database.Table<ServiceType>().ToListAsync();
        }

        public Task<int> SaveListServiceTypeAsync(ListService listp)
        {
            if (listp.ID != 0)
            {
                return _database.UpdateAsync(listp);
            }
            else
            {
                return _database.InsertAsync(listp);
            }
        }

        public Task<List<ServiceType>> GetListServicesAsync(int shoplistid)
        {
            return _database.QueryAsync<ServiceType>(
            "select P.ID, P.Description from ServiceType P"
            + " inner join ListService LP"
            + " on P.ID = LP.ServiceTypeID where LP.ServiceListID = ?",
            shoplistid);
        }

    }
}
