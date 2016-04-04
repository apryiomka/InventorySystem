using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using AutoMapper;
using inventorySyctem.Services;
using inventorySyctem.Services.Entities;
using inventorySystem.Web.Models;

namespace inventorySystem.Web.Controllers
{
    /// <summary>
    /// Inventory controller.
    /// </summary>
    /// <remarks>
    /// The main controller to manage inventory items
    /// </remarks>
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
        public async Task<IHttpActionResult> PostAddItem(InventoryItemModel itemModel)
        {
            var inventoryItem = _mapper.Map<InventoryItemModel, InventoryItem>(itemModel);
            var addresult = await _inventoryManager.AddNewItem(inventoryItem);

            if (addresult.Result == ActionResult.ResultType.Success)
            {
                return Ok(addresult.Id);
            }

            if (addresult.Result == ActionResult.ResultType.NotSet)
            {
                return InternalServerError(); //something went wrong
            }

            //show the error list, etc
            return BadRequest(string.Join(" ", addresult.Errors));
        }

        /// <summary>
        /// Deletes items by label from the inventory
        /// </summary>
        /// <returns></returns>
        public async Task<IHttpActionResult> PostDeleteItem(string labelName)
        {
            var addresult = await _inventoryManager.DeleteItems(labelName);

            if (addresult.Result == ActionResult.ResultType.Success)
            {
                return Ok(Mapper.Map<IEnumerable<InventoryItem>, IEnumerable<InventoryItemModel>>(addresult.DeletedItems));
            }

            if (addresult.Result == ActionResult.ResultType.NotSet)
            {
                return InternalServerError(); //something went wrong
            }

            //show the error list, etc
            return BadRequest("Invalid inventory item");
        }
    }
}
