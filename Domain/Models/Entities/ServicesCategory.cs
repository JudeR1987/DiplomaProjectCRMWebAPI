using Microsoft.EntityFrameworkCore;
using Domain.Configurations;
using Domain.Models.Dto;

namespace Domain.Models.Entities;

// сущность для таблицы "КАТЕГОРИИ_УСЛУГ" (ServicesCategories)

// Атрибут задания класса конфигурирования сущности
[EntityTypeConfiguration(typeof(ServicesCategoryConfiguration))]
public class ServicesCategory(string name, DateTime? deleted)
{
    // первичный ключ - идентификатор записи о категории услуг
    public int Id { get; set; }


    // наименование категории услуг
    public string Name { get; set; } = name;


    // дата и время удаления записи о специальности
    public DateTime? Deleted { get; set; } = deleted;


    // связное свойство для таблицы "УСЛУГИ", связь 1:M
    // (одна категория услуг может быть во многих услугах)
    public virtual List<Service> Services { get; set; } = [];


    // конструктор по умолчанию
    public ServicesCategory() : this("", null) {
    } // ServicesCategory


    // статический метод, возвращающий новый объект-копию
    public static ServicesCategory NewServicesCategory(
        ServicesCategory srcServicesCategory) =>
        new(srcServicesCategory.Name,
            srcServicesCategory.Deleted) {
            Id = srcServicesCategory.Id,
            Services = srcServicesCategory.Services
        };


    // статический метод, возвращающий объект-DTO
    public static ServicesCategoryDto ServicesCategoryToDto(
        ServicesCategory srcServicesCategory) =>
        new(srcServicesCategory.Id,
            srcServicesCategory.Name,
            srcServicesCategory.Deleted
        );

} // class ServicesCategory