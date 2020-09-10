using System;
using EasyNetQ;
using Goodboy.Practices.Command;
using Goodboy.Practices.Event;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Goodboy.AMQP.Easynetq
{
    /// <summary>
    /// Goodboy AMQP Service host - Creates Host application running a webserver and amqp subscriber
    /// 
    /// Notes:
    /// AMQP host servive will be configured based on subscriptions made using subscribe methods (SubscribeToCommand<TCommand>, SubscribeToEvent<TEvent>.
    /// A Queue will be created for each subscriberId (defined in configuration file through the structure: rabbitmq:subscriberId),
    /// it means that if two different Applications want to consume the same events/commands it must exists two different Queues bounded to each Application subscriber (with different subscriber ids).
    /// If there's multiple subscribers bounded to a queue with the same subscriberId, messages will be round robbined between the subscribers. This arquitectural details leads
    /// to the need of having different Queues for each EntityType and SubscriberId
    /// </summary>
    public class GoodboyAMQPServiceHost : IServiceHost
    {
        private readonly IWebHost _webHost;

        public GoodboyAMQPServiceHost(IWebHost webHost)
        {
            _webHost = webHost;
        }

        public void Run() => _webHost.Run();


        public static HostBuilder Create(string[] args, string assemblyName)
        {
            Console.Title = assemblyName;
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            var webHostBuilder = WebHost.CreateDefaultBuilder(args)
                                        .UseConfiguration(config)
                                        .UseStartup(assemblyName)
                                        .UseDefaultServiceProvider(options => options.ValidateScopes = false);

            return new HostBuilder(webHostBuilder.Build());
        }


        public class HostBuilder : BuilderBase
        {
            private readonly IWebHost _webHost;
            private IBus _bus;
            private IConfiguration _config;

            public HostBuilder(IWebHost webHost)
            {
                _webHost = webHost;
            }

            public BusBuilder UseRabbitMq()
            {
                _bus = (IBus)_webHost.Services.GetService(typeof(IBus));
                _config = (IConfiguration)_webHost.Services.GetService(typeof(IConfiguration));

                return new BusBuilder(_webHost, _bus, _config);
            }


            public override GoodboyAMQPServiceHost Build()
            {
                return new GoodboyAMQPServiceHost(_webHost);
            }
        }

        public class BusBuilder : BuilderBase
        {
            private readonly IWebHost _webHost;
            private IBus _bus;
            private IConfiguration _config;

            public BusBuilder(IWebHost webHost, IBus bus, IConfiguration config)
            {
                _webHost = webHost;
                _bus = bus;
                _config = config;
            }

			/// <summary>
			/// Subscribes to an entity type command.
			/// </summary>
			/// <returns>The to command.</returns>
			/// <param name="topic">Topic.</param>
			/// <typeparam name="TCommand">Command type to subscribe with.</typeparam>
            public BusBuilder SubscribeToCommand<TCommand>(string topic = null) where TCommand : class, ICommand
            {
                var handler = (ICommandHandler<TCommand>)_webHost.Services.GetService(typeof(ICommandHandler<TCommand>));
                _bus.WithCommandHandlerAsync(handler, _config.GetValue<string>("rabbitmq:subscriberId") ?? "", topic);

                return this;
            }

            public BusBuilder SubscribeToEvent<TEvent>(string topic = null) where TEvent : class, IEvent
            {
                var handler = (IEventHandler<TEvent>)_webHost.Services.GetService(typeof(IEventHandler<TEvent>));
                _bus.WithEventHandlerAsync(handler, _config.GetValue<string>("rabbitmq:subscriberId") ?? "", topic);

                return this;
            }

			public BusBuilder SubscribeTo(string topic, IEventHandler<IEvent> handler)
			{
				// TODO: Test this
				_bus.WithEventHandlerAsync(handler, _config.GetValue<string>("rabbitmq:subscriberId") ?? "", topic);
				return this;
			}


            public override GoodboyAMQPServiceHost Build()
            {
                return new GoodboyAMQPServiceHost(_webHost);
            }
        }


        public abstract class BuilderBase
        {
            public abstract GoodboyAMQPServiceHost Build();
        }
    }
}
