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
    public class ShippersAsyncController : ApiController
    {
        private IShippersAsyncRepository<shipperVm> Repo;
        public ShippersAsyncController()
        {
            Repo = new ShippersAsyncJsonRepository();
        }
        public ShippersAsyncController(IShippersAsyncRepository<shipperVm> _Repo)
        {
            Repo = _Repo;
        }

        #region "Shippers"
        [ResponseType(typeof(shippersManagerVM))]
        public async Task<shippersManagerVM> GetShippers()
        {
            var shippers = await Repo.GetShippers();
            return shippers;
        }
        #endregion

        #region "Shippers Get By Id "
        [ResponseType(typeof(shipperVm))]
        public async Task<IHttpActionResult> GetShipper(int id)
        {
            var Vm =await Repo.GetShipper(id);
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
        public async Task<IHttpActionResult> PostShipper(shipperVm vm)
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

        #region "Shippers Put"
        // PUT api/Shippers/5
        [ResponseType(typeof(shipperVm))]
        public async Task<IHttpActionResult> PutShipper(int id, shipperVm vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!(id == vm.shipperID))
            {
                return BadRequest();
            }
          
              await  Repo.Update(vm);
              await  Repo.Save();
            return Ok(vm);
        }
        #endregion

        #region "Shippers Delete"
        //  DELETE api/Shippers/5
        [ResponseType(typeof(shipperVm))]
        public async Task<IHttpActionResult> DeleteShipper(int id)
        {
            var vm = await Repo.GetShipper(id);
            if (vm == null)
            {
                return NotFound();
            }
           await Repo.RemoveItem(vm);
           await Repo.Save();
            return Ok(vm);
        }
        #endregion


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
