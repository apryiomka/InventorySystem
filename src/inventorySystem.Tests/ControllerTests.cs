using System;
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
        [ExpectedException(typeof(AggregateException))]
        public void Throws_Exception_When_Argument_Null_Exception_Is_Thrown()
        {
            _invenMock.Setup(manager => manager.AddNewItem(It.IsAny<InventoryItem>())).Throws(new ArgumentNullException());

            var inventoryController = new InventoryController(_invenMock.Object, _mapper);
            var result = inventoryController.Post(new InventoryItemModel()).Result;

            Assert.IsNull(result);
        }
    }
}
