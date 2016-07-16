using System.Collections.Generic;

namespace NWWebApi.Models
{
    public class shippersManagerVM
    {
        public List<shipperVm> shippers { get; set; }
        public List<customerOrdersVm> orders { get; set; }
    }
}