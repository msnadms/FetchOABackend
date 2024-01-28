using FetchOA.Interfaces;
using FetchOA.Models;

namespace FetchOA
{
    public class ReceiptsRepository
    {

        // When using a database, operations would be done asynchronously and which would propagate throughout the services and controllers.
        // This change would involve wrapping return values in Task<>, making the methods async, and awaiting calls.
        // However, this is not necessary when using in memory storage

        private List<Receipt> receipts;

        public ReceiptsRepository() {
            this.receipts = new List<Receipt>();
        }
        public void AddReceipt(Receipt receipt)
        {
            receipts.Add(receipt);
        }

        public Receipt? GetReceiptById(Guid id)
        {
            return receipts.FirstOrDefault(x => x.Id == id);
        }
    }
}
