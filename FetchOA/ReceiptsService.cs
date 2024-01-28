using AutoMapper;
using FetchOA.Dtos;
using FetchOA.Interfaces;
using FetchOA.Models;

namespace FetchOA
{
    public class ReceiptsService : IReceiptsService
    {

        private IMapper mapper;
        private ReceiptsRepository receiptsRepository;

        public ReceiptsService(IMapper mapper, ReceiptsRepository receiptsRepository)
        {
            this.mapper = mapper;
            this.receiptsRepository = receiptsRepository;
        }

        public Guid ProcessReceipts(ReceiptDto receiptDto)
        {
            var receipt = mapper.Map<Receipt>(receiptDto);
            receipt.Id = Guid.NewGuid();
            receiptsRepository.AddReceipt(receipt);
            return receipt.Id;
        }

        public int GetPoints(Guid receiptId)
        {
            Receipt? receipt = receiptsRepository.GetReceiptById(receiptId);
            if (receipt == null)
            {
                return -1;
            }

            // Check for proper formatting and set to default values if not properly formatted
            TimeOnly purchaseTime = default;
            TimeOnly.TryParse(receipt.PurchaseTime, out purchaseTime);

            DateOnly purchaseDate = default;
            DateOnly.TryParse(receipt.PurchaseDate, out purchaseDate);

            int points = 0;
            var total = receipt.Total;
            var retailer = receipt.Retailer;
            points += Array.FindAll(retailer.ToCharArray(), Char.IsLetterOrDigit).Length;
            if (total == Math.Floor(total))
            {
                points += 50;
            }
            if (total % 0.25 == 0)
            {
                points += 25;
            }
            points += ((receipt.Items.Count / 2) * 5);
            foreach (var item in receipt.Items)
            {
                if (item.ShortDescription.Trim().Length % 3 == 0)
                {
                    points += ((int) Math.Ceiling(item.Price * 0.2));
                }
            }
            if (purchaseDate.Day % 2 == 1)
            {
                points += 6;
            }
            TimeOnly start = new(14, 0);
            TimeOnly end = new(16, 0);
            if (purchaseTime >= start && purchaseTime <= end)
            {
                points += 10;
            }

            return points;

        }


    }
}
