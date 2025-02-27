using Domain.Configurations;
using Domain.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models.Entities;

// сущность для таблицы "СТРАНЫ" (Countries)

// Атрибут задания класса конфигурирования сущности
[EntityTypeConfiguration(typeof(CountryConfiguration))]
public class Country(string name, DateTime? deleted)
{
    // первичный ключ - идентификатор записи о стране
    public int Id { get; set; }


    // наименование страны
    public string Name { get; set; } = name;


    // дата и время удаления записи о стране
    public DateTime? Deleted { get; set; } = deleted;


    // связное свойство для таблицы "ГОРОДА", связь 1:M
    // (одна страна может быть для многих городов)
    public virtual List<City> Cities { get; set; } = [];


    // конструктор по умолчанию
    public Country() : this("", null) {
    } // Country()


    // статический метод, возвращающий новый объект-копию
    public static Country NewCountry(Country srcCountry) =>
        new(srcCountry.Name,
            srcCountry.Deleted) {
            Id = srcCountry.Id,
            Cities = srcCountry.Cities
        };


    // статический метод, возвращающий объект-DTO
    public static CountryDto CountryToDto(Country srcCountry) =>
        new(srcCountry.Id,
            srcCountry.Name,
            srcCountry.Deleted
        );

} // class Country