using System;

namespace inventorySystem.Web.Models
{
    /// <summary>
    /// Inventory item model
    /// </summary>
    public class InventoryItemModel : AddInventoryItemModel
    {
        /// <summary>
        /// Gets or sets the item ID
        /// </summary>
        public Guid Id { get; set; }
    }
}