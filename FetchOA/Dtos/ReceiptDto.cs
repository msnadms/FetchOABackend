using FetchOA.Models;

namespace FetchOA.Dtos
{
    public class ReceiptDto
    {
        public string Retailer { get; set; }
        public string PurchaseDate { get; set; }
        public string PurchaseTime { get; set; }
        public float Total { get; set; }
        public List<Item> Items { get; set; }

    }
}
