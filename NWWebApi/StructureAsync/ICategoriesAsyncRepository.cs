using NWWebApi.Models;
using System.Threading.Tasks;

namespace NWWebApi.StructureAsync
{
    public interface ICategoriesAsyncRepository<Items> : IRepositoryBaseAsync<categoryVm>
    {
       Task<CategoriesManagerVM> GetCategories();
       Task<categoryVm> GetCategory(int id);
    }
    
}
