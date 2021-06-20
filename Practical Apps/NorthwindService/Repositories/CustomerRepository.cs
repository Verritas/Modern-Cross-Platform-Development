using Microsoft.EntityFrameworkCore.ChangeTracking;
using Packt.Shared;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindService.Repositories
{
    public class CustomerRepository:ICustomerRepository {
        private static ConcurrentDictionary<string, Customer> customersCache;

        private Northwind db;
        
        public CustomerRepository(Northwind db) {
            this.db = db;
            if (customersCache == null) {
                customersCache = new ConcurrentDictionary<string, Customer>(db.Customers.ToDictionary(c=>c.CustomerId));
            }
        }

        public async Task<Customer> CreateAsync(Customer c)
        {
            c.CustomerId = c.CustomerId.ToUpper();

            EntityEntry<Customer> added = await db.Customers.AddAsync(c);
            int affected = await db.SaveChangesAsync();
            if(affected == 1) {
                return customersCache.AddOrUpdate(c.CustomerId, c, UpdateCache);
            }
            else {
                return null;
            }
        }

        public async Task<bool?> DeleteAsync(string id)
        {
            id = id.ToUpper();
            Customer c = db.Customers.Find(id);
            db.Customers.Remove(c);
            int affected = await db.SaveChangesAsync();
            if (affected == 1) {
                return customersCache.TryRemove(id, out c);
            }
            else {
                return null;
            }
        }

        public Task<IEnumerable<Customer>> RetrieveAllAsync()
        {
            return Task.Run<IEnumerable<Customer>>(()=>customersCache.Values);
        }

        public Task<Customer> RetrieveAsync(string id)
        {
            return Task.Run(()=> {
                id = id.ToUpper();
                customersCache.TryGetValue(id, out Customer c);
                return c;
            });
        }

        public async Task<Customer> UpdateAsync(string id, Customer c)
        {
            id = id.ToUpper();
            c.CustomerId = c.CustomerId.ToUpper();
            db.Customers.Update(c);
            int affected = await db.SaveChangesAsync();
            if (affected == 1) {
                return UpdateCache(id, c);
            }
            return null;
        }

        private Customer UpdateCache(string id, Customer c) {
            Customer old;
            if (customersCache.TryGetValue(id, out old)) {
                if (customersCache.TryUpdate(id,c,old)) {
                    return c;
                }
            }
            return null;
        }
    }
}