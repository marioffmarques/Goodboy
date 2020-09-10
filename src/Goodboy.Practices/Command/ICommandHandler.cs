using System;
using System.Threading.Tasks;

namespace Goodboy.Practices.Command
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        /// <summary>
        /// Handles the command
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="command">Command.</param>
        Task HandleAsync(T command);
    }
}
