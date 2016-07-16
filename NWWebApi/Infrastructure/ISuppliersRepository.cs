using NWWebApi.Models;

namespace NWWebApi.Infrastructure
{
    public interface ISuppliersRepository<Items> : IRepositoryBase<supplierVm>
    {

        SuppliersManagerVM GetSuppliers();
        supplierVm GetSupplier(int id);

    }
}
