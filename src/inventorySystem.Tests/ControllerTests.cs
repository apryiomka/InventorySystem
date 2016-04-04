using System.Threading.Tasks;
using System.Web.Http.Results;
using AutoMapper;
using inventorySyctem.Services;
using inventorySyctem.Services.Entities;
using inventorySystem.Web.Controllers;
using inventorySystem.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace inventorySystem.Tests
{
    [TestClass]
    public class ControllerTests
    {
        private readonly Mock<IInventoryManager> _invenMock = new Mock<IInventoryManager>(MockBehavior.Strict);
        private readonly IMapper _mapper = Web.App_Start.Mapper.ConfigureMapper();

        [TestMethod]
        public void lBad_Request_When_manager_Returns_Error()
        {
            var addErrorResult = new AddInventoryItemResult
            {
                Result = ActionResult.ResultType.Error,
                Errors = new []{ "My test error" }
            };

            _invenMock.Setup(manager => manager.AddNewItem(It.IsAny<InventoryItem>())).Returns(Task.FromResult(addErrorResult));

            var inventoryController = new InventoryController(_invenMock.Object, _mapper);
            var result = inventoryController.PostAddItem(new InventoryItemModel()).Result as BadRequestErrorMessageResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Message.Equals("My test error"));
        }
    }
}
