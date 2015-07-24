using System;
using Bardock.Utils.Extensions;
using System.Linq.Expressions;
using Ploeh.AutoFixture;

namespace OriginSoftware.Kairos.Core.Tests.Fixtures.Customizations
{
    /// <summary>
    /// Customizes a property for the destination type and customizes the specified value for the source type.
    /// </summary>
    public class MappedPropertyCustomization<TSource, TDestination> : ICustomization
    {
        private Expression<Func<TDestination, object>> _destinationProperty;
        private Expression<Func<TSource, object>> _sourceProperty;
        private object _value;

        public MappedPropertyCustomization(
            Expression<Func<TSource, object>> sourceProperty,
            Expression<Func<TDestination, object>> destinationProperty,
            object value)
        {
            if (sourceProperty == null || !(sourceProperty.RemoveConvert() is MemberExpression))
                throw new ArgumentException("sourceProperty must be a MemberExpression");

            if (destinationProperty == null || !(destinationProperty.RemoveConvert() is MemberExpression))
                throw new ArgumentException("destinationProperty must be a MemberExpression");

            _sourceProperty = sourceProperty;
            _destinationProperty = destinationProperty;
            _value = value;
        }

        public void Customize(IFixture fixture)
        {
            fixture.Customize<TSource>(c => c.With(_sourceProperty, _value));
            fixture.Register(() => _destinationProperty);
        }
    }
}