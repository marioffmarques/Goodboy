using System;
namespace Goodboy.Practices.Command
{
    public interface IAuthenticatedCommand : ICommand
    {
        /// <summary>
        /// Authenticated User id
        /// </summary>
        /// <value>The user identifier.</value>
        Guid UserId { get; set; }
    }
}
