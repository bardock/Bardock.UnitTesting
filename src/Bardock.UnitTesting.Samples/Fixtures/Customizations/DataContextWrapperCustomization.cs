using Bardock.UnitTesting.Data;
using Bardock.UnitTesting.Data.EF;
using Bardock.UnitTesting.Samples.SUT.Infra;
using Ploeh.AutoFixture;

namespace Bardock.UnitTesting.Samples.Fixtures.Customizations
{
    internal class DataContextWrapperCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            var db = fixture.Create<DataContext>();
            var wrapper = new DataContextWrapper(db);
            fixture.Register<IDataContextWrapper>(() => wrapper);

            var scopeFactory = new DataContextScopeFactory(wrapper);
            fixture.Register<IDataContextScopeFactory>(() => scopeFactory);
        }
    }
}