using System;
using inventorySyctem.Services.Bus.Messages;
using inventorySyctem.Services.Reporitory;
using Quartz;
using Quartz.Impl;

namespace inventorySyctem.Services.Bus.Subscribers
{
    /// <summary>
    /// Schedules deletion job
    /// </summary>
    public class SchedulerSubscriber : Subscriber<ScheduleDeleteMessage>
    {
        private readonly IBus _bus;
        private readonly IInventoryRepository _repository;
        private readonly IScheduler _scheduler;

        /// <summary>
        /// Creates the instance of <see cref="SchedulerSubscriber"/>
        /// </summary>
        /// <param name="bus"></param>
        /// <param name="repository"></param>
        public SchedulerSubscriber(
            IBus bus,
            IInventoryRepository repository)
        {
            _bus = bus;
            _repository = repository;
            _scheduler = StdSchedulerFactory.GetDefaultScheduler();

            // and start it off
            _scheduler.Start();
        }

        protected override void ProcessMessage(ScheduleDeleteMessage message)
        {
            //build job details with injected dependencies
            var job = JobBuilder.Create<DeleteExpiredItemJob>()
                    .WithIdentity(message.InventoryItemId.ToString())
                    .Build();
            job.JobDataMap["InventoryRepository"] = _repository;
            job.JobDataMap["Bus"] = _bus;
            job.JobDataMap["Id"] = message.InventoryItemId;

            //build the trigger to run when item expires
            var trigger = (ISimpleTrigger)TriggerBuilder.Create()
                .WithIdentity(message.InventoryItemId.ToString())
                .StartAt(message.DeletionDate)
                .Build();

            //schedule the job
            _scheduler.ScheduleJob(job, trigger);
        }

        /// <summary>
        /// Deletes the expired item from the inventory
        /// </summary>
        public class DeleteExpiredItemJob : IJob
        {
            void IJob.Execute(IJobExecutionContext context)
            {
                var repository = (IInventoryRepository)context.JobDetail.JobDataMap["InventoryRepository"];
                var id = (Guid)context.JobDetail.JobDataMap["Id"];

                //delete expired items
                var deletedItem = repository.DeleteInventoryItem(id).Result;
                //no items deleted, just return
                if (deletedItem == null) return;

                //send email and SMS message
                var bus = (IBus)context.JobDetail.JobDataMap["Bus"];
                bus.Publish(new EmailMessage { Email = "someemail@example.com" });
                bus.Publish(new SmsMessage { SmsNumber = "1234567890" });
                bus.Commit();
            }
        }
    }
}
