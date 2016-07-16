using NWWebApi.Models;

namespace NWWebApi.Infrastructure
{
    public interface ICustomersRepository<Items> : IRepositoryBase<customerVm>
    {

        customersManagerVM GetCustomers();
        customerVm GetCustomer(string id);


    }
}
