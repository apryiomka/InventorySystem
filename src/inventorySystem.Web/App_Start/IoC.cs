using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using inventorySyctem.Services;
using inventorySyctem.Services.Bus;
using inventorySyctem.Services.Bus.Subscribers;
using inventorySyctem.Services.Reporitory;

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


            builder.RegisterType<InventoryManager>().As<IInventoryManager>();
            builder.RegisterType<InMemoryInventoryRepository>().As<IInventoryRepository>();
            builder.RegisterType<Bus>().As<IBus>();
            builder.RegisterType<SubscriberRouter>().As<IRouter>();

            //subscribers
            builder.RegisterType<EmailSubscriber>().As<ISubscriber>();
            builder.RegisterType<SchedulerSubscriber>().As<ISubscriber>();
            builder.RegisterType<SmsSubscriber>().As<ISubscriber>();

            //add mapper
            builder.RegisterInstance(Mapper.ConfigureMapper()).As<IMapper>();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

        }

        
    }
}