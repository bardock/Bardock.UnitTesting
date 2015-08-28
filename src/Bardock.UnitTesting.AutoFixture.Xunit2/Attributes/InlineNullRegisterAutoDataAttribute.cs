using Bardock.UnitTesting.AutoFixture.Customizations;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Bardock.UnitTesting.AutoFixture.Xunit2.Attributes
{
    /// <summary>
    /// Makes null the parameter that has a name that matches with the
    /// parameterName provided.
    /// </summary>
    public abstract class InlineNullRegisterAutoDataAttribute : InlineAutoDataAttribute
    {
        private string _parameterName;

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineNullRegisterAutoDataAttribute"/> class.
        /// </summary>
        /// <param name="autoDataAttribute">The <see cref="AutoDataAttribute"/> instance that provides auto-generated data specimens.</param>
        /// <param name="parameterName">Name of the null parameter.</param>
        public InlineNullRegisterAutoDataAttribute(
            AutoDataAttribute autoDataAttribute,
            string parameterName)
            : base(autoDataAttribute)
        {
            _parameterName = parameterName;
        }

        public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest)
        {
            var parameter = methodUnderTest.GetParameters().First(p => p.Name == _parameterName);
            var fixture = AutoDataAttribute.Fixture;

            var nullCustomization = ((ICustomization)
                                            Activator
                                                .CreateInstance(typeof(NullRegisterCustomization<>)
                                                .MakeGenericType(parameter.ParameterType)));

            fixture.Customize(nullCustomization);
            fixture.Customize<NullRegisterArgument>(e => e.With(p => p.Name, _parameterName));

            return base.GetData(methodUnderTest);
        }

    }

    public class NullRegisterArgument
    {
        public string Name { get; set; }
    }
}