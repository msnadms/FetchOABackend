using FetchOA.Dtos;
using FetchOA.Interfaces;
using FetchOA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace FetchOA
{
    [ApiController]
    [Route("[controller]")]
    public class ReceiptsController : ControllerBase, IReceiptsController
    {

        private IReceiptsService receiptsService;

        public ReceiptsController(IReceiptsService receiptsService) 
        {
            this.receiptsService = receiptsService;
        }

        [Route("process")]
        [HttpPost]
        public ActionResult<IdDto> ProcessReceipts([FromBody] ReceiptDto receiptDto)
        {
            var receiptId = receiptsService.ProcessReceipts(receiptDto);
            var idDto = new IdDto
            {
                Id = receiptId,
            };
            return Ok(idDto);

        }

        [Route("{id}/points")]
        [HttpGet]
        public ActionResult<PointsDto> GetPoints([FromRoute] Guid id)
        {
            int points = receiptsService.GetPoints(id);
            if (points == -1)
            {
                return NotFound("No receipt found for that id");
            }
            var pointsDto = new PointsDto
            {
                points = points,
            };
            return pointsDto;
        }





    }
}
