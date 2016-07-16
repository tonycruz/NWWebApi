using System.Collections.Generic;

namespace NWWebApi.Models
{
    public class customersManagerVM
    {
        public List<customerVm> customers { get; set; }
        public List<customerOrdersVm> orders { get; set; }
    }
}