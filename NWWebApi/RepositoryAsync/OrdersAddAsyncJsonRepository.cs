using NWWebApi.Models;
using NWWebApi.StructureAsync;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NWWebApi.RepositoryAsync
{
    public class OrdersAddAsyncJsonRepository : IOrderAddAsyncRepository<customerOrdersVm>
    {
        private OrderManagerVM db;
        public OrdersAddAsyncJsonRepository()
        {
            db = Retrieve();
        }
        public Task<EditOrderManagerVM> GetOrder(int id)
        {
            var result = RetrieveJson(id);
            return Task.FromResult(result);
        }

        public Task<OrderManagerVM> GetOrders()
        {
            return Task.FromResult(db);
        }
        public Task<customerOrdersVm> GetOrderByID(int id)
        {
           return Task.FromResult(db.orders.Where(_Ord => _Ord.orderID == id).SingleOrDefault());
        }
        public Task Add(customerOrdersVm Item)
        {
            var newID = db.orders.Max(o => o.orderID) + 1;
            Item.orderID = newID;
            db.orders.Add(Item);
            return Task.FromResult<customerOrdersVm>(null);
        }

        public Task RemoveItem(customerOrdersVm Item)
        {
            customerOrdersVm Vm = db.orders.Where(_Ord => _Ord.orderID == Item.orderID).SingleOrDefault();
            db.orders.Remove(Vm);
            return Task.FromResult<customerOrdersVm>(null);
        }

        public Task Save()
        {
            WriteData(db.orders);
            return Task.FromResult<customerOrdersVm>(null);
        }
        public Task Update(customerOrdersVm Ord)
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
            return Task.FromResult(Ord);
        }
        private bool WriteData(List<customerOrdersVm> orders)
        {
               return HelperCode.WriteJsont(orders, "orders.json");
        }
        internal OrderManagerVM Retrieve()
        {

            OrderManagerVM results = new OrderManagerVM();
            results.customers = HelperCode.GetJson<customerVm>("customers.json");
            results.orders = HelperCode.GetJson<customerOrdersVm>("orders.json");
            results.products = HelperCode.GetJson<productVm>("products.json");
           
            return results;
        }
       
        internal EditOrderManagerVM RetrieveJson(int id)
        {
          
            EditOrderManagerVM results = new EditOrderManagerVM();
            results.order = HelperCode.GetJson<customerOrdersVm>("orders.json").Where(od => od.orderID == id).SingleOrDefault();
            results.orderDetails = HelperCode.GetJson<order_DetailVm>("orderDetails.json").Where(od=> od.orderID== id).ToList();
            results.products = HelperCode.GetJson<productVm>("products.json");
            results.customers = HelperCode.GetJson<customerVm>("customers.json");
            return results;
        }


    }
}