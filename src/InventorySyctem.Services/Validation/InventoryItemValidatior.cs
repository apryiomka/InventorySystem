using System.Collections.Generic;
using System.Linq;
using inventorySyctem.Services.Entities;

namespace inventorySyctem.Services.Validation
{
    /// <summary>
    /// validates the inventory item against predefined validation rules
    /// </summary>
    public static class InventoryItemValidatior
    {
        private static readonly List<ValidationRule<InventoryItem>> _validationRules 
            = new List<ValidationRule<InventoryItem>>
            {
                new LabelPresentValidationRule()
            };

        /// <summary>
        /// validates the inventory item and returns the first broken rule
        /// </summary>
        /// <param name="inventoryItem">The instance of <see cref="InventoryItem"/></param>
        /// <returns>
        /// We do not check for all broken rules, just return the first one
        /// </returns>
        public static string Validate(this InventoryItem inventoryItem)
        {
            return _validationRules.FirstOrDefault(rule => !rule.IsValid(inventoryItem))?.BrokenRuleLabel;
        }
    }
}
