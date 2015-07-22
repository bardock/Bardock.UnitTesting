using Bardock.UnitTesting.Data.EF.Effort.DataLoaders.Tests.DataLoaders;
using Bardock.UnitTesting.Data.EF.Effort.DataLoaders.Tests.Entities;
using Bardock.UnitTesting.Data.EF.Effort.DataLoaders.Tests.Helpers;
using Bardock.UnitTesting.Data.EF.Effort.DataLoaders.Tests.Extensions;
using Effort.DataLoaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Bardock.UnitTesting.Data.EF.Effort.DataLoaders.Tests
{
    public class EntityObjectDataLoaderWrapperTests
    {
        #region Test Helpers

        private TableDescriptionBuilder<TestModel> GetTestModelTableDescriptionBuilder()
        {
            return new TableDescriptionBuilder<TestModel>()
                    .Add(x => x.Id)
                    .Add(x => x.Name)
                    .Add(x => x.Description);
        }

        private TableDescription BuildTestModelTableDescription()
        {
            return GetTestModelTableDescriptionBuilder().Build();
        }

        private TableDescription BuildTestModelTableDescriptionWithNonExistentColumn()
        {
            return GetTestModelTableDescriptionBuilder()
                    .Add("nonExistentColumn", typeof(int))
                    .Build();
        }

        private void AssertEqual(TestModel[] expected, object[][] actual)
        {
            for (int i = 0; i < expected.Count(); i++)
            {
                Assert.Equal(expected[i].Id, actual[i][0]);
                Assert.Equal(expected[i].Name, actual[i][1]);
                Assert.Equal(expected[i].Description, actual[i][2]);
            }
        }

        #endregion

        [Fact]
        public void Ctor_NullEntityDataLoader_ShouldThrowArgumentNullException()
        {
            // Exercise 
            Action act = () => new EntityObjectDataLoaderWrapper(null, null);

            // Verify outcome
            var ex = Assert.Throws<ArgumentNullException>(act);
            Assert.Equal("entityDataLoader", ex.ParamName);
        }

        [Fact]
        public void Ctor_NullColumnsDescription_ShouldThrowArgumentNullException()
        {
            // Setup
            IEntityDataLoader<object> entityDataLoader = new TestModelDataLoader();

            // Exercise 
            Action act = () => new EntityObjectDataLoaderWrapper(entityDataLoader, null);

            // Verify outcome
            var ex = Assert.Throws<ArgumentNullException>(act);
            Assert.Equal("columnsDescription", ex.ParamName);
        }

        [Fact]
        public void Ctor_EmptyColumnsDescription_ShouldThrowArgumentException()
        {
            // Setup
            IEntityDataLoader<object> entityDataLoader = new TestModelDataLoader();
            IReadOnlyCollection<ColumnDescription> columnsDescription = new List<ColumnDescription>();

            // Exercise 
            Action act = () => new EntityObjectDataLoaderWrapper(entityDataLoader, columnsDescription);

            // Verify outcome
            var ex = Assert.Throws<ArgumentException>(act);
            Assert.Equal("columnsDescription", ex.ParamName);
        }

        [Fact]
        public void GetData_ShouldExpectedBeEqualToActual()
        {
            // Setup
            IEntityDataLoader<TestModel> entityDataLoader = new TestModelDataLoader();
            IReadOnlyCollection<ColumnDescription> columnsDescription = BuildTestModelTableDescription().Columns;

            var sut = new EntityObjectDataLoaderWrapper(entityDataLoader, columnsDescription);

            var expected = entityDataLoader.GetData().ToArray();

            // Exercise
            var actual = sut.GetData().ToArray();

            // Verify outcome
            AssertEqual(expected, actual);
        }

        [Fact]
        public void GetData_TargetTypeNotContainsColumnDescription_ShouldThrowMissingPropertyException()
        {
            // Setup
            IEntityDataLoader<TestModel> entityDataLoader = new TestModelDataLoader();
            var expected = BuildTestModelTableDescriptionWithNonExistentColumn().Columns;
            IReadOnlyCollection<ColumnDescription> columnsDescription = expected;

            var sut = new EntityObjectDataLoaderWrapper(entityDataLoader, columnsDescription);

            // Exercise
            Action act = () => sut.GetData().ToArray();

            // Verify outcome
            var ex = Assert.Throws<Bardock.UnitTesting.Data.EF.Effort.DataLoaders.EntityObjectDataLoaderWrapper.MissingPropertyException>(act);
            var actual = ex.ColumnsDescription;
            Assert.Equal(expected, actual);
        }
    }
}
