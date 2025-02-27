using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models.Entities;
using Domain.Models.Infrastructure;

namespace Domain.Configurations;

// конфигурация для сущности RecordService, задаётся атрибутом в классе сущности
public class RecordServiceConfiguration : IEntityTypeConfiguration<RecordService>
{
    void IEntityTypeConfiguration<RecordService>.Configure(EntityTypeBuilder<RecordService> builder) {

        #region Задание ограничений полей таблицы "ЗАПИСИ_УСЛУГИ" при помощи Fluent API

        // настроить SQL-ограничение поля Amount для RecordService:
        // задать ограничение минимального значения количества оказанных услуг
        builder
            .ToTable(recordService => recordService
            .HasCheckConstraint("Amount", "Amount >= 0"));

        // настроить SQL-ограничение поля Price для RecordService:
        // задать ограничение минимального значения цены на данную услугу
        builder
            .ToTable(recordService => recordService
            .HasCheckConstraint("Price", "Price >= 0"));

        // настроить SQL-ограничение поля Discount для RecordService:
        // задать ограничения минимального и максимального значений скидки на данную услугу
        builder
            .ToTable(recordService => recordService
            .HasCheckConstraint("Discount", "Discount between 0 and 100"));

        #endregion


        #region Задание отношений между таблицами при помощи Fluent API

        #endregion


        #region Инициализация таблицы "ЗАПИСИ_УСЛУГИ"

        var recordsServices = new List<RecordService> {
            new() { Id = 1, RecordId = 1, ServiceId = 21, Amount = 1, Price = 2_700, Discount =   0, Deleted = null },
            new() { Id = 2, RecordId = 1, ServiceId = 22, Amount = 1, Price = 3_100, Discount =   0, Deleted = null },
            new() { Id = 3, RecordId = 2, ServiceId =  1, Amount = 2, Price = 2_000, Discount = 10d, Deleted = null },
            new() { Id = 4, RecordId = 3, ServiceId =  3, Amount = 1, Price = 2_800, Discount =   0, Deleted = null },
            new() { Id = 5, RecordId = 3, ServiceId =  4, Amount = 1, Price = 1_000, Discount =   0, Deleted = null },
            new() { Id = 6, RecordId = 4, ServiceId = 22, Amount = 1, Price = 3_100, Discount =   0, Deleted = null },
            new() { Id = 7, RecordId = 4, ServiceId = 23, Amount = 1, Price = 2_100, Discount =   0, Deleted = null },
            new() { Id = 8, RecordId = 5, ServiceId = 22, Amount = 1, Price = 3_100, Discount =   0, Deleted = Utils.GetRandomDateTime() }
        };

        // инициализация таблицы "ЗАПИСИ_УСЛУГИ"
        builder.HasData(recordsServices);

        #endregion

    } // Configure

} // class RecordServiceConfiguration