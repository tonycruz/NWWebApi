using NWWebApi.Infrastructure;
using NWWebApi.Models;
using NWWebApi.Repository;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace NWWebApi.Controllers
{
    [EnableCors("*", headers: "*", methods: "*")]
    public class CategoriesApiController : ApiController
    {
        private ICategoriesRepository<categoryVm> Repo;
        public CategoriesApiController()
        {
            Repo = new CategoriesJsonRepository();
        }
        public CategoriesApiController(ICategoriesRepository<categoryVm> _Repo)
        {
            Repo = _Repo;
        }

        #region "Categories"
        [ResponseType(typeof(CategoriesManagerVM))]
        public CategoriesManagerVM GetCategories()
        {
            var categories = Repo.GetCategories();
             return categories;
        }
        #endregion

        #region "Categories GetById "
        [ResponseType(typeof(categoryVm))]
        public IHttpActionResult GetCategory(int id)
        {
            var Vm = Repo.GetCategory(id);
            if ((Vm == null))
            {
                return NotFound();
            }
            return Ok(Vm);
        }
        #endregion

        #region "Categories Post"
        // POST api/CategoryVm
        [ResponseType(typeof(categoryVm))]
        public IHttpActionResult PostCategory(categoryVm vm)
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

        #region "Categories Put"
        // PUT api/Categories/5
        [ResponseType(typeof(categoryVm))]
        public IHttpActionResult Put(int id, categoryVm vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           // var Vm = Repo.GetCategory(id);
            Repo.Update(vm);
            Repo.Save();

            return Ok(vm);
        }
        #endregion

        #region "Categories Put"
        //  DELETE api/Categories/5
        [ResponseType(typeof(categoryVm))]
        public IHttpActionResult Delete(int id)
        {
            var vmcategory = Repo.GetCategory(id);
            if (vmcategory == null)
            {
                return NotFound();
            }
            Repo.RemoveItem(vmcategory);
            Repo.Save();

            return Ok(vmcategory);
        }
        #endregion


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

    }
}
