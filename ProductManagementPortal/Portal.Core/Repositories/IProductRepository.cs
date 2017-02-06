using System.Linq;

namespace ProductManagement.Core.Repositories
{
    internal interface IProductRepository
    {
        IQueryable<T> GetAll<T>();
        T Save<T>(T item);
        void Delete<T>(T item);
    }
}
