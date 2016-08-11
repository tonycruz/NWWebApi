using NWWebApi.Models;
using System.Threading.Tasks;
namespace NWWebApi.StructureAsync
{
    interface IOrderDetailsAsyncRepository<Items> : IRepositoryBaseAsync<order_DetailVm>
    {
        Task<order_DetailManagerVm> GetOrderDetails(int id);
        Task<order_DetailVm> GetOrderDetailById(order_DetailVm od);
        Task<order_DetailVm> DeleteOrderDetailById(int id);
    }
}
