namespace core.Domain;

public class Item : IItem, IHasId
{
    public Guid Id { get; set; }
    public string Description { get; set; } = "";
    public int Quantity { get; set; } = 0;

    public void Add(int quantity)
    {
        this.Quantity += quantity;
    }

    public void Adjust(string description)
    {
        throw new NotImplementedException();
    }

    public void Remove(int quantity)
    {
        if (this.Quantity - quantity < 0) throw new Exception("Unable to remove more items than we have");

        this.Quantity -= quantity;

        if (this.Quantity == 0)
        {
            // remove from appropriate bin (fire a command) and forget for
            // some eventual consistency

            // I don't want to remove the item at this time, because potentially
            // we're just out of stock and want to still show this item while
            // we wait for restock
        }
    }
}