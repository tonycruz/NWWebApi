using NWWebApi.Models;

namespace NWWebApi.Infrastructure
{
    interface IOrderDetailsRepository<Items> : IRepositoryBase<order_DetailVm>
    {
        order_DetailManagerVm GetOrderDetails(int id);
        order_DetailVm GetOrderDetailById(order_DetailVm od);
        order_DetailVm DeleteOrderDetailById(int id);
    }
}
