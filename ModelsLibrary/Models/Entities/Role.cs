using Microsoft.EntityFrameworkCore;
using ModelsLibrary.Configurations;

namespace ModelsLibrary.Models.Entities;

// сущность для таблицы "РОЛИ" (Roles)

// Атрибут задания класса конфигурирования сущности
[EntityTypeConfiguration(typeof(RoleConfiguration))]
public class Role(string name)
{
    // первичный ключ - идентификатор роли пользователя
    public int Id { get; set; }


    // наименование роли пользователя
    public string Name { get; set; } = name;


    // навигационные свойства для связи "многие ко многим" Roles <--> Users
    // связь через таблицу "ПОЛЬЗОВАТЕЛИ_РОЛИ"(UsersRoles)

    // связное свойство для таблицы "ПОЛЬЗОВАТЕЛИ_РОЛИ", связь 1:M
    // (одна роль может использоваться во многих связях)
    // public virtual List<UsersRoles> UsersRoles { get; set; } = [];

    // связное свойство для таблицы "ПОЛЬЗОВАТЕЛИ", связь M:M
    // (многие роли могут быть у множества пользователей)
    // public virtual List<User> Users { get; set; } = [];


    // связное свойство для таблицы "ЛОГИНЫ", связь 1:M
    // (одна роль может быть во многих учётных записях)
    public virtual List<Login> Logins { get; set; } = [];


    // конструктор по умолчанию
    public Role() : this("") {
    } // Role

} // class Role