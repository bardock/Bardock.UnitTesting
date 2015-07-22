using Bardock.UnitTesting.Data.EF.Effort.DataLoaders.Tests.DataLoaders;
using Bardock.UnitTesting.Data.EF.Effort.DataLoaders.Tests.Entities;
using System.Linq;
using Xunit;

namespace Bardock.UnitTesting.Data.EF.Effort.DataLoaders.Tests
{
    public class EntityObjectDataLoaderBindingsBuilderTests
    {
        [Fact]
        public void AddOfTEntityDataLoader_ShouldContainKeyAndValue()
        {
            // Setup
            var sut = new EntityObjectDataLoader.BindingsBuilder();

            var testModelDataLoaderType = typeof(TestModelDataLoader);
            var expectedKey = testModelDataLoaderType.Name.Replace("DataLoader", string.Empty);
            var expectedValue = string.Format("{0}, {1}", testModelDataLoaderType.FullName, testModelDataLoaderType.Assembly.FullName);

            // Exercise
            sut.Add<TestModelDataLoader>();

            var actual = sut.Build();

            // Verify
            Assert.Contains(actual, (kv) => kv.Key == expectedKey && kv.Value == expectedValue);
        }

        [Fact]
        public void AddOfTEntityDataLoader_CustomTableName_ShouldContainKeyAndValue()
        {
            // Setup
            var sut = new EntityObjectDataLoader.BindingsBuilder();

            var testModelDataLoaderType = typeof(TestModelDataLoader);
            var expectedKey = "TestModel";
            var expectedValue = string.Format("{0}, {1}", testModelDataLoaderType.FullName, testModelDataLoaderType.Assembly.FullName);

            // Exercise
            sut.Add<TestModelDataLoader>(expectedKey);

            var actual = sut.Build();

            // Verify
            Assert.Contains(actual, (kv) => kv.Key == expectedKey && kv.Value == expectedValue);
        }

        [Fact]
        public void Build_EmptyBindings_ShouldReturnEmptyBindings()
        {
            // Setup
            var sut = new EntityObjectDataLoader.BindingsBuilder();

            // Exercise
            var bindings = sut.Build();

            // Verify
            Assert.True(!bindings.Any(), "Bindings count should be 0");
        }
    }
}