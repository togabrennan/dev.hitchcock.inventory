using core.Domain;

namespace test.Domain;

[TestFixture]
public class ItemTest
{
    [Test]
    public void Test_Item_ConstructedWithDefaultValues()
    {
        // Arrange
        var item = new Item();

        // Assert
        Assert.That(item.Id, Is.EqualTo(Guid.Empty));
        Assert.That(item.Description, Is.EqualTo(string.Empty));
        Assert.That(item.Quantity, Is.EqualTo(0));
    }

    [Test]
    public void Test_Item_ConstructedWithValues()
    {
        // Arrange
        var id = Guid.NewGuid();
        var description = "Test Item";
        var quantity = 42;

        // Act

        var item = new Item
        {
            Id = id,
            Description = description,
            Quantity = quantity
        };

        // Assert
        Assert.That(item.Id, Is.EqualTo(id));
        Assert.That(item.Description, Is.EqualTo(description));
        Assert.That(item.Quantity, Is.EqualTo(quantity));
    }

    [Test]
    [TestCase("0ab2ef64-8977-4b8d-ae3f-228041a0d344", "Magic Spanner", "3")]
    [TestCase("1d07effc-b02c-4ded-a540-b64d33507728", "Rusty spoon", "67")]
    [TestCase("50dc0173-94c4-4076-8500-f336f2bbcfae", "Arc Reactor", "1")]
    public void Test_Item_Add(Guid id, string description, int quantity)
    {
        // Arrange
        var item = new Item
        {
            Id = id,
            Description = description,
            Quantity = quantity
        };

        // Act
        item.Add(7);

        // Assert
        Assert.That(item.Quantity, Is.EqualTo(quantity + 7));
    }

    [Test]
    [TestCase("1d07effc-b02c-4ded-a540-b64d33507728", "Rusty spoon", "67")]
    [TestCase("50dc0173-94c4-4076-8500-f336f2bbcfae", "Arc Reactor", "1")]
    public void Test_Item_Remove(Guid id, string description, int quantity)
    {
        // Arrange
        var item = new Item
        {
            Id = id,
            Description = description,
            Quantity = quantity
        };

        // Act
        item.Remove(1);

        // Assert
        Assert.That(item.Quantity, Is.EqualTo(quantity - 1));
    }

    [Test]
    [TestCase("0ab2ef64-8977-4b8d-ae3f-228041a0d344", "Magic Spanner", "3")]
    [TestCase("1d07effc-b02c-4ded-a540-b64d33507728", "Rusty spoon", "67")]
    [TestCase("50dc0173-94c4-4076-8500-f336f2bbcfae", "Arc Reactor", "1")]
    public void Test_Item_Remove_Throws_Exception_when_too_many_removed(
        Guid id,
        string description,
        int quantity)
    {
        // Arrange
        var expectedErrorMessage = "Unable to remove more items than we have";
        var item = new Item
        {
            Id = id,
            Description = description,
            Quantity = quantity
        };

        // Act & Assert

        // Assert
        var exception = Assert.Throws<Exception>(() => item.Remove(100), "Llama");
        Assert.That(exception.Message, Is.EqualTo(expectedErrorMessage));     
    }


}