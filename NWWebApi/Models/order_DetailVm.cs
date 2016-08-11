namespace NWWebApi.Models
{
    public class order_DetailVm
    {
        public int orderDetailID { get; set; }
        public int? orderID { get; set; }
        public int? productID { get; set; }
        public string productName { get; set; }
        public decimal? unitPrice { get; set; }
        public short? quantity { get; set; }
        public float? discount { get; set; }
        public decimal? extendedPrice { get; set; }
    }
}