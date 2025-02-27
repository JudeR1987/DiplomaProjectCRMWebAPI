using Domain.Models.Entities;
using Domain.Models.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations;

// конфигурация для сущности City, задаётся атрибутом в классе сущности
public class CityConfiguration : IEntityTypeConfiguration<City>
{
    void IEntityTypeConfiguration<City>.Configure(EntityTypeBuilder<City> builder) {

        #region Задание ограничений полей таблицы "ГОРОДА" при помощи Fluent API

        // настроить ограничение поля Name для City:
        // задать ограничение максимальной длины строкового поля наименования города
        // nvarchar(50) not null
        builder
            .Property(city => city.Name)
            .HasMaxLength(50)
            .IsRequired()
            .IsUnicode();

        #endregion


        #region Задание отношений между таблицами при помощи Fluent API

        // связь с таблицей "СТРАНЫ"
        builder
            .HasOne(city => city.Country)
            .WithMany(country => country.Cities)
            .HasForeignKey(city => city.CountryId);

        
        // Настройка отношения "многие ко многим"  Cities <- Addresses -> Streets
        builder
            .HasMany(city => city.Streets)
            .WithMany(street => street.Cities)
            .UsingEntity<Address>(
                address => address
                    .HasOne(a => a.Street)
                    .WithMany(street => street.Addresses)
                    .HasForeignKey(a => a.StreetId),
                address => address
                    .HasOne(a => a.City)
                    .WithMany(city => city.Addresses)
                    .HasForeignKey(a => a.CityId)
            );

        #endregion


        #region Инициализация таблицы "ГОРОДА"

        var cities = new List<City>{
            new() { Id =  1, Name = "Донецк",      CountryId = 16, Deleted = null },
            new() { Id =  2, Name = "Макеевка",    CountryId = 16, Deleted = null },
            new() { Id =  3, Name = "Харцызск",    CountryId = 16, Deleted = null },
            new() { Id =  4, Name = "Снежное",     CountryId = 16, Deleted = null },
            new() { Id =  5, Name = "Торез",       CountryId = 16, Deleted = null },
            new() { Id =  6, Name = "Ясиноватая",  CountryId = 16, Deleted = null },
            new() { Id =  7, Name = "Иловайск",    CountryId = 16, Deleted = null },
            new() { Id =  8, Name = "Моспино",     CountryId = 16, Deleted = null },
            new() { Id =  9, Name = "Шахтёрск",    CountryId = 16, Deleted = null },
            new() { Id = 10, Name = "Амвросиевка", CountryId = 16, Deleted = null },
            new() { Id = 11, Name = "Зугрес",      CountryId = 16, Deleted = null },
            new() { Id = 12, Name = "Горловка",    CountryId = 16, Deleted = null },
            new() { Id = 13, Name = "Енакиево",    CountryId = 16, Deleted = null },
            new() { Id = 14, Name = "Новоазовск",  CountryId = 16, Deleted = null },
            new() { Id = 15, Name = "Кировское",   CountryId = 16, Deleted = null },
            new() { Id = 16, Name = "Сартана",     CountryId = 16, Deleted = null },
            new() { Id = 17, Name = "Мариуполь",   CountryId = 16, Deleted = null },
            new() { Id = 18, Name = "Волноваха",   CountryId = 16, Deleted = null },
            new() { Id = 19, Name = "Седово",      CountryId = 16, Deleted = Utils.GetRandomDateTime() },
            new() { Id = 20, Name = "Еленовка",    CountryId = 16, Deleted = null },
            new() { Id = 21, Name = "Тельманово",  CountryId = 16, Deleted = Utils.GetRandomDateTime() },
            new() { Id = 22, Name = "Старобешево", CountryId = 16, Deleted = null },
            new() { Id = 23, Name = "Рим",         CountryId = 17, Deleted = null },
            new() { Id = 24, Name = "Лондон",      CountryId =  7, Deleted = null },
            new() { Id = 25, Name = "Токио",       CountryId =  4, Deleted = null },
            new() { Id = 26, Name = "Париж",       CountryId =  8, Deleted = null }
        };

        // инициализация таблицы "ГОРОДА"
        builder.HasData(cities);

        #endregion

    } // Configure

} // class CityConfiguration