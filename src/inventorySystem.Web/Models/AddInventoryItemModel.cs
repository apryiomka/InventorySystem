using System;
using inventorySyctem.Services.Entities;

namespace inventorySystem.Web.Models
{
    /// <summary>
    /// The add inventory item model
    /// </summary>
    public class AddInventoryItemModel
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
        public InventoryItem.Type ItemType { get; set; }
    }
}