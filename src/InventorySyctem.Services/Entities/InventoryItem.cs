using System;
using System.ComponentModel;

namespace inventorySyctem.Services.Entities
{
    /// <summary>
    /// The inventory Item
    /// </summary>
    public class InventoryItem : Entity<Guid>
    {
        /// <summary>
        /// Gets or sets the item label
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// Gets or sets the item expiration date
        /// </summary>
        /// <remarks>
        /// If the value is not set, the item will never expire
        /// </remarks>
        public DateTime? Expiration { get; set; }
        /// <summary>
        /// Gets or sets the type of the item
        /// </summary>
        public Type ItemType { get; set; }

        public enum Type
        {
            [Description("The default item type. Set to unset")]
            Unset = 0,
            [Description("Item type 1")]
            ItemType1,
            [Description("Item type 2")]
            ItemType2,
            [Description("Item type 3")]
            ItemType3
        }
    }
}
