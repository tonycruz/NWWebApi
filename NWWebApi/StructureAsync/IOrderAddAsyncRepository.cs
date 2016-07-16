using NWWebApi.Models;
using System.Threading.Tasks;

namespace NWWebApi.StructureAsync
{
    interface IOrderAddAsyncRepository<Items> :IRepositoryBaseAsync<customerOrdersVm>
    {
        Task<OrderManagerVM> GetOrders();
        Task<EditOrderManagerVM> GetOrder(int id);
        Task<customerOrdersVm> GetOrderByID(int id);
    }
}
