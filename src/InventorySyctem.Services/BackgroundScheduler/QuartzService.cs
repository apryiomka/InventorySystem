using System;
using System.Collections.Generic;
using inventorySyctem.Services.EmailService;
using inventorySyctem.Services.Reporitory;
using Quartz;
using Quartz.Impl;

namespace inventorySyctem.Services.BackgroundScheduler
{
    /// <summary>
    /// Implementation of <see cref="IQuartzService"/>
    /// </summary>
    public class QuartzService : IQuartzService
    {
        private readonly IEmailService _emailService;
        private readonly IInventoryRepository _repository;
        private readonly IScheduler _scheduler;

        /// <summary>
        /// Creates instance of <see cref="QuartzService"/>
        /// </summary>
        /// <param name="emailService">Email service</param>
        /// <param name="repository">Database repository</param>
        public QuartzService(
            IEmailService emailService,
            IInventoryRepository repository)
        {
            _emailService = emailService;
            _repository = repository;

            _scheduler = StdSchedulerFactory.GetDefaultScheduler();

            // and start it off
            _scheduler.Start();
        }

        void IQuartzService.ScheduleItemDeletion(Guid iemtId, DateTime deletionTime)
        {
            //build job details with injected dependencies
            var job = JobBuilder.Create<DeleteExpiredItemJob>()
                    .WithIdentity(iemtId.ToString())
                    .Build();
            job.JobDataMap["InventoryRepository"] = _repository;
            job.JobDataMap["EmailService"] = _emailService;
            job.JobDataMap["Id"] = iemtId;

            //build the trigger to run when item expires
            var trigger = (ISimpleTrigger)TriggerBuilder.Create()
                .WithIdentity(iemtId.ToString())
                .StartAt(deletionTime) 
                .Build();

            //schedule the job
            _scheduler.ScheduleJob(job, trigger);
        }

        void IQuartzService.UnscheduleItemDeletion(IEnumerable<Guid> itemIds)
        {
            foreach (var id in itemIds)
            {
                //delete all scheduled jobs if any
                _scheduler.DeleteJob(new JobKey(id.ToString()));
            }
        }
    }
}
