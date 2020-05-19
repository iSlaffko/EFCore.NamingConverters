using System;

namespace EFCore.NamingConverters
{
    /// <summary>Флаги преобразования имён объектов</summary>
    [Flags]
    public enum NamingConverterFlags
    {
        /// <summary>Не выбрано</summary>
        None = 0,

        /// <summary>Схемы</summary>
        Schemas = 1,

        /// <summary>Таблицы</summary>
        Tables = 2,

        /// <summary>Ключи</summary>
        Keys = 4,

        /// <summary>Внешние ключи</summary>
        ForeignKeys = 8,

        /// <summary>Индексы</summary>
        Indexes = 16,

        /// <summary>Колонки</summary>
        Columns = 32,

        /// <summary>Все</summary>
        All = ~0
    }
}
