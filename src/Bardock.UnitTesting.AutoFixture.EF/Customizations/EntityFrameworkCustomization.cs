using Ploeh.AutoFixture;
using System;
using System.Data.Entity;

namespace Bardock.UnitTesting.AutoFixture.EF.Customizations
{
    /// <summary>
    /// A composite customization that adds <see cref="EntityFrameworkEntityConfigurationCustomization"/>,
    /// <see cref="IgnoreEntityNavigationPropsCustomization"/> and <see cref="IgnoreEntityKeysCustomization"/>
    /// customizations
    /// </summary>
    /// <typeparam name="TDbContext">The type of the database context.</typeparam>
    public class EntityFrameworkCustomization<TDbContext> : CompositeCustomization
        where TDbContext : DbContext
    {
        [Obsolete("Please move over to using EntityFrameworkCustomization(Func<TDbContext> factoryFunc) as this method will be removed in the next release")]
        public EntityFrameworkCustomization()
            : base(
                new EntityConfigurationCustomization<TDbContext>(),
                new IgnoreEntityNavigationPropsCustomization<TDbContext>(),
                new IgnoreEntityKeysCustomization<TDbContext>())
        { }

        public EntityFrameworkCustomization(Func<TDbContext> factoryFunc)
            : base (
                new EntityConfigurationCustomization<TDbContext>(factoryFunc),
                new IgnoreEntityNavigationPropsCustomization<TDbContext>(),
                new IgnoreEntityKeysCustomization<TDbContext>()
            )
        { }
    }
}