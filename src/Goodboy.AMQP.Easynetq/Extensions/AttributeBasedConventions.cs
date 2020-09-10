using System;
using System.Reflection;
using EasyNetQ;

namespace Goodboy.AMQP.Easynetq
{
    /// <summary>
    /// Attribute based conventions - Defines convention for AMQP brokert definitions and communication
    /// ExchangeNameConvention - {EntryAssemblyName}-{ExecutionEnvironment}
    /// QueueNameConvention    - {MessageClassName}-{ExecutionEnvironment}_{subscriberId}
    /// TopicNameConvention    - {MessageNamespace}.{MessageClassName}
    /// 
    /// Notes: subscriberId is options and can be provided in configuration environment file
    /// </summary>
    public class AttributeBasedConventions : Conventions
    {
        private readonly ITypeNameSerializer _typeNameSerializer;

        public AttributeBasedConventions(ITypeNameSerializer typeNameSerializer)
            : base(typeNameSerializer)
        {
            if (typeNameSerializer == null)
                throw new ArgumentNullException(nameof(typeNameSerializer));

            _typeNameSerializer = typeNameSerializer;

            ExchangeNamingConvention = GenerateExchangeName;
            QueueNamingConvention = GenerateQueueName;
            TopicNamingConvention = GenerateTopicName;
        }

        private string GenerateExchangeName(Type messageType)
        {
            return $"{Assembly.GetEntryAssembly().GetName().Name}-{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}";
        }

        private string GenerateQueueName(Type messageType, string subscriptionId)
        {
            string queuename = $"{messageType.Name}-{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}";

            return string.IsNullOrEmpty(subscriptionId) ? queuename : string.Concat(queuename, "_", subscriptionId);
        }

        private string GenerateTopicName(Type messageType)
        {
            return $"{messageType.Namespace}.{messageType.Name}";
        }
    }
}