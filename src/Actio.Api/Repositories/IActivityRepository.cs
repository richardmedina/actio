using Actio.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Api.Repositories
{
    public interface IActivityRepository
    {
        Task<ActivityModel> GetAsync(Guid id);
        Task AddAsync(ActivityModel model);
        Task<IEnumerable<ActivityModel>> BrowseAsync(Guid userId);
    }
}
