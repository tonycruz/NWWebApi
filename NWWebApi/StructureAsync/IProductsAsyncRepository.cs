using NWWebApi.Models;
using System.Threading.Tasks;
namespace NWWebApi.StructureAsync
{
    public interface IProductsAsyncRepository<Items> : IRepositoryBaseAsync<productVm>
    {
        Task<productsManagerVM> GetProducts();
        Task<productVm> GetProduct(int id);

    }
}
