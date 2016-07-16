using NWWebApi.Models;
using System.Threading.Tasks;

namespace NWWebApi.StructureAsync
{
    public interface ICustomersAsyncRepository<Items> : IRepositoryBaseAsync<customerVm>
    {
        Task<customersManagerVM> GetCustomers();
        Task<customerVm> GetCustomer(string id);
    }
}
