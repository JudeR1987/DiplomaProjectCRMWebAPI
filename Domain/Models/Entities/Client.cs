using Domain.Configurations;
using Domain.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models.Entities;

// сущность для таблицы "КЛИЕНТЫ" (Clients)

// Атрибут задания класса конфигурирования сущности
[EntityTypeConfiguration(typeof(ClientConfiguration))]
public class Client(string? surname, string name, string? patronymic,
    string phone, string email, int gender, int importanceId,
    double discount, string? card, DateTime birthDate, string? comment,
    int spent, int balance, DateTime? deleted)
{
    // первичный ключ - идентификатор записи о клиенте
    public int Id { get; set; }


    // фамилия клиента
    public string? Surname { get; set; } = surname;


    // имя клиента
    public string Name { get; set; } = name;


    // отчество клиента
    public string? Patronymic { get; set; } = patronymic;


    // номер телефона клиента
    public string Phone { get; set; } = phone;


    // адрес электронной почты клиента
    public string Email { get; set; } = email;


    // пол клиента
    // (0 - не известен, 1 - мужской, 2 - женский)
    public int Gender { get; set; } = gender;


    // класс важности клиента
    // (0 - нет, 1 - бронза, 2 - серебро, 3 - золото)
    public int ImportanceId { get; set; } = importanceId;


    // скидка клиента
    public double Discount { get; set; } = discount;


    // номер карты клиента
    public string? Card { get; set; } = card;


    // дата рождения клиента (в формате yyyy-mm-dd)
    public DateTime BirthDate { get; set; } = birthDate;


    // комментарий к записи о клиенте
    public string? Comment { get; set; } = comment;


    // ? сумма потраченных средств в компании на момент добавления
    public int Spent { get; set; } = spent;


    // баланс клиента
    public int Balance { get; set; } = balance;


    // дата и время удаления записи о клиенте
    public DateTime? Deleted { get; set; } = deleted;


    // навигационные свойства для связи "многие ко многим" Clients <--> Employees

    // связное свойство для таблицы "ЗАПИСИ_НА_СЕАНС", связь 1:M
    // (один клиент может участвовать во множестве записей на сеанс)
    public virtual List<Record> Records { get; set; } = [];

    // связное свойство для таблицы "СОТРУДНИКИ", связь M:M
    // (многие клиенты могут быть записаны на сеанс к множеству сотрудников)
    public virtual List<Employee> Employees { get; set; } = [];


    // Ф.И.О. клиента
    public string FullName =>
        string.IsNullOrEmpty(Surname) || string.IsNullOrEmpty(Patronymic)
        ? Name
        : $"{Surname} {Name[0]}.{Patronymic[0]}.";


    // конструктор по умолчанию
    public Client() : this(null, "", null, "", "", 0, 0,
        0, null, DateTime.Now.Date, null, 0, 0, null/*, 0, 0*/) {
    } // Client


    // статический метод, возвращающий новый объект-копию
    public static Client NewClient(Client srcClient) =>
        new(srcClient.Surname,
            srcClient.Name,
            srcClient.Patronymic,
            srcClient.Phone,
            srcClient.Email,
            srcClient.Gender,
            srcClient.ImportanceId,
            srcClient.Discount,
            srcClient.Card,
            srcClient.BirthDate,
            srcClient.Comment,
            srcClient.Spent,
            srcClient.Balance,
            srcClient.Deleted) {
            Id = srcClient.Id,
            Records = srcClient.Records,
            Employees = srcClient.Employees
        };


    // статический метод, возвращающий объект-DTO
    public static ClientDto ClientToDto(Client srcClient) =>
        new(srcClient.Id,
            srcClient.Surname,
            srcClient.Name,
            srcClient.Patronymic,
            srcClient.Phone,
            srcClient.Email,
            srcClient.Gender,
            srcClient.ImportanceId,
            srcClient.Discount,
            srcClient.Card,
            srcClient.BirthDate,
            srcClient.Comment,
            srcClient.Spent,
            srcClient.Balance,
            srcClient.Deleted
        );

} // class Client