using Microsoft.EntityFrameworkCore;
using Domain.Configurations;
using Domain.Models.Dto;

namespace Domain.Models.Entities;

// сущность для таблицы "ПОЛЬЗОВАТЕЛИ" (Users)

// Атрибут задания класса конфигурирования сущности
[EntityTypeConfiguration(typeof(UserConfiguration))]
public class User(string userName, string login, string phone, string email,
    string password, string avatar, string userToken, bool isLogin, DateTime? deleted)
{
    // первичный ключ - идентификатор пользователя
    public int Id { get; set; }


    // имя пользователя
    public string UserName { get; set; } = userName;


    // логин пользователя (логин=телефону)
    public string Login { get; set; } = login;


    // номер телефона пользователя
    public string Phone { get; set; } = phone;


    // адрес электронной почты пользователя
    public string Email { get; set; } = email;


    // пароль учётной записи пользователя
    public string Password { get; set; } = password;


    // путь к файлу аватарки пользователя
    public string Avatar { get; set; } = avatar;


    // пользовательский токен обновления
    public string UserToken { get; set; } = userToken;


    // состояние входа пользователя в учётную запись
    // (true - вошёл, false - вышел)
    public bool IsLogin { get; set; } = isLogin;


    // дата и время удаления учётной записи
    public DateTime? Deleted { get; set; } = deleted;


    // навигационные свойства для связи "многие ко многим" Users <--> Roles
    // связь через таблицу "ПОЛЬЗОВАТЕЛИ_РОЛИ"(UsersRoles)

    // связное свойство для таблицы "ПОЛЬЗОВАТЕЛИ_РОЛИ", связь 1:M
    // (один пользователь может использоваться во многих связях)
    public virtual List<UsersRoles> UsersRoles { get; set; } = [];

    // связное свойство для таблицы "РОЛИ", связь M:M
    // (многие пользователи могут иметь множество ролей)
    public virtual List<Role> Roles { get; set; } = [];


    // связное свойство для таблицы "ЛОГИНЫ", связь 1:M
    // (один пользователь может использовать множество учётных записей)
    //public virtual List<Login> Logins { get; set; } = [];


    // конструктор по умолчанию
    public User() : this("", "", "", "", "", "", "", false, null) {
    } // User


    // статический метод, возвращающий новый объект-копию
    public static User NewUser(User srcUser) =>
        new(srcUser.UserName,
            srcUser.Login,
            srcUser.Phone,
            srcUser.Email,
            srcUser.Password,
            srcUser.Avatar,
            srcUser.UserToken,
            srcUser.IsLogin,
            srcUser.Deleted) {
            Id = srcUser.Id,
            UsersRoles = srcUser.UsersRoles,
            Roles = srcUser.Roles
        };


    // статический метод, возвращающий объект-DTO
    public static UserDto UserToDto(User srcUser) =>
        new UserDto(
            srcUser.Id,
            srcUser.UserName,
            srcUser.Login,
            srcUser.Phone,
            srcUser.Email,
            srcUser.Password,
            srcUser.Avatar,
            srcUser.UserToken,
            srcUser.IsLogin,
            srcUser.Deleted,
            Role.RolesToDto(srcUser.Roles));

} // class User