using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Dsl;
using Ploeh.AutoFixture.Kernel;
using System;
using System.Linq;
using System.Threading;

namespace Bardock.UnitTesting.AutoFixture.Extensions
{
    public static class IFixtureRegisterExtensions
    {
        /// <summary>
        /// Registers a lazy creation function for a specific type.
        /// The <paramref name="T"/> will be created when first requested.
        /// The generated instance is thread safe.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fixture">The fixture.</param>
        /// <param name="creator">A function that will be used to create objects of type T every time the Ploeh.AutoFixture.Fixture is asked to create an object of that type.</param>
        public static void RegisterFrozen<T>(this IFixture fixture, Func<T> creator)
        {
            fixture.RegisterFrozen(creator, isThreadSafe: false);
        }

        /// <summary>
        /// Registers a lazy creation function for a specific type.
        /// The <paramref name="T"/> will be created when first requested.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fixture">The fixture.</param>
        /// <param name="creator">A function that will be used to create objects of type T every time the Ploeh.AutoFixture.Fixture is asked to create an object of that type.</param>
        /// <param name="isThreadSafe">true to make this instance usable concurrently by multiple threads; false to make this instance usable by only one thread at a time.</param>
        public static void RegisterFrozen<T>(this IFixture fixture, Func<T> creator, bool isThreadSafe)
        {
            var lazy = new Lazy<T>(creator, isThreadSafe);
            fixture.Register<T>(() => lazy.Value);
        }

        /// <summary>
        /// Registers a lazy creation function for a specific type.
        /// The <paramref name="T"/> will be created when first requested.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fixture">The fixture.</param>
        /// <param name="creator">A function that will be used to create objects of type T every time the Ploeh.AutoFixture.Fixture is asked to create an object of that type.</param>
        /// <param name="mode">One of the enumeration values that specifies the <see cref="LazyThreadSafetyMode"/>.</param>
        public static void RegisterFrozen<T>(this IFixture fixture, Func<T> creator, LazyThreadSafetyMode mode)
        {
            var lazy = new Lazy<T>(creator, mode);
            fixture.Register<T>(() => lazy.Value);
        }
    }
}
