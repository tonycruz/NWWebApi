using NWWebApi.Infrastructure;
using NWWebApi.Models;
using NWWebApi.Repository;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace NWWebApi.Controllers
{
    [EnableCors("*", headers: "*", methods: "*")]
    public class OrderDetailsApiController : ApiController
    {
        private IOrderDetailsRepository<order_DetailManagerVm> Repo;
        public OrderDetailsApiController()
        {
            Repo = new OrderDetailsJsonRepository();
        }

       
        #region "Orders"
        [ResponseType(typeof(order_DetailManagerVm))]
        public order_DetailManagerVm GetOrders(int id)
        {
            var orders = Repo.GetOrderDetails(id);
            return orders;
        }
        #endregion
        #region "Orders Post"
        // POST api/OrderVm
        [ResponseType(typeof(order_DetailVm))]
        public IHttpActionResult PostOrder(order_DetailVm vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Repo.Add(vm);
            Repo.Save();
            return Ok(vm);
        }
        #endregion

        #region "Orders Put"
        // PUT api/Orders/5
        [ResponseType(typeof(order_DetailVm))]
        public IHttpActionResult PutOrder(int id, order_DetailVm vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           // var vm = Repo.GetOrderDetails(id);
            Repo.Update(vm);
            Repo.Save();
            return Ok();
           
        }
        #endregion

        #region "Delete"
        //  DELETE api/Orders/5
        [ResponseType(typeof(order_DetailVm))]
        public IHttpActionResult DeleteOrder(int id,int prodid)
        {
            var od = Repo.DeleteOrderDetailById(id, prodid);
            if (od == null)
            {
                return NotFound();
            }
              Repo.Save();
            return Ok(od);
        }
        #endregion


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

    }
}
