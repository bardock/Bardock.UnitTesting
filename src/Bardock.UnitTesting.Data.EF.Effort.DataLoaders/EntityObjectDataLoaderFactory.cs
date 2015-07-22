using Effort.DataLoaders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bardock.UnitTesting.Data.EF.Effort.DataLoaders
{
    internal class EntityObjectDataLoaderFactory : ITableDataLoaderFactory
    {
        private IDictionary<string, string> _bindings;

        public EntityObjectDataLoaderFactory(IDictionary<string, string> bindings)
        {
            if (bindings == null)
                throw new ArgumentNullException("bindings");

            if (!bindings.Any())
                throw new ArgumentException("Parameter bindings cannot be empty", "bindings");

            _bindings = bindings;
        }

        public ITableDataLoader CreateTableDataLoader(TableDescription tableDescription)
        {
            if (tableDescription == null)
                throw new ArgumentNullException("tableDescription");

            if (!_bindings.ContainsKey(tableDescription.Name))
                return new EmptyTableDataLoader();

            var entry = _bindings.Single(x => x.Key == tableDescription.Name);

            return new EntityObjectDataLoaderWrapper(
                (IEntityDataLoader<object>)Activator.CreateInstance(Type.GetType(entry.Value, throwOnError: false)),
                tableDescription.Columns
            );
        }

        public void Dispose()
        {
        }
    }
}