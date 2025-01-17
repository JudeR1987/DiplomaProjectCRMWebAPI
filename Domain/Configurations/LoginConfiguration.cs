using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models.Entities;

namespace Domain.Configurations;

// конфигурация для сущности Login, задаётся атрибутом в классе сущности
public class LoginConfiguration : IEntityTypeConfiguration<Login>
{
    void IEntityTypeConfiguration<Login>.Configure(EntityTypeBuilder<Login> builder)
    {

        #region Задание ограничений полей таблицы "ЛОГИНЫ" при помощи Fluent API

        // настроить ограничение поля LoginName для Login:
        // задать ограничение максимальной длины строкового поля
        // значения логина пользователя
        // nvarchar(50) not null
        builder
            .Property(login => login.LoginName)
            .HasMaxLength(50)
            .IsRequired()
            .IsUnicode();

        // настроить ограничение поля LoginName для Login:
        // задать уникальность строкового поля логина пользователя
        builder
            .HasIndex(login => login.LoginName)
            .IsUnique();

        // настроить ограничение поля Password для Login:
        // задать ограничение максимальной длины строкового поля пароля пользователя
        // nvarchar(50) not null
        builder
            .Property(login => login.Password)
            .HasMaxLength(50)
            .IsRequired()
            .IsUnicode();

        #endregion


        #region Задание отношений между таблицами при помощи Fluent API

        // Настройка отношения "многие к одному"

        // связь с таблицей "ПОЛЬЗОВАТЕЛИ"
        //builder
        //    .HasOne(login => login.User)
        //    .WithMany(user => user.Logins)
        //    .HasForeignKey(login => login.UserId);

        // связь с таблицей "РОЛИ"
        //builder
        //    .HasOne(login => login.Role)
        //    .WithMany(role => role.Logins)
        //    .HasForeignKey(login => login.RoleId);

        #endregion


        #region Инициализация таблицы "ЛОГИНЫ"

        var logins = new List<Login> {
            new() { Id =  1, UserId = 1, LoginName = "2332123456",         Password = "2332123456", RoleId = 1 },
            new() { Id =  2, UserId = 1, LoginName = "diaspora@inbox.ru",  Password = "2332123456", RoleId = 3 },
            new() { Id =  3, UserId = 1, LoginName = "123",                Password = "2332123456", RoleId = 4 },
            new() { Id =  4, UserId = 2, LoginName = "2252546231",         Password = "2252546231", RoleId = 3 },
            new() { Id =  5, UserId = 3, LoginName = "4251635241",         Password = "4251635241", RoleId = 4 },
            new() { Id =  6, UserId = 4, LoginName = "8990123987",         Password = "8990123987", RoleId = 6 },
            new() { Id =  7, UserId = 5, LoginName = "9090159951",         Password = "9090159951", RoleId = 6 },
            new() { Id =  8, UserId = 6, LoginName = "capacity@yandex.ru", Password = "1459024075", RoleId = 6 }
        };

        // инициализация таблицы "ЛОГИНЫ"
        //builder.HasData(logins);

        #endregion

    } // Configure

} // class LoginConfiguration