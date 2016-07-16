using NWWebApi.Models;

namespace NWWebApi.Infrastructure
{
    public interface ICategoriesRepository<Items> : IRepositoryBase<categoryVm>
    {

        CategoriesManagerVM GetCategories();
        categoryVm GetCategory(int id);

    }
}
