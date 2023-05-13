using System;
namespace core.Domain
{
    // Making some assumptions here that bins have unlimited capacity, and that
    // (at least currently) that we add and remove all of a discrete item from
    // inventory at the same time. (Although an Item does have a quantity so 
    // potentially we could do a check and throw errors if the quantity is too
    // high


    /// <summary>
    /// Defines the interface for a bin and associated operations
    /// </summary>
	public interface IBin
	{
        Guid Id { get; set; }
        string Description { get; set; }
        List<Item> Items { get; set; }

        void AddItem(Item item);
        void RemoveItem(Item item);
    }
}

