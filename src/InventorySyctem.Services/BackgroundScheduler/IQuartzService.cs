using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace inventorySyctem.Services.BackgroundScheduler
{
    /// <summary>
    /// Quartz service to schedule items notification and removal
    /// </summary>
    public interface IQuartzService
    {
        /// <summary>
        /// Schedules item deletion to run in future
        /// </summary>
        /// <param name="iemtId">Item id</param>
        /// <param name="deletionTime">Deletion time</param>
        void ScheduleItemDeletion(Guid iemtId, DateTime deletionTime);

        /// <summary>
        /// unschedules the scheduled jobs to delete items
        /// </summary>
        /// <param name="itemIds"></param>
        void UnscheduleItemDeletion(IEnumerable<Guid> itemIds);
    }
}
