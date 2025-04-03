using Domain.Configurations;
using Domain.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models.Entities;

// сущность для таблицы "КОМПАНИИ" (Companies)

// Атрибут задания класса конфигурирования сущности
[EntityTypeConfiguration(typeof(CompanyConfiguration))]
public class Company(int userOwnerId, string name, int addressId,
    string phone, string? description, string logo, string titleImage,
    string schedule, string site, DateTime? deleted)
{
    // первичный ключ - идентификатор записи о компании
    public int Id { get; set; }


    // данные о пользователе-владельце
    // свойство внешнего ключа
    public int UserOwnerId { get; set; } = userOwnerId;

    // связное свойство для таблицы "ПОЛЬЗОВАТЕЛИ", связь М:1
    // (у многих компаний может быть только один пользователь-владелец)
    public virtual User UserOwner { get; set; } = null!;


    // название компании
    public string Name { get; set; } = name;


    // данные об адресе
    // свойство внешнего ключа
    public int AddressId { get; set; } = addressId;

    // связное свойство для таблицы "АДРЕСА", связь М:1
    // (у многих компаний может быть только один адрес)
    public virtual Address Address { get; set; } = null!;


    // телефон компании
    public string Phone { get; set; } = phone;


    // описание компании
    public string? Description { get; set; } = description;


    // путь к файлу изображения логотипа компании
    public string Logo { get; set; } = logo;


    // путь к файлу основного изображения компании
    public string TitleImage { get; set; } = titleImage;


    // график работы компании
    public string Schedule { get; set; } = schedule;


    // сайт компании
    public string Site { get; set; } = site;


    // дата и время удаления записи о компании
    public DateTime? Deleted { get; set; } = deleted;


    // связное свойство для таблицы "СОТРУДНИКИ", связь 1:M
    // (в одной компании могут работать многие сотрудники)
    public virtual List<Employee> Employees { get; set; } = [];


    // связное свойство для таблицы "УСЛУГИ", связь 1:M
    // (к одной компании могут относиться многие услуги)
    public virtual List<Service> Services { get; set; } = [];


    /*
     * coordinate_lat number <double> Широта
     * 
     * coordinate_lon number <double> Долгота
     * 
     * allow_delete_record boolean
     * allow_change_record boolean
     * site string
     * currency_short_title string
     * allow_change_record_delay_step integer <int32>
     * allow_delete_record_delay_step integer <int32>
     */


    // конструктор по умолчанию
    public Company() : this(0, "", 0, "", null, "", "", "", "", null) {
    } // Company


    // статический метод, возвращающий новый объект-копию
    public static Company NewCompany(Company srcCompany) =>
        new(srcCompany.UserOwnerId,
            srcCompany.Name,
            srcCompany.AddressId,
            srcCompany.Phone,
            srcCompany.Description,
            srcCompany.Logo,
            srcCompany.TitleImage,
            srcCompany.Schedule,
            srcCompany.Site,
            srcCompany.Deleted) {
            Id = srcCompany.Id,
            UserOwner = srcCompany.UserOwner,
            Address = srcCompany.Address,
            Employees = srcCompany.Employees,
            Services = srcCompany.Services
        };

    // статический метод, возвращающий объект-DTO
    public static CompanyDto CompanyToDto(Company srcCompany) =>
        new(srcCompany.Id,
            srcCompany.UserOwnerId,
            srcCompany.Name,
            Address.AddressToDto(srcCompany.Address ?? new Address()),
            srcCompany.Phone,
            srcCompany.Description,
            srcCompany.Logo,
            srcCompany.TitleImage,
            srcCompany.Schedule,
            srcCompany.Site,
            srcCompany.Deleted
        );

} // class Company