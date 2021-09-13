using System.Threading.Tasks;

namespace ShipsBackEnd.Repositories
{
    public interface IBaseRepo
    {
        void Add<T>(T entity) where T : class;
        void Delete<T> (T entity) where T:class;

        Task<bool> SaveAll();
    }
}