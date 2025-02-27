using Domain.Models.Entities;
using Domain.Models.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations;

// конфигурация для сущности Country, задаётся атрибутом в классе сущности
public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    void IEntityTypeConfiguration<Country>.Configure(EntityTypeBuilder<Country> builder) {

        #region Задание ограничений полей таблицы "СТРАНЫ" при помощи Fluent API

        // настроить ограничение поля Name для Country:
        // задать ограничение максимальной длины строкового поля наименования страны
        // nvarchar(50) not null
        builder
            .Property(country => country.Name)
            .HasMaxLength(50)
            .IsRequired()
            .IsUnicode();

        #endregion


        #region Задание отношений между таблицами при помощи Fluent API

        #endregion


        #region Инициализация таблицы "СТРАНЫ"

        var countries = new List<Country>{
            new() { Id =  1, Name = "США",               Deleted = null },
            new() { Id =  2, Name = "Индия",             Deleted = null },
            new() { Id =  3, Name = "Нигерия",           Deleted = null },
            new() { Id =  4, Name = "Япония",            Deleted = null },
            new() { Id =  5, Name = "Китай",             Deleted = null },
            new() { Id =  6, Name = "Южная Корея",       Deleted = null },
            new() { Id =  7, Name = "Великобритания",    Deleted = null },
            new() { Id =  8, Name = "Франция",           Deleted = null },
            new() { Id =  9, Name = "Бразилия",          Deleted = null },
            new() { Id = 10, Name = "Саудовская Аравия", Deleted = null },
            new() { Id = 11, Name = "Ирак",              Deleted = null },
            new() { Id = 12, Name = "Египет",            Deleted = null },
            new() { Id = 13, Name = "Турция",            Deleted = null },
            new() { Id = 14, Name = "Таиланд",           Deleted = null },
            new() { Id = 15, Name = "Австралия",         Deleted = null },
            new() { Id = 16, Name = "Россия",            Deleted = null },
            new() { Id = 17, Name = "Италия",            Deleted = null },
            new() { Id = 18, Name = "Украина",           Deleted = null },
            new() { Id = 19, Name = "Беларусь",          Deleted = null },
            new() { Id = 20, Name = "Германия",          Deleted = null },
            new() { Id = 21, Name = "Югославия",         Deleted = Utils.GetRandomDateTime() },
            new() { Id = 22, Name = "Мексика",           Deleted = null }
        };

        // инициализация таблицы "СТРАНЫ"
        builder.HasData(countries);

        #endregion

    } // Configure

} // class CountryConfiguration