using NWWebApi.StructureAsync;
using NWWebApi.Models;
using NWWebApi.Repository;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using System.Threading.Tasks;

namespace NWWebApi.Controllers
{
    [EnableCors("*", headers: "*", methods: "*")]
    public class CategoriesAsyncController : ApiController
    {
        private ICategoriesAsyncRepository<categoryVm> Repo;
        public CategoriesAsyncController()
        {
            Repo = new CategoriesAsyncJsonRepository();
        }
        public CategoriesAsyncController(ICategoriesAsyncRepository<categoryVm> _Repo)
        {
            Repo = _Repo;
        }

        #region "Categories"
        [ResponseType(typeof(CategoriesManagerVM))]
        public async Task<CategoriesManagerVM> GetCategories()
        {
            var categories = await Repo.GetCategories();
             return categories;
        }
        #endregion

        #region "Categories GetById "
        [ResponseType(typeof(categoryVm))]
        public async Task<IHttpActionResult> GetCategory(int id)
        {
            var Vm = await Repo.GetCategory(id);
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
        public async Task<IHttpActionResult> PostCategory(categoryVm vm)
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

        #region "Categories Put"
        // PUT api/Categories/5
        [ResponseType(typeof(categoryVm))]
        public async Task<IHttpActionResult> PutCategory(int id, categoryVm vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                    
               await Repo.Update(vm);
               await Repo.Save();

            return Ok(vm);
        }
        #endregion

        #region "Categories Put"
        //  DELETE api/Categories/5
        [ResponseType(typeof(categoryVm))]
        public async Task<IHttpActionResult> DeleteCategory(categoryVm vm)
        {
            var vmcategory = await Repo.GetCategory(vm.categoryID);
            if (vm == null)
            {
                return NotFound();
            }
            await Repo.RemoveItem(vm);
            await Repo.Save();

            return Ok();
        }
        #endregion


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

    }
}
