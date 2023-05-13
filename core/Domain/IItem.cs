using System;
namespace core.Domain
{
    /// <summary>
    /// Defines the interface for an Item and associated operations
    /// </summary>
	public interface IItem
	{
        public Guid Id { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }

        public void Add(int quantity);
        public void Adjust(string description);
        public void Remove(int quantity);
    }
}

