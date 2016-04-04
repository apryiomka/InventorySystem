using AutoMapper;
using inventorySyctem.Services.Entities;
using inventorySystem.Web.Models;

namespace inventorySystem.Web.App_Start
{
    /// <summary>
    /// Mapper for the exntities
    /// </summary>
    public static class Mapper
    {
        /// <summary>
        /// Creates object mapping
        /// </summary>
        /// <returns></returns>
        public static IMapper ConfigureMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AddInventoryItemModel, InventoryItem>();
                cfg.CreateMap<InventoryItem, InventoryItemModel>();
            }).CreateMapper();
        }
    }
}