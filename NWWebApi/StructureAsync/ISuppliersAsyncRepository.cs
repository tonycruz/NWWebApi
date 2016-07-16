using NWWebApi.Models;
using System.Threading.Tasks;
namespace NWWebApi.StructureAsync
{
    public interface ISuppliersAsyncRepository<Items> : IRepositoryBaseAsync<supplierVm>
    {
        Task<SuppliersManagerVM> GetSuppliers();
        Task<supplierVm> GetSupplier(int id);

    }
}
