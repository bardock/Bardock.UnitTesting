using Ploeh.AutoFixture;

namespace Bardock.UnitTesting.AutoFixture.Customizations
{
    /// <summary>
    /// Customizes the <typeparamref name="T"/> of specified fixture to return a null instance.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NullRegisterCustomization<T> : ICustomization
        where T : class
    {
        public void Customize(IFixture fixture)
        {
            fixture.Register<T>(() => null);
        }
    }
}