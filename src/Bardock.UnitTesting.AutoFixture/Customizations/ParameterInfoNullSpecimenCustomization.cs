using Bardock.UnitTesting.AutoFixture.SpecimenBuilders.Relays;
using Ploeh.AutoFixture;
using System;

namespace Bardock.UnitTesting.AutoFixture.Customizations
{
    /// <summary>
    /// Customize the fixture so it returns a null reference for the given type of the target parameter
    /// Also Customize the fixture so that when a request of a <see cref="NullSpecimenDescriptor"/> type is issued, it will
    /// return an instance that has the target parameter name
    /// </summary>
    public class ParameterInfoNullSpecimenCustomization : ICustomization
    {
        private Type _type;

        private string _parameterName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterInfoNullSpecimenCustomization"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        public ParameterInfoNullSpecimenCustomization(Type type, string parameterName)
        {
            _type = type;
            _parameterName = parameterName;
        }

        /// <summary>
        /// Customizes the specified fixture.
        /// </summary>
        /// <param name="fixture">The fixture to customize.</param>
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(new NullSpecimenRequestParameterInfoRelay(_parameterName));

            fixture
                .Customize(new NullSpecimenCustomization(_type, _parameterName))
                .Customize<NullSpecimenDescriptor>(e => e.With(p => p.Alias, _parameterName));
        }
    }

    public class NullSpecimenDescriptor
    {
        public string Alias { get; set; }
    }
}