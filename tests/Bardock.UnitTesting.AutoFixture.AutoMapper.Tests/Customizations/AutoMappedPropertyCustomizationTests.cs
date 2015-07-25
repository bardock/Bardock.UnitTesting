using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OriginSoftware.Kairos.Core.Tests.Fixtures.Customizations;
using Ploeh.AutoFixture;
using Xunit;
using System.Linq.Expressions;

namespace Bardock.UnitTesting.AutoFixture.AutoMapper.Tests.Customizations
{
    public class AutoMappedPropertyCustomizationTests : Bardock.UnitTesting.AutoFixture.Tests.Customizations.MappedPropertyCustomizationTests
    {
        public new class AutoDataAttribute : Bardock.UnitTesting.AutoFixture.Tests.Customizations.MappedPropertyCustomizationTests.AutoDataAttribute
        {
            public AutoDataAttribute(bool mapClasses = true, bool mapProps = true)
                : base()
            {
                if (mapClasses)
                {
                    var mapping = Mapper.CreateMap<A, B>();
                    var reverseMapping = mapping.ReverseMap();
                    if (mapProps)
                    {
                        mapping.ForMember(b => b.Prop2, o => o.MapFrom(a => a.Prop1));
                        reverseMapping.ForMember(a => a.Prop1, o => o.MapFrom(b => b.Prop2));
                    }
                }
            }
        }

        [Theory]
        [AutoData]
        public void Construct_IntToShortProperties_ShouldThrowArgumentException(
            MyFixture<B, A, int> myFixture,
            Expression<Func<B, int>> sourceProperty,
            int value)
        {
            //Exercise
            Action act = () => new AutoMappedPropertyCustomization<B, A, int>(sourceProperty, value);

            //Verify
            act.ShouldThrow<ArgumentException>()
                .WithMessage("Expression of type 'System.Int16' cannot be used for return type 'System.Int32'");
        }

        [Theory]
        [AutoData]
        public void Customize_ValidProperties_FixtureShouldBeCustomized(
            MyFixture myFixture,
            AutoMappedPropertyCustomization<A, B, int> sut)
        {
            //Exercise
            sut.Customize(myFixture.Fixture);

            //Verify
            FixtureShouldBeCustomized(myFixture, sut);
        }
    }
}
