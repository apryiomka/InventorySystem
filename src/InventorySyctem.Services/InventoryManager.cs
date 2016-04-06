using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using inventorySyctem.Services.Bus;
using inventorySyctem.Services.Bus.Messages;
using inventorySyctem.Services.Entities;
using inventorySyctem.Services.Reporitory;
using inventorySyctem.Services.Validation;

namespace inventorySyctem.Services
{
    /// <summary>
    /// The main implementation of the <see cref="IInventoryManager"/>
    /// </summary>
    public class InventoryManager : IInventoryManager
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IBus _bus;

        /// <summary>
        /// Creates the instance of <see cref="InventoryManager"/>
        /// </summary>
        /// <param name="inventoryRepository">The instance of <see cref="IInventoryRepository"/></param>
        /// <param name="bus">The service bus</param>
        public InventoryManager(
            IInventoryRepository inventoryRepository,
            IBus bus)
        {
            _inventoryRepository = inventoryRepository;
            _bus = bus;
        }

        async Task<Guid> IInventoryManager.AddNewItem(InventoryItem inventoryItem)
        {
            if (inventoryItem == null) throw new ArgumentNullException(nameof(inventoryItem));

            //validate the item
            //tyhrows an error if not valid
            inventoryItem.Validate();

            //assign a new item id
            inventoryItem.Id = Guid.NewGuid();
            await _inventoryRepository.SaveInventoryItem(inventoryItem);

            if (inventoryItem.Expiration.HasValue)
            {
                //schedule a message
                _bus.Publish(new ScheduleDeleteMessage
                {
                    DeletionDate = inventoryItem.Expiration.Value,
                    InventoryItemId = inventoryItem.Id
                });
            }

            //return saved item
            return inventoryItem.Id;
        }

        async Task<IEnumerable<InventoryItem>> IInventoryManager.TakeItems(string itemLabel)
        {
            //return error if label is null or white spaces
            if (string.IsNullOrWhiteSpace(itemLabel)) throw new ArgumentNullException(nameof(itemLabel));

            //call repo to delete the items
            var deletedItems = await _inventoryRepository.DeleteInventoryItem(itemLabel);
            //return the deleted items
            foreach (var deletedItem in deletedItems)
            {
                //send email of the deleted item
                _bus.Publish(new EmailMessage { Email = "someemail@example.com"});
                //send SMS message
                _bus.Publish(new SmsMessage { SmsNumber = "123456789" });
                _bus.Commit();
            }
            //return error if no items were deleted
            return deletedItems;
        }
    }
}
