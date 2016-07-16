using System.Collections.Generic;

namespace NWWebApi.Models
{
    public class productsManagerVM
    {
        public List<productVm> products { get; set; }
        public List<categoryVm> categories { get; set; }
        public List<supplierVm> suppliers { get; set; }
    }
}