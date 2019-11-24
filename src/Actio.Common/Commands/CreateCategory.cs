using System;
using System.Collections.Generic;
using System.Text;

namespace Actio.Common.Commands
{
    public class CreateCategory : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
