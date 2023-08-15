using MapMarkerCrafts.Models;

namespace MapMarkerCrafts.Repositories.Interfaces
{
    public interface IRepository<T> where T : PersistentObject
    {
        IQueryable<T> Get();
        Task<IQueryable<T>> GetAsync();
        IQueryable<T> GetAll();
        Task<IQueryable<T>> GetAllAsync();
        void Create(T item);
        Task CreateAsync(T item);
        void Delete(T entity);
        void Update(T entity, string ip);
    }
}
