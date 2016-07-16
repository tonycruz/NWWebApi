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
    public class ProductsAsyncController : ApiController
    {
        private IProductsAsyncRepository<productVm> Repo;
        public ProductsAsyncController()
        {
            Repo = new ProductsAsyncJsonRepository();
        }
        public ProductsAsyncController(IProductsAsyncRepository<productVm> _Repo)
        {
            Repo = _Repo;
        }
        #region "Products"
        [ResponseType(typeof(productsManagerVM))]
        public async Task<productsManagerVM> GetProducts()
        {
            var products = await Repo.GetProducts();
            return products;
        }
        #endregion

        #region "Products Get By Id "
        [ResponseType(typeof(productVm))]
        public async Task<IHttpActionResult> GetProduct(int id)
        {
           var Vm = await Repo.GetProduct(id);
            if ((Vm == null))
            {
                return NotFound();
            }
            return Ok(Vm);
        }
        #endregion

        #region "Products Post"
        // POST api/ProductVm
        [ResponseType(typeof(productVm))]
        public async Task<IHttpActionResult> PostProduct(productVm vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

          await  Repo.Add(vm);
          await  Repo.Save();
            return Ok(vm);
        }
        #endregion

        #region "Products Put"
        // PUT api/Products/5
        [ResponseType(typeof(productVm))]
        public async Task<IHttpActionResult> PutProduct(productVm vm)
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

        #region "Products Delete"
        //  DELETE api/Products/5
        [ResponseType(typeof(productVm))]
        public async Task<IHttpActionResult> DeleteProduct(int id)
        {
            var vm = await Repo.GetProduct(id);
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
