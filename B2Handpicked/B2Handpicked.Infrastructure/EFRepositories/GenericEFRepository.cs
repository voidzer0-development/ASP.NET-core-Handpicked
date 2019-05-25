using B2Handpicked.DomainModel;
using B2Handpicked.DomainServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace B2Handpicked.Infrastructure.EFRepositories {
    public abstract class GenericEFRepository<T> : IRepository<T> where T : class, IEntity {
        protected readonly IAppDbContext _database;

        protected abstract DbSet<T> DbSet { get; }

        public GenericEFRepository(IAppDbContext database) {
            _database = database;
        }

        public async Task<bool> Create(T t) {
            if (!await DoesExist(t.Id)) {
                await DbSet.AddAsync(t);
                return await _database.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<IEnumerable<T>> GetAllAsList() => await DbSet.ToListAsync();

        public IQueryable<T> GetAllAsQueryable() => DbSet;

        public async Task<IEnumerable<T>> Filter(Func<T, Task<bool>> function) {
            var results = new ConcurrentQueue<T>();
            var tasks = ((IEnumerable<T>) DbSet).Select(async x => {
                if (await function(x)) results.Enqueue(x);
            });
            await Task.WhenAll(tasks);
            return results;
        }

        public IEnumerable<T> Filter(Func<T, bool> function) => DbSet.Where(function);

        public async Task<bool> DoesExist(int? id) => id is null ? false : await DbSet.AnyAsync(e => e.Id == id);

        public async Task<T> GetById(int? id) => id is null ? null : await DbSet.FindAsync(id);

        public async Task<bool> Update(T t) {
            var changingEntity = await GetById(t.Id);

            Map(changingEntity, t);
            
            return (await _database.SaveChangesAsync()) > 0;
        }

        // Function to retreive updated values from the updatedEntity and store them into the entity.
        protected abstract void Map(T entity, T updatedEntity);

        public async Task<bool> Delete(T t) {
            DbSet.Remove(t);
            return (await _database.SaveChangesAsync()) > 0;
        }

        public async Task<bool> Delete(int? id) => await Delete(await GetById(id));

        public abstract Task<IDictionary<string, string>> GetModelErrors(T t);
    }
}
