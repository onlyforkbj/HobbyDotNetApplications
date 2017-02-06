using System.Linq;

namespace ProductManagement.Core.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public IQueryable<T> GetAll<T>()
        {
            throw new System.NotImplementedException();
        }

        public T Save<T>(T item)
        {
            throw new System.NotImplementedException();
        }

        public void Delete<T>(T item)
        {
            throw new System.NotImplementedException();
        }
    }
}