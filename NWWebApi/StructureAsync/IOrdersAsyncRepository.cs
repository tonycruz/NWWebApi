using NWWebApi.Models;
using System.Threading.Tasks;
namespace NWWebApi.StructureAsync
{
    public interface IOrdersAsyncRepository<Items> : IRepositoryBaseAsync<customerOrdersVm>
    {
       Task<CustomerOrderManagerVM> GetOrders();
       Task<customerOrdersVm> GetOrder(int id);
    }
}
