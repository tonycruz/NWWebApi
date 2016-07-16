using NWWebApi.Models;

namespace NWWebApi.Infrastructure
{
    interface IOrderAddRepository <Items> : IRepositoryBase<customerOrdersVm>
    {
        OrderManagerVM GetOrders();
        EditOrderManagerVM GetOrder(int id);
        customerOrdersVm GetOrderByID(int id);
    }
}
