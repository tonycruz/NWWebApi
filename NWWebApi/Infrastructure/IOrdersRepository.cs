using NWWebApi.Models;

namespace NWWebApi.Infrastructure
{
    public interface IOrdersRepository<Items> : IRepositoryBase<customerOrdersVm>
    {

        CustomerOrderManagerVM GetOrders();
        customerOrdersVm GetOrder(int id);

    }
}
