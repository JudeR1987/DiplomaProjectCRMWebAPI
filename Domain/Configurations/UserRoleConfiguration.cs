﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models.Entities;

namespace Domain.Configurations;

// конфигурация для сущности UserRole, задаётся атрибутом в классе сущности
public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    void IEntityTypeConfiguration<UserRole>.Configure(EntityTypeBuilder<UserRole> builder) {

        #region Задание ограничений полей таблицы "ПОЛЬЗОВАТЕЛИ_РОЛИ" при помощи Fluent API

        #endregion


        #region Задание отношений между таблицами при помощи Fluent API

        #endregion


        #region Инициализация таблицы "ПОЛЬЗОВАТЕЛИ_РОЛИ"

        var usersRoles = new List<UserRole> {
            new() { Id = 1, UserId = 1, RoleId = 1 },
            new() { Id = 2, UserId = 1, RoleId = 3 },
            new() { Id = 3, UserId = 1, RoleId = 4 },
            new() { Id = 4, UserId = 2, RoleId = 3 },
            new() { Id = 5, UserId = 3, RoleId = 4 },
            new() { Id = 6, UserId = 4, RoleId = 6 },
            new() { Id = 7, UserId = 5, RoleId = 6 },
            new() { Id = 8, UserId = 6, RoleId = 6 }
        };

        // инициализация таблицы "ПОЛЬЗОВАТЕЛИ_РОЛИ"
        builder.HasData(usersRoles);

        #endregion

    } // Configure

} // class UserRoleConfiguration