using System.Collections.Generic;

namespace NWWebApi.Models
{
    public class CustomerOrderManagerVM
    {
        public List<customerOrdersVm> orders { get; set; }
        public List<order_DetailVm> orderDetails { get; set; }
       
    }
    public class OrderManagerVM
    {
        public List<customerOrdersVm> orders { get; set; }
        public List<order_DetailVm> orderDetails { get; set; }
        public List<productVm> products { get; set; }
        public List<customerVm> customers { get; set; }
        public customerOrdersVm order { get; set; }
        public List<shipperVm> shippers { get; set; }
    }
    public class EditOrderManagerVM
    {
        public customerOrdersVm order { get; set; }
        public List<order_DetailVm> orderDetails { get; set; }
        public List<productVm> products { get; set; }
        public List<customerVm> customers { get; set; }
        public List<shipperVm> shippers { get; set; }

    }
}