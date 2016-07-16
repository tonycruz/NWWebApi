using NWWebApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using NWWebApi.StructureAsync;
using System.Threading.Tasks;

namespace NWWebApi.RepositoryAsync
{
    public class OrderDetailsAsyncJsonRepository : IOrderDetailsAsyncRepository<order_DetailManagerVm>
    {
        private order_DetailManagerVm db;
        public OrderDetailsAsyncJsonRepository()
        {
            db = Retrieve();
        }
        public Task Add(order_DetailVm Item)
        {
           db.orderDetails.Add(Item);
            return Task.FromResult<order_DetailVm>(null);
        }

        public Task<order_DetailManagerVm> GetOrderDetails(int id)
        {
            order_DetailManagerVm Vm = new order_DetailManagerVm();
            Vm.orderDetails = db.orderDetails.Where(_Ord => _Ord.orderID == id).ToList();

            if ((Vm == null))
            {
                return Task.FromResult<order_DetailManagerVm>(null);
            }

            return Task.FromResult(Vm); ;
        }

        public Task RemoveItem(order_DetailVm Item)
        {
            var remove = db.orderDetails.Where(OD => OD.orderID == Item.orderID && OD.productName == Item.productName).SingleOrDefault();
            db.orderDetails.Remove(remove);
            return Task.FromResult<order_DetailManagerVm>(null);
        }
       public Task<order_DetailVm> DeleteOrderDetailById(int id, int prodid)
        {
            var remove = db.orderDetails.Where(OD => OD.orderID == id && OD.productID == prodid).SingleOrDefault();
            db.orderDetails.Remove(remove);
            return Task.FromResult(remove); ;
        }

        public Task Save()
        {
            WriteData(db.orderDetails);
            return Task.FromResult<order_DetailManagerVm>(null);
        }

        public Task Update(order_DetailVm Item)
        {
            var UpdateRecord = db.orderDetails.Where(OD => OD.orderID == Item.orderID && OD.productName == Item.productName).SingleOrDefault();
            UpdateRecord.unitPrice = Item.unitPrice;
            UpdateRecord.quantity = Item.quantity;
            UpdateRecord.discount = Item.discount;
            UpdateRecord.extendedPrice = Item.extendedPrice;
            return Task.FromResult<order_DetailManagerVm>(null);

        }
        private bool WriteData(List<order_DetailVm> orders)
        {
            return HelperCode.WriteJsont(orders, "orderDetails.json");
        }
        internal order_DetailManagerVm Retrieve()
        {
            order_DetailManagerVm result = new order_DetailManagerVm();
            result.orderDetails = HelperCode.GetJson<order_DetailVm>("orderDetails.json");

            return result;
        }
        public Task<order_DetailVm> GetOrderDetailById(order_DetailVm od)
        {
           var results = HelperCode.GetJson<order_DetailVm>("orderDetails.json")
                .Where(OD => OD.orderID == od.orderID && OD.productName == od.productName).SingleOrDefault();
            return Task.FromResult(results);
        }
    }
}
