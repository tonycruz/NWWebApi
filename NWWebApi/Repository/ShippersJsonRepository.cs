using System.Collections.Generic;
using System.Linq;
using System.Web.Hosting;
using Newtonsoft.Json;
using NWWebApi.Infrastructure;
using NWWebApi.Models;
namespace NWWebApi.Repository
{
    public class ShippersJsonRepository : IShippersRepository<shipperVm>
    {
        private shippersManagerVM db;
        public ShippersJsonRepository()
        {
            db = Retrieve();
        }
        public shippersManagerVM GetShippers()
        {
            return db;
        }

        public shipperVm GetShipper(int id)
        {
            shipperVm result = db.shippers.Where(_Shi => _Shi.shipperID == id).SingleOrDefault();

            if ((result == null))
            {
                return null;
            }

            return result;
        }

        public void Add(shipperVm Item)
        {
            var newID = db.shippers.Max(s => s.shipperID) + 1;
            Item.shipperID = newID;
            db.shippers.Add(Item);
        }

        public void RemoveItem(shipperVm Item)
        {
            db.shippers.Remove(Item);
        }


        public void Save()
        {
            WriteData(db.shippers);
        }


        public void Update(shipperVm Item)
        {
            shipperVm result = db.shippers.Where(_Shi => _Shi.shipperID == Item.shipperID).SingleOrDefault();
            result.shipperID = Item.shipperID;
            result.companyName = Item.companyName;
            result.phone = Item.phone;
        }
        private bool WriteData(List<shipperVm> shippers)
        {
            // Write out the Json
            var filePath = HostingEnvironment.MapPath("~/App_Data/shippers.json");

            var json = JsonConvert.SerializeObject(shippers, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, json);

            return true;
        }
        internal shippersManagerVM Retrieve()
        {
            var filePathShippers = HostingEnvironment.MapPath("~/App_Data/shippers.json");
            var filePathOrders = HostingEnvironment.MapPath("~/App_Data/orders.json");

            var jsonShippers = System.IO.File.ReadAllText(filePathShippers);
            var jsonOrders = System.IO.File.ReadAllText(filePathOrders);

            var shippers = JsonConvert.DeserializeObject<List<shipperVm>>(jsonShippers);
            var orders = JsonConvert.DeserializeObject<List<customerOrdersVm>>(jsonOrders);
            shippersManagerVM results = new shippersManagerVM();
            results.shippers = shippers;
            results.orders = orders;
            return results;
        }

    }
}