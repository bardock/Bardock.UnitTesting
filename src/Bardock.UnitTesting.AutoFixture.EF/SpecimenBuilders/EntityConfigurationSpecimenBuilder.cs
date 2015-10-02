using Bardock.UnitTesting.AutoFixture.EF.Helpers;
using Bardock.UnitTesting.AutoFixture.SpecimenBuilders;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using System;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;

namespace Bardock.UnitTesting.AutoFixture.EF.SpecimenBuilders
{
    /// <summary>
    /// A specimen builder that creates valid instances for entities of entity framework
    /// by adding support for entity framework's fluent configuration
    /// </summary>
    /// <typeparam name="TDbContext">The type of the database context.</typeparam>
    public class EntityConfigurationSpecimenBuilder<TDbContext> : StringDataAnnotationsSpecimenBuilder
        where TDbContext : DbContext
    {
        private Lazy<TDbContext> _dbCtx;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityConfigurationSpecimenBuilder{TDbContext}"/> class.
        /// </summary>
        /// <remarks>Obsolete: Please move over to using <see cref="EntityConfigurationSpecimenBuilder(Func<TDbContext> factoryFunc)"/> as this method will be removed in the next release</remarks>
        [Obsolete("Please move over to using EntityConfigurationSpecimenBuilder(Func<TDbContext> factoryFunc) as this method will be removed in the next release")]
        public EntityConfigurationSpecimenBuilder()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityConfigurationSpecimenBuilder{TDbContext}"/> class.
        /// </summary>
        /// <param name="factoryFunc">The factory function to instance a <typeparamref name="TDbContext"/> for reading EF metadata.</param>
        /// <exception cref="System.ArgumentNullException">factoryFunc</exception>
        public EntityConfigurationSpecimenBuilder(Func<TDbContext> factoryFunc)
        {
            if (factoryFunc == null)
                throw new ArgumentNullException("factoryFunc");

            _dbCtx = new Lazy<TDbContext>(factoryFunc);
        }

        protected override bool IsValidType(PropertyInfo pi)
        {
            return pi.DeclaringType.IsMappedEntity<TDbContext>();
        }

        protected override int? GetStringMaxLength(PropertyInfo pi, ISpecimenContext context)
        {
            var length = base.GetStringMaxLength(pi, context);

            //DataAnnotations wins vs EF fluent config
            if (length.HasValue)
            {
                return length;
            }

            var edmProperty = GetProperty(pi, context);
            if (edmProperty != null)
            {
                length = edmProperty.MaxLength.Value;
            }

            return length;
        }

        private EdmProperty GetProperty(PropertyInfo pi, ISpecimenContext context)
        {
            if (_dbCtx == null)
                _dbCtx = new Lazy<TDbContext>(() => context.Create<TDbContext>());

            return ((IObjectContextAdapter)_dbCtx.Value)
                        .ObjectContext
                        .MetadataWorkspace
                        .GetItemCollection(DataSpace.CSpace)
                        .Where(gi => gi.BuiltInTypeKind == BuiltInTypeKind.EntityType)
                        .Cast<EntityType>()
                        .SelectMany(et =>
                            et.Properties
                                .Where(p => p.DeclaringType.Name == pi.DeclaringType.Name)
                                .Where(p => p.PrimitiveType.ClrEquivalentType == typeof(string))
                                .Where(p => p.Name == pi.Name)
                                .Where(p => p.MaxLength.HasValue))
                        .FirstOrDefault();
        }
    }
}