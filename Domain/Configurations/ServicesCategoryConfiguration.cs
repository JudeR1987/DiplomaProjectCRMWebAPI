using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models.Entities;

namespace Domain.Configurations;

// конфигурация для сущности ServicesCategory,
// задаётся атрибутом в классе сущности
public class ServicesCategoryConfiguration : IEntityTypeConfiguration<ServicesCategory>
{
    void IEntityTypeConfiguration<ServicesCategory>
        .Configure(EntityTypeBuilder<ServicesCategory> builder) {

        #region Задание ограничений полей таблицы "КАТЕГОРИИ_УСЛУГ" при помощи Fluent API

        // настроить ограничение поля Name для ServicesCategory:
        // задать ограничение максимальной длины строкового поля
        // наименования категории услуг
        // nvarchar(50) not null
        builder
            .Property(servicesCategory => servicesCategory.Name)
            .HasMaxLength(50)
            .IsRequired()
            .IsUnicode();

        #endregion


        #region Задание отношений между таблицами при помощи Fluent API

        #endregion


        #region Инициализация таблицы "КАТЕГОРИИ_УСЛУГ"

        var servicesCategories = new List<ServicesCategory> {
            new() { Id = 1, Name = "Стрижки, укладки",          Deleted = null },
            new() { Id = 2, Name = "Окрашивание волос",         Deleted = null },
            new() { Id = 3, Name = "Уход за волосами",          Deleted = null },
            new() { Id = 4, Name = "Маникюр",                   Deleted = null },
            new() { Id = 5, Name = "Педикюр",                   Deleted = null },
            new() { Id = 6, Name = "Брови, ресницы, макияж",    Deleted = null },
            new() { Id = 7, Name = "Перманентный макияж",       Deleted = null },
            new() { Id = 8, Name = "Макияж",                    Deleted = null },
            new() { Id = 9, Name = "Эстетическая косметология", Deleted = null }
        };

        // инициализация таблицы "КАТЕГОРИИ_УСЛУГ"
        builder.HasData(servicesCategories);

        #endregion

    } // Configure

} // class ServicesCategoryConfiguration