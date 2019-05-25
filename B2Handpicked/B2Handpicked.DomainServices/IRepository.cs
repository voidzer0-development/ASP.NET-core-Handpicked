using B2Handpicked.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace B2Handpicked.DomainServices {
    public interface IRepository<T> where T : IEntity {
        Task<bool> DoesExist(int? id);

        Task<T> GetById(int? id);

        Task<IEnumerable<T>> GetAllAsList();

        IQueryable<T> GetAllAsQueryable();
        
        // Filter with async function
        Task<IEnumerable<T>> Filter(Func<T, Task<bool>> function);

        IEnumerable<T> Filter(Func<T, bool> function);

        Task<bool> Create(T t);

        Task<bool> Update(T t);

        Task<bool> Delete(T t);

        Task<bool> Delete(int? id);

        Task<IDictionary<string, string>> GetModelErrors(T t);
    }
}
