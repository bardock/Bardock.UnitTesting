using Bardock.UnitTesting.AutoFixture.SpecimenBuilders.Requests;
using Ploeh.AutoFixture.Kernel;
using System.Reflection;

namespace Bardock.UnitTesting.AutoFixture.SpecimenBuilders.Relays
{
    /// <summary>
    /// Relays a request for a given specimen with an alias.
    /// </summary>
    public class NullSpecimenRequestParameterInfoRelay : ISpecimenBuilder
    {
        private string _parameterName;

        /// <summary>
        /// Initializes a new instance of the <see cref="NullSpecimenRequestParameterInfoRelay"/> class.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        public NullSpecimenRequestParameterInfoRelay(string parameterName)
        {
            _parameterName = parameterName;
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
            var pi = request as ParameterInfo;

            if (pi == null || pi.Name != _parameterName)
                return new NoSpecimen(request);

            return context.Resolve(new NullSpecimenRequest(type: pi.ParameterType, alias: pi.Name));
        }
    }
}