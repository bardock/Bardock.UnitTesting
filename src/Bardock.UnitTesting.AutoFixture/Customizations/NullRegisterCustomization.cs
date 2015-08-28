using Ploeh.AutoFixture;

namespace Bardock.UnitTesting.AutoFixture.Customizations
{
    public class NullRegisterCustomization<T> : ICustomization
        where T : class
    {
        public void Customize(IFixture fixture)
        {
            fixture.Register<T>(() => null);
        }
    }
}