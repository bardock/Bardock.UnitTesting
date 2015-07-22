using Effort.DataLoaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Bardock.UnitTesting.Data.EF.Effort.DataLoaders
{
    internal class EntityObjectDataLoaderWrapper : ITableDataLoader
    {
        private IReadOnlyCollection<ColumnDescription> _columnsDescription;
        private IEntityDataLoader<object> _entityDataLoader;

        public EntityObjectDataLoaderWrapper(
            IEntityDataLoader<object> entityDataLoader,
            IReadOnlyCollection<ColumnDescription> columnsDescription)
        {
            if (entityDataLoader == null)
                throw new ArgumentNullException("entityDataLoader");

            if (columnsDescription == null)
                throw new ArgumentNullException("columnsDescription");

            if (!columnsDescription.Any())
                throw new ArgumentException("Parameter columnsDescription cannot be empty", "columnsDescription");

            _entityDataLoader = entityDataLoader;
            _columnsDescription = columnsDescription;
        }

        public IEnumerable<object[]> GetData()
        {
            return _entityDataLoader
                    .GetData()
                    .Select(obj => _columnsDescription
                                    .Select(c => {
                                        var targetType = obj.GetType();

                                        var targetProperty = targetType.GetProperty(c.Name);

                                        if (targetProperty == null)
                                            throw new MissingPropertyException(c.Name, targetType, _columnsDescription);

                                        return targetProperty.GetValue(obj);
                                    })
                                    .ToArray());
        }

        public class MissingPropertyException : Exception
        {
            public IEnumerable<ColumnDescription> ColumnsDescription { get; private set; }

            public MissingPropertyException(
                string targetPropertyName,
                Type targetType,
                IReadOnlyCollection<ColumnDescription> columnsDescription)
                : base(
                    string.Format(
                        "Could not find property with name {0} on type {1}, see {2} to view a complete list of columns to be mapped",
                        targetPropertyName,
                        targetType.Name,
                        "ColumnsDescription"))
            {
                ColumnsDescription = columnsDescription;
            }
        }
    }
}