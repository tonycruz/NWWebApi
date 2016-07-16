using Newtonsoft.Json;
using NWWebApi.Infrastructure;
using NWWebApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Hosting;

namespace NWWebApi.Repository
{
    public class OrdersJsonRepository : IOrdersRepository<customerOrdersVm>
    {
        private CustomerOrderManagerVM db;
        public OrdersJsonRepository()
        {
            db = Retrieve();
        }
        public customerOrdersVm GetOrder(int id)
        {
            customerOrdersVm Vm = db.orders.Where(_Ord => _Ord.orderID == id).SingleOrDefault();

            if ((Vm == null))
            {
                return null;
            }

            return Vm;
        }

        public CustomerOrderManagerVM GetOrders()
        {
            return db;
        }

        public void Add(customerOrdersVm Item)
        {
            var newID = db.orders.Max(o => o.orderID) + 1;
            Item.orderID = newID;
            db.orders.Add(Item);
        }

        public void RemoveItem(customerOrdersVm Item)
        {
            customerOrdersVm Vm = db.orders.Where(_Ord => _Ord.orderID == Item.orderID).SingleOrDefault();
            db.orders.Remove(Vm);
        }

        public void Save()
        {
            WriteData(db.orders);
        }
        public void Update(customerOrdersVm Ord)
        {
            customerOrdersVm result = db.orders.Where(_Ord => _Ord.orderID == Ord.orderID).SingleOrDefault();
            var Order = result;
            Order.customerID = Ord.customerID;
            Order.companyName = Ord.companyName;
            Order.contactName = Ord.contactName;
            Order.address = Ord.address;
            Order.region = Ord.region;
            Order.postalCode = Ord.postalCode;
            Order.city = Ord.city;
            Order.country = Ord.country;
            Order.employeeID = Ord.employeeID;
            Order.orderDate = Ord.orderDate;
            Order.requiredDate = Ord.requiredDate;
            Order.shippedDate = Ord.shippedDate;
            Order.shipVia = Ord.shipVia;
            Order.freight = Ord.freight;
            Order.shipName = Ord.shipName;
            Order.shipAddress = Ord.shipAddress;
            Order.shipCity = Ord.shipCity;
            Order.shipRegion = Ord.shipRegion;
            Order.shipPostalCode = Ord.shipPostalCode;

        }
        private bool WriteData(List<customerOrdersVm> orders)
        {
            // Write out the Json
            var filePath = HostingEnvironment.MapPath("~/App_Data/orders.json");

            var json = JsonConvert.SerializeObject(orders, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, json);

            return true;
        }
        internal CustomerOrderManagerVM Retrieve()
        {
            var filePathOrders = HostingEnvironment.MapPath("~/App_Data/orders.json");
            var filePathOrderDetails = HostingEnvironment.MapPath("~/App_Data/orderDetails.json");

            var jsonOrders = System.IO.File.ReadAllText(filePathOrders);
            var jsonOrderDetails = System.IO.File.ReadAllText(filePathOrderDetails);

            var orders = JsonConvert.DeserializeObject<List<customerOrdersVm>>(jsonOrders);
            var orderDetails = JsonConvert.DeserializeObject<List<order_DetailVm>>(jsonOrderDetails);

            CustomerOrderManagerVM result = new CustomerOrderManagerVM();
            result.orders = orders.OrderByDescending(o => o.orderID).ToList();
            result.orderDetails = orderDetails;
            return result;
        }



    }
}