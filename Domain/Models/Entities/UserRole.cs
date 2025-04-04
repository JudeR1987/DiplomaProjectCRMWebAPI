﻿using Microsoft.EntityFrameworkCore;
using Domain.Configurations;

namespace Domain.Models.Entities;

// сущность для таблицы "ПОЛЬЗОВАТЕЛИ_РОЛИ"(UserRole)
// связь "многие ко многим" между таблицами
// "ПОЛЬЗОВАТЕЛИ" (Users) и "РОЛИ" (Roles)

// Атрибут задания класса конфигурирования сущности
[EntityTypeConfiguration(typeof(UserRoleConfiguration))]
public class UserRole(int userId, int roleId)
{
    // первичный ключ - идентификатор связи
    public int Id { get; set; }


    // данные о пользователе
    // свойство внешнего ключа
    public int UserId { get; set; } = userId;

    // связное свойство для таблицы "ПОЛЬЗОВАТЕЛИ", связь М:1
    // (во многих связях может быть только один пользователь)
    public virtual User User { get; set; } = null!;


    // данные о роли
    // свойство внешнего ключа
    public int RoleId { get; set; } = roleId;

    // связное свойство для таблицы "РОЛИ", связь М:1
    // (во многих связях может быть выбрана только одна роль)
    public virtual Role Role { get; set; } = null!;


    // конструктор по умолчанию
    public UserRole() : this(0, 0) {
    } // UserRole

} // class UserRole