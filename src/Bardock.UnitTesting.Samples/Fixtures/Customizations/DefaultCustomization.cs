using Bardock.UnitTesting.AutoFixture.Customizations;
using Bardock.UnitTesting.AutoFixture.EF.Customizations;
using Bardock.UnitTesting.Samples.SUT.Infra;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace Bardock.UnitTesting.Samples.Fixtures.Customizations
{
    internal class DefaultCustomization : CompositeCustomization
    {
        public DefaultCustomization()
            : base(
                new EntityFrameworkCustomization<DataContext>(),
                new StringDataAnnotationsCustomization(),
                new DataContextCustomization(),
                new DataContextWrapperCustomization(),
                new AutoMoqCustomization())
        {
            Bootstrapper.Init();
        }
    }
}