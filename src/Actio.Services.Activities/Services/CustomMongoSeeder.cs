﻿using Actio.Common.Mongo;
using Actio.Services.Activities.Domain.Models;
using Actio.Services.Activities.Domain.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Services.Activities.Services
{
    public class CustomMongoSeeder : MongoSeeder
    {
        private readonly ICategoryRepository _categoryRepository;

        public CustomMongoSeeder(IMongoDatabase database, ICategoryRepository categoryRepository)
            : base(database)
        {
            _categoryRepository = categoryRepository;
        }

        protected override async Task CustomSeedAsync()
        {
            var categories = new List<string> { 
                "work",
                "sport",
                "hobby"
            };

            await Task.WhenAll(
                categories.Select(c =>
                    _categoryRepository.AddAsync(new Category(Guid.NewGuid(), c))
                )
            );
        }
    }
}
