using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using inventorySyctem.Services.Entities;

namespace inventorySyctem.Services
{
    /// <summary>
    /// The inventory manager
    /// </summary>
    /// <remarks>
    /// Stores and retrieves items from the database as well as validates the incoming items
    /// </remarks>
    public interface IInventoryManager
    {
        /// <summary>
        /// Adds a new item to the inventory
        /// </summary>
        /// <param name="inventoryItem"></param>
        /// <returns>
        /// The add result of the Inventory Item
        /// </returns>
        /// <exception cref="ArgumentNullException">inventoryItem is required</exception>
        Task<Guid> AddNewItem(InventoryItem inventoryItem);

        /// <summary>
        /// Deletes items from the inventory by label
        /// </summary>
        /// <param name="itemLabel">The item label</param>
        /// <returns>The list of the removed items</returns>
        /// <exception cref="ArgumentNullException">itemLabel is required</exception>
        Task<IEnumerable<InventoryItem>> TakeItems(string itemLabel);
    }
}
