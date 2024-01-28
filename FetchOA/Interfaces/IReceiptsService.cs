using FetchOA.Dtos;
using FetchOA.Models;

namespace FetchOA.Interfaces
{
    public interface IReceiptsService
    {
        Guid ProcessReceipts(Receipt receipt);

        int GetPoints(Guid receiptId);
    }
}
