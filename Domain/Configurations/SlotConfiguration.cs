using Domain.Models.Entities;
using Domain.Models.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations;

// конфигурация для сущности WorkDay, задаётся атрибутом в классе сущности
public class SlotConfiguration : IEntityTypeConfiguration<Slot>
{
    void IEntityTypeConfiguration<Slot>.Configure(EntityTypeBuilder<Slot> builder) {

        #region Задание ограничений полей таблицы "ПРОМЕЖУТКИ_ВРЕМЕНИ" при помощи Fluent API

        // настроить SQL-ограничение поля Length для Slot:
        // задать ограничение минимального значения минимальной цены на услугу
        builder
            .ToTable(slot => slot.HasCheckConstraint("Length", "Length >= 0"));

        #endregion


        #region Задание отношений между таблицами при помощи Fluent API

        // связь с таблицей "СОТРУДНИКИ"
        /*builder
            .HasOne(workDay => workDay.Employee)
            .WithMany(employee => employee.Schedule)
            .HasForeignKey(workDay => workDay.EmployeeId);*/


        // Настройка отношения "многие ко многим"  Cities <- Addresses -> Streets
        /*builder
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
            );*/

        #endregion


        #region Инициализация таблицы "ПРОМЕЖУТКИ_ВРЕМЕНИ"

        var slots = new List<Slot>{
            new() { Id =  1, From = new TimeOnly( 8,  0, 0), Length = 15 * 60, Deleted = null },
            new() { Id =  2, From = new TimeOnly( 8, 15, 0), Length = 15 * 60, Deleted = null },
            new() { Id =  3, From = new TimeOnly( 8, 30, 0), Length = 15 * 60, Deleted = null },
            new() { Id =  4, From = new TimeOnly( 8, 45, 0), Length = 15 * 60, Deleted = null },
            new() { Id =  5, From = new TimeOnly( 9,  0, 0), Length = 15 * 60, Deleted = null },
            new() { Id =  6, From = new TimeOnly( 9, 15, 0), Length = 15 * 60, Deleted = null },
            new() { Id =  7, From = new TimeOnly( 9, 30, 0), Length = 15 * 60, Deleted = null },
            new() { Id =  8, From = new TimeOnly( 9, 45, 0), Length = 15 * 60, Deleted = null },
            new() { Id =  9, From = new TimeOnly(10,  0, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 10, From = new TimeOnly(10, 15, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 11, From = new TimeOnly(10, 30, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 12, From = new TimeOnly(10, 45, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 13, From = new TimeOnly(11,  0, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 14, From = new TimeOnly(11, 15, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 15, From = new TimeOnly(11, 30, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 16, From = new TimeOnly(11, 45, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 17, From = new TimeOnly(12,  0, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 18, From = new TimeOnly(12, 15, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 19, From = new TimeOnly(12, 30, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 20, From = new TimeOnly(12, 45, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 21, From = new TimeOnly(13,  7, 0), Length =  6 * 60, Deleted = Utils.GetRandomDateTime() },
            new() { Id = 22, From = new TimeOnly(13,  0, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 23, From = new TimeOnly(13, 15, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 24, From = new TimeOnly(13, 30, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 25, From = new TimeOnly(13, 45, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 26, From = new TimeOnly(14,  0, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 27, From = new TimeOnly(14, 15, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 28, From = new TimeOnly(14, 30, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 29, From = new TimeOnly(14, 45, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 30, From = new TimeOnly(15,  0, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 31, From = new TimeOnly(15, 15, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 32, From = new TimeOnly(15, 30, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 33, From = new TimeOnly(15, 45, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 34, From = new TimeOnly(16,  0, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 35, From = new TimeOnly(16, 15, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 36, From = new TimeOnly(16, 30, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 37, From = new TimeOnly(16, 45, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 38, From = new TimeOnly(17,  0, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 39, From = new TimeOnly(17, 15, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 40, From = new TimeOnly(17, 30, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 41, From = new TimeOnly(17, 45, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 42, From = new TimeOnly(18,  0, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 43, From = new TimeOnly(18, 15, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 44, From = new TimeOnly(18, 30, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 45, From = new TimeOnly(18, 45, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 46, From = new TimeOnly(19,  0, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 47, From = new TimeOnly(19, 15, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 48, From = new TimeOnly(19, 30, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 49, From = new TimeOnly(19, 45, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 50, From = new TimeOnly(20,  0, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 51, From = new TimeOnly(20, 15, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 52, From = new TimeOnly(20, 30, 0), Length = 15 * 60, Deleted = null },
            new() { Id = 53, From = new TimeOnly(20, 45, 0), Length = 15 * 60, Deleted = null }
        };

        // инициализация таблицы "ПРОМЕЖУТКИ_ВРЕМЕНИ"
        builder.HasData(slots);

        #endregion

    } // Configure

} // class SlotConfiguration