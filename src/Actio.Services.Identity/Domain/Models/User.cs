using Actio.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Services.Identity.Domain.Models
{
    public class User
    {
        public Guid Id { get; protected set; }
        public string Email { get; protected set; }
        public string Name { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected User()
        {

        }

        public User(string email, string name)
        {
            if (string.IsNullOrWhiteSpace (email))
            {
                throw new ActionException("empty_user_name",
                    $"Email can not be empty");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ActionException("empty_user_name",
                    $"Name can not be empty");
            }

            Id = Guid.NewGuid();
            Email = email;
            Name = name;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
