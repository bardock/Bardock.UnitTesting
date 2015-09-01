using System;

namespace Bardock.UnitTesting.AutoFixture.SpecimenBuilders.Requests
{
    /// <summary>
    /// Encapsulates a pattern for a null reference
    /// </summary>
    public class NullSpecimenRequest
    {
        public Type Type { get; private set; }

        public string Alias { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="NullSpecimenRequest"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="alias">The alias.</param>
        public NullSpecimenRequest(Type type, string alias)
        {
            Type = type;
            Alias = alias;
        }
    }
}