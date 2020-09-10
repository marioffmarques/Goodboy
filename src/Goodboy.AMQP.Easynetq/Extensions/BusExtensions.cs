using System;
using System.Threading.Tasks;
using EasyNetQ;
using Goodboy.Practices.Command;
using Goodboy.Practices.Event;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Goodboy.AMQP.Easynetq
{
    public static class BusExtensions
    {
        public static Task WithCommandHandlerAsync<TCommand>(this IBus bus, ICommandHandler<TCommand> handler, string subscriberId, string topic = null) where TCommand : class, ICommand
        {
            return Task.FromResult(bus.SubscribeAsync<TCommand>(subscriberId, (arg) => handler.HandleAsync(arg), conf =>
            {
                conf.AsExclusive(false);
                conf.WithTopic(topic ?? $"{typeof(TCommand).Namespace}.{typeof(TCommand).Name}");
            }));
        }


        public static Task WithEventHandlerAsync<TEvent>(this IBus bus, IEventHandler<TEvent> handler, string subscriberId, string topic = null) where TEvent : class, IEvent
        {
            return Task.FromResult(bus.SubscribeAsync<TEvent>(subscriberId, (arg) => handler.HandleAsync(arg), conf =>
            {
                conf.AsExclusive(false);
                conf.WithTopic(topic ?? $"{typeof(TEvent).Namespace}.{typeof(TEvent).Name}");
            }));
        }


        public static void AddRabbitMq(this IServiceCollection service, IConfiguration configuration)
        {
            var rabbitConnection = configuration.GetValue<string>("rabbitmq:connection");

            var _bus = RabbitHutch.CreateBus(rabbitConnection,
                                         x => x.Register<IConventions, AttributeBasedConventions>());

            service.AddSingleton<IBus>(_ => _bus);
        }
    }
}
