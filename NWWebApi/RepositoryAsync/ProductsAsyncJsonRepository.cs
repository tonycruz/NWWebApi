using NWWebApi.Models;
using NWWebApi.StructureAsync;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NWWebApi.RepositoryAsync
{
    public class ProductsAsyncJsonRepository : IProductsAsyncRepository<productVm>
    {

        private productsManagerVM db;
        public ProductsAsyncJsonRepository()
        {
            db = Retrieve();
        }
        public Task<productsManagerVM> GetProducts()
        {
            return Task.FromResult(db);
        }

        public Task<productVm> GetProduct(int id)
        {
           return Task.FromResult(db.products.Where(p => p.productID == id).SingleOrDefault());
        }

        public Task Add(productVm Item)
        {
            var newID = db.products.Max(p => p.productID) + 1;
            Item.productID = newID;
            db.products.Add(Item);
            return Task.FromResult(Item);
        }
        public Task RemoveItem(productVm Item)
        {
            productVm result = db.products.Where(_Pro => _Pro.productID == Item.productID).SingleOrDefault();
            db.products.Remove(result);
            return Task.FromResult(Item);
        }
        public Task Save()
        {
            WriteData(db.products);
            return Task.FromResult<productVm>(null);
        }
        public Task Update(productVm Item)
        {
            productVm result = db.products.Where(_Pro => _Pro.productID == Item.productID).SingleOrDefault();
            result.productName = Item.productName;
            result.supplierID = Item.supplierID;
            result.categoryID = Item.categoryID;
            result.quantityPerUnit = Item.quantityPerUnit;
            result.unitPrice = Item.unitPrice;
            result.unitsInStock = Item.unitsInStock;
            result.unitsOnOrder = Item.unitsOnOrder;
            result.reorderLevel = Item.reorderLevel;
            result.discontinued = Item.discontinued;
            return Task.FromResult(Item);
        }
        private bool WriteData(List<productVm> products)
        {
           return HelperCode.WriteJsont(products, "products.json");
          
        }
        internal productsManagerVM Retrieve()
        {
            productsManagerVM results = new productsManagerVM();
            results.products = HelperCode.GetJson<productVm>("products.json");
            results.categories = HelperCode.GetJson<categoryVm>("categories.json");
            results.suppliers = HelperCode.GetJson<supplierVm>("suppliers.json");
            return results;
        }
    }
}