using Microsoft.EntityFrameworkCore;
using Domain.Configurations;

namespace Domain.Models.Entities;

// сущность для таблицы "ЛОГИНЫ" (Logins)

// Атрибут задания класса конфигурирования сущности
//[EntityTypeConfiguration(typeof(LoginConfiguration))]
public class Login(int userId, string loginName, string password, int roleId)
{
    // первичный ключ - идентификатор записи о логине пользователя
    public int Id { get; set; }


    // идентификатор пользователя
    // свойство внешнего ключа
    public int UserId { get; set; } = userId;

    // связное свойство для таблицы "ПОЛЬЗОВАТЕЛИ", связь М:1
    // (у многих логинов может быть только один пользователь)
    public virtual User User { get; set; } = null!;


    // значение логина пользователя
    public string LoginName { get; set; } = loginName;


    // пароль данной учётной записи пользователя
    public string Password { get; set; } = password;


    // идентификатор роли пользователя
    // свойство внешнего ключа
    public int RoleId { get; set; } = roleId;

    // связное свойство для таблицы "РОЛИ", связь М:1
    // (у многих логинов может быть только одна роль пользователя)
    public virtual Role Role { get; set; } = null!;


    // конструктор по умолчанию
    public Login() : this(0, "", "", 0) {
    } // Login

} // class Login