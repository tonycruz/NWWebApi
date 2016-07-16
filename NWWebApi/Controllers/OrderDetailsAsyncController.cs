using NWWebApi.StructureAsync;
using NWWebApi.Models;
using NWWebApi.RepositoryAsync;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using System.Threading.Tasks;

namespace NWWebApi.Controllers
{
    [EnableCors("*", headers: "*", methods: "*")]
    public class OrderDetailsAsyncController : ApiController
    {
        private IOrderDetailsAsyncRepository<order_DetailManagerVm> Repo;
        public OrderDetailsAsyncController()
        {
            Repo = new OrderDetailsAsyncJsonRepository();
        }

       
        #region "Orders"
        [ResponseType(typeof(order_DetailManagerVm))]
        public async Task<order_DetailManagerVm> GetOrders(int id)
        {
            var orders = await Repo.GetOrderDetails(id);
            return orders;
        }
        #endregion
        #region "Orders Post"
        // POST api/OrderVm
        [ResponseType(typeof(order_DetailVm))]
        public async Task<IHttpActionResult> PostOrder(order_DetailVm vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           await Repo.Add(vm);
           await Repo.Save();
            return Ok(vm);
        }
        #endregion

        #region "Orders Put"
        // PUT api/Orders/5
        [ResponseType(typeof(order_DetailVm))]
        public async Task<IHttpActionResult> PutOrder(int id, order_DetailVm vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
          await  Repo.Update(vm);
          await  Repo.Save();
            return Ok(vm);
           
        }
        #endregion

        #region "Delete"
        //  DELETE api/Orders/5
        [ResponseType(typeof(order_DetailVm))]
        public async Task<IHttpActionResult> DeleteOrder(int id,int prodid)
        {
            var od = Repo.DeleteOrderDetailById(id, prodid).Result;
            if (od == null)
            {
                return NotFound();
            }
             await  Repo.Save();
            return Ok(od);
        }
        #endregion


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

    }
}
