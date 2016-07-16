using System.Threading.Tasks;
namespace NWWebApi.Infrastructure
{
    public interface IRepositoryBase<T> where T : class
    {
        void Add(T Item);

        void Update(T Item);

        void RemoveItem(T Item);

        void Save();
    }
    public interface IRepositoryBaseAsync<T> where T : class
    {
       Task Add(T Item);

       Task Update(T Item);

       Task RemoveItem(T Item);

       Task Save();
    }
}
