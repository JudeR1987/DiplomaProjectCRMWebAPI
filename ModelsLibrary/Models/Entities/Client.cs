using Domain.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models.Entities;

// сущность для таблицы "КЛИЕНТЫ" (Clients)

// Атрибут задания класса конфигурирования сущности
[EntityTypeConfiguration(typeof(ClientConfiguration))]
public class Client(string surname, string name, string patronymic,
    string phone, string email, int gender, int importanceId,
    double discount, string card, DateTime birthDate, string comment,
    int spent, int balance, int smsBirthday, int smsNot)
{
    // первичный ключ - идентификатор клиента
    public int Id { get; set; }


    // фамилия клиента
    public string Surname { get; set; } = surname;


    // имя клиента
    public string Name { get; set; } = name;


    // отчество клиента
    public string Patronymic { get; set; } = patronymic;


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
    public string Card { get; set; } = card;


    // birth_date string Дата рождения клиента в формате yyyy-mm-dd

    // дата рождения клиента
    public DateTime BirthDate { get; set; } = birthDate;


    // комментарий к записи о клиенте
    public string Comment { get; set; } = comment;


    // ? сумма потраченных средств в компании на момент добавления
    public int Spent { get; set; } = spent;


    // баланс клиента
    public int Balance { get; set; } = balance;


    // признак отправки поздравления на День Рождения клиента по SMS
    // (0 - не поздравлять, 1 - поздравлять)
    public int SmsBirthday { get; set; } = smsBirthday;


    // признак исключения клиента из SMS-рассылок
    // (0 - не исключать, 1 - исключить)
    public int SmsNot { get; set; } = smsNot;


    // ? categories object Массив идентификаторов категорий клиента

    // ? custom_fields object Массив дополнительных полей клиента
    // в виде пар "api-key": "value"

    /*
     * name string Имя клиента
     * surname string Фамилия клиента
     * patronymic string Отчество клиента
     * phone string Телефон клиента
     * email string Email клиента
     * sex_id number Пол клиента (1 - мужской, 2 - женский, 0 - не известен)
     * importance_id number Класс важности клиента (0 - нет, 1 - бронза, 2 - серебро, 3 - золото)
     * discount number Скидка клиента
     * card string Номер карты клиента
     * birth_date string Дата рождения клиента в формате yyyy-mm-dd
     * comment string Комментарий
     * spent number Сколько потратил средств в компании на момент добавления
     * balance number Баланс клиента
     * sms_check number 1 - Поздравлять с Днем Рождения по SMS, 0 - не поздравлять
     * sms_not number 1 - Исключить клиента из SMS рассылок, 0 - не исключать
     ? categories object Массив идентификаторов категорий клиента
     ? custom_fields object Массив дополнительных полей клиента в виде пар "api-key": "value"
     */

    // Ф.И.О. клиента
    public string FullName =>
        $"{(string.IsNullOrEmpty(Surname) ? "" : $"{Surname} ")}" +
        $"{(string.IsNullOrEmpty(Name) ? "" : $"{Name[0]}.")}" +
        $"{(string.IsNullOrEmpty(Patronymic) ? "" : $"{Patronymic[0]}.")}";


    // конструктор по умолчанию
    public Client() : this("", "", "", "", "", 0, 0,
        0, "", DateTime.Now.Date, "", 0, 0, 0, 0)
    {
    } // Client

} // class Client