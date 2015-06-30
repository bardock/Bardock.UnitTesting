using Bardock.UnitTesting.Data.EF.Effort.DataLoaders.Tests.Entities;
using System.Collections.Generic;

namespace Bardock.UnitTesting.Data.EF.Effort.DataLoaders.Tests.DataLoaders
{
    public class ModelDataLoader : IEntityDataLoader<Model>
    {
        public IEnumerable<Model> GetData()
        {
            yield return new Model()
            {
                Id = 1,
                Name = "Test",
                Description = "TST"
            };
        }
    }
}