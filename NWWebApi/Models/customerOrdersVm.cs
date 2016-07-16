namespace NWWebApi.Models
{
    public class customerOrdersVm
    {
        public string companyName { get; set; }
        public string contactName { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string region { get; set; }
        public string postalCode { get; set; }
        public int orderID { get; set; }
        public string customerID { get; set; }
        public int? employeeID { get; set; }
        public System.DateTime? orderDate { get; set; }
        public System.DateTime? requiredDate { get; set; }
        public System.DateTime? shippedDate { get; set; }
        public int? shipVia { get; set; }
        public decimal? freight { get; set; }
        public string shipName { get; set; }
        public string shipAddress { get; set; }
        public string shipCity { get; set; }
        public string shipRegion { get; set; }
        public string shipPostalCode { get; set; }
        public string shipCountry { get; set; }
    }
}