using Domain.Models.Entities;
using Domain.Models.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations;

// конфигурация для сущности Company, задаётся атрибутом в классе сущности
public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    void IEntityTypeConfiguration<Company>.Configure(EntityTypeBuilder<Company> builder) {

        #region Задание ограничений полей таблицы "КОМПАНИИ" при помощи Fluent API

        // настроить ограничение поля Name для Company:
        // задать ограничение максимальной длины строкового поля названия компании
        // nvarchar(50) not null
        builder
            .Property(company => company.Name)
            .HasMaxLength(50)
            .IsRequired()
            .IsUnicode();

        // настроить ограничение поля Phone для Company:
        // задать ограничение максимальной длины строкового поля телефона компании
        // nvarchar(12) not null
        builder
            .Property(company => company.Phone)
            .HasMaxLength(12)
            .IsRequired()
            .IsUnicode();

        // настроить ограничение поля Description для Company:
        // задать ограничение максимальной длины строкового поля описания компании
        // nvarchar(500)
        builder
            .Property(company => company.Description)
            .HasMaxLength(500)
            .IsUnicode();

        #endregion


        #region Задание отношений между таблицами при помощи Fluent API

        // Настройка отношения "многие к одному"

        // связь с таблицей "ПОЛЬЗОВАТЕЛИ"
        builder
            .HasOne(company => company.OwnerUser)
            .WithMany(user => user.Companies)
            .HasForeignKey(company => company.OwnerUserId);


        // Настройка отношения "многие к одному"

        // связь с таблицей "АДРЕСА"
        builder
            .HasOne(company => company.Address)
            .WithMany(address => address.Companies)
            .HasForeignKey(company => company.AddressId);

        #endregion


        #region Инициализация таблицы "КОМПАНИИ"

        var companies = new List<Company>{
            new() { Id = 1, OwnerUserId = 1, Name = "Красотка", AddressId = 12, Phone = "+79998887766", Description = "Описание 1", Deleted = null },
            new() { Id = 2, OwnerUserId = 1, Name = "Ноготок",  AddressId = 16, Phone = "+79998887755", Description = "Описание 2", Deleted = null },
            new() { Id = 3, OwnerUserId = 2, Name = "БЭБИ 21",  AddressId = 22, Phone = "+79998887744", Description = "Описание 3", Deleted = null },
            new() { Id = 4, OwnerUserId = 2, Name = "Крис",     AddressId = 27, Phone = "+79998887733", Description = "Описание 4", Deleted = Utils.GetRandomDateTime() }
        };

        // инициализация таблицы "КОМПАНИИ"
        builder.HasData(companies);

        #endregion

    } // Configure

} // class CompanyConfiguration