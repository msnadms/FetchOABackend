using FetchOA.Models;

namespace FetchOA.Dtos
{
    public class ReceiptDto
    {
        public string? Retailer { get; set; }
        public string? PurchaseDate { get; set; }
        public string? PurchaseTime { get; set; }
        public List<ItemDto>? Items { get; set; }
        public string? Total { get; set; }

    }
}
