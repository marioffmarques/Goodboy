using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Goodboy.AMQP.Easynetq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Goodboy.Test.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
			var assemblyName = typeof(Startup).GetTypeInfo().Assembly.FullName;
			BuildServiceHost(args, assemblyName).Run();
		}

		public static IServiceHost BuildServiceHost(string[] args, string assemblyName) =>
			GoodboyAMQPServiceHost.Create(args, assemblyName)
								  .UseRabbitMq()
		                          .SubscribeToCommand<CreateValueCommand>("#")
								  .SubscribeToCommand<CreateProductCommand>("#")
								  //.SubscribeToCommand<CreateProductCommand>()
								  //.SubscribeToEvent<CreatedProductEvent>()
								  .Build();
	}
}
