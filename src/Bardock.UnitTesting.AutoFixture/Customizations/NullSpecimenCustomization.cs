using Bardock.UnitTesting.AutoFixture.SpecimenBuilders.Generators;
using Ploeh.AutoFixture;
using System;

namespace Bardock.UnitTesting.AutoFixture.Customizations
{
    /// <summary>
    /// Customizes the type of specified fixture to return a null instance for a given alias.
    /// </summary>
    public class NullSpecimenCustomization : ICustomization
    {
        private Type _type;

        private string _alias;

        /// <summary>
        /// Initializes a new instance of the <see cref="NullSpecimenCustomization"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="alias">The alias.</param>
        public NullSpecimenCustomization(Type type, string alias)
        {
            _type = type;
            _alias = alias;
        }

        /// <summary>
        /// Customizes the specified fixture.
        /// </summary>
        /// <param name="fixture">The fixture to customize.</param>
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(new NullSpecimenGenerator(_type, _alias));
        }
    }

    /// <summary>
    /// Customizes the <typeparamref name="T"/> of specified fixture to return a null instance for a given alias.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NullSpecimenCustomization<T> : NullSpecimenCustomization
        where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullSpecimenCustomization{T}"/> class.
        /// </summary>
        /// <param name="alias">The alias for the request.</param>
        public NullSpecimenCustomization(string alias)
            : base(typeof(T), alias)
        { }
    }
}