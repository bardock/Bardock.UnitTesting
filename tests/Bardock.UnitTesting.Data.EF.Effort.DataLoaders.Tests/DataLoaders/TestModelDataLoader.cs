using Bardock.UnitTesting.Data.EF.Effort.DataLoaders.Tests.Entities;
using System.Collections.Generic;

namespace Bardock.UnitTesting.Data.EF.Effort.DataLoaders.Tests.DataLoaders
{
    public class TestModelDataLoader : IEntityDataLoader<TestModel>
    {
        public IEnumerable<TestModel> GetData()
        {
            yield return new TestModel()
            {
                Id = 1,
                Name = "Test",
                Description = "TST"
            };
        }
    }
}