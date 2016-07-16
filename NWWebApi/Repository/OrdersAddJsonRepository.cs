using Newtonsoft.Json;
using NWWebApi.Infrastructure;
using NWWebApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Hosting;
using System;

namespace NWWebApi.Repository
{
    public class OrdersAddJsonRepository :IOrderAddRepository<customerOrdersVm>
    {
        private OrderManagerVM db;
        public OrdersAddJsonRepository()
        {
            db = Retrieve();
        }
        public EditOrderManagerVM GetOrder(int id)
        {
            var result = RetrieveJson(id);
            return result;
        }

        public OrderManagerVM GetOrders()
        {
            return db;
        }
        public customerOrdersVm GetOrderByID(int id)
        {
           return db.orders.Where(_Ord => _Ord.orderID == id).SingleOrDefault();
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
        internal OrderManagerVM Retrieve()
        {
            var filePathOrders = HostingEnvironment.MapPath("~/App_Data/orders.json");
            var filePathProducts = HostingEnvironment.MapPath("~/App_Data/products.json");
            var filePathCustomers = HostingEnvironment.MapPath("~/App_Data/customers.json");
            var filePathshipping = HostingEnvironment.MapPath("~/App_Data/shippers.json");

            var jsonOrders = System.IO.File.ReadAllText(filePathOrders);
            var jsonProducts = System.IO.File.ReadAllText(filePathProducts);
            var jsonCustomers = System.IO.File.ReadAllText(filePathCustomers);
            var jsonShipping = System.IO.File.ReadAllText(filePathshipping);

            var orders = JsonConvert.DeserializeObject<List<customerOrdersVm>>(jsonOrders);
            var products = JsonConvert.DeserializeObject<List<productVm>>(jsonProducts);
            var customers = JsonConvert.DeserializeObject<List<customerVm>>(jsonCustomers);
            var shipping = JsonConvert.DeserializeObject<List<shipperVm>>(jsonShipping);

            OrderManagerVM result = new OrderManagerVM();
            result.orders = orders;
            result.products = products;
            result.customers = customers;
            result.shippers = shipping;
            return result;
        }
        private bool WriteJson(List<categoryVm> categories)
        {
            return HelperCode.WriteJsont(categories, "categories.json");
        }
        internal EditOrderManagerVM RetrieveJson(int id)
        {
          
            EditOrderManagerVM results = new EditOrderManagerVM();
            results.order = HelperCode.GetJson<customerOrdersVm>("orders.json").Where(od => od.orderID == id).SingleOrDefault();
            results.orderDetails = HelperCode.GetJson<order_DetailVm>("orderDetails.json").Where(od=> od.orderID== id).ToList();
            results.products = HelperCode.GetJson<productVm>("products.json");
            results.customers = HelperCode.GetJson<customerVm>("customers.json");
            results.shippers = HelperCode.GetJson<shipperVm>("shippers.json");

            return results;
        }


    }
}