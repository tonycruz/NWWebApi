using NWWebApi.Models;
using NWWebApi.StructureAsync;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NWWebApi.RepositoryAsync
{
    public class OrdersAsyncJsonRepository : IOrdersAsyncRepository<customerOrdersVm>
    {
        private CustomerOrderManagerVM db;
        public OrdersAsyncJsonRepository()
        {
            db = Retrieve();
        }
        public Task<customerOrdersVm> GetOrder(int id)
        {
            customerOrdersVm Vm = db.orders.Where(_Ord => _Ord.orderID == id).SingleOrDefault();

            if ((Vm == null))
            {
                return Task.FromResult< customerOrdersVm>(null);
            }

            return Task.FromResult(Vm);
        }

        public Task<CustomerOrderManagerVM> GetOrders()
        {
            return Task.FromResult(db);
        }

        public Task Add(customerOrdersVm Item)
        {
            var newID = db.orders.Max(o => o.orderID) + 1;
            Item.orderID = newID;
            db.orders.Add(Item);
            return Task.FromResult(Item);
        }

        public Task RemoveItem(customerOrdersVm Item)
        {
            customerOrdersVm Vm = db.orders.Where(_Ord => _Ord.orderID == Item.orderID).SingleOrDefault();
            db.orders.Remove(Vm);
            return Task.FromResult(Item);
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
        internal CustomerOrderManagerVM Retrieve()
        {
            CustomerOrderManagerVM results = new CustomerOrderManagerVM();
            results.orders = HelperCode.GetJson<customerOrdersVm>("orders.json").OrderByDescending(o => o.orderID).ToList();
            results.orderDetails = HelperCode.GetJson<order_DetailVm>("orderDetails.json");
            return results;
        }



    }
}