using System.Collections.Generic;
using System.Linq;
using System.Web.Hosting;
using Newtonsoft.Json;
using NWWebApi.Infrastructure;
using NWWebApi.Models;


namespace NWWebApi.Repository
{
    public class ProductsJsonRepository : IProductsRepository<productVm>
    {

        private productsManagerVM db;
        public ProductsJsonRepository()
        {
            db = Retrieve();
        }
        public productsManagerVM GetProducts()
        {
            return db;
        }

        public productVm GetProduct(int id)
        {
            productVm result = db.products.Where(p => p.productID == id).SingleOrDefault();
            return result;
        }

        public void Add(productVm Item)
        {
            var newID = db.products.Max(p => p.productID) + 1;
            Item.productID = newID;
            db.products.Add(Item);
            
        }
        public void RemoveItem(productVm Item)
        {
            productVm result = db.products.Where(_Pro => _Pro.productID == Item.productID).SingleOrDefault();
            db.products.Remove(result);
        }
        public void Save()
        {
            WriteData(db.products);
        }
        public void Update(productVm Item)
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
        }
        private bool WriteData(List<productVm> products)
        {
            // Write out the Json
            var filePath = HostingEnvironment.MapPath("~/App_Data/products.json");

            var json = JsonConvert.SerializeObject(products,Formatting.Indented);
            System.IO.File.WriteAllText(filePath, json);

            return true;
        }
        internal productsManagerVM Retrieve()
        {
            var filePathProducts = HostingEnvironment.MapPath("~/App_Data/products.json");
            var filePathCategories = HostingEnvironment.MapPath("~/App_Data/categories.json");
            var filePathSuppliers = HostingEnvironment.MapPath("~/App_Data/suppliers.json");

            var jsonProducts = System.IO.File.ReadAllText(filePathProducts);
            var jsonCategories = System.IO.File.ReadAllText(filePathCategories);
            var jsonSuppliers = System.IO.File.ReadAllText(filePathSuppliers);


            var products = JsonConvert.DeserializeObject<List<productVm>>(jsonProducts);
            var categories = JsonConvert.DeserializeObject<List<categoryVm>>(jsonCategories);
            var suppliers = JsonConvert.DeserializeObject<List<supplierVm>>(jsonSuppliers);

            productsManagerVM results = new productsManagerVM();
            results.categories = categories;
            results.products = products;
            results.suppliers = suppliers;
            return results;
        }
    }
}