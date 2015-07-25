using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;
using Bardock.Utils.Extensions;

namespace OriginSoftware.Kairos.Core.Tests.Fixtures.Customizations
{
    /// <summary>
    /// Customizes a property for the destination type and customizes the specified value for the source type using an existing mapping.
    /// </summary>
    public class AutoMappedPropertyCustomization<TSource, TDestination, TValue> : MappedPropertyCustomization<TSource, TDestination, TValue>
    {
        public AutoMappedPropertyCustomization(Expression<Func<TSource, TValue>> property, TValue value)
            : base(property, GetDestinationProperty(property), value)
        { }

        private static Expression<Func<TDestination, TValue>> GetDestinationProperty(Expression<Func<TSource, TValue>> property)
        {
            var map = Mapper.GetAllTypeMaps()
                .Where(x => x.SourceType == typeof(TSource))
                .Where(x => x.DestinationType == typeof(TDestination))
                .FirstOrDefault();

            if (map == null)
                throw new InvalidOperationException(string.Format("Missing mapping between {0} and {1}", typeof(TSource).FullName, typeof(TDestination).FullName));

            var sourceMemberInfo = ((MemberExpression)property.RemoveConvert()).Member;

            var propInfo = map.GetPropertyMaps()
                .Where(x => x.SourceMember == sourceMemberInfo)
                .Select(x => x.DestinationProperty.MemberInfo)
                .FirstOrDefault() as PropertyInfo;

            if (propInfo == null)
                throw new InvalidOperationException(string.Format("A mapping for property {0} of {1} was not found", sourceMemberInfo.Name, typeof(TDestination).FullName));

            var exprParam = Expression.Parameter(typeof(TDestination), "x");
            var propertyExpr = Expression.Property(exprParam, propInfo.GetGetMethod());

            return Expression.Lambda<Func<TDestination, TValue>>(propertyExpr, exprParam);
        }
    }
}