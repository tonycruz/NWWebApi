using NWWebApi.Infrastructure;
using NWWebApi.Models;
using NWWebApi.Repository;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace NWWebApi.Controllers
{
    [EnableCors("*", headers: "*", methods: "*")]
    public class OrdersApiController : ApiController
    {
        private IOrdersRepository<customerOrdersVm> Repo;
        public OrdersApiController()
        {
            Repo = new OrdersJsonRepository();
        }
        public OrdersApiController(IOrdersRepository<customerOrdersVm> _Repo)
        {
            Repo = _Repo;
        }

        #region "Orders"
        [ResponseType(typeof(CustomerOrderManagerVM))]
        public CustomerOrderManagerVM GetOrders()
        {
            var orders = Repo.GetOrders();
            return orders;
        }
        #endregion

        #region "Orders Get By Id "
        [ResponseType(typeof(customerOrdersVm))]
        public IHttpActionResult GetOrder(int id)
        {
            var Vm = Repo.GetOrder(id);
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
        public IHttpActionResult PostOrder(customerOrdersVm vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Repo.Add(vm);
            Repo.Save();
            return CreatedAtRoute("DefaultApi", new { id = vm.orderID }, vm);
        }
        #endregion

        #region "Orders Put"
        // PUT api/Orders/5
        [ResponseType(typeof(customerOrdersVm))]
        public IHttpActionResult PutOrder(int id, customerOrdersVm vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!(id == vm.orderID))
            {
                return BadRequest();
            }

            Repo.Update(vm);
            Repo.Save();
            return StatusCode(HttpStatusCode.NoContent);
        }
        #endregion

        #region "Orders Delete"
        //  DELETE api/Orders/5
        [ResponseType(typeof(customerOrdersVm))]
        public IHttpActionResult DeleteOrder(int id)
        {
            var vm = Repo.GetOrder(id);
            if (vm == null)
            {
                return NotFound();
            }
            Repo.RemoveItem(vm);
            Repo.Save();
            return Ok(vm);
        }
        #endregion


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

    }
}
