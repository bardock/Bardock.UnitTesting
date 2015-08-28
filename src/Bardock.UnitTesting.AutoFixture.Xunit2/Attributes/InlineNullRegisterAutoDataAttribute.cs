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
    /// <paramref name="parameterName"/> provided.
    /// Also Customizes the fixture to return a NullRegisterArgument instance with the <paramref name="parameterName"/> provided.
    /// </summary>
    public abstract class InlineNullRegisterAutoDataAttribute : InlineAutoDataAttribute
    {
        private string _parameterName;

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineNullRegisterAutoDataAttribute"/> class.
        /// Inherits from <see cref="InlineAutoDataAttribute" />
        /// </summary>
        /// <param name="autoDataAttribute">The <see cref="AutoDataAttribute"/> instance that provides auto-generated data specimens.</param>
        /// <param name="parameterName">Name of the null parameter.</param>
        /// <exception cref="ArgumentException">Parameter <paramref name="parameterName"/> cannot be a null or empty string</exception>
        protected InlineNullRegisterAutoDataAttribute(
            AutoDataAttribute autoDataAttribute,
            string parameterName)
            : base(autoDataAttribute)
        {
            if (string.IsNullOrWhiteSpace(parameterName))
                throw new ArgumentException("Parameter 'parameterName' cannot be a null or empty string", "parameterName");

            _parameterName = parameterName;
        }


        /// <summary>
        /// Returns the composition of data to be used to test the theory. Favors the data returned
        /// by DataAttributes in ascending order. Data already returned is ignored on next
        /// DataAttribute returned data.
        /// </summary>
        /// <param name="methodUnderTest">The method that is being tested.</param>
        /// <returns>
        /// Returns the composition of the theory data.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        /// </exception>
        /// <remarks>
        /// The number of combined data sets is restricted to the length of the attribute which provides the fewest data sets
        /// </remarks>
        public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest)
        {
            var parameter = methodUnderTest.GetParameters()
                                .Where(p => p.Name == _parameterName)
                                .FirstOrDefault();

            if (parameter == null)
                throw new InvalidOperationException(string.Format("Parameter '{0}' has not been found on method under test '{1}'. Cannot apply customization for parameter '{0}'", _parameterName, methodUnderTest.Name));

            if (!parameter.ParameterType.IsClass || !parameter.ParameterType.IsInterface)
                throw new InvalidOperationException(string.Format("Parameter '{0}' type must be a class or an interface on method under test '{1}'. Cannot apply customization for parameter '{0}'", _parameterName, methodUnderTest.Name));

            // Customize the fixture so it returns a null reference for the given type of the target parameter
            // Also Customize the fixture so that when a request of a NullRegisterArgument type is issued, it will
            // return one that has the target parameter name
            AutoDataAttribute.Fixture
                .Customize((ICustomization)Activator.CreateInstance(typeof(NullRegisterCustomization<>).MakeGenericType(parameter.ParameterType)))
                .Customize<NullRegisterArgument>(e => e.With(p => p.Name, _parameterName));

            return base.GetData(methodUnderTest);
        }
    }

    public class NullRegisterArgument
    {
        public string Name { get; set; }
    }
}