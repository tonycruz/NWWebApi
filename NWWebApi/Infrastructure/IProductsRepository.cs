using NWWebApi.Models;

namespace NWWebApi.Infrastructure
{
    public interface IProductsRepository<Items> : IRepositoryBase<productVm>
    {

        productsManagerVM GetProducts();
        productVm GetProduct(int id);

    }
}
