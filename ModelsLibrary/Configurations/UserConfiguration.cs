using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModelsLibrary.Models.Entities;

namespace ModelsLibrary.Configurations;

// конфигурация для сущности User, задаётся атрибутом в классе сущности
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    void IEntityTypeConfiguration<User>.Configure(EntityTypeBuilder<User> builder) {

        #region Задание ограничений полей таблицы "ПОЛЬЗОВАТЕЛИ" при помощи Fluent API

        // настроить ограничение поля UserToken для User:
        // задать ограничение максимальной длины строкового поля пользовательского токена
        // nvarchar(300) not null
        builder
            .Property(user => user.UserToken)
            .HasMaxLength(300)
            .IsRequired()
            .IsUnicode();

        // настроить ограничение поля UserName для User:
        // задать ограничение максимальной длины строкового поля имени пользователя
        // nvarchar(50) not null
        builder
            .Property(user => user.UserName)
            .HasMaxLength(50)
            .IsRequired()
            .IsUnicode();

        // настроить ограничение поля Phone для User:
        // задать ограничение максимальной длины строкового поля телефона пользователя
        // nvarchar(12) not null
        builder
            .Property(user => user.Phone)
            .HasMaxLength(12)
            .IsRequired()
            .IsUnicode();

        // настроить ограничение поля Login для User:
        // задать ограничение максимальной длины строкового поля логина пользователя
        // nvarchar(50) not null
        //builder
        //    .Property(user => user.Login)
        //    .HasMaxLength(50)
        //    .IsRequired()
        //    .IsUnicode();

        // настроить ограничение поля Login для User:
        // задать уникальность строкового поля логина пользователя
        //builder
        //    .HasIndex(user => user.Login)
        //    .IsUnique();

        // настроить ограничение поля Email для User:
        // задать ограничение максимальной длины строкового поля
        // адреса электронной почты клиента
        // nvarchar(50) not null
        builder
            .Property(user => user.Email)
            .HasMaxLength(50)
            .IsRequired()
            .IsUnicode();

        #endregion


        #region Задание отношений между таблицами при помощи Fluent API

        // Настройка отношения "многие ко многим"
        // Users <-UsersRoles-> Roles
        /*builder
            .HasMany(user => user.Roles)
            .WithMany(role => role.Users)
            .UsingEntity<UsersRoles>(
                usersRoles => usersRoles
                    .HasOne(uR => uR.Role)
                    .WithMany(role => role.UsersRoles)
                    .HasForeignKey(uR => uR.RoleId),
                usersRoles => usersRoles
                    .HasOne(uR => uR.User)
                    .WithMany(user => user.UsersRoles)
                    .HasForeignKey(uR => uR.UserId)
            );*/

        #endregion


        #region Инициализация таблицы "ПОЛЬЗОВАТЕЛИ"

        var users = new List<User> {
            new() { Id = 1, UserToken = "123456", UserName = "Андрей 12",   Phone = "+72332123456", Email = "diaspora@inbox.ru",   Avatar = "http://localhost:4200/users/123456.jpg" },
            new() { Id = 2, UserToken = "123457", UserName = "Вераника 23", Phone = "+72252546231", Email = "pilka@bk.ru",         Avatar = "http://localhost:4200/users/123457.jpg" },
            new() { Id = 3, UserToken = "123458", UserName = "Иван 34",     Phone = "+74251635241", Email = "holiday@hotmail.com", Avatar = "http://localhost:4200/users/123458.jpg" },
            new() { Id = 4, UserToken = "123459", UserName = "Татьяна 45",  Phone = "+78990123987", Email = "astrology@live.com",  Avatar = "http://localhost:4200/users/123459.jpg" },
            new() { Id = 5, UserToken = "123460", UserName = "Вадим 56",    Phone = "+79090159951", Email = "kabala@furmail.ru",   Avatar = "http://localhost:4200/users/123460.jpg" },
            new() { Id = 6, UserToken = "123461", UserName = "Надежда 67",  Phone = "+71459024075", Email = "capacity@yandex.ru",  Avatar = "http://localhost:4200/users/123461.jpg" },
            new() { Id = 7, UserToken = "123462", UserName = "Валерия 78",  Phone = "+72113391752", Email = "big@hotmail.com",     Avatar = "http://localhost:4200/users/123462.jpg" }
        };

        // инициализация таблицы "ПОЛЬЗОВАТЕЛИ"
        builder.HasData(users);

        #endregion

    } // Configure

} // class UserConfiguration