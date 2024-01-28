﻿using FetchOA.Dtos;
using FetchOA.Models;

namespace FetchOA.Interfaces
{
    public interface IReceiptsService
    {
        Guid ProcessReceipts(ReceiptDto receiptDto);

        int GetPoints(Guid receiptId);
    }
}
