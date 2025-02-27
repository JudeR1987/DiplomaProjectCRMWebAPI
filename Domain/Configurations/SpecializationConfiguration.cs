using Domain.Models.Entities;
using Domain.Models.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations;

// конфигурация для сущности Specialization, задаётся атрибутом в классе сущности
public class SpecializationConfiguration : IEntityTypeConfiguration<Specialization>
{
    void IEntityTypeConfiguration<Specialization>
        .Configure(EntityTypeBuilder<Specialization> builder) {

        #region Задание ограничений полей таблицы "СПЕЦИАЛЬНОСТИ" при помощи Fluent API

        // настроить ограничение поля Name для Specialization:
        // задать ограничение максимальной длины строкового поля
        // наименования специальности
        // nvarchar(80) not null
        builder
            .Property(specialization => specialization.Name)
            .HasMaxLength(80)
            .IsRequired()
            .IsUnicode();

        // настроить ограничение поля Name для Specialization:
        // задать уникальность строкового поля наименования специальности
        builder
            .HasIndex(specialization => specialization.Name)
            .IsUnique();

        #endregion


        #region Задание отношений между таблицами при помощи Fluent API

        #endregion


        #region Инициализация таблицы "СПЕЦИАЛЬНОСТИ"

        var specializations = new List<Specialization>{
            new() { Id =  1, Name = "мастер маникюра", Deleted = null },
            new() { Id =  2, Name = "мастер бровист",  Deleted = null },
            new() { Id =  3, Name = "парикмахер",      Deleted = null },
            new() { Id =  4, Name = "визажист",        Deleted = Utils.GetRandomDateTime() },
            new() { Id =  5, Name = "колорист",        Deleted = null }
        };

        // инициализация таблицы "СПЕЦИАЛЬНОСТИ"
        builder.HasData(specializations);

        #endregion

    } // Configure

} // class SpecializationConfiguration