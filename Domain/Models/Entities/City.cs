using Domain.Configurations;
using Domain.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models.Entities;

// сущность для таблицы "ГОРОДА" (Cities)

// Атрибут задания класса конфигурирования сущности
[EntityTypeConfiguration(typeof(CityConfiguration))]
public class City(string name, int countryId, DateTime? deleted)
{
    // первичный ключ - идентификатор записи о городе
    public int Id { get; set; }


    // наименование города
    public string Name { get; set; } = name;


    // данные о стране принадлежности города
    // свойство внешнего ключа
    public int CountryId { get; set; } = countryId;

    // связное свойство для таблицы "СТРАНЫ", связь М:1
    // (многие города могут принадлежать только одной стране)
    public virtual Country Country { get; set; } = null!;


    // дата и время удаления записи о городе
    public DateTime? Deleted { get; set; } = deleted;


    // навигационные свойства для связи "многие ко многим" Cities <--> Streets

    // связное свойство для таблицы "АДРЕСА", связь 1:M
    // (один город может быть во многих адресах)
    public virtual List<Address> Addresses { get; set; } = [];

    // связное свойство для таблицы "УЛИЦЫ", связь M:M
    // (во многих городах может быть множество улиц)
    public virtual List<Street> Streets { get; set; } = [];


    // конструктор по умолчанию
    public City() : this("", 0, null) {
    } // City()


    // статический метод, возвращающий новый объект-копию
    public static City NewCity(City srcCity) =>
        new(srcCity.Name,
            srcCity.CountryId,
            srcCity.Deleted) {
            Id = srcCity.Id,
            Country = srcCity.Country,
            Addresses = srcCity.Addresses,
            Streets = srcCity.Streets
        };


    // статический метод, возвращающий объект-DTO
    public static CityDto CityToDto(City srcCity) =>
        new(srcCity.Id,
            srcCity.Name,
            Country.CountryToDto(srcCity.Country ?? new Country()),
            //srcCity.CountryId,
            srcCity.Deleted
        );


    // статический метод, возвращающий список объектов-DTO
    /*public static List<CityDto> CitiesToDto(List<City> srcCities) =>
        //srcCities.Select(CityToDto).ToList();
        [.. srcCities.Select(CityToDto)];*/

} // class City