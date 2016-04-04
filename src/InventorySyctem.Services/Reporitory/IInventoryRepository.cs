using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using inventorySyctem.Services.Entities;

namespace inventorySyctem.Services.Reporitory
{
    /// <summary>
    /// The database repository
    /// </summary>
    /// <remarks>
    /// The repository facilitates access to the database
    /// </remarks>
    public interface IInventoryRepository
    {
        /// <summary>
        /// Saves inventory item in the database
        /// </summary>
        /// <param name="inventoryItem">The <see cref="InventoryItem"/> instance</param>
        Task SaveInventoryItem(InventoryItem inventoryItem);

        /// <summary>
        /// Gets the inventory item from the database by label
        /// </summary>
        /// <param name="label">The item label</param>
        /// <param name="startIndex">Item start index</param>
        /// <param name="count">Total number of items</param>
        /// <returns>The collection of <see cref="InventoryItem"/> items</returns>
        Task<IEnumerable<InventoryItem>> GetInventoryItems(string label, int startIndex = 0, int count = 10);

        /// <summary>
        /// Deletes the item from the inventory by label
        /// </summary>
        /// <param name="label">The item label</param>
        /// <returns>The instance of the deleted <see cref="InventoryItem"/></returns>
        Task<IList<InventoryItem>> DeleteInventoryItem(string label);

        /// <summary>
        /// Deletes inventory item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<InventoryItem> DeleteInventoryItem(Guid id);
    }
}
