#### Конвертер имён объектов для EF Core

Позволяет переименовать таблицы, колонки, ключи, внешние ключи и индексы с использованием собственного конвертера или привести к snake_case.

Пример использования:
```c#
public class MyContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Все! действия над моделью данных
        
        modelBuilder.ConvertNaming(x => x.ToSnakeCase());
    }
}
```