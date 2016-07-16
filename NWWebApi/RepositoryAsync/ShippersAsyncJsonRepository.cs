using NWWebApi.Models;
using NWWebApi.StructureAsync;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace NWWebApi.RepositoryAsync
{
    public class ShippersAsyncJsonRepository : IShippersAsyncRepository<shipperVm>
    {
        private shippersManagerVM db;
        public ShippersAsyncJsonRepository()
        {
            db = Retrieve();
        }
        public Task<shippersManagerVM> GetShippers()
        {
            return Task.FromResult(db);
        }

        public Task<shipperVm> GetShipper(int id)
        {
            shipperVm result = db.shippers.Where(_Shi => _Shi.shipperID == id).SingleOrDefault();

            if ((result == null))
            {
               
                return Task.FromResult<shipperVm>(null);
            }

            return Task.FromResult(result);
        }

        public Task Add(shipperVm Item)
        {
            var newID = db.shippers.Max(s => s.shipperID) + 1;
            Item.shipperID = newID;
            db.shippers.Add(Item);
            return Task.FromResult(Item);
        }

        public Task RemoveItem(shipperVm Item)
        {
            db.shippers.Remove(Item);
            return Task.FromResult(Item);
        }


        public Task Save()
        {
            WriteData(db.shippers);
            return Task.FromResult<shipperVm>(null);
        }


        public Task Update(shipperVm Item)
        {
            shipperVm result = db.shippers.Where(_Shi => _Shi.shipperID == Item.shipperID).SingleOrDefault();
            result.shipperID = Item.shipperID;
            result.companyName = Item.companyName;
            result.phone = Item.phone;
            return Task.FromResult(Item);
        }
        private bool WriteData(List<shipperVm> shippers)
        {
            return HelperCode.WriteJsont(shippers, "shippers.json");
        }
        internal shippersManagerVM Retrieve()
        {
            shippersManagerVM results = new shippersManagerVM();
            results.shippers = HelperCode.GetJson<shipperVm>("shippers.json");
            results.orders = HelperCode.GetJson<customerOrdersVm>("orders.json");
            return results;
        }

    }
}