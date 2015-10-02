using Bardock.UnitTesting.AutoFixture.EF.SpecimenBuilders;
using Bardock.Utils.Types;
using Ploeh.AutoFixture;
using System;
using System.Data.Entity;

namespace Bardock.UnitTesting.AutoFixture.EF.Customizations
{
    /// <summary>
    /// A customization that provides support for generating valid specimens for an entity framework <see cref="DbContext"/>
    /// </summary>
    /// <typeparam name="TDbContext">The type of the database context.</typeparam>
    public class EntityConfigurationCustomization<TDbContext> : ICustomization
        where TDbContext : DbContext
    {
        private Func<TDbContext> _factoryFunc;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityConfigurationCustomization{TDbContext}"/> class.
        /// </summary>
        /// <remarks>Obsolete: Please move over to using <see cref="EntityConfigurationCustomization(Func<TDbContext> factoryFunc)"/> as this method will be removed in the next release</remarks>
        [Obsolete("Please move over to using EntityConfigurationCustomization(Func<TDbContext> factoryFunc) as this method will be removed in the next release")]
        public EntityConfigurationCustomization()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityConfigurationCustomization{TDbContext}"/> class.
        /// </summary>
        /// <param name="factoryFunc">The factory function to instance a <typeparamref name="TDbContext"/> for reading EF metadata.</param>
        /// <exception cref="System.ArgumentNullException">factoryFunc</exception>
        public EntityConfigurationCustomization(Func<TDbContext> factoryFunc)
        {
            if (factoryFunc == null)
                throw new ArgumentNullException("factoryFunc");

            _factoryFunc = factoryFunc;
        }

        /// <summary>
        /// Customizes the specified fixture by adding support for generating valid specimens for an entity framework <see cref="DbContext"/>
        /// </summary>
        /// <param name="fixture">The fixture to customize.</param>
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(
                new EntityConfigurationSpecimenBuilder<TDbContext>(
                    _factoryFunc ?? 
                    Lambda.Func(() => fixture.Create<TDbContext>())));
        }
    }
}