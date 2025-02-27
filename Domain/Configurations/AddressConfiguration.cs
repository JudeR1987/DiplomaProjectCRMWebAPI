using Domain.Models.Entities;
using Domain.Models.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations;

// конфигурация для сущности Address, задаётся атрибутом в классе сущности
public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    void IEntityTypeConfiguration<Address>.Configure(EntityTypeBuilder<Address> builder) {

        #region Задание ограничений полей таблицы "АДРЕСА" при помощи Fluent API

        // настроить ограничение поля Building для Address:
        // задать ограничение максимальной длины строкового поля номера дома/строения
        // nvarchar(10) not null
        builder
            .Property(address => address.Building)
            .HasMaxLength(10)
            .IsRequired()
            .IsUnicode();

        // настроить SQL-ограничение поля Flat для Address:
        // задать ограничение минимального значения номера квартиры
        builder
            .ToTable(address => address.HasCheckConstraint("Flat", "Flat > 0"));

        #endregion


        #region Задание отношений между таблицами при помощи Fluent API

        #endregion


        #region Инициализация таблицы "АДРЕСА"

        var addresses = new List<Address>{
            new() { Id =  1, CityId =  1, StreetId =  1, Building = "д. 12",  Flat =    41, Deleted = null },
            new() { Id =  2, CityId =  1, StreetId =  3, Building = "д. 20",  Flat =    15, Deleted = null },
            new() { Id =  3, CityId =  2, StreetId = 19, Building = "д. 144", Flat =    84, Deleted = null },
            new() { Id =  4, CityId =  2, StreetId =  7, Building = "д. 14",  Flat =  null, Deleted = null },
            new() { Id =  5, CityId =  2, StreetId = 16, Building = "д. 122", Flat =  null, Deleted = null },
            new() { Id =  6, CityId =  3, StreetId = 18, Building = "д. 17",  Flat =    45, Deleted = null },
            new() { Id =  7, CityId =  3, StreetId =  6, Building = "д. 48",  Flat =    74, Deleted = null },
            new() { Id =  8, CityId =  3, StreetId =  5, Building = "д. 13",  Flat =    45, Deleted = null },
            new() { Id =  9, CityId =  5, StreetId =  2, Building = "д. 22",  Flat =  null, Deleted = null },
            new() { Id = 10, CityId =  6, StreetId =  4, Building = "д. 56",  Flat =    12, Deleted = null },
            new() { Id = 11, CityId =  7, StreetId = 11, Building = "д. 32",  Flat =   144, Deleted = null },
            new() { Id = 12, CityId =  9, StreetId = 12, Building = "д. 13б", Flat =    75, Deleted = null },
            new() { Id = 13, CityId =  9, StreetId = 14, Building = "д. 7",   Flat =    22, Deleted = null },
            new() { Id = 14, CityId = 10, StreetId = 15, Building = "д. 14",  Flat =    69, Deleted = null },
            new() { Id = 15, CityId = 11, StreetId = 20, Building = "д. 44а", Flat =    13, Deleted = null },
            new() { Id = 16, CityId = 13, StreetId = 17, Building = "д. 8",   Flat =    53, Deleted = null },
            new() { Id = 17, CityId = 13, StreetId = 16, Building = "д. 41",  Flat =    10, Deleted = null },
            new() { Id = 18, CityId = 13, StreetId = 12, Building = "д. 144", Flat =  null, Deleted = null },
            new() { Id = 19, CityId = 15, StreetId =  8, Building = "д. 14",  Flat =    23, Deleted = null },
            new() { Id = 20, CityId = 15, StreetId = 14, Building = "д. 107", Flat =  null, Deleted = null },
            new() { Id = 21, CityId = 18, StreetId =  2, Building = "д. 42",  Flat =    14, Deleted = null },
            new() { Id = 22, CityId = 20, StreetId =  5, Building = "д. 18",  Flat =    67, Deleted = null },
            new() { Id = 23, CityId = 20, StreetId =  9, Building = "д. 67",  Flat =   104, Deleted = null },
            new() { Id = 24, CityId = 22, StreetId = 14, Building = "д. 147", Flat =  null, Deleted = null },
            new() { Id = 25, CityId = 22, StreetId = 11, Building = "д. 8в",  Flat =    54, Deleted = null },
            new() { Id = 26, CityId =  1, StreetId = 11, Building = "д. 18",  Flat =   142, Deleted = null },
            new() { Id = 27, CityId =  3, StreetId =  6, Building = "д. 144", Flat =    12, Deleted = null },
            new() { Id = 28, CityId =  5, StreetId = 20, Building = "д. 86",  Flat =    35, Deleted = Utils.GetRandomDateTime() },
            new() { Id = 29, CityId =  7, StreetId = 15, Building = "д. 13",  Flat =    24, Deleted = null },
            new() { Id = 30, CityId =  9, StreetId = 12, Building = "д. 87",  Flat =    53, Deleted = null },
            new() { Id = 31, CityId = 10, StreetId = 17, Building = "д. 19",  Flat =    21, Deleted = null },
            new() { Id = 32, CityId = 10, StreetId = 24, Building = "д. с.1", Flat =   225, Deleted = null },
            new() { Id = 33, CityId = 12, StreetId = 22, Building = "д. 10б", Flat =  null, Deleted = null },
            new() { Id = 34, CityId = 14, StreetId = 26, Building = "д. 44",  Flat =   185, Deleted = null },
            new() { Id = 35, CityId = 16, StreetId = 26, Building = "д. 1б",  Flat =    40, Deleted = null },
            new() { Id = 36, CityId = 16, StreetId = 21, Building = "д. 86",  Flat =    86, Deleted = Utils.GetRandomDateTime() },
            new() { Id = 37, CityId = 17, StreetId = 21, Building = "д. 14",  Flat =    40, Deleted = null },
            new() { Id = 38, CityId = 18, StreetId = 21, Building = "д. 21",  Flat =   121, Deleted = null },
            new() { Id = 39, CityId = 20, StreetId = 21, Building = "д. 22",  Flat =    22, Deleted = null },
            new() { Id = 40, CityId = 22, StreetId = 21, Building = "д. 22",  Flat =    23, Deleted = null }
        };

        // инициализация таблицы "АДРЕСА"
        builder.HasData(addresses);

        #endregion

    } // Configure

} // class AddressConfiguration