using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using inventorySyctem.Services;
using inventorySyctem.Services.Entities;
using inventorySystem.Web.Filters;
using inventorySystem.Web.Models;

namespace inventorySystem.Web.Controllers
{
    /// <summary>
    /// Inventory controller.
    /// </summary>
    /// <remarks>
    /// The main controller to manage inventory items
    /// </remarks>
    [ArgumentNullExceptionFilter]
    public class InventoryController : ApiController
    {
        private readonly IInventoryManager _inventoryManager;
        private readonly IMapper _mapper;

        /// <summary>
        /// Create instance of <see cref="InventoryController"/>
        /// </summary>
        /// <param name="inventoryManager">the inventory manager</param>
        /// <param name="mapper">the model mapper</param>
        public InventoryController(
            IInventoryManager inventoryManager,
            IMapper mapper)
        {
            _inventoryManager = inventoryManager;
            _mapper = mapper;
        }

        /// <summary>
        /// The basic test action to test in the web api is running
        /// </summary>
        /// <returns>
        /// http://host/api/inventory/test
        /// </returns>
        public IHttpActionResult GetTest()
        {
            return Ok("Running");
        }

        /// <summary>
        /// Adds item to the inventory
        /// </summary>
        /// <returns></returns>
        /// <remarks>Post JSON object with POST verb to  http://host/api/inventory </remarks>
        [EntityValidationExceptionFilter]
        public async Task<IHttpActionResult> Post(InventoryItemModel itemModel)
        {
            var inventoryItem = _mapper.Map<InventoryItemModel, InventoryItem>(itemModel);
            var itemId = await _inventoryManager.AddNewItem(inventoryItem);

            return Ok(itemId);
        }

        /// <summary>
        /// Deletes items by label from the inventory
        /// </summary>
        /// <returns></returns>
        /// <remarks>Post with DELETE verb to  http://host/api/inventory/my awesome label</remarks>
        public async Task<IHttpActionResult> Delete(string labelName)
        {
            var deletedItems = await _inventoryManager.TakeItems(labelName);
            return Ok(Mapper.Map<IEnumerable<InventoryItem>, IEnumerable<InventoryItemModel>>(deletedItems));
        }
    }
}
