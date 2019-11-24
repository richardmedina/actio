using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Services.Activities.Services
{
    public interface ICategoryService
    {
        Task AddAsync(Guid id, string name);
    }
}
