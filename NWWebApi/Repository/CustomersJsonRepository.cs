using System.Collections.Generic;
using System.Linq;
using System.Web.Hosting;
using Newtonsoft.Json;
using NWWebApi.Models;
using NWWebApi.Infrastructure;

namespace NWWebApi.Repository
{
    public class CustomersJsonRepository : ICustomersRepository<customerVm>
    {
        private customersManagerVM db;
        public CustomersJsonRepository()
        {
            db = Retrieve();
        }
        public customerVm GetCustomer(string id)
        {
            customerVm custVM = db.customers.Where(_cus => _cus.customerID == id).SingleOrDefault();
            if ((custVM == null))
            {
                return null;
            }
            return custVM;
        }

        public customersManagerVM GetCustomers()
        {
            return db;
        }

        public void Add(customerVm Item)
        {
            db.customers.Add(Item);
        }

        public void RemoveItem(customerVm Item)
        {
            customerVm customer = db.customers.Where(cus => cus.customerID == Item.customerID).SingleOrDefault();
            db.customers.Remove(customer);

        }

        public void Save()
        {
            WriteData(db.customers);
        }

        public void Update(customerVm Item)
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
        }
        private bool WriteData(List<customerVm> customers)
        {
            // Write out the Json
            var filePath = HostingEnvironment.MapPath("~/App_Data/customers.json");

            var json = JsonConvert.SerializeObject(customers, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, json);

            return true;
        }
        internal customersManagerVM Retrieve()
        {
            var filePathCustomer = HostingEnvironment.MapPath("~/App_Data/customers.json");
            var filePathOrders = HostingEnvironment.MapPath("~/App_Data/orders.json");

            var jsonCustomer = System.IO.File.ReadAllText(filePathCustomer);
            var jsonOrders = System.IO.File.ReadAllText(filePathOrders);

            var customes = JsonConvert.DeserializeObject<List<customerVm>>(jsonCustomer);
            var orders = JsonConvert.DeserializeObject<List<customerOrdersVm>>(jsonOrders);
            customersManagerVM Results = new customersManagerVM();
            Results.customers = customes;
            Results.orders = orders;

            return Results;
        }
    }
}