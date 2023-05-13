using System.Threading;
using System.Threading.Tasks;
using core.Commands;
using core.Data;
using core.Domain;
using core.Handlers;
using Moq;
using NUnit.Framework;

namespace test.Handlers
{
    [TestFixture]
    public class AddItemToBinHandlerTest
    {
        private Mock<IPersistence<Bin>> _mockBinStore;
        private AddItemHandler _handler;

        [SetUp]
        public void Setup()
        {
            _mockBinStore = new Mock<IPersistence<Bin>>();
            _handler = new AddItemHandler(new JsonStore<Bin>());
        }

        [Test]
        public async Task Handle_ValidInput_AddsItemToBinAndReturnsBin()
        {
            // Arrange
            var binId = Guid.NewGuid();
            var item = new Item { Id = Guid.NewGuid(), Description = "Test Item", Quantity = 1 };
            var bin = new Bin { Id = binId, Description = "Test Bin", Items = new List<Item>() };
            var originalBin = new Bin { Id = binId, Description = "Test Bin", Items = new List<Item>() };
            var command = new AddItemToBinCommand(item, bin);
            _mockBinStore.Setup(s => s.GetById(binId)).Returns(bin);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);

            Assert.Multiple(() =>
            {
                Assert.That(result.Id, Is.EqualTo(originalBin.Id));
                Assert.That(result.Description, Is.EqualTo(originalBin.Description));
                Assert.That(result.Items.Count, Is.EqualTo(originalBin.Items.Count + 1));
                Assert.IsTrue(result.Items.Contains(item));
            });
        }
    }
}