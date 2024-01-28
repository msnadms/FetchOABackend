using System.Runtime.CompilerServices;

namespace FetchOA.Models
{
    public class Receipt
    {
        public Guid Id { get; set; }
        public string Retailer { get; set; }
        public string PurchaseDate { get; set; }
        public string PurchaseTime { get; set; }
        public float Total { get; set; }
        public List<Item> Items { get; set; }

    }
}
