using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EFCore.NamingConverters
{
    /// <summary>Преобразователь имён объектов</summary>
    public class MutableEntityTypeRenamer
    {
        private readonly Func<string, string> _renameFunc;
        private readonly IReadOnlyList<Action<IMutableEntityType>> _renames;

        /// <inheritdoc cref="MutableEntityTypeRenamer" />
        /// <param name="renameFunc">Функция преобразования</param>
        /// <param name="namingConverterFlags">
        /// <see cref="NamingConverterFlags" />
        /// </param>
        public MutableEntityTypeRenamer(
            Func<string, string> renameFunc,
            NamingConverterFlags namingConverterFlags = NamingConverterFlags.All)
        {
            _renameFunc = renameFunc;
            _renames = new Dictionary<NamingConverterFlags, Action<IMutableEntityType>>
                {
                    { NamingConverterFlags.Schemas, this.RenameSchema },
                    { NamingConverterFlags.Tables, this.RenameTable },
                    { NamingConverterFlags.Keys, this.RenameKeys },
                    { NamingConverterFlags.ForeignKeys, this.RenameForeignKeys },
                    { NamingConverterFlags.Indexes, this.RenameIndexes },
                    { NamingConverterFlags.Columns, this.RenameColumns }
                }
                .Where(x => namingConverterFlags.HasFlag(x.Key))
                .Select(x => x.Value)
                .ToList();
        }

        /// <summary>Преобразование имён объекта</summary>
        /// <param name="mutableEntityType"></param>
        public void Rename(IMutableEntityType mutableEntityType)
        {
            foreach (var rename in _renames)
            {
                rename(mutableEntityType);
            }
        }

        private void RenameSchema(IMutableEntityType entity) => entity.SetSchema(_renameFunc(entity.GetSchema()));

        private void RenameTable(IMutableEntityType entity) => entity.SetTableName(_renameFunc(entity.GetTableName()));

        private void RenameKeys(IMutableEntityType entity)
        {
            foreach (var key in entity.GetKeys())
            {
                key.SetName(_renameFunc(key.GetName()));
            }
        }

        private void RenameForeignKeys(IMutableEntityType entity)
        {
            foreach (var key in entity.GetForeignKeys())
            {
                key.SetConstraintName(_renameFunc(key.GetConstraintName()));
            }
        }

        private void RenameIndexes(IMutableEntityType entity)
        {
            foreach (var index in entity.GetIndexes())
            {
                index.SetName(_renameFunc(index.GetName()));
            }
        }

        private void RenameColumns(IMutableEntityType entity)
        {
            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(_renameFunc(property.GetColumnName()));
            }
        }
    }
}
