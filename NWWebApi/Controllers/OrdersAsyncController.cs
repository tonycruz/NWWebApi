using NWWebApi.Models;
using NWWebApi.RepositoryAsync;
using NWWebApi.StructureAsync;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace NWWebApi.Controllers
{
    [EnableCors("*", headers: "*", methods: "*")]
    public class OrdersAsyncController : ApiController
    {
        private IOrdersAsyncRepository<customerOrdersVm> Repo;
        public OrdersAsyncController()
        {
            Repo = new OrdersAsyncJsonRepository();
        }
        public OrdersAsyncController(IOrdersAsyncRepository<customerOrdersVm> _Repo)
        {
            Repo = _Repo;
        }

        #region "Orders"
        [ResponseType(typeof(CustomerOrderManagerVM))]
        public async Task<CustomerOrderManagerVM> GetOrders()
        {
            var orders = await Repo.GetOrders();
            return orders;
        }
        #endregion

        #region "Orders Get By Id "
        [ResponseType(typeof(customerOrdersVm))]
        public async Task<IHttpActionResult> GetOrder(int id)
        {
            var Vm = await Repo.GetOrder(id);
            if ((Vm == null))
            {
                return NotFound();
            }
            return Ok(Vm);
        }
        #endregion

        #region "Orders Post"
        // POST api/OrderVm
        [ResponseType(typeof(customerOrdersVm))]
        public async Task<IHttpActionResult> PostOrder(customerOrdersVm vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           await Repo.Add(vm);
           await Repo.Save();
            return CreatedAtRoute("DefaultApi", new { id = vm.orderID }, vm);
        }
        #endregion

        #region "Orders Put"
        // PUT api/Orders/5
        [ResponseType(typeof(customerOrdersVm))]
        public async Task<IHttpActionResult> PutOrder(int id, customerOrdersVm vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!(id == vm.orderID))
            {
                return BadRequest();
            }

           await  Repo.Update(vm);
           await Repo.Save();
            return Ok(vm);
        }
        #endregion

        #region "Orders Delete"
        //  DELETE api/Orders/5
        [ResponseType(typeof(customerOrdersVm))]
        public async Task<IHttpActionResult> DeleteOrder(int id)
        {
            var vm = await Repo.GetOrder(id);
            if (vm == null)
            {
                return NotFound();
            }
           await  Repo.RemoveItem(vm);
           await  Repo.Save();
            return Ok(vm);
        }
        #endregion


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

    }
}
