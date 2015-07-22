using Bardock.UnitTesting.Data.EF.Effort.DataLoaders.Tests.DataLoaders;
using Bardock.UnitTesting.Data.EF.Effort.DataLoaders.Tests.Entities;
using Bardock.UnitTesting.Data.EF.Effort.DataLoaders.Tests.Extensions;
using Bardock.UnitTesting.Data.EF.Effort.DataLoaders.Tests.Helpers;
using Effort.DataLoaders;
using System;
using System.Linq;
using Xunit;

namespace Bardock.UnitTesting.Data.EF.Effort.DataLoaders.Tests
{
    public class EntityObjectDataLoaderTests
    {
        #region Test Helpers

        private EntityObjectDataLoader CreateSut()
        {
            return new EntityObjectDataLoader(bindings => bindings.Add<TestModelDataLoader>());
        }

        #endregion

        [Fact]
        public void Ctor_Parameterless_ShouldBeAssignableToIDataLoader()
        {
            // Exercise
            var sut = new EntityObjectDataLoader();

            // Verify outcome
            Assert.IsAssignableFrom<IDataLoader>(sut);
        }

        [Fact]
        public void Ctor_ShouldBeAssignableToIDataLoader()
        {
            // Exercise
            var sut = CreateSut();

            // Verify outcome
            Assert.IsAssignableFrom<IDataLoader>(sut);
        }

        [Fact]
        public void Ctor_NullConfig_ShouldThrowArgumentNullException()
        {
            // Exercise
            Action act = () => new EntityObjectDataLoader(null);

            // Verify outcome
            var ex = Assert.Throws<ArgumentNullException>(act);
            Assert.Equal("config", ex.ParamName);
        }

        [Fact]
        public void Ctor_NullConfig_ShouldThrowNotValidBindingsException()
        {
            // Exercise
            Action act = () => new EntityObjectDataLoader(config => { });

            // Verify outcome
            Assert.Throws<Bardock.UnitTesting.Data.EF.Effort.DataLoaders.EntityObjectDataLoader.NotValidBindingsException>(act);
        }

        [Fact]
        public void CreateTableDataLoaderFactory_ShouldBeAssignableToEntityObjectDataLoaderFactory()
        {
            // Setup
            var sut = CreateSut();

            // Exercise
            var actual = sut.CreateTableDataLoaderFactory();

            // Verify outcome
            Assert.IsAssignableFrom<EntityObjectDataLoaderFactory>(actual);
        }

        [Fact]
        public void GetArgument_ShouldBeEqualToExpectedBindings()
        {
            // Setup
            EntityObjectDataLoader.BindingsBuilder expectedBindings = null;

            var sut = new EntityObjectDataLoader(bindings => {
                bindings.Add<TestModelDataLoader>();
                expectedBindings = bindings;
            });

            var expected = Newtonsoft.Json.JsonConvert.SerializeObject(expectedBindings.Build());

            // Exercise
            var actual = sut.Argument;

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SetArgument_ShouldBeEqualToExpectedBindings()
        {
            // Setup
            var expected = new EntityObjectDataLoader.BindingsBuilder().Add<TestModelDataLoader>().Build();

            var sut = new EntityObjectDataLoader();

            // Exercise
            sut.Argument = Newtonsoft.Json.JsonConvert.SerializeObject(expected);

            //Verify outcome
            var actual = sut.Bindings;

            Assert.Collection(expected.Keys, (s) => actual.Keys.Contains(s));
            Assert.Collection(expected.Values, (s) => actual.Values.Contains(s));
        }
    }
}