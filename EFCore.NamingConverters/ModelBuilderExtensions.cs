using System;
using Microsoft.EntityFrameworkCore;

namespace EFCore.NamingConverters
{
    /// <summary>Методы-расширения для <see cref="ModelBuilder" /></summary>
    public static class ModelBuilderExtensions
    {
        /// <summary>Преобразование имён объектов</summary>
        /// <param name="modelBuilder">
        /// <see cref="ModelBuilder" />
        /// </param>
        /// <param name="renameFunc">Функция преобразования</param>
        /// <param name="namingConverterFlags">
        /// <see cref="NamingConverterFlags" />
        /// </param>
        public static void ConvertNaming(
            this ModelBuilder modelBuilder,
            Func<string, string> renameFunc,
            NamingConverterFlags namingConverterFlags = NamingConverterFlags.All)
        {
            var renamer = new MutableEntityTypeRenamer(renameFunc, namingConverterFlags);

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                renamer.Rename(entity);
            }
        }
    }
}
