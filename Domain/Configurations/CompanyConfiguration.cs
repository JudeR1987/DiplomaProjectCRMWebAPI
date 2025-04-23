using Domain.Models.Entities;
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

        // настроить ограничение поля Schedule для Company:
        // задать ограничение максимальной длины строкового поля графика работы компании
        // nvarchar(50) not null
        builder
            .Property(company => company.Schedule)
            .HasMaxLength(50)
            .IsRequired()
            .IsUnicode();

        // настроить ограничение поля Site для Company:
        // задать ограничение максимальной длины строкового поля сайта компании
        // nvarchar(30) not null
        builder
            .Property(company => company.Site)
            .HasMaxLength(30)
            .IsRequired()
            .IsUnicode();

        #endregion


        #region Задание отношений между таблицами при помощи Fluent API

        // Настройка отношения "многие к одному"

        // связь с таблицей "ПОЛЬЗОВАТЕЛИ"
        builder
            .HasOne(company => company.UserOwner)
            .WithMany(user => user.Companies)
            .HasForeignKey(company => company.UserOwnerId);


        // Настройка отношения "многие к одному"

        // связь с таблицей "АДРЕСА"
        builder
            .HasOne(company => company.Address)
            .WithMany(address => address.Companies)
            .HasForeignKey(company => company.AddressId);

        #endregion


        #region Инициализация таблицы "КОМПАНИИ"

        var companies = new List<Company>{
            new() { Id = 1, UserOwnerId = 1, Name = "Красотка", AddressId = 12, Phone = "+79998887766", Description = "Описание 1", Logo = "http://localhost:5297/download/getimage/companies/logos/logo001_121.png", TitleImage = "http://localhost:5297/download/getimage/companies/images/company001_121.jpg", Schedule = "Пн-Вс: 9:00-21:00",  Site = "https://www.donstep.com", Deleted = null },
            new() { Id = 2, UserOwnerId = 1, Name = "Ноготок",  AddressId = 16, Phone = "+79998887755", Description = "Описание 2", Logo = "http://localhost:5297/download/getimage/companies/logos/logo002_122.png", TitleImage = "http://localhost:5297/download/getimage/companies/images/company002_122.jpg", Schedule = "Пн-Вс: 8:00-20:00",  Site = "https://www.donstep.com", Deleted = null },
            new() { Id = 3, UserOwnerId = 2, Name = "БЭБИ 21",  AddressId = 22, Phone = "+79998887744", Description = "Описание 3", Logo = "http://localhost:5297/download/getimage/companies/logos/logo003_123.png", TitleImage = "http://localhost:5297/download/getimage/companies/images/company003_123.jpg", Schedule = "Пн-Вс: 10:00-22:00", Site = "https://www.donstep.com", Deleted = null },
            new() { Id = 4, UserOwnerId = 2, Name = "Крис",     AddressId = 27, Phone = "+79998887733", Description = "Описание 4", Logo = "http://localhost:5297/download/getimage/companies/logos/logo004_124.png", TitleImage = "http://localhost:5297/download/getimage/companies/images/company004_124.jpg", Schedule = "Пн-Вс: 8:00-21:00",  Site = "https://www.donstep.com", Deleted = null }
        };

        // для тестов
        /*var companies = new List<Company>{
            new() { Id =  1, UserOwnerId = 1, Name = "Красотка", AddressId = 12, Phone = "+79998887766", Description = "Описание 1", Logo = "http://localhost:5297/download/getimage/companies/logos/company001_123.png", TitleImage = "http://localhost:5297/download/getimage/companies/images/company001_123.jpg", Schedule = "Пн-Вс: 9:00-21:00",  Site = "https://www.donstep.com", Deleted = null },
            new() { Id =  2, UserOwnerId = 1, Name = "Ноготок",  AddressId = 16, Phone = "+79998887755", Description = "Описание 2", Logo = "http://localhost:5297/download/getimage/companies/logos/company001_124.png", TitleImage = "http://localhost:5297/download/getimage/companies/images/company001_124.jpg", Schedule = "Пн-Вс: 8:00-20:00",  Site = "https://www.donstep.com", Deleted = null },
            new() { Id =  3, UserOwnerId = 2, Name = "БЭБИ 21",  AddressId = 22, Phone = "+79998887744", Description = "Описание 3", Logo = "http://localhost:5297/download/getimage/companies/logos/company001_125.png", TitleImage = "http://localhost:5297/download/getimage/companies/images/company001_125.jpg", Schedule = "Пн-Вс: 10:00-22:00", Site = "https://www.donstep.com", Deleted = null },
            new() { Id =  4, UserOwnerId = 2, Name = "БЭБИ 21",  AddressId = 22, Phone = "+79998887744", Description = "Описание 3", Logo = "http://localhost:5297/download/getimage/companies/logos/company001_125.png", TitleImage = "http://localhost:5297/download/getimage/companies/images/company001_125.jpg", Schedule = "Пн-Вс: 10:00-22:00", Site = "https://www.donstep.com", Deleted = null },
            new() { Id =  5, UserOwnerId = 2, Name = "БЭБИ 21",  AddressId = 22, Phone = "+79998887744", Description = "Описание 3", Logo = "http://localhost:5297/download/getimage/companies/logos/company001_125.png", TitleImage = "http://localhost:5297/download/getimage/companies/images/company001_125.jpg", Schedule = "Пн-Вс: 10:00-22:00", Site = "https://www.donstep.com", Deleted = null },
            new() { Id =  6, UserOwnerId = 2, Name = "БЭБИ 21",  AddressId = 22, Phone = "+79998887744", Description = "Описание 3", Logo = "http://localhost:5297/download/getimage/companies/logos/company001_125.png", TitleImage = "http://localhost:5297/download/getimage/companies/images/company001_125.jpg", Schedule = "Пн-Вс: 10:00-22:00", Site = "https://www.donstep.com", Deleted = null },
            new() { Id =  7, UserOwnerId = 2, Name = "БЭБИ 21",  AddressId = 22, Phone = "+79998887744", Description = "Описание 3", Logo = "http://localhost:5297/download/getimage/companies/logos/company001_125.png", TitleImage = "http://localhost:5297/download/getimage/companies/images/company001_125.jpg", Schedule = "Пн-Вс: 10:00-22:00", Site = "https://www.donstep.com", Deleted = null },
            new() { Id =  8, UserOwnerId = 2, Name = "БЭБИ 21",  AddressId = 22, Phone = "+79998887744", Description = "Описание 3", Logo = "http://localhost:5297/download/getimage/companies/logos/company001_125.png", TitleImage = "http://localhost:5297/download/getimage/companies/images/company001_125.jpg", Schedule = "Пн-Вс: 10:00-22:00", Site = "https://www.donstep.com", Deleted = null },
            new() { Id =  9, UserOwnerId = 2, Name = "БЭБИ 21",  AddressId = 22, Phone = "+79998887744", Description = "Описание 3", Logo = "http://localhost:5297/download/getimage/companies/logos/company001_125.png", TitleImage = "http://localhost:5297/download/getimage/companies/images/company001_125.jpg", Schedule = "Пн-Вс: 10:00-22:00", Site = "https://www.donstep.com", Deleted = null },
            new() { Id = 10, UserOwnerId = 2, Name = "БЭБИ 21",  AddressId = 22, Phone = "+79998887744", Description = "Описание 3", Logo = "http://localhost:5297/download/getimage/companies/logos/company001_125.png", TitleImage = "http://localhost:5297/download/getimage/companies/images/company001_125.jpg", Schedule = "Пн-Вс: 10:00-22:00", Site = "https://www.donstep.com", Deleted = null },
            new() { Id = 11, UserOwnerId = 2, Name = "БЭБИ 21",  AddressId = 22, Phone = "+79998887744", Description = "Описание 3", Logo = "http://localhost:5297/download/getimage/companies/logos/company001_125.png", TitleImage = "http://localhost:5297/download/getimage/companies/images/company001_125.jpg", Schedule = "Пн-Вс: 10:00-22:00", Site = "https://www.donstep.com", Deleted = null },
            new() { Id = 12, UserOwnerId = 2, Name = "БЭБИ 21",  AddressId = 22, Phone = "+79998887744", Description = "Описание 3", Logo = "http://localhost:5297/download/getimage/companies/logos/company001_125.png", TitleImage = "http://localhost:5297/download/getimage/companies/images/company001_125.jpg", Schedule = "Пн-Вс: 10:00-22:00", Site = "https://www.donstep.com", Deleted = null },
            new() { Id = 13, UserOwnerId = 2, Name = "БЭБИ 21",  AddressId = 22, Phone = "+79998887744", Description = "Описание 3", Logo = "http://localhost:5297/download/getimage/companies/logos/company001_125.png", TitleImage = "http://localhost:5297/download/getimage/companies/images/company001_125.jpg", Schedule = "Пн-Вс: 10:00-22:00", Site = "https://www.donstep.com", Deleted = null },
            new() { Id = 14, UserOwnerId = 2, Name = "БЭБИ 21",  AddressId = 22, Phone = "+79998887744", Description = "Описание 3", Logo = "http://localhost:5297/download/getimage/companies/logos/company001_125.png", TitleImage = "http://localhost:5297/download/getimage/companies/images/company001_125.jpg", Schedule = "Пн-Вс: 10:00-22:00", Site = "https://www.donstep.com", Deleted = null },
            new() { Id = 15, UserOwnerId = 2, Name = "БЭБИ 21",  AddressId = 22, Phone = "+79998887744", Description = "Описание 3", Logo = "http://localhost:5297/download/getimage/companies/logos/company001_125.png", TitleImage = "http://localhost:5297/download/getimage/companies/images/company001_125.jpg", Schedule = "Пн-Вс: 10:00-22:00", Site = "https://www.donstep.com", Deleted = null },
            new() { Id = 16, UserOwnerId = 2, Name = "БЭБИ 21",  AddressId = 22, Phone = "+79998887744", Description = "Описание 3", Logo = "http://localhost:5297/download/getimage/companies/logos/company001_125.png", TitleImage = "http://localhost:5297/download/getimage/companies/images/company001_125.jpg", Schedule = "Пн-Вс: 10:00-22:00", Site = "https://www.donstep.com", Deleted = null },
            new() { Id = 17, UserOwnerId = 2, Name = "БЭБИ 21",  AddressId = 22, Phone = "+79998887744", Description = "Описание 3", Logo = "http://localhost:5297/download/getimage/companies/logos/company001_125.png", TitleImage = "http://localhost:5297/download/getimage/companies/images/company001_125.jpg", Schedule = "Пн-Вс: 10:00-22:00", Site = "https://www.donstep.com", Deleted = null },
            new() { Id = 18, UserOwnerId = 2, Name = "БЭБИ 21",  AddressId = 22, Phone = "+79998887744", Description = "Описание 3", Logo = "http://localhost:5297/download/getimage/companies/logos/company001_125.png", TitleImage = "http://localhost:5297/download/getimage/companies/images/company001_125.jpg", Schedule = "Пн-Вс: 10:00-22:00", Site = "https://www.donstep.com", Deleted = null },
            new() { Id = 19, UserOwnerId = 2, Name = "БЭБИ 21",  AddressId = 22, Phone = "+79998887744", Description = "Описание 3", Logo = "http://localhost:5297/download/getimage/companies/logos/company001_125.png", TitleImage = "http://localhost:5297/download/getimage/companies/images/company001_125.jpg", Schedule = "Пн-Вс: 10:00-22:00", Site = "https://www.donstep.com", Deleted = null },
            new() { Id = 20, UserOwnerId = 2, Name = "БЭБИ 21",  AddressId = 22, Phone = "+79998887744", Description = "Описание 3", Logo = "http://localhost:5297/download/getimage/companies/logos/company001_125.png", TitleImage = "http://localhost:5297/download/getimage/companies/images/company001_125.jpg", Schedule = "Пн-Вс: 10:00-22:00", Site = "https://www.donstep.com", Deleted = null },
            new() { Id = 21, UserOwnerId = 2, Name = "БЭБИ 21",  AddressId = 22, Phone = "+79998887744", Description = "Описание 3", Logo = "http://localhost:5297/download/getimage/companies/logos/company001_125.png", TitleImage = "http://localhost:5297/download/getimage/companies/images/company001_125.jpg", Schedule = "Пн-Вс: 10:00-22:00", Site = "https://www.donstep.com", Deleted = null },
            new() { Id = 22, UserOwnerId = 2, Name = "БЭБИ 21",  AddressId = 22, Phone = "+79998887744", Description = "Описание 3", Logo = "http://localhost:5297/download/getimage/companies/logos/company001_125.png", TitleImage = "http://localhost:5297/download/getimage/companies/images/company001_125.jpg", Schedule = "Пн-Вс: 10:00-22:00", Site = "https://www.donstep.com", Deleted = null },
            new() { Id = 23, UserOwnerId = 2, Name = "БЭБИ 21",  AddressId = 22, Phone = "+79998887744", Description = "Описание 3", Logo = "http://localhost:5297/download/getimage/companies/logos/company001_125.png", TitleImage = "http://localhost:5297/download/getimage/companies/images/company001_125.jpg", Schedule = "Пн-Вс: 10:00-22:00", Site = "https://www.donstep.com", Deleted = null },
            new() { Id = 24, UserOwnerId = 2, Name = "БЭБИ 21",  AddressId = 22, Phone = "+79998887744", Description = "Описание 3", Logo = "http://localhost:5297/download/getimage/companies/logos/company001_125.png", TitleImage = "http://localhost:5297/download/getimage/companies/images/company001_125.jpg", Schedule = "Пн-Вс: 10:00-22:00", Site = "https://www.donstep.com", Deleted = null },
            new() { Id = 25, UserOwnerId = 2, Name = "БЭБИ 21",  AddressId = 22, Phone = "+79998887744", Description = "Описание 3", Logo = "http://localhost:5297/download/getimage/companies/logos/company001_125.png", TitleImage = "http://localhost:5297/download/getimage/companies/images/company001_125.jpg", Schedule = "Пн-Вс: 10:00-22:00", Site = "https://www.donstep.com", Deleted = null },
            new() { Id = 26, UserOwnerId = 2, Name = "БЭБИ 21",  AddressId = 22, Phone = "+79998887744", Description = "Описание 3", Logo = "http://localhost:5297/download/getimage/companies/logos/company001_125.png", TitleImage = "http://localhost:5297/download/getimage/companies/images/company001_125.jpg", Schedule = "Пн-Вс: 10:00-22:00", Site = "https://www.donstep.com", Deleted = null },
            new() { Id = 27, UserOwnerId = 2, Name = "Крис",     AddressId = 27, Phone = "+79998887733", Description = "Описание 4", Logo = "http://localhost:5297/download/getimage/companies/logos/company001_126.png", TitleImage = "http://localhost:5297/download/getimage/companies/images/company001_126.jpg", Schedule = "Пн-Вс: 8:00-21:00",  Site = "https://www.donstep.com", Deleted = Utils.GetRandomDateTime() }
        };*/

        // инициализация таблицы "КОМПАНИИ"
        builder.HasData(companies);

        #endregion

    } // Configure

} // class CompanyConfiguration