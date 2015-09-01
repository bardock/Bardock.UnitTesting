using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bardock.UnitTesting.Samples.Fixtures.Attributes
{
    public class InlineUseNullAttribute : Bardock.UnitTesting.AutoFixture.Xunit2.Attributes.InlineUseNullSpecimenAttribute
    {
        public InlineUseNullAttribute(string parameterName)
            :base(new DefaultDataAttribute(), parameterName)
        {

        }
    }
}
