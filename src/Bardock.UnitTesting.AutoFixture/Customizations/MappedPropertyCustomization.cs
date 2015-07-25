using System;
using System.Linq.Expressions;
using Bardock.Utils.Extensions;
using Ploeh.AutoFixture;

namespace OriginSoftware.Kairos.Core.Tests.Fixtures.Customizations
{
    /// <summary>
    /// Customizes a property for the destination type and customizes the specified value for the source type.
    /// </summary>
    public class MappedPropertyCustomization<TSource, TDestination, TValue> : ICustomization
    {
        public Expression<Func<TDestination, TValue>> DestinationProperty { get; protected set; }

        public Expression<Func<TSource, TValue>> SourceProperty { get; protected set; }

        public TValue Value { get; protected set; }

        public MappedPropertyCustomization(
            Expression<Func<TSource, TValue>> sourceProperty,
            Expression<Func<TDestination, TValue>> destinationProperty,
            TValue value)
        {
            if (sourceProperty == null || !(sourceProperty.RemoveConvert() is MemberExpression))
                throw new ArgumentException("sourceProperty must be a MemberExpression");

            if (destinationProperty == null || !(destinationProperty.RemoveConvert() is MemberExpression))
                throw new ArgumentException("destinationProperty must be a MemberExpression");

            SourceProperty = sourceProperty;
            DestinationProperty = destinationProperty;
            Value = value;
        }

        public void Customize(IFixture fixture)
        {
            fixture.Customize<TSource>(c => c.With(SourceProperty, Value));
            fixture.Register(() => DestinationProperty);
        }
    }
}