using FetchOA.Dtos;
using FetchOA.Models;
using Microsoft.AspNetCore.Mvc;

namespace FetchOA.Interfaces
{
    public interface IReceiptsController
    {
        ActionResult<IdDto> ProcessReceipts([FromBody] ReceiptDto receiptDto);
        ActionResult<PointsDto> GetPoints([FromRoute] Guid id);
    }
}
