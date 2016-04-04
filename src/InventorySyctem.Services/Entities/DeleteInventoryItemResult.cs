using System.Collections.Generic;

namespace inventorySyctem.Services.Entities
{
    /// <summary>
    /// Inventory delete result
    /// </summary>
    public class DeleteInventoryItemResult : ActionResult
    {
        /// <summary>
        /// Gets or sets the deleted inventory items
        /// </summary>
        public IEnumerable<InventoryItem> DeletedItems { get; internal set; } 
    }
}
