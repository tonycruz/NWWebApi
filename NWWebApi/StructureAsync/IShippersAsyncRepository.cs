using NWWebApi.Models;
using System.Threading.Tasks;
namespace NWWebApi.StructureAsync
{
    public interface IShippersAsyncRepository<Items> : IRepositoryBaseAsync<shipperVm>
    {
       Task<shippersManagerVM> GetShippers();
       Task<shipperVm> GetShipper(int id);
    }
}
