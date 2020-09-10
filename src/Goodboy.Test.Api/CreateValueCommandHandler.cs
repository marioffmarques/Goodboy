using System;
using System.Threading.Tasks;
using Goodboy.Practices.Command;

namespace Goodboy.Test.Api
{
	public class CreateValueCommandHandler : ICommandHandler<CreateValueCommand>
	{
		public async Task HandleAsync(CreateValueCommand command)
		{
			await Task.CompletedTask;
			return;
		}
	}
}
