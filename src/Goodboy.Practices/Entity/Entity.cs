using System;

namespace Goodboy.Practices
{
    public class Entity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this Entity is deleted.
        /// </summary>
        /// <value><c>true</c> if is deleted; otherwise, <c>false</c>.</value>
        public bool IsDeleted { get; set; }


        public Entity()
        {
            this.Id = Guid.NewGuid();
            this.IsDeleted = false;
        }
    }
}
