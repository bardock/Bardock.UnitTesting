using Bardock.UnitTesting.Data.EF.Effort.DataLoaders;
using Bardock.UnitTesting.Samples.SUT.Entities;
using System.Collections.Generic;

namespace Bardock.UnitTesting.Samples.Fixtures.DataLoaders
{
    public class AddressesDataLoader : IEntityDataLoader<Address>
    {
        public IEnumerable<Address> GetData()
        {
            yield return new Address()
            {
                ID = 1,
                CountryID = (int)Country.Options.USA,
                CustomerID = CustomersDataLoader.AdultFromUS.ID,
                Line1 = "asd",
                State = "CA"
            };
            yield return new Address()
            {
                ID = 2,
                CountryID = (int)Country.Options.USA,
                CustomerID = CustomersDataLoader.AdultFromUS.ID,
                Line1 = "qwe",
                State = "MA"
            };
        }
    }
}