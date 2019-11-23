using Actio.Api.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Api.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly IMongoDatabase _database;

        public ActivityRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task AddAsync(ActivityModel model)
            => await Collection.InsertOneAsync(model);

        public async Task<IEnumerable<ActivityModel>> BrowseAsync(Guid userId)
            => await Collection
                .AsQueryable()
                .Where(a => a.UserId == userId)
                .ToListAsync();

        public async Task<ActivityModel> GetAsync(Guid id)
            => await Collection
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Id == id);

        private IMongoCollection<ActivityModel> Collection
            => _database.GetCollection<ActivityModel>("Activities");
    }
}
