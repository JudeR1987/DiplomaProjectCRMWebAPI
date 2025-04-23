using Domain.Configurations;
using Domain.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models.Entities;

// сущность для таблицы "АДРЕСА" (Addresses)
// связная таблица для отношения "многие ко многим" Cities <--> Streets

// Атрибут задания класса конфигурирования сущности
[EntityTypeConfiguration(typeof(AddressConfiguration))]
public class Address(int cityId, int streetId, string building,
    int? flat, DateTime? deleted)
{
    // первичный ключ - идентификатор записи об адресе
    public int Id { get; set; }


    // данные о городе
    // свойство внешнего ключа
    public int CityId { get; set; } = cityId;

    // связное свойство для таблицы "ГОРОДА", связь М:1
    // (во многих адресах может быть только один город)
    public virtual City City { get; set; } = null!;


    // данные об улице
    // свойство внешнего ключа
    public int StreetId { get; set; } = streetId;

    // связное свойство для таблицы "УЛИЦЫ", связь М:1
    // (во многих адресах может быть только одна улица)
    public virtual Street Street { get; set; } = null!;


    // дом/строение
    public string Building { get; set; } = building;


    // квартира(возможно её отсутствие)
    public int? Flat { get; set; } = flat;


    // дата и время удаления записи об адресе
    public DateTime? Deleted { get; set; } = deleted;


    // связное свойство для таблицы "КОМПАНИИ", связь 1:M
    // (один адрес может быть у многих компаний)
    public virtual List<Company> Companies { get; set; } = [];


    // конструктор по умолчанию
    public Address() : this(0, 0, "", null, null) {
    } // Address


    // статический метод, возвращающий новый объект-копию
    public static Address NewAddress(Address srcAddress) =>
        new(srcAddress.CityId,
            srcAddress.StreetId,
            srcAddress.Building,
            srcAddress.Flat,
            srcAddress.Deleted) {
            Id = srcAddress.Id,
            City = srcAddress.City,
            Street = srcAddress.Street,
            Companies = srcAddress.Companies
        };


    // статический метод, возвращающий объект-DTO
    public static AddressDto AddressToDto(Address srcAddress) =>
        new(srcAddress.Id,
            City.CityToDto(srcAddress.City ?? new City()),
            Street.StreetToDto(srcAddress.Street ?? new Street()),
            srcAddress.Building,
            srcAddress.Flat,
            srcAddress.Deleted
        );

} // class Address