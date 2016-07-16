using Newtonsoft.Json;
using NWWebApi.Models;
using NWWebApi.StructureAsync;
using System.Collections.Generic;
using System.Linq;
using System.Web.Hosting;
using System.Threading.Tasks;

namespace NWWebApi.Repository
{
    public class CategoriesAsyncJsonRepository : ICategoriesAsyncRepository<categoryVm>
    {
        private CategoriesManagerVM db;
        public CategoriesAsyncJsonRepository(){
            db = Retrieve();
        }

        public Task Add(categoryVm Item)
        {
            var newID = db.categories.Max(c => c.categoryID) + 1;
                Item.categoryID = newID;
                db.categories.Add(Item);
            return Task.FromResult<object>(null);
         }

        public Task<CategoriesManagerVM> GetCategories()
        {
            var results = Retrieve();
            return Task.FromResult<CategoriesManagerVM>(results);
        }

        public Task<categoryVm> GetCategory(int id)
        {
               categoryVm Vm = db.categories.Where(_Cat => _Cat.categoryID == id).SingleOrDefault();

               if ((Vm == null))
                {
                return Task.FromResult<categoryVm>(null);
                   
                }

            return Task.FromResult<categoryVm>(Vm);
        }

        public Task RemoveItem(categoryVm Item)
        {
                categoryVm Vm = db.categories.Where(_Cat => _Cat.categoryID == Item.categoryID).SingleOrDefault();
                db.categories.Remove(Vm);
            return Task.FromResult<object>(null);
        }

        public Task Save()
        {
            WriteData(db.categories);
            return Task.FromResult<object>(null);
        }

        public Task Update(categoryVm Item)
        {
            categoryVm Vm = db.categories.Where(_Cat => _Cat.categoryID == Item.categoryID).SingleOrDefault();
            Vm.categoryName = Item.categoryName;
            Vm.description = Item.description;
            return Task.FromResult<object>(null);
        }


        private bool WriteData(List<categoryVm> categories)
        {
            // Write out the Json
            var filePath = HostingEnvironment.MapPath("~/App_Data/categories.json");

            var json = JsonConvert.SerializeObject(categories, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, json);

            return true;
        }
        internal CategoriesManagerVM Retrieve()
        {
            var filePathCategories = HostingEnvironment.MapPath("~/App_Data/categories.json");
            var filePathProducts = HostingEnvironment.MapPath("~/App_Data/products.json");

            var jsonCategories = System.IO.File.ReadAllText(filePathCategories);
            var jsonProducts = System.IO.File.ReadAllText(filePathProducts);

            var categories = JsonConvert.DeserializeObject<List<categoryVm>>(jsonCategories);
            var products = JsonConvert.DeserializeObject<List<productVm>>(jsonProducts);

            CategoriesManagerVM results = new CategoriesManagerVM();

            results.categories = categories;
            results.products = products;
            return results;
        }
    }
}