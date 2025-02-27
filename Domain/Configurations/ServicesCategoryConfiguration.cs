using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models.Entities;
using Domain.Models.Infrastructure;

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

        // настроить SQL-ограничение поля Weight для ServicesCategory:
        // задать ограничение минимального значения веса категории для сортировки
        /*builder
            .ToTable(servicesCategory =>
            servicesCategory.HasCheckConstraint("Weight", "Weight >= 0"));*/

        #endregion


        #region Задание отношений между таблицами при помощи Fluent API

        #endregion


        #region Инициализация таблицы "КАТЕГОРИИ_УСЛУГ"

        var servicesCategories = new List<ServicesCategory> {
            new() { Id = 1, Name = "Стрижки, укладки",          Deleted = null/*, Weight = 2*/ },
            new() { Id = 2, Name = "Окрашивание волос",         Deleted = null/*, Weight = 4*/ },
            new() { Id = 3, Name = "Уход за волосами",          Deleted = null/*, Weight = 8*/ },
            new() { Id = 4, Name = "Маникюр",                   Deleted = null/*, Weight = 5*/ },
            new() { Id = 5, Name = "Педикюр",                   Deleted = null/*, Weight = 1*/ },
            new() { Id = 6, Name = "Брови, ресницы, макияж",    Deleted = null/*, Weight = 3*/ },
            new() { Id = 7, Name = "Перманентный макияж",       Deleted = null/*, Weight = 7*/ },
            new() { Id = 8, Name = "Макияж",                    Deleted = Utils.GetRandomDateTime()/*, Weight = 7*/ },
            new() { Id = 9, Name = "Эстетическая косметология", Deleted = null/*, Weight = 6*/ }
        };

        // инициализация таблицы "КАТЕГОРИИ_УСЛУГ"
        builder.HasData(servicesCategories);

        #endregion

    } // Configure

} // class ServicesCategoryConfiguration