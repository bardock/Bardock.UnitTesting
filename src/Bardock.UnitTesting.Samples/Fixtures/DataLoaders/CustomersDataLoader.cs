using Bardock.UnitTesting.Data.EF.Effort.DataLoaders;
using Bardock.UnitTesting.Samples.SUT.Entities;
using Ploeh.AutoFixture;
using System.Collections.Generic;

namespace Bardock.UnitTesting.Samples.Fixtures.DataLoaders
{
    public class CustomersDataLoader : IEntityDataLoader<Customer>
    {
        public IEnumerable<Customer> GetData()
        {
            yield return AdultFromUS;
        }

        internal static Customer AdultFromUS = BuildAdultFromUS;

        private static Customer BuildAdultFromUS
        {
            get
            {
                return new LoaderFixture().Create<Customer>();
            }
        }
    }
}