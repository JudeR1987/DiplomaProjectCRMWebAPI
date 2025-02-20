﻿namespace Domain.Models.Dto;

// DTO: Data Transfer Object - объект для переноса данных
// Данные для отображения сведений о сущности таблицы "ПОЛЬЗОВАТЕЛИ" (Users)
public record UserDto(

    // идентификатор записи о пользователе
    int Id,

    // имя пользователя
    string UserName,

    // логин пользователя
    string Login,

    // номер телефона пользователя
    string Phone,

    // адрес электронной почты пользователя
    string Email,

    // пароль учётной записи пользователя
    string Password,

    // путь к файлу аватарки пользователя
    string Avatar,

    // пользовательский токен
    string UserToken,

    // состояние входа пользователя в учётную запись
    // (true - вошёл, false - вышел)
    bool IsLogin,

    // дата и время удаления учётной записи
    DateTime? Deleted,

    // роли пользователя
    List<RoleDto> Roles

    );
// record UserDto