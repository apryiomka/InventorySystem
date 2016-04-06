using System;

namespace inventorySyctem.Services.Bus.Messages
{
    /// <summary>
    /// The deletion message that will be scheduled
    /// </summary>
    public class ScheduleDeleteMessage : IMessage
    {
        /// <summary>
        /// The item id
        /// </summary>
        public Guid InventoryItemId { get; set; }

        /// <summary>
        /// Date when the item should be deleted
        /// </summary>
        public DateTime DeletionDate { get; set; }
    }
}
