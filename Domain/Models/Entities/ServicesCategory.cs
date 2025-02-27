using Microsoft.EntityFrameworkCore;
using Domain.Configurations;
using Domain.Models.Dto;

namespace Domain.Models.Entities;

// сущность для таблицы "КАТЕГОРИИ_УСЛУГ" (ServicesCategories)

// Атрибут задания класса конфигурирования сущности
[EntityTypeConfiguration(typeof(ServicesCategoryConfiguration))]
public class ServicesCategory(string name/*, int weight*/, DateTime? deleted)
{
    // первичный ключ - идентификатор категории услуг
    public int Id { get; set; }


    // название категории услуг
    public string Name { get; set; } = name;


    // дата и время удаления записи о специальности
    public DateTime? Deleted { get; set; } = deleted;


    // ?
    // api_id  Integer Внешний идентификатор категории


    // ? вес категории(используется для сортировки категорий при отображении)
    //public int Weight { get; set; } = weight;


    // ?
    // staff array   Список ID сотрудников, оказывающих услугу


    // связное свойство для таблицы "УСЛУГИ", связь 1:M
    // (одна категория услуг может быть во многих услугах)
    public virtual List<Service> Services { get; set; } = [];


    // * id	number	Идентификатор категории
    // * title string Название категории
    // ? api_id  Integer Внешний идентификатор категории
    // * weight number  Вес категории(используется для сортировки категорий при отображении)
    // ? staff array   Список ID сотрудников, оказывающих услугу


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