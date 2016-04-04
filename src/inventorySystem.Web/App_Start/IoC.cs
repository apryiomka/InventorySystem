using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using inventorySyctem.Services;
using inventorySyctem.Services.BackgroundScheduler;
using inventorySyctem.Services.EmailService;
using inventorySyctem.Services.Entities;
using inventorySyctem.Services.Reporitory;
using inventorySystem.Web.Models;

namespace inventorySystem.Web.App_Start
{
    /// <summary>
    /// Registers the dependencies
    /// </summary>
    public static class IoC
    {
        /// <summary>
        /// Initializes the IoC
        /// </summary>
        public static void Initialize()
        {
            var builder = new ContainerBuilder();
            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //register the InventoryManager instance
            builder.RegisterType<InventoryManager>().As<IInventoryManager>();
            builder.RegisterType<InMemoryInventoryRepository>().As<IInventoryRepository>();
            builder.RegisterType<QuartzService>().As<IQuartzService>();
            builder.RegisterType<EmailService>().As<IEmailService>();

            //add mapper
            builder.RegisterInstance(Mapper.ConfigureMapper()).As<IMapper>();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

        }

        
    }
}