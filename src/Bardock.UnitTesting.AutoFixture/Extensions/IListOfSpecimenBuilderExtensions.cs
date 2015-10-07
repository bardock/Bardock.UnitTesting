using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture.Kernel;

namespace Bardock.UnitTesting.AutoFixture.Extensions
{
    public static class IListOfSpecimenBuilderExtensions
    {
        public static void AddToTop(this IList<ISpecimenBuilder> list, ISpecimenBuilder builder)
        {
            list.Insert(0, builder);
        }
    }
}
