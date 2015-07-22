using Bardock.UnitTesting.Data.EF.Effort.DataLoaders.Tests.DataLoaders;
using Bardock.UnitTesting.Data.EF.Effort.DataLoaders.Tests.Entities;
using Bardock.UnitTesting.Data.EF.Effort.DataLoaders.Tests.Extensions;
using Bardock.UnitTesting.Data.EF.Effort.DataLoaders.Tests.Helpers;
using Effort.DataLoaders;
using System;
using System.Collections.Generic;
using Xunit;

namespace Bardock.UnitTesting.Data.EF.Effort.DataLoaders.Tests
{
    public class EntityObjectDataLoaderFactoryTests
    {
        #region Test Helpers

        private EntityObjectDataLoaderFactory CreateSut()
        {
            var bindingsBuilder = new EntityObjectDataLoader.BindingsBuilder()
                                    .Add<TestModelDataLoader>();

            return new EntityObjectDataLoaderFactory(bindingsBuilder.Build());
        }

        private TableDescriptionBuilder<TestModel> GetTestModelTableDescriptionBuilder()
        {
            return new TableDescriptionBuilder<TestModel>()
                    .Add(x => x.Id)
                    .Add(x => x.Name)
                    .Add(x => x.Description);
        }

        private TableDescriptionBuilder<TestModel2> GetTestModel2TableDescriptionBuilder()
        {
            return new TableDescriptionBuilder<TestModel2>()
                    .Add(x => x.Id)
                    .Add(x => x.Name)
                    .Add(x => x.Description);
        }

        private TableDescription BuildTestModelTableDescription()
        {
            return GetTestModelTableDescriptionBuilder().Build();
        }

        private TableDescription BuildTestModel2TableDescription()
        {
            return GetTestModel2TableDescriptionBuilder().Build();
        }

        #endregion Test Helpers

        [Fact]
        public void Ctor_ShouldBeAssignableToITableDataLoaderFactory()
        {
            // Setup
            EntityObjectDataLoaderFactory sut = null;

            // Exercise
            sut = CreateSut();

            // Verify outcome
            Assert.NotNull(sut);
            Assert.IsAssignableFrom<ITableDataLoaderFactory>(sut);
        }

        [Fact]
        public void Ctor_NullBindings_ShouldThrowArgumentNullException()
        {
            // Setup
            EntityObjectDataLoaderFactory sut = null;

            // Exercise
            Action act = () => sut = new EntityObjectDataLoaderFactory(null);

            // Verify outcome
            var ex = Assert.Throws<ArgumentNullException>(act);
            Assert.Equal("bindings", ex.ParamName);
        }

        [Fact]
        public void Ctor_EmptyBindings_ShouldThrowArgumentNullException()
        {
            // Setup
            EntityObjectDataLoaderFactory sut = null;

            // Exercise
            Action act = () => sut = new EntityObjectDataLoaderFactory(new Dictionary<string,string>());

            // Verify outcome
            var ex = Assert.Throws<ArgumentException>(act);
            Assert.Equal("bindings", ex.ParamName);
        }

        [Fact]
        public void CreateTableDataLoader_NullTableDescription_ShouldThrowArgumentNullException()
        {
            // Setup
            var sut = CreateSut();

            // Exercise
            Action act = () => sut.CreateTableDataLoader(null);

            // Verify outcome
            var ex = Assert.Throws<ArgumentNullException>(act);
            Assert.Equal("tableDescription", ex.ParamName);
        }

        [Fact]
        public void CreateTableDataLoader_NotBindedTableDescription_ShouldReturnEmptyTableDataLoader()
        {
            // Setup
            var sut = CreateSut();

            // Exercise
            ITableDataLoader actual = sut.CreateTableDataLoader(BuildTestModel2TableDescription());

            // Verify outcome
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<EmptyTableDataLoader>(actual);
        }

        [Fact]
        public void CreateTableDataLoader_ShouldReturnEntityObjectDataLoaderWrapper()
        {
            // Setup
            var sut = CreateSut();

            // Exercise
            ITableDataLoader actual = sut.CreateTableDataLoader(BuildTestModelTableDescription());

            // Verify outcome
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<EntityObjectDataLoaderWrapper>(actual);
        }
    }
}