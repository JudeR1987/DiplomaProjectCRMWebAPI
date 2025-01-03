using Microsoft.EntityFrameworkCore;
using Domain.Configurations;

namespace Domain.Models.Entities;

// сущность для таблицы "УСЛУГИ" (Services)

// Атрибут задания класса конфигурирования сущности
[EntityTypeConfiguration(typeof(ServiceConfiguration))]
public class Service(string name, int servicesCategoryId, int priceMin, int priceMax,
    int duration, int serviceType, string comment, int weight, List<string> imageGroup)
{
    // первичный ключ - идентификатор услуги
    public int Id { get; set; }


    // название услуги
    public string Name { get; set; } = name;


    // идентификатор категории услуг, в которой состоит услуга
    // свойство внешнего ключа
    public int ServicesCategoryId { get; set; } = servicesCategoryId;

    // связное свойство для таблицы "КАТЕГОРИИ_УСЛУГ", связь М:1
    // (у многих услуг может быть только одна категория услуг)
    public virtual ServicesCategory ServicesCategory { get; set; } = null!;


    // минимальная цена на услугу
    public int PriceMin { get; set; } = priceMin;


    // максимальная цена на услугу
    public int PriceMax { get; set; } = priceMax;


    // длительность услуги, по умолчанию равна 3600 секундам(1 час)
    public int Duration { get; set; } = duration;


    // доступность для онлайн записи
    // (1 - доступна для онлайн записи, 0 - не доступна)
    public int ServiceType { get; set; } = serviceType;


    // комментарий к услуге
    public string Comment { get; set; } = comment;


    // ?
    // api_service_id  Integer Внешний идентификатор услуги


    // ? вес категории(используется для сортировки категорий при отображении)
    public int Weight { get; set; } = weight;


    // ?
    // staff array   Список сотрудников, оказывающих услугу и длительность сеанса


    // список имён файлов изображений услуги
    public List<string> ImageGroup { get; set; } = imageGroup;


    // * id	number	Идентификатор услуги
    // * category_id number  Идентификатор категории, в которой состоит услуга
    // * title   string Название категории
    // * price_min   number Минимальная цена на услугу
    // * price_max   number Максимальная цена на услугу
    // * duration    number Длительность услуги, по умолчанию равна 3600 секундам
    // * service_type    number	1 - доступна для онлайн записи, 0 - не доступна
    // * comment string комментарий у услуге
    // ? api_service_id Integer Внешний идентификатор услуги
    // * weight  number Вес категории(используется для сортировки категорий при отображении)
    // ? staff array   Список сотрудников, оказывающих услугу и длительность сеанса
    // * image_group object Группа изображений услуги


    // конструктор по умолчанию
    public Service() : this("", 0, 0, 0, 3600, 0, "", 0, []) {
    } // Service

} // class Service