using Domain.Configurations;
using Domain.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models.Entities;

// сущность для таблицы "УЛИЦЫ" (Streets)

// Атрибут задания класса конфигурирования сущности
[EntityTypeConfiguration(typeof(StreetConfiguration))]
public class Street(string name, DateTime? deleted)
{
    // первичный ключ - идентификатор записи об улице
    public int Id { get; set; }


    // наименование улицы
    public string Name { get; set; } = name;


    // дата и время удаления записи об улице
    public DateTime? Deleted { get; set; } = deleted;


    // навигационные свойства для связи "многие ко многим" Streets <--> Cities

    // связное свойство для таблицы "АДРЕСА", связь 1:M
    // (одна улица может быть во многих адресах)
    public virtual List<Address> Addresses { get; set; } = [];

    // связное свойство для таблицы "ГОРОДА", связь M:M
    // (многие улицы могут быть во множестве городов)
    public virtual List<City> Cities { get; set; } = [];


    // конструктор по умолчанию
    public Street() : this("", null) {
    } // Street


    // статический метод, возвращающий новый объект-копию
    public static Street NewStreet(Street srcStreet) =>
        new(srcStreet.Name,
            srcStreet.Deleted) {
            Id = srcStreet.Id,
            Addresses = srcStreet.Addresses,
            Cities = srcStreet.Cities
        };


    // статический метод, возвращающий объект-DTO
    public static StreetDto StreetToDto(Street srcStreet) =>
        new(srcStreet.Id,
            srcStreet.Name,
            srcStreet.Deleted
        );

} // class Street