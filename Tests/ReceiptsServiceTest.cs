namespace Tests
{
    public class ReceiptsServiceTest
    {

        private ReceiptsRepository receiptsRepository { get; set; }
        private IReceiptsService receiptsService { get; set; }

        [SetUp]
        public void Setup()
        {
            receiptsRepository = Mock.Of<ReceiptsRepository>();
            receiptsService = new ReceiptsService(receiptsRepository);
        }

        [Test]
        public void ProcessReceipts_ReturnsId_WhenGivenReceipt()
        {
            var receipt = new Receipt
            {
                Id = Guid.NewGuid(),
                Retailer = "test",
                PurchaseDate = DateOnly.MinValue,
                PurchaseTime = TimeOnly.MinValue,
            };

            var result = receiptsService.ProcessReceipts(receipt);

            Assert.That(result, Is.Not.EqualTo(default(Guid)));
        }

        [Test]
        public void GetPoints_ReturnsNegativeOne_When_Invalid_Id()
        {
            var result = receiptsService.GetPoints(Guid.NewGuid());

            Assert.That(result, Is.EqualTo(-1));
        }

        [Test]
        public void GetPoints_ReturnsCorrectPoints_GivenReceiptId()
        {
            var receipt = new Receipt
            {
                Id = Guid.NewGuid(),
                Retailer = "M&M Corner Market",
                PurchaseDate = DateOnly.Parse("2022-03-20"),
                PurchaseTime = TimeOnly.Parse("14:33"),
                Items = new List<Item>
                {
                    new Item { ShortDescription = "Gatorade", Price = 2.25F },
                    new Item { ShortDescription = "Gatorade", Price = 2.25F },
                    new Item { ShortDescription = "Gatorade", Price = 2.25F },
                    new Item { ShortDescription = "Gatorade", Price = 2.25F },
                },
                Total = 9F,
            };

            var id = receiptsService.ProcessReceipts(receipt);

            var points = receiptsService.GetPoints(id);

            Assert.That(points, Is.EqualTo(109));
        }
    }
}
