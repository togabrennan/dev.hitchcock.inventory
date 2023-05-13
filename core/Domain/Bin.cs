using System;
using System.Text.Json.Serialization;

namespace core.Domain
{
	public class Bin : IBin, IHasId
	{
        [JsonPropertyName("id")]
		public Guid Id { get; set; }
        [JsonPropertyName("description")]
		public string Description { get; set; } = "";
        [JsonPropertyName("items")]
		public List<Item> Items { get; set; } = new List<Item>();

        public void AddItem(Item item)
        {
            if (!Items.Contains(item))
                Items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            Items.Remove(item);
        }
    }
}

