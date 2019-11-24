using System;
using System.Collections.Generic;
using System.Text;

namespace Actio.Common.Events
{
    public class CategoryCreated : IEvent
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }

        public CategoryCreated(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
