using NWWebApi.Models;
using NWWebApi.RepositoryAsync;
using NWWebApi.StructureAsync;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace NWWebApi.Controllers
{
    [EnableCors("*", headers: "*", methods: "*")]
    public class CustomersAsyncController : ApiController
    {
        private ICustomersAsyncRepository<customerVm> Repo;
        public CustomersAsyncController()
        {
            Repo = new CustomersAsyncJsonRepository();
        }
        public CustomersAsyncController(ICustomersAsyncRepository<customerVm> _Repo)
        {
            Repo = _Repo;
        }
        [ResponseType(typeof(customersManagerVM))]
        public async Task<customersManagerVM> GetCustomers()
        {
            var customers = await Repo.GetCustomers();
            return customers;
        }
        [ResponseType(typeof(customerVm))]
        public async Task<IHttpActionResult> GetCustomer(string id)
        {
            var custVM = await Repo.GetCustomer(id);

            if ((custVM == null))
            {
                return NotFound();
            }

            return Ok(custVM);
        }

        // POST api/customers
        [ResponseType(typeof(customerVm))]
        public async Task<IHttpActionResult> PostCustomer(customerVm customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           await Repo.Add(customer);
           await Repo.Save();
           return Ok(customer);

        }

        // PUT api/customers/5
        [ResponseType(typeof(customerVm))]
        public async Task<IHttpActionResult> PutCustomer(string id, customerVm customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
                
                await Repo.Update(customer);
                await Repo.Save();
            return Ok(customer);
        }

        // DELETE api/customers/5
        [ResponseType(typeof(customerVm))]
        public async Task<IHttpActionResult> DeleteCustomer(string id)
        {

            var custVM = Repo.GetCustomer(id).Result;

            if (custVM == null)
            {
                return NotFound();
            }
            await Repo.RemoveItem(custVM);
            await Repo.Save();
            return Ok(custVM);
        }
    }
}
