using Bardock.UnitTesting.AutoFixture.Xunit2.Attributes;

namespace Bardock.UnitTesting.Samples.Fixtures.Attributes
{
    internal class InlineDefaultDataAttribute : InlineAutoDataAndCustomizationsAttribute
    {
        public InlineDefaultDataAttribute(params object[] valuesAndCustomizationTypes)
            : base(new DefaultDataAttribute(), valuesAndCustomizationTypes)
        {
        }
    }
}