using System;
using Goodboy.Practices.Command;

namespace Goodboy.Test.Api
{
	public class CreateProductCommand : ICommand
	{
		public string Product { get; set; }

		protected CreateProductCommand() { }

		public CreateProductCommand(string prod)
		{
			Product = prod;
		}
	}
}
