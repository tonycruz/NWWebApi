using System.Collections.Generic;

namespace NWWebApi.Models
{
    public class SuppliersManagerVM
    {
        public List<productVm> products { get; set; }
        public List<supplierVm> suppliers { get; set; }
    }
}