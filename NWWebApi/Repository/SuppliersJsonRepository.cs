using System.Linq;
using System.Web.Hosting;
using Newtonsoft.Json;
using NWWebApi.Models;
using NWWebApi.Infrastructure;
using System.Collections.Generic;

namespace NWWebApi.Repository
{
    public class SuppliersJsonRepository : ISuppliersRepository<supplierVm>
    {
        private SuppliersManagerVM db;
        public SuppliersJsonRepository()
        {
            db = Retrieve();
        }
        public SuppliersManagerVM GetSuppliers()
        {
            return db;
        }

        public supplierVm GetSupplier(int id)
        {
            supplierVm result = db.suppliers.Where(_Sup => _Sup.supplierID == id).SingleOrDefault();

            if ((result == null))
            {
                return null;
            }
            return result;
        }

        public void Add(supplierVm Item)
        {
            var newID = db.suppliers.Max(s=> s.supplierID) +1;
            Item.supplierID = newID;
            db.suppliers.Add(Item);
        }

        public void RemoveItem(supplierVm Item)
        {
            supplierVm result = db.suppliers.Where(_Sup => _Sup.supplierID == Item.supplierID).SingleOrDefault();
            db.suppliers.Remove(result);
        }


        public void Save()
        {
            WriteData(db.suppliers);
        }


        public void Update(supplierVm Item)
        {
            supplierVm result = db.suppliers.Where(_Sup => _Sup.supplierID == Item.supplierID).SingleOrDefault();
            result.supplierID = Item.supplierID;
            result.companyName = Item.companyName;
            result.contactName = Item.contactName;
            result.contactTitle = Item.contactTitle;
            result.address = Item.address;
            result.city = Item.city;
            result.region = Item.region;
            result.postalCode = Item.postalCode;
            result.country = Item.country;
            result.phone = Item.phone;
            result.fax = Item.fax;
            result.homePage = Item.homePage;
        }
        private bool WriteData(List<supplierVm> suppliers)
        {
            // Write out the Json
            var filePath = HostingEnvironment.MapPath("~/App_Data/suppliers.json");

            var json = JsonConvert.SerializeObject(suppliers, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, json);

            return true;
        }
        internal SuppliersManagerVM Retrieve()
        {
            var filePathSuppliers = HostingEnvironment.MapPath("~/App_Data/suppliers.json");
            var filePathProducts = HostingEnvironment.MapPath("~/App_Data/products.json");

            var jsonSuppliers = System.IO.File.ReadAllText(filePathSuppliers);
            var jsonProducts = System.IO.File.ReadAllText(filePathProducts);

            var suppliers = JsonConvert.DeserializeObject<List<supplierVm>>(jsonSuppliers);
            var products = JsonConvert.DeserializeObject<List<productVm>>(jsonProducts);
            SuppliersManagerVM results = new SuppliersManagerVM();
            results.suppliers = suppliers;
            results.products = products;
            
            return results;
        }
    }
}