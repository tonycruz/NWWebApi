using NWWebApi.Models;

namespace NWWebApi.Infrastructure
{
    public interface IShippersRepository<Items> : IRepositoryBase<shipperVm>
    {

        shippersManagerVM GetShippers();
        shipperVm GetShipper(int id);

    }
}
