using System;
using Goodboy.Practices.Command;

namespace Goodboy.Test.Api
{
	public class CreateValueCommand : ICommand
	{
		public string Value { get; set; }

		protected CreateValueCommand()
		{
		}

		public CreateValueCommand(string value)
		{
			Value = value;
		}
	}
}
