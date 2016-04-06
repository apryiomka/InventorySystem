using System;
using inventorySyctem.Services.Entities;

namespace inventorySyctem.Services.Validation
{
    /// <summary>
    /// Validates the date to be in future
    /// </summary>
    public class DateInFutureRule : ValidationRule<InventoryItem>
    {
        /// <summary>
        /// Validates the date to be in future or null
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool IsValid(InventoryItem entity)
        {
            return !entity.Expiration.HasValue || entity.Expiration.Value >= DateTime.UtcNow; //the date is either null or in future
        }

        /// <summary>
        /// Gets the label of the broken rule
        /// </summary>
        public override EntityValidationException ValidationException => new EntityValidationException("Expiration", "Expiration date must be null or in future");

    }
}
