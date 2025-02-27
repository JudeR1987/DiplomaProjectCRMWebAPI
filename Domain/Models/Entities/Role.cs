using Microsoft.EntityFrameworkCore;
using Domain.Configurations;
using Domain.Models.Dto;

namespace Domain.Models.Entities;

// сущность для таблицы "РОЛИ" (Roles)

// Атрибут задания класса конфигурирования сущности
[EntityTypeConfiguration(typeof(RoleConfiguration))]
public class Role(string name, DateTime? deleted)
{
    // первичный ключ - идентификатор роли пользователя
    public int Id { get; set; }


    // наименование роли пользователя
    public string Name { get; set; } = name;

    // дата и время удаления записи о роли пользователя
    public DateTime? Deleted { get; set; } = deleted;


    // навигационные свойства для связи "многие ко многим" Roles <--> Users
    // связь через таблицу "ПОЛЬЗОВАТЕЛИ_РОЛИ"(UserRole)

    // связное свойство для таблицы "ПОЛЬЗОВАТЕЛИ_РОЛИ", связь 1:M
    // (одна роль может использоваться во многих связях)
    public virtual List<UserRole> UsersRoles { get; set; } = [];

    // связное свойство для таблицы "ПОЛЬЗОВАТЕЛИ", связь M:M
    // (многие роли могут быть у множества пользователей)
    public virtual List<User> Users { get; set; } = [];


    // конструктор по умолчанию
    public Role() : this("", null) {
    } // Role


    // статический метод, возвращающий новый объект-копию
    public static Role NewRole(Role srcRole) =>
        new(srcRole.Name,
            srcRole.Deleted) {
            Id = srcRole.Id,
            Users = srcRole.Users,
            UsersRoles = srcRole.UsersRoles
        };


    // статический метод, возвращающий объект-DTO
    public static RoleDto RoleToDto(Role srcRole) =>
        new(srcRole.Id,
            srcRole.Name,
            srcRole.Deleted
        );


    // статический метод, возвращающий список объектов-DTO
    public static List<RoleDto> RolesToDto(List<Role> srcRoles) =>
        srcRoles.Select(RoleToDto).ToList();

} // class Role