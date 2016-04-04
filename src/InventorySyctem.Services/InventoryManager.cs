using System;
using System.Threading.Tasks;
using inventorySyctem.Services.BackgroundScheduler;
using inventorySyctem.Services.EmailService;
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
        private readonly IQuartzService _quartzService;
        private readonly IEmailService _emailService;

        /// <summary>
        /// Creates the instance of <see cref="InventoryManager"/>
        /// </summary>
        /// <param name="inventoryRepository">The instance of <see cref="IInventoryRepository"/></param>
        /// <param name="quartzService">The instance of <see cref="IQuartzService"/></param>
        /// <param name="emailService">Email service to send item notifiacations</param>
        public InventoryManager(
            IInventoryRepository inventoryRepository,
            IQuartzService quartzService,
            IEmailService emailService)
        {
            _inventoryRepository = inventoryRepository;
            _quartzService = quartzService;
            _emailService = emailService;
        }

        async Task<AddInventoryItemResult> IInventoryManager.AddNewItem(InventoryItem inventoryItem)
        {
            if (inventoryItem == null) return new AddInventoryItemResult
            {
                Result = ActionResult.ResultType.Error,
                Errors = new []{ "Inventory item is required." }
            };

            //validate the item
            var validationError = inventoryItem.Validate();
            if (validationError != null)
            {
                return new AddInventoryItemResult
                {
                    Result = ActionResult.ResultType.Error,
                    Errors = new[] {validationError}
                };
            }

            //assign a new item id
            inventoryItem.Id = Guid.NewGuid();
            await _inventoryRepository.SaveInventoryItem(inventoryItem);

            //return saved item
            return new AddInventoryItemResult
            {
                Result = ActionResult.ResultType.Success,
                Id = inventoryItem.Id
            };
        }

        async Task<DeleteInventoryItemResult> IInventoryManager.DeleteItems(string itemLabel)
        {
            //return error if label is null or white spaces
            if (string.IsNullOrWhiteSpace(itemLabel)) return new DeleteInventoryItemResult
            {
                Result = ActionResult.ResultType.Error,
                Errors = new []{ "Item label is required" }
            };
            //call repo to delete the items
            var deletedItems = await _inventoryRepository.DeleteInventoryItem(itemLabel);
            //return the deleted items
            if (deletedItems.Count > 0)
            {
                foreach (var deletedItem in deletedItems)
                {
                    await _emailService.SendNotificationEmail(deletedItem.Label);
                }

                return new DeleteInventoryItemResult
                {
                    DeletedItems = await _inventoryRepository.DeleteInventoryItem(itemLabel),
                    Result = ActionResult.ResultType.Success
                };
            }
            //return error if no items were deleted
            return new DeleteInventoryItemResult
            {
                Result = ActionResult.ResultType.Error,
                Errors = new[] { "No items were deleted." }
            };
        }
    }
}
