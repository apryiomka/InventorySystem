using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inventorySyctem.Services.Entities;

namespace inventorySyctem.Services.Reporitory
{
    /// <summary>
    /// Simple in-memory implementaion of the <see cref="IInventoryRepository"/>
    /// </summary>
    /// <remarks>
    /// The data is store in memory while the application is running and will be discarded once the application stops
    /// </remarks>
    public class InMemoryInventoryRepository : IInventoryRepository
    {
        private readonly Dictionary<Guid, InventoryItem> _inventoryItems 
            = new Dictionary<Guid, InventoryItem>();

        async Task IInventoryRepository.SaveInventoryItem(InventoryItem inventoryItem)
        {
            _inventoryItems[inventoryItem.Id] = inventoryItem;
            await Task.FromResult(0); //simulate async save
        }

        async Task<IEnumerable<InventoryItem>> IInventoryRepository.GetInventoryItems(string label, int startIndex, int count)
        {
            await Task.FromResult(0); //simulate async retrieve 
            return _inventoryItems.Values
                .Where(i => i.Label.Equals(label, StringComparison.InvariantCultureIgnoreCase))
                .Skip(startIndex)
                .Take(count);
        }

        async Task<IList<InventoryItem>> IInventoryRepository.DeleteInventoryItem(string label)
        {
            await Task.FromResult(0); //simulate async delete 
            var currentItems = _inventoryItems
                .Where(i => i.Value.Label.Equals(label, StringComparison.InvariantCultureIgnoreCase))
                .Select(i => i.Value).ToArray();

            foreach (var item in currentItems)
            {
                _inventoryItems.Remove(item.Id);
            }

            return currentItems;
        }

        async Task<InventoryItem> IInventoryRepository.DeleteInventoryItem(Guid id)
        {
            await Task.FromResult(0); //simulate async delete 
            if (!_inventoryItems.ContainsKey(id)) return null;

            var item = _inventoryItems[id];
            _inventoryItems.Remove(id);
            return item;
        }

    }
}
