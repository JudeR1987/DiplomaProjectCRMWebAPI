using Microsoft.EntityFrameworkCore;
using Domain.Configurations;

namespace Domain.Models.Entities;

// сущность для таблицы "ПОЛЬЗОВАТЕЛИ" (Users)

// Атрибут задания класса конфигурирования сущности
[EntityTypeConfiguration(typeof(UserConfiguration))]
public class User(string userToken, string userName,
    string phone, string email, string avatar)
{
    // первичный ключ - идентификатор пользователя
    public int Id { get; set; }


    // пользовательский токен
    public string UserToken { get; set; } = userToken;


    // имя пользователя
    public string UserName { get; set; } = userName;


    // номер телефона пользователя
    public string Phone { get; set; } = phone;


    // адрес электронной почты пользователя
    public string Email { get; set; } = email;


    // путь к файлу аватарки пользователя
    public string Avatar { get; set; } = avatar;


    // навигационные свойства для связи "многие ко многим" Users <--> Roles
    // связь через таблицу "ПОЛЬЗОВАТЕЛИ_РОЛИ"(UsersRoles)

    // связное свойство для таблицы "ПОЛЬЗОВАТЕЛИ_РОЛИ", связь 1:M
    // (один пользователь может использоваться во многих связях)
    //public virtual List<UsersRoles> UsersRoles { get; set; } = [];

    // связное свойство для таблицы "РОЛИ", связь M:M
    // (многие пользователи могут иметь множество ролей)
    //public virtual List<Role> Roles { get; set; } = [];


    // связное свойство для таблицы "ЛОГИНЫ", связь 1:M
    // (один пользователь может использовать множество учётных записей)
    public virtual List<Login> Logins { get; set; } = [];


    // конструктор по умолчанию
    public User() : this("", "", "", "", "") {
    } // User

} // class User