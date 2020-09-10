using System;
using System.Threading.Tasks;
using Goodboy.Practices.Command;

namespace Goodboy.Test.Api
{
	public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand>
	{
		public CreateProductCommandHandler()
		{
		}

		public async Task HandleAsync(CreateProductCommand command)
		{
			await Task.CompletedTask;
			return;
		}
	}
}
