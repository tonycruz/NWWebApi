using NWWebApi.Infrastructure;
using NWWebApi.Models;
using NWWebApi.Repository;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace NWWebApi.Controllers
{
    [EnableCors("*", headers: "*", methods: "*")]
    public class ProductsApiController : ApiController
    {
        private IProductsRepository<productVm> Repo;
        public ProductsApiController()
        {
            Repo = new ProductsJsonRepository();
        }
        public ProductsApiController(IProductsRepository<productVm> _Repo)
        {
            Repo = _Repo;
        }
        #region "Products"
        [ResponseType(typeof(productsManagerVM))]
        public productsManagerVM GetProducts()
        {
            var products = Repo.GetProducts();
            return products;
        }
        #endregion

        #region "Products Get By Id "
        [ResponseType(typeof(productVm))]
        public IHttpActionResult GetProduct(int id)
        {
           var Vm = Repo.GetProduct(id);
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
        public IHttpActionResult PostProduct(productVm vm)
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

        #region "Products Put"
        // PUT api/Products/5
        [ResponseType(typeof(productVm))]
        public IHttpActionResult PutProduct(productVm vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
          
          
                Repo.Update(vm);
                Repo.Save();
           return Ok(vm);
        }
        #endregion

        #region "Products Delete"
        //  DELETE api/Products/5
        [ResponseType(typeof(productVm))]
        public IHttpActionResult DeleteProduct(int id)
        {
            var vm = Repo.GetProduct(id);
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
