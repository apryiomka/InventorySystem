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
                new LabelPresentValidationRule(), //validate the label
                new DateInFutureRule() // validate the date
            };

        /// <summary>
        /// validates the inventory item and returns the first broken rule
        /// </summary>
        /// <param name="inventoryItem">The instance of <see cref="InventoryItem"/></param>
        /// <exception cref="EntityValidationException">Throws entity validation exception if the rule is broken</exception>
        public static void Validate(this InventoryItem inventoryItem)
        {
            foreach (var rule in _validationRules.Where(rule => !rule.IsValid(inventoryItem)))
            {
                throw rule.ValidationException;
            }
        }
    }
}
