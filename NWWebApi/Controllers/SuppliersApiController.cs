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
    public class SuppliersApiController : ApiController
    {
        private ISuppliersRepository<supplierVm> Repo;
        public SuppliersApiController()
        {
            Repo = new SuppliersJsonRepository();
        }
        public SuppliersApiController(ISuppliersRepository<supplierVm> _Repo)
        {
            Repo = _Repo;
        }

        #region "Suppliers"
        [ResponseType(typeof(SuppliersManagerVM))]
        public SuppliersManagerVM GetSuppliers()
        {
            var suppliers = Repo.GetSuppliers();
            return suppliers;
        }
        #endregion

        #region "Suppliers Get By Id "
        [ResponseType(typeof(supplierVm))]
        public IHttpActionResult GetSupplier(int id)
        {
            var Vm = Repo.GetSupplier(id);
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
        public IHttpActionResult PostSupplier(supplierVm vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Repo.Add(vm);
            Repo.Save();
            return CreatedAtRoute("DefaultApi", new { id = vm.supplierID }, vm);
        }
        #endregion

        #region "Suppliers Put"
        // PUT api/Suppliers/5
        [ResponseType(typeof(supplierVm))]
        public IHttpActionResult PutSupplier(int id, supplierVm vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!(id == vm.supplierID))
            {
                return BadRequest();
            }
            Repo.Update(vm);
            Repo.Save();
                     
            return StatusCode(HttpStatusCode.NoContent);
        }
        #endregion

        #region "Suppliers Delete"
        //  DELETE api/Suppliers/5
        [ResponseType(typeof(supplierVm))]
        public IHttpActionResult DeleteSupplier(int id)
        {
            var vm = Repo.GetSupplier(id);
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
