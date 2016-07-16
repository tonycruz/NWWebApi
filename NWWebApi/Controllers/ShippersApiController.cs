using NWWebApi.Infrastructure;
using NWWebApi.Models;
using NWWebApi.Repository;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace NWWebApi.Controllers
{
    [EnableCors("*", headers: "*", methods: "*")]
    public class ShippersApiController : ApiController
    {
        private IShippersRepository<shipperVm> Repo;
        public ShippersApiController()
        {
            Repo = new ShippersJsonRepository();
        }
        public ShippersApiController(IShippersRepository<shipperVm> _Repo)
        {
            Repo = _Repo;
        }

        #region "Shippers"
        [ResponseType(typeof(shippersManagerVM))]
        public shippersManagerVM GetShippers()
        {
            var shippers = Repo.GetShippers();
            return shippers;
        }
        #endregion

        #region "Shippers Get By Id "
        [ResponseType(typeof(shipperVm))]
        public IHttpActionResult GetShipper(int id)
        {
            var Vm = Repo.GetShipper(id);
            if ((Vm == null))
            {
                return NotFound();
            }
            return Ok(Vm);
        }
        #endregion

        #region "Shippers Post"
        // POST api/ShipperVm
        [ResponseType(typeof(shipperVm))]
        public IHttpActionResult PostShipper(shipperVm vm)
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

        #region "Shippers Put"
        // PUT api/Shippers/5
        [ResponseType(typeof(shipperVm))]
        public IHttpActionResult PutShipper(int id, shipperVm vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!(id == vm.shipperID))
            {
                return BadRequest();
            }
          
                Repo.Update(vm);
                Repo.Save();
            return Ok(vm);
        }
        #endregion

        #region "Shippers Delete"
        //  DELETE api/Shippers/5
        [ResponseType(typeof(shipperVm))]
        public IHttpActionResult DeleteShipper(int id)
        {
            var vm = Repo.GetShipper(id);
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
