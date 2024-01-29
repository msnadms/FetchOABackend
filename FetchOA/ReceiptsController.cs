using AutoMapper;
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

        private IReceiptsService ReceiptsService { get; set; }
        private IMapper Mapper { get; set; }

        public ReceiptsController(IMapper mapper, IReceiptsService receiptsService) 
        {
            this.Mapper = mapper;
            this.ReceiptsService = receiptsService;
        }

        [Route("process")]
        [HttpPost]
        public ActionResult<IdDto> ProcessReceipts([FromBody] ReceiptDto receiptDto)
        {
            if (!ModelState.IsValid 
                || receiptDto.PurchaseDate == null 
                || receiptDto.PurchaseTime == null 
                || receiptDto.Retailer == null 
                || receiptDto.Items == null || !receiptDto.Items.Any()
                || receiptDto.Total == null)
            {
                return BadRequest("The receipt is invalid");
            }
            try
            {
                var receipt = Mapper.Map<Receipt>(receiptDto);
                receipt.Id = Guid.NewGuid();
                var receiptId = ReceiptsService.ProcessReceipts(receipt);
                var idDto = new IdDto
                {
                    Id = receiptId,
                };
                return Ok(idDto);
            } catch (AutoMapperMappingException)
            {
                return BadRequest("The receipt is invalid");
            }

        }

        [Route("{id}/points")]
        [HttpGet]
        public ActionResult<PointsDto> GetPoints([FromRoute] Guid id)
        {
            int points = ReceiptsService.GetPoints(id);
            if (points == -1)
            {
                return NotFound("No receipt found for that id");
            }
            var pointsDto = new PointsDto
            {
                points = points,
            };
            return Ok(pointsDto);
        }





    }
}
