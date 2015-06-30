using Bardock.UnitTesting.Data.EF.Effort.DataLoaders;
using Bardock.UnitTesting.Samples.SUT.Entities;
using System.Collections.Generic;

namespace Bardock.UnitTesting.Samples.Fixtures.DataLoaders
{
    public class CountriesDataLoader : IEntityDataLoader<Country>
    {
        public IEnumerable<Country> GetData()
        {
            yield return new Country()
            {
                ID = (int)Country.Options.Canada,
                Name = "Canada",
                IsoCode = "CA"
            };
            yield return new Country()
            {
                ID = (int)Country.Options.USA,
                Name = "United States of America",
                IsoCode = "US"
            };
        }
    }
}