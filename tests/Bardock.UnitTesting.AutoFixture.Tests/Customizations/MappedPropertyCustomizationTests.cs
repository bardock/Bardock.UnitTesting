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
            public int Prop1 { get; set; }
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
                Fixture.Register(() => Lambda.Expr<A, object>(x => x.Prop1));
                Fixture.Register(() => Lambda.Expr<B, object>(x => x.Prop2));
            }
        }

        public class MyFixture
        {
            public IFixture Fixture { get; private set; }

            public Mock<ICustomizationComposer<A>> SourcePropertyComposerMock { get; private set; }

            public Mock<ICustomizationComposer<Expression<Func<B, object>>>> DestinationPropertyComposerMock { get; private set; }

            public MyFixture()
            {
                var fixtureMock = new Mock<IFixture>();
                Fixture = fixtureMock.Object;
                SourcePropertyComposerMock = SetupCustomizeFor<A>(fixtureMock);
                DestinationPropertyComposerMock = SetupCustomizeFor<Expression<Func<B, object>>>(fixtureMock);
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
            Expression<Func<B, object>> destinationProperty,
            int value)
        {
            //Exercise
            Action act = () => new MappedPropertyCustomization<A, B>(null, destinationProperty, value);

            //Verify
            act.ShouldThrow<ArgumentException>();
        }

        [Theory]
        [AutoData]
        public void Constructor_InvalidSourceProperty_ShouldThrowArgumentException(
            Expression<Func<B, object>> destinationProperty,
            int value)
        {
            //Exercise
            Action act = () => new MappedPropertyCustomization<A, B>(
                x => x.ToString(),
                destinationProperty,
                value);

            //Verify
            act.ShouldThrow<ArgumentException>();
        }

        [Theory]
        [AutoData]
        public void Constructor_NullDestinationProperty_ShouldThrowArgumentException(
            Expression<Func<A, object>> sourceProperty,
            int value)
        {
            //Exercise
            Action act = () => new MappedPropertyCustomization<A, B>(sourceProperty, null, value);

            //Verify
            act.ShouldThrow<ArgumentException>();
        }

        [Theory]
        [AutoData]
        public void Constructor_InvalidDestinationProperty_ShouldThrowArgumentException(
            Expression<Func<A, object>> sourceProperty,
            int value)
        {
            //Exercise
            Action act = () => new MappedPropertyCustomization<A, B>(
                sourceProperty,
                x => x.ToString(),
                value);

            //Verify
            act.ShouldThrow<ArgumentException>();
        }

        [Theory]
        [AutoData]
        public void Customize_ValidExpressions_ShouldSucced(
            MyFixture myFixture,
            Expression<Func<A, object>> sourceProperty,
            Expression<Func<B, object>> destinationProperty,
            int value)
        {
            //Setup
            var sut = new MappedPropertyCustomization<A, B>(sourceProperty, destinationProperty, value);

            //Exercise
            sut.Customize(myFixture.Fixture);

            //Verify
            myFixture.SourcePropertyComposerMock
                .Verify(x => x.With(sourceProperty, value));
            myFixture.DestinationPropertyComposerMock
                .Verify(x => x.FromFactory(It.Is<Func<Expression<Func<B, object>>>>(f => f() == destinationProperty)));
        }
    }
}