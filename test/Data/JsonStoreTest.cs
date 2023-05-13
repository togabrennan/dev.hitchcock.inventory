using core.Data;
using core.Domain;
using NUnit.Framework;

namespace test.Data;

[TestFixture]
public class JsonStoreTest
{
    /// <summary>
    /// Verifying that both store types are added appropriately here
    /// </summary>
    /// <param name="type">Type of JsonStore (Bin / Item)</param>
    /// <param name="expectedQuantity">Expected items from seed</param>
    [Test]
    [TestCase(typeof(Bin), 5)]
    [TestCase(typeof(Item), 15)]
    public void TestGetAll(Type type, int expectedQuantity)
    {
        dynamic store = Activator.CreateInstance(typeof(JsonStore<>).MakeGenericType(type));
        var objects = store.GetAll();
        Assert.That(objects.Count, Is.EqualTo(expectedQuantity));
    }

    [Test]
    public void TestAddAndGetById()
    {
        var store = new JsonStore<Bin>();
        var bin = new Bin
        {
            Id = new Guid("a6f5c8d2-6cd8-4c44-8c6d-53d0a8f4d7c7"),
            Description = "Test Bin",
            Items = new List<Item>()
        };
        store.Add(bin);
        var result = store.GetById(bin.Id);
        var objects = store.GetAll();
        Assert.That(result, Is.EqualTo(bin));
        Assert.That(objects.Count, Is.EqualTo(6));
    }

    [Test]
    public void TestDelete()
    {
        var store = new JsonStore<Bin>();
        var id = new Guid("7dd5c7e5-2e9e-4ebb-b7f6-269c611c83b6");
        var bin = store.GetById(id);
        Assert.IsNotNull(bin);
        store.Delete(id);
        bin = store.GetById(id);
        Assert.IsNull(bin);
    }

    [Test]
    public void TestUpdate()
    {
        var store = new JsonStore<Bin>();
        var id = new Guid("7dd5c7e5-2e9e-4ebb-b7f6-269c611c83b6");
        var bin = store.GetById(id);

        Assert.IsNotNull(bin);
        bin.Description = "Updated Bin";
        store.Update(bin);
        bin = store.GetById(id);
        Assert.That(bin.Description, Is.EqualTo("Updated Bin"));
    }

}

