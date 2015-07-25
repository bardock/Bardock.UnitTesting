using System;
using System.Linq.Expressions;
using Bardock.Utils.Types;
using FluentAssertions;
using Moq;
using OriginSoftware.Kairos.Core.Tests.Fixtures.Customizations;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Dsl;
using Ploeh.AutoFixture.Kernel;
using Xunit;

namespace Bardock.UnitTesting.AutoFixture.Tests.Customizations
{
    public class MappedPropertyCustomizationTests
    {
        public class A
        {
            public short Prop1 { get; set; }
        }

        public class B
        {
            public int Prop2 { get; set; }
        }

        public class AutoDataAttribute : Ploeh.AutoFixture.Xunit2.AutoDataAttribute
        {
            public AutoDataAttribute()
            {
                Fixture.Register(() => new MyFixture());
                Fixture.Register(() => Lambda.Expr<A, int>(x => x.Prop1));
                Fixture.Register(() => Lambda.Expr<B, int>(x => x.Prop2));
            }
        }

        public class MyFixture : MyFixture<A, B, int>
        { }

        public class MyFixture<TSource, TDestination, TValue>
        {
            public IFixture Fixture { get; private set; }

            public Mock<ICustomizationComposer<TSource>> SourcePropertyComposerMock { get; private set; }

            public Mock<ICustomizationComposer<Expression<Func<TDestination, TValue>>>> DestinationPropertyComposerMock { get; private set; }

            public MyFixture()
            {
                var fixtureMock = new Mock<IFixture>();
                Fixture = fixtureMock.Object;
                SourcePropertyComposerMock = SetupCustomizeFor<TSource>(fixtureMock);
                DestinationPropertyComposerMock = SetupCustomizeFor<Expression<Func<TDestination, TValue>>>(fixtureMock);
            }

            private Mock<ICustomizationComposer<T>> SetupCustomizeFor<T>(Mock<IFixture> fixtureMock)
            {
                var custComposerMock = new Mock<ICustomizationComposer<T>>() { DefaultValue = DefaultValue.Mock };

                fixtureMock.Setup(x => x.Customize<T>(It.IsAny<Func<ICustomizationComposer<T>, ISpecimenBuilder>>()))
                    .Callback((Func<ICustomizationComposer<T>, ISpecimenBuilder> creator) => creator(custComposerMock.Object));

                return custComposerMock;
            }
        }

        [Theory]
        [AutoData]
        public void Constructor_NullSourceProperty_ShouldThrowArgumentException(
            Expression<Func<B, int>> destinationProperty,
            int value)
        {
            //Exercise
            Action act = () => new MappedPropertyCustomization<A, B, int>(null, destinationProperty, value);

            //Verify
            act.ShouldThrow<ArgumentException>();
        }

        [Theory]
        [AutoData]
        public void Constructor_InvalidSourceProperty_ShouldThrowArgumentException(
            Expression<Func<B, int>> destinationProperty,
            int value)
        {
            //Exercise
            Action act = () => new MappedPropertyCustomization<A, B, int>(
                x => x.Prop1 * 5,
                destinationProperty,
                value);

            //Verify
            act.ShouldThrow<ArgumentException>();
        }

        [Theory]
        [AutoData]
        public void Constructor_NullDestinationProperty_ShouldThrowArgumentException(
            Expression<Func<A, int>> sourceProperty,
            int value)
        {
            //Exercise
            Action act = () => new MappedPropertyCustomization<A, B, int>(sourceProperty, null, value);

            //Verify
            act.ShouldThrow<ArgumentException>();
        }

        [Theory]
        [AutoData]
        public void Constructor_InvalidDestinationProperty_ShouldThrowArgumentException(
            Expression<Func<A, int>> sourceProperty,
            int value)
        {
            //Exercise
            Action act = () => new MappedPropertyCustomization<A, B, int>(
                sourceProperty,
                x => x.Prop2 * 5,
                value);

            //Verify
            act.ShouldThrow<ArgumentException>();
        }

        [Theory]
        [AutoData]
        public void Constructor_ValidProperties_ShouldSucced(
            Expression<Func<A, int>> sourceProperty,
            Expression<Func<B, int>> destinationProperty,
            int value)
        {
            //Exercise
            var sut = new MappedPropertyCustomization<A, B, int>(
                sourceProperty,
                destinationProperty,
                value);

            //Verify
            sut.SourceProperty.Should().Be(sourceProperty);
            sut.DestinationProperty.Should().Be(destinationProperty);
            sut.Value.Should().Be(value);
        }

        [Theory]
        [AutoData]
        public void Customize_ValidProperties_FixtureShouldBeCustomized(
            MyFixture myFixture,
            MappedPropertyCustomization<A, B, int> sut)
        {
            //Exercise
            sut.Customize(myFixture.Fixture);

            //Verify
            FixtureShouldBeCustomized(myFixture, sut);
        }

        protected void FixtureShouldBeCustomized<TSource, TDestination, TValue>(
            MyFixture<TSource, TDestination, TValue> myFixture,
            MappedPropertyCustomization<TSource, TDestination, TValue> sut)
        {
            myFixture.SourcePropertyComposerMock
                .Verify(x => x.With(sut.SourceProperty, sut.Value));

            myFixture.DestinationPropertyComposerMock
                .Verify(x => x.FromFactory(It.Is<Func<Expression<Func<TDestination, TValue>>>>(f => f() == sut.DestinationProperty)));
        }
    }
}