using System.Collections.Generic;
using System.Linq;
using NWWebApi.Models;
using NWWebApi.StructureAsync;
using System.Threading.Tasks;

namespace NWWebApi.RepositoryAsync
{
    public class CustomersAsyncJsonRepository : ICustomersAsyncRepository<customerVm>
    {
        private customersManagerVM db;
        public CustomersAsyncJsonRepository()
        {
            db = Retrieve();
        }
        public Task<customerVm> GetCustomer(string id)
        {
            customerVm custVM = db.customers.Where(_cus => _cus.customerID == id).SingleOrDefault();
            if ((custVM == null))
            {
                return Task.FromResult<customerVm>(null);
            }
            return Task.FromResult(custVM);
        }
        public Task<customersManagerVM> GetCustomers()
        {
            return Task.FromResult(db);
        }

        public Task Add(customerVm Item)
        {
            db.customers.Add(Item);
            return Task.FromResult<customerVm>(null);
        }

        public Task RemoveItem(customerVm Item)
        {
            customerVm customer = db.customers.Where(cus => cus.customerID == Item.customerID).SingleOrDefault();
            db.customers.Remove(customer);
            return Task.FromResult<customerVm>(null);

        }

        public Task Save()
        {
            WriteData(db.customers);
            return Task.FromResult<customerVm>(null);
        }

        public Task Update(customerVm Item)
        {
            customerVm cus = db.customers.Where(_cus => _cus.customerID == Item.customerID).SingleOrDefault();

            cus.companyName = Item.companyName;
            cus.contactName = Item.contactName;
            cus.contactTitle = Item.contactTitle;
            cus.address = Item.address;
            cus.city = Item.city;
            cus.region = Item.region;
            cus.postalCode = Item.postalCode;
            cus.country = Item.country;
            cus.phone = Item.phone;
            cus.fax = Item.fax;
            return Task.FromResult<customerVm>(null);
        }
        private bool WriteData(List<customerVm> customers)
        {
            return HelperCode.WriteJsont(customers, "customers.json");
        }
        internal customersManagerVM Retrieve()
        {
            customersManagerVM Results = new customersManagerVM();
            Results.customers = HelperCode.GetJson<customerVm>("customers.json");
            Results.orders = HelperCode.GetJson<customerOrdersVm>("orders.json"); ;
            return Results;
        }
    }
}