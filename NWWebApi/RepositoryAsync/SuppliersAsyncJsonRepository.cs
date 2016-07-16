using NWWebApi.Models;
using NWWebApi.StructureAsync;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NWWebApi.RepositoryAsync
{
    public class SuppliersAsyncJsonRepository : ISuppliersAsyncRepository<supplierVm>
    {
        private SuppliersManagerVM db;
        public SuppliersAsyncJsonRepository()
        {
            db = Retrieve();
        }
        public Task<SuppliersManagerVM> GetSuppliers()
        {
            return Task.FromResult(db);
        }

        public Task<supplierVm> GetSupplier(int id)
        {
            supplierVm result = db.suppliers.Where(_Sup => _Sup.supplierID == id).SingleOrDefault();

            if ((result == null))
            {
                return Task.FromResult<supplierVm>(null);
            }
            return Task.FromResult(result);
        }

        public Task Add(supplierVm Item)
        {
            var newID = db.suppliers.Max(s=> s.supplierID) +1;
            Item.supplierID = newID;
            db.suppliers.Add(Item);
            return Task.FromResult(Item);
        }

        public Task RemoveItem(supplierVm Item)
        {
            supplierVm result = db.suppliers.Where(_Sup => _Sup.supplierID == Item.supplierID).SingleOrDefault();
            db.suppliers.Remove(result);
            return Task.FromResult(Item);
        }


        public Task Save()
        {
            WriteData(db.suppliers);
            return Task.FromResult<supplierVm>(null);
        }


        public Task Update(supplierVm Item)
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
            return Task.FromResult(Item);
        }
        private bool WriteData(List<supplierVm> suppliers)
        {
            return HelperCode.WriteJsont(suppliers, "suppliers.json");
         
        }
        internal SuppliersManagerVM Retrieve()
        {
            SuppliersManagerVM results = new SuppliersManagerVM();
            results.suppliers = HelperCode.GetJson<supplierVm>("suppliers.json");
            results.products = HelperCode.GetJson<productVm>("products.json");
            return results;
        }
    }
}