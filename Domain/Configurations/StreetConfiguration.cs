using Domain.Models.Entities;
using Domain.Models.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations;

// конфигурация для сущности Street, задаётся атрибутом в классе сущности
public class StreetConfiguration : IEntityTypeConfiguration<Street>
{
    void IEntityTypeConfiguration<Street>.Configure(EntityTypeBuilder<Street> builder) {

        #region Задание ограничений полей таблицы "УЛИЦЫ" при помощи Fluent API

        // настроить ограничение поля Name для Street:
        // задать ограничение максимальной длины строкового поля наименования улицы
        // nvarchar(80) not null
        builder
            .Property(street => street.Name)
            .HasMaxLength(80)
            .IsRequired()
            .IsUnicode();

        #endregion


        #region Задание отношений между таблицами при помощи Fluent API

        #endregion


        #region Инициализация таблицы "УЛИЦЫ"

        var streets = new List<Street>{
            new() { Id =  1, Name = "ул. Ефремова",              Deleted = null },
            new() { Id =  2, Name = "ул. Интернациональная",     Deleted = null },
            new() { Id =  3, Name = "ул. Дружбы",                Deleted = null },
            new() { Id =  4, Name = "ул. Освобождения Донбасса", Deleted = null },
            new() { Id =  5, Name = "ул. Международная",         Deleted = null },
            new() { Id =  6, Name = "ул. Ленина",                Deleted = null },
            new() { Id =  7, Name = "ул. Годунова",              Deleted = null },
            new() { Id =  8, Name = "ул. Судовая",               Deleted = null },
            new() { Id =  9, Name = "ул. Розы Люксембург",       Deleted = null },
            new() { Id = 10, Name = "ул. Содовая",               Deleted = Utils.GetRandomDateTime() },
            new() { Id = 11, Name = "ул. Садовая",               Deleted = null },
            new() { Id = 12, Name = "ул. Школьная",              Deleted = null },
            new() { Id = 13, Name = "пр. Мира",                  Deleted = null },
            new() { Id = 14, Name = "пр. Свободы",               Deleted = null },
            new() { Id = 15, Name = "пр. Вернадского",           Deleted = null },
            new() { Id = 16, Name = "пр. Рижский",               Deleted = null },
            new() { Id = 17, Name = "пр. Академика Сахарова",    Deleted = null },
            new() { Id = 18, Name = "пр. Ватутина",              Deleted = null },
            new() { Id = 19, Name = "пр. Ильича",                Deleted = null },
            new() { Id = 20, Name = "пр. Киевский",              Deleted = null },
            new() { Id = 21, Name = "бул. Шевченко",             Deleted = null },
            new() { Id = 22, Name = "бул. Звёздный",             Deleted = null },
            new() { Id = 23, Name = "пл. Ленина",                Deleted = Utils.GetRandomDateTime() },
            new() { Id = 24, Name = "пл. Победы",                Deleted = null },
            new() { Id = 25, Name = "пл. Свободы",               Deleted = null },
            new() { Id = 26, Name = "пер. Октября",              Deleted = null },
            new() { Id = 27, Name = "ул. Артёма",                Deleted = null }
        };

        // инициализация таблицы "УЛИЦЫ"
        builder.HasData(streets);

        #endregion

    } // Configure

} // class StreetConfiguration