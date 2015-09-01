using Bardock.UnitTesting.AutoFixture.SpecimenBuilders.Requests;
using Ploeh.AutoFixture.Kernel;
using System;

namespace Bardock.UnitTesting.AutoFixture.SpecimenBuilders.Generators
{
    /// <summary>
    /// Returns null when a <see cref="NullSpecimenRequest"/> matches its type and alias
    /// </summary>
    public class NullSpecimenGenerator : ISpecimenBuilder
    {
        private Type _type;

        private string _alias;

        /// <summary>
        /// Initializes a new instance of the <see cref="NullSpecimenGenerator"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="alias">The alias.</param>
        public NullSpecimenGenerator(Type type, string alias)
        {
            _type = type;
            _alias = alias;
        }

        /// <summary>
        /// Creates a new specimen based on a request.
        /// </summary>
        /// <param name="request">The request that describes what to create.</param>
        /// <param name="context">A context that can be used to create other specimens.</param>
        /// <returns>
        /// The requested specimen if possible; otherwise a <see cref="T:Ploeh.AutoFixture.Kernel.NoSpecimen" /> instance.
        /// </returns>
        /// <remarks>
        /// <para>
        /// The <paramref name="request" /> can be any object, but will often be a
        /// <see cref="T:System.Type" /> or other <see cref="T:System.Reflection.MemberInfo" /> instances.
        /// </para>
        /// <para>
        /// Note to implementers: Implementations are expected to return a
        /// <see cref="T:Ploeh.AutoFixture.Kernel.NoSpecimen" /> instance if they can't satisfy the request.
        /// </para>
        /// </remarks>
        public object Create(object request, ISpecimenContext context)
        {
            var rq = request as NullSpecimenRequest;

            if (rq == null || rq.Type != _type || rq.Alias != _alias)
                return new NoSpecimen(request);

            return null;
        }
    }
}