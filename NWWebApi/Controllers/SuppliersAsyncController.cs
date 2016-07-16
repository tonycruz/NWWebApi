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
    public class SuppliersAsyncController : ApiController
    {
        private ISuppliersAsyncRepository<supplierVm> Repo;
        public SuppliersAsyncController()
        {
            Repo = new SuppliersAsyncJsonRepository();
        }
        public SuppliersAsyncController(ISuppliersAsyncRepository<supplierVm> _Repo)
        {
            Repo = _Repo;
        }

        #region "Suppliers"
        [ResponseType(typeof(SuppliersManagerVM))]
        public async Task<SuppliersManagerVM> GetSuppliers()
        {
            var suppliers = await Repo.GetSuppliers();
            return suppliers;
        }
        #endregion

        #region "Suppliers Get By Id "
        [ResponseType(typeof(supplierVm))]
        public async Task<IHttpActionResult> GetSupplier(int id)
        {
            var Vm = await Repo.GetSupplier(id);
            if ((Vm == null))
            {
                return NotFound();
            }
            return Ok(Vm);
        }
        #endregion

        #region "Suppliers Post"
        // POST api/SupplierVm
        [ResponseType(typeof(supplierVm))]
        public async Task<IHttpActionResult> PostSupplier(supplierVm vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await  Repo.Add(vm);
            await  Repo.Save();
            return CreatedAtRoute("DefaultApi", new { id = vm.supplierID }, vm);
        }
        #endregion

        #region "Suppliers Put"
        // PUT api/Suppliers/5
        [ResponseType(typeof(supplierVm))]
        public async Task<IHttpActionResult> PutSupplier(int id, supplierVm vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!(id == vm.supplierID))
            {
                return BadRequest();
            }
            await Repo.Update(vm);
            await  Repo.Save();
            return Ok(vm);
         }
        #endregion

        #region "Suppliers Delete"
        //  DELETE api/Suppliers/5
        [ResponseType(typeof(supplierVm))]
        public async Task<IHttpActionResult> DeleteSupplier(int id)
        {
            var vm = await Repo.GetSupplier(id);
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
