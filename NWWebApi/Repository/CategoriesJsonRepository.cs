using Newtonsoft.Json;
using NWWebApi.Infrastructure;
using NWWebApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Hosting;

namespace NWWebApi.Repository
{
    public class CategoriesJsonRepository : ICategoriesRepository<categoryVm>
    {
        private CategoriesManagerVM db;
        public CategoriesJsonRepository(){
            db = Retrieve();
        }
        public CategoriesManagerVM GetCategories()
        {
            var results = Retrieve();
            return db;
        }

        public categoryVm GetCategory(int id)
        {
            
            categoryVm Vm = db.categories.Where(_Cat => _Cat.categoryID == id).SingleOrDefault();

            if ((Vm == null))
            {
                return null;
            }

            return Vm;
        }

        public void Add(categoryVm Item)
        {
            var newID = db.categories.Max(c => c.categoryID) + 1;
            Item.categoryID = newID;
            db.categories.Add(Item);
        }

        public void RemoveItem(categoryVm Item)
        {
            categoryVm Vm = db.categories.Where(_Cat => _Cat.categoryID == Item.categoryID).SingleOrDefault();
            db.categories.Remove(Vm);
        }



        public void Save()
        {
            WriteData(db.categories);
        }



        public void Update(categoryVm Item)
        {
            categoryVm Vm = db.categories.Where(_Cat => _Cat.categoryID == Item.categoryID).SingleOrDefault();
            Vm.categoryName = Item.categoryName;
            Vm.description = Item.description;
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