using System;
using api.Controllers;
using core.Domain;
using MediatR;
using Moq;
using Microsoft.AspNetCore.Mvc;
using core.Queries;
using core.Commands;

namespace test.Controllers
{
    [TestFixture]
    public class BinControllerTests
    {
        private Mock<ISender> _mockSender;
        private BinController _controller;

        [SetUp]
        public void Setup()
        {
            _mockSender = new Mock<ISender>();
            _controller = new BinController(_mockSender.Object);
        }

        [Test]
        public async Task AddItemToBin_ValidInput_ReturnsOk()
        {
            // Arrange
            var binId = Guid.NewGuid();
            var item = new Item { Id = Guid.NewGuid(), Description = "Test Item", Quantity = 1 };
            var bin = new Bin { Id = binId, Description = "Test Bin", Items = new List<Item>() };
            var resultBin = new Bin { Id = binId, Description = "Test Bin", Items = new List<Item> { item } };
            _mockSender.Setup(s => s.Send(It.IsAny<GetBinDetailQuery>(), CancellationToken.None)).ReturnsAsync(bin);
            _mockSender.Setup(s => s.Send(It.IsAny<AddItemToBinCommand>(), CancellationToken.None)).ReturnsAsync(resultBin);

            // Act
            var result = await _controller.AddItemToBin(binId, item);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.IsInstanceOf<Bin>(okResult.Value);

            var fullerBin = (Bin)okResult.Value;

            Assert.Multiple(() =>
            {
                Assert.That(fullerBin.Id, Is.EqualTo(binId));
                Assert.That(fullerBin.Description, Is.EqualTo(bin.Description));
                Assert.That(fullerBin.Items.Count, Is.EqualTo(bin.Items.Count + 1));
                Assert.That(fullerBin.Items.Any(i => i.Id == item.Id), Is.True);
            });
        }

        [Test]
        public async Task AddItemToBin_InvalidBin_ReturnsBadRequest()
        {
            // Arrange
            var binId = Guid.NewGuid();
            var item = new Item { Id = Guid.NewGuid(), Description = "Test Item", Quantity = 1 };
            _mockSender.Setup(s => s.Send(It.IsAny<GetBinDetailQuery>(), CancellationToken.None));

            // Act
            var result = await _controller.AddItemToBin(binId, item);

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.That(badRequestResult.Value, Is.InstanceOf<string>());
            var errorMessage = (string)badRequestResult.Value;
            Assert.That(errorMessage, Is.EqualTo("Invalid Bin specified"));
        }
    }
}