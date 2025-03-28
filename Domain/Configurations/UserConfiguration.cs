﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models.Entities;
using Domain.Models.Infrastructure;

namespace Domain.Configurations;

// конфигурация для сущности User, задаётся атрибутом в классе сущности
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    void IEntityTypeConfiguration<User>.Configure(EntityTypeBuilder<User> builder) {

        #region Задание ограничений полей таблицы "ПОЛЬЗОВАТЕЛИ" при помощи Fluent API

        // настроить ограничение поля UserName для User:
        // задать ограничение максимальной длины строкового поля имени пользователя
        // nvarchar(50) not null
        builder
            .Property(user => user.UserName)
            .HasMaxLength(50)
            .IsRequired()
            .IsUnicode();

        // настроить ограничение поля Login для User:
        // задать ограничение максимальной длины строкового поля логина пользователя
        // nvarchar(50) not null
        /*builder
            .Property(user => user.Login)
            .HasMaxLength(50)
            .IsRequired()
            .IsUnicode();*/

        // настроить ограничение поля Login для User:
        // задать уникальность строкового поля логина пользователя
        /*builder
            .HasIndex(user => user.Login)
            .IsUnique();*/

        // настроить ограничение поля Phone для User:
        // задать ограничение максимальной длины строкового поля телефона пользователя
        // nvarchar(12) not null
        builder
            .Property(user => user.Phone)
            .HasMaxLength(12)
            .IsRequired()
            .IsUnicode();

        // настроить ограничение поля Phone для User:
        // задать уникальность строкового поля телефона пользователя
        /*builder
            .HasIndex(user => user.Phone)
            .IsUnique();*/

        // настроить ограничение поля Email для User:
        // задать ограничение максимальной длины строкового поля
        // адреса электронной почты пользователя
        // nvarchar(50) not null
        builder
            .Property(user => user.Email)
            .HasMaxLength(50)
            .IsRequired()
            .IsUnicode();

        // настроить ограничение поля Email для User:
        // задать уникальность строкового поля электронной почты пользователя
        /*builder
            .HasIndex(user => user.Email)
            .IsUnique();*/

        // настроить ограничение поля Password для User:
        // задать ограничение минимальной длины строкового поля пароля пользователя
        builder
            .ToTable(user => user
            .HasCheckConstraint("Password", "LEN(Password) > 3"));

        // настроить ограничение поля Password для User:
        // задать ограничение максимальной длины строкового поля пароля пользователя
        // nvarchar(50) not null
        builder
            .Property(user => user.Password)
            .HasMaxLength(50)
            .IsRequired()
            .IsUnicode();

        // настроить ограничение поля UserToken для User:
        // задать ограничение максимальной длины строкового поля
        // пользовательского токена
        // nvarchar(300) not null
        builder
            .Property(user => user.UserToken)
            .HasMaxLength(300)
            .IsRequired()
            .IsUnicode();

        #endregion


        #region Задание отношений между таблицами при помощи Fluent API

        // Настройка отношения "многие ко многим"
        // Users <-UserRole-> Roles
        builder
            .HasMany(user => user.Roles)
            .WithMany(role => role.Users)
            .UsingEntity<UserRole>(
                userRole => userRole
                    .HasOne(uR => uR.Role)
                    .WithMany(role => role.UsersRoles)
                    .HasForeignKey(uR => uR.RoleId),
                userRole => userRole
                    .HasOne(uR => uR.User)
                    .WithMany(user => user.UsersRoles)
                    .HasForeignKey(uR => uR.UserId)
            );

        #endregion


        #region Инициализация таблицы "ПОЛЬЗОВАТЕЛИ"

        var users = new List<User> {
            new() { Id = 1, UserName = "Андрей 12",   /*Login = "+72332123456", */Phone = "+72332123456", Email = "diaspora@inbox.ru",   Password = "+72332123456", Avatar = "http://localhost:5297/download/getimage/users/photos/user001_31710.jpg", UserToken = Utils.GetRandomString(), IsLogin = false, Deleted = null },
            new() { Id = 2, UserName = "Вераника 23", /*Login = "+72252546231", */Phone = "+72252546231", Email = "pilka@bk.ru",         Password = "+72252546231", Avatar = "http://localhost:5297/download/getimage/users/photos/user002_5fed0.jpg", UserToken = Utils.GetRandomString(), IsLogin = false, Deleted = null },
            new() { Id = 3, UserName = "Иван 34",     /*Login = "+74251635241", */Phone = "+74251635241", Email = "holiday@hotmail.com", Password = "+74251635241", Avatar = "http://localhost:5297/download/getimage/users/photos/user003_56c80.jpg", UserToken = Utils.GetRandomString(), IsLogin = false, Deleted = null },
            new() { Id = 4, UserName = "Татьяна 45",  /*Login = "+78990123987", */Phone = "+78990123987", Email = "astrology@live.com",  Password = "+78990123987", Avatar = "http://localhost:5297/download/getimage/users/photos/user004_40af0.jpg", UserToken = Utils.GetRandomString(), IsLogin = false, Deleted = null },
            new() { Id = 5, UserName = "Вадим 56",    /*Login = "+79090159951", */Phone = "+79090159951", Email = "kabala@furmail.ru",   Password = "+79090159951", Avatar = "http://localhost:5297/download/getimage/users/photos/user005_d7810.jpg", UserToken = Utils.GetRandomString(), IsLogin = false, Deleted = null },
            new() { Id = 6, UserName = "Надежда 67",  /*Login = "+71459024075", */Phone = "+71459024075", Email = "capacity@yandex.ru",  Password = "+71459024075", Avatar = "http://localhost:5297/download/getimage/users/photos/user006_86940.jpg", UserToken = Utils.GetRandomString(), IsLogin = false, Deleted = Utils.GetRandomDateTime() },
            new() { Id = 7, UserName = "Валерия 78",  /*Login = "+72113391752", */Phone = "+72113391752", Email = "big@hotmail.com",     Password = "+72113391752", Avatar = "http://localhost:5297/download/getimage/users/photos/user007_24e30.jpg", UserToken = Utils.GetRandomString(), IsLogin = false, Deleted = Utils.GetRandomDateTime() },
            new() { Id = 8, UserName = "Иван 99",     /*Login = "+79004005566", */Phone = "+79004005566", Email = "big99@hotmail.com",   Password = "+79004005566", Avatar = "http://localhost:5297/download/getimage/users/photos/user008_1cd1.jpg",  UserToken = Utils.GetRandomString(), IsLogin = false, Deleted = Utils.GetRandomDateTime() }
        };

        // инициализация таблицы "ПОЛЬЗОВАТЕЛИ"
        builder.HasData(users);

        #endregion

    } // Configure

} // class UserConfiguration