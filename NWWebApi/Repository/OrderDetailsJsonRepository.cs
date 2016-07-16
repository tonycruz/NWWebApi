using NWWebApi.Infrastructure;
using NWWebApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Hosting;

namespace NWWebApi.Repository
{
    public class OrderDetailsJsonRepository : IOrderDetailsRepository<order_DetailManagerVm>
    {
        private order_DetailManagerVm db;
        public OrderDetailsJsonRepository()
        {
            db = Retrieve();
        }
        public void Add(order_DetailVm Item)
        {
           db.orderDetails.Add(Item);
        }

        public order_DetailManagerVm GetOrderDetails(int id)
        {
            order_DetailManagerVm Vm = new order_DetailManagerVm();
            Vm.orderDetails = db.orderDetails.Where(_Ord => _Ord.orderID == id).ToList();

            if ((Vm == null))
            {
                return null;
            }

            return Vm;
        }

        public void RemoveItem(order_DetailVm Item)
        {
            var remove = db.orderDetails.Where(OD => OD.orderID == Item.orderID && OD.productName == Item.productName).SingleOrDefault();
            db.orderDetails.Remove(remove);
        }
       public order_DetailVm DeleteOrderDetailById(int id, int prodid)
        {
            var remove = db.orderDetails.Where(OD => OD.orderID == id && OD.productID == prodid).SingleOrDefault();
            db.orderDetails.Remove(remove);
            return remove;
        }

        public void Save()
        {
            WriteData(db.orderDetails);
        }

        public void Update(order_DetailVm Item)
        {
            var UpdateRecord = db.orderDetails.Where(OD => OD.orderID == Item.orderID && OD.productName == Item.productName).SingleOrDefault();
            UpdateRecord.unitPrice = Item.unitPrice;
            UpdateRecord.quantity = Item.quantity;
            UpdateRecord.discount = Item.discount;
            UpdateRecord.extendedPrice = Item.extendedPrice;
          
    }
        private bool WriteData(List<order_DetailVm> orders)
        {
            // Write out the Json
            var filePath = HostingEnvironment.MapPath("~/App_Data/orderDetails.json");

            var json = JsonConvert.SerializeObject(orders, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, json);

            return true;
        }
        internal order_DetailManagerVm Retrieve()
        {
          
            var filePathOrderDetails = HostingEnvironment.MapPath("~/App_Data/orderDetails.json");
            var jsonOrderDetails = System.IO.File.ReadAllText(filePathOrderDetails);
            var orderDetails = JsonConvert.DeserializeObject<List<order_DetailVm>>(jsonOrderDetails);

            order_DetailManagerVm result = new order_DetailManagerVm();
            result.orderDetails = orderDetails;
            return result;
        }

        public order_DetailVm GetOrderDetailById(order_DetailVm od)
        {
            var result = db.orderDetails.Where(OD => OD.orderID == od.orderID && OD.productName == od.productName).SingleOrDefault();
            return result;
        }
    }
}
