using NWWebApi.Infrastructure;
using NWWebApi.Models;
using NWWebApi.Repository;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace NWWebApi.Controllers
{
    [EnableCors("*", headers: "*", methods: "*")]
    public class CustomersApiController : ApiController
    {
        private ICustomersRepository<customerVm> Repo;
        public CustomersApiController()
        {
            Repo = new CustomersJsonRepository();
        }
        public CustomersApiController(ICustomersRepository<customerVm> _Repo)
        {
            Repo = _Repo;
        }
        [ResponseType(typeof(customersManagerVM))]
        public customersManagerVM GetCustomers()
        {
            var customers = Repo.GetCustomers();
            return customers;
        }
        [ResponseType(typeof(customerVm))]
        public IHttpActionResult GetCustomer(string id)
        {
            var custVM = Repo.GetCustomer(id);

            if ((custVM == null))
            {
                return NotFound();
            }

            return Ok(custVM);
        }

        // POST api/customers
        [ResponseType(typeof(customerVm))]
        public IHttpActionResult PostCustomer(customerVm customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Repo.Add(customer);
            Repo.Save();
            return Ok(customer);

        }

        // PUT api/customers/5
        [ResponseType(typeof(customerVm))]
        public IHttpActionResult PutCustomer(string id, customerVm customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
                
                Repo.Update(customer);
                Repo.Save();
            return Ok(customer);
        }

        // DELETE api/customers/5
        [ResponseType(typeof(customerVm))]
        public IHttpActionResult DeleteCustomer(string id)
        {

            var custVM = Repo.GetCustomer(id);

            if (custVM == null)
            {
                return NotFound();
            }
            Repo.RemoveItem(custVM);
            Repo.Save();
            return Ok();
        }
    }
}
