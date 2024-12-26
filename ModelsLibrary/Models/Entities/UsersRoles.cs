using Microsoft.EntityFrameworkCore;
using ModelsLibrary.Configurations;

namespace ModelsLibrary.Models.Entities;

// сущность для таблицы "ПОЛЬЗОВАТЕЛИ_РОЛИ"(UsersRoles)
// связь "многие ко многим" между таблицами
// "ПОЛЬЗОВАТЕЛИ" (Users) и "РОЛИ" (Roles)

// Атрибут задания класса конфигурирования сущности
//[EntityTypeConfiguration(typeof(UsersRolesConfiguration))]
public class UsersRoles(int userId, int roleId)
{
    // первичный ключ - идентификатор связи
    public int Id { get; set; }


    // данные о пользователе
    // свойство внешнего ключа
    public int UserId { get; set; } = userId;

    // связное свойство для таблицы "ПОЛЬЗОВАТЕЛИ", связь М:1
    // (во многих связях может быть только один пользователь)
    //public virtual User User { get; set; } = null!;


    // данные о роли
    // свойство внешнего ключа
    public int RoleId { get; set; } = roleId;

    // связное свойство для таблицы "РОЛИ", связь М:1
    // (во многих связях может быть выбрана только одна роль)
    //public virtual Role Role { get; set; } = null!;


    // конструктор по умолчанию
    public UsersRoles() : this(0, 0) {
    } // UsersRoles

} // class UsersRoles