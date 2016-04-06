using inventorySyctem.Services.Entities;

namespace inventorySyctem.Services.Validation
{
    /// <summary>
    /// Validates presence of the label in the <see cref="InventoryItem"/>
    /// </summary>
    public class LabelPresentValidationRule : ValidationRule<InventoryItem>
    {
        /// <summary>
        /// Validates the label
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool IsValid(InventoryItem entity)
        {
            return string.IsNullOrWhiteSpace(entity.Label);
        }
        /// <summary>
        /// Gets the label of the broken rule
        /// </summary>
        public override EntityValidationException ValidationException => new EntityValidationException("Label", "Label is required");
    }
}
