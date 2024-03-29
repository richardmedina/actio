﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Services.Activities.Domain.Models
{
    public class Category
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }

        protected Category()
        {
        }

        public Category(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
