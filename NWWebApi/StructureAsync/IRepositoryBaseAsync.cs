using System.Threading.Tasks;

namespace NWWebApi.StructureAsync
{
    public interface IRepositoryBaseAsync<T> where T : class
    {
        Task Add(T Item);

        Task Update(T Item);

        Task RemoveItem(T Item);

        Task Save();
    }
}
