using Domain.Configurations;
using Domain.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models.Entities;

// сущность для таблицы "КОМПАНИИ" (Companies)

// Атрибут задания класса конфигурирования сущности
[EntityTypeConfiguration(typeof(CompanyConfiguration))]
public class Company(int ownerUserId, string name, int addressId,
    string phone, string description, DateTime? deleted/*,
    string clientName, string clientEmail,
    string? comment, int staffId*/)
{
    // первичный ключ - идентификатор записи о компании
    public int Id { get; set; }


    // данные о пользователе-владельце
    // свойство внешнего ключа
    public int OwnerUserId { get; set; } = ownerUserId;

    // связное свойство для таблицы "ПОЛЬЗОВАТЕЛИ", связь М:1
    // (у многих компаний может быть только один пользователь-владелец)
    public virtual User OwnerUser { get; set; } = null!;


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
    public string Description { get; set; } = description;


    // дата и время удаления записи о компании
    public DateTime? Deleted { get; set; } = deleted;


    // связное свойство для таблицы "СОТРУДНИКИ", связь 1:M
    // (в одной компании могут работать многие сотрудники)
    public virtual List<Employee> Employees { get; set; } = [];


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
    public Company() : this(0, "", 0, "", "", null) {
    } // Company


    // статический метод, возвращающий новый объект-копию
    public static Company NewCompany(Company srcCompany) =>
        new(srcCompany.OwnerUserId,
            srcCompany.Name,
            srcCompany.AddressId,
            srcCompany.Phone,
            srcCompany.Description,
            srcCompany.Deleted) {
            Id = srcCompany.Id,
            OwnerUser = srcCompany.OwnerUser,
            Address = srcCompany.Address,
            Employees = srcCompany.Employees
        };

    // статический метод, возвращающий объект-DTO
    public static CompanyDto CompanyToDto(Company srcCompany) =>
        new(srcCompany.Id,
            srcCompany.OwnerUserId,
            srcCompany.Name,
            Address.AddressToDto(srcCompany.Address),
            srcCompany.Phone,
            srcCompany.Description,
            srcCompany.Deleted
        );

} // class Company