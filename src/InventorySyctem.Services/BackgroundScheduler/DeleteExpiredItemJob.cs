using System;
using System.Threading.Tasks;
using inventorySyctem.Services.EmailService;
using inventorySyctem.Services.Reporitory;
using Quartz;

namespace inventorySyctem.Services.BackgroundScheduler
{
    /// <summary>
    /// Deletes the expired item from the inventory
    /// </summary>
    public class DeleteExpiredItemJob : IJob
    {
        void IJob.Execute(IJobExecutionContext context)
        {
            var repository = (IInventoryRepository) context.JobDetail.JobDataMap["InventoryRepository"];
            var id = (Guid)context.JobDetail.JobDataMap["Id"];

            //delete expired items
            var deletedItem = repository.DeleteInventoryItem(id).Result;
            //no items deleted, just return
            if (deletedItem == null) return;

            var emailService = (IEmailService)context.JobDetail.JobDataMap["EmailService"];
            Task.WaitAll(emailService.SendNotificationEmail(deletedItem.Label)); //wait for the email
        }
    }
}
