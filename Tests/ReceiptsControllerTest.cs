using AutoMapper;
using FetchOA.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Tests
{
    public class ReceiptsControllerTest
    {

        private Mock<IReceiptsService> receiptsService { get; set; }
        private IMapper mapper { get; set; }
        private ReceiptsController receiptsController { get; set; }
        private Guid Id { get; set; }

        [SetUp]
        public void Setup()
        {
            Id = Guid.NewGuid();
            receiptsService = new Mock<IReceiptsService>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            mapper = new Mapper(config);
            receiptsService.Setup(rs => rs.ProcessReceipts(It.IsAny<Receipt>())).Returns(Id);
            receiptsService.Setup(rs => rs.GetPoints(Id)).Returns(1);
            receiptsService.Setup(rs => rs.GetPoints(Guid.Empty)).Returns(-1);
            receiptsController = new ReceiptsController(mapper, receiptsService.Object);

        }

        [Test]
        public void ProcessReceipts_ReturnsOk_When_GivenValidReceiptDto()
        {
            var receiptDto = new ReceiptDto
            {
                Retailer = "test",
                PurchaseDate = "2024-1-28",
                PurchaseTime = "12:00",
                Items = new List<ItemDto>() { new ItemDto { ShortDescription = "test", Price = "1" } },
                Total = "1",
            };
        
            var result = receiptsController.ProcessReceipts(receiptDto);

            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public void ProcessReceipts_ReturnsBadRequest_When_GivenInvalidReceiptDto()
        {
            var receiptDto = new ReceiptDto
            {
                Retailer = "test",
                PurchaseDate = "2024-1-28",
                Items = new List<ItemDto>() { new ItemDto { ShortDescription = "test", Price = "1" } },
                Total = "1",
            }; 
            //No PurchaseTime

            var result = receiptsController.ProcessReceipts(receiptDto);

            Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());

        }

        [Test]
        public void ProcessReceipts_ReturnsBadRequest_WhenAutoMapperException()
        {
            var receiptDto = new ReceiptDto
            {
                Retailer = "test",
                PurchaseDate = "test",
                PurchaseTime = "12:00",
                Items = new List<ItemDto>() { new ItemDto { ShortDescription = "test", Price = "1" } },
                Total = "1",
            };

            var result = receiptsController.ProcessReceipts(receiptDto);

            Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public void GetPoints_ReturnsOk_When_GivenValidId()
        {
            var result = receiptsController.GetPoints(Id);

            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public void GetPoints_ReturnsBadRequest_When_GivenInvalidId()
        {
            var result = receiptsController.GetPoints(Guid.Empty);

            Assert.That(result.Result, Is.InstanceOf<NotFoundObjectResult>());
        }

    }
}
