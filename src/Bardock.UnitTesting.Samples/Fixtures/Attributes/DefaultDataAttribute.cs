using Bardock.UnitTesting.Samples.Fixtures.Customizations;
using System;

namespace Bardock.UnitTesting.Samples.Fixtures.Attributes
{
    public class DefaultDataAttribute : Bardock.UnitTesting.AutoFixture.Xunit2.Fixtures.Attributes.DefaultDataAttribute
    {
        public DefaultDataAttribute()
            : base(new DefaultCustomization())
        { }

        public DefaultDataAttribute(params Type[] customizationTypes)
            : base(new DefaultCustomization(), customizationTypes)
        { }
    }
}