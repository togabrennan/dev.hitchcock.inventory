using core.Domain;

namespace test.Domain;

[TestFixture]
public class BinTest
{
    [Test]
    public void Test_Bin_ConstructedWithDefaultValues()
    {
        // Arrange
        var bin = new Bin();

        // Assert
        Assert.That(bin.Id, Is.EqualTo(Guid.Empty));
        Assert.That(bin.Description, Is.EqualTo(string.Empty));
        Assert.That(bin.Items.Count, Is.EqualTo(0));
    }

    [Test]
    public void Test_Bin_ConstructedWithValues()
    {
        // Arrange
        var id = Guid.NewGuid();
        var description = "Test Item";
        var items = new List<Item> { new Item { Id = Guid.NewGuid(), Description = "New Item", Quantity = 10 } };


        // Act

        var bin = new Bin()
        {
            Id = id,
            Description = description,
            Items = items
        };

        // Assert
        Assert.That(bin.Id, Is.EqualTo(id));
        Assert.That(bin.Description, Is.EqualTo(description));
        Assert.That(bin.Items.Count, Is.EqualTo(1));
    }
}