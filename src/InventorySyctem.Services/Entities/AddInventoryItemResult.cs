using System;

namespace inventorySyctem.Services.Entities
{
    /// <summary>
    /// The result of added item
    /// </summary>
    public class AddInventoryItemResult : ActionResult
    {
        /// <summary>
        /// Gets or sets the item id after it was added
        /// </summary>
        public Guid? Id { get; set; }
    }
}
