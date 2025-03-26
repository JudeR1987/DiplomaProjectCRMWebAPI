using Microsoft.EntityFrameworkCore;
using Domain.Configurations;
using Domain.Models.Dto;

namespace Domain.Models.Entities;

// сущность для таблицы "УСЛУГИ" (Services)

// Атрибут задания класса конфигурирования сущности
[EntityTypeConfiguration(typeof(ServiceConfiguration))]
public class Service(string name, int servicesCategoryId, int companyId,
    int priceMin, int priceMax, int duration/*, int serviceType*/,
    string? comment, DateTime? deleted/*, int weight, List<string> imageGroup*/)
{
    // первичный ключ - идентификатор записи об услуге
    public int Id { get; set; }


    // наименование услуги
    public string Name { get; set; } = name;


    // данные о категории услуг
    // свойство внешнего ключа
    public int ServicesCategoryId { get; set; } = servicesCategoryId;

    // связное свойство для таблицы "КАТЕГОРИИ_УСЛУГ", связь М:1
    // (у многих услуг может быть только одна категория услуг)
    public virtual ServicesCategory ServicesCategory { get; set; } = null!;


    // данные о компании, для которой услуга определена
    // свойство внешнего ключа
    public int CompanyId { get; set; } = companyId;

    // связное свойство для таблицы "КОМПАНИИ", связь М:1
    // (у многих услуг может быть только одна компания, для которой она определена)
    public virtual Company Company { get; set; } = null!;


    // минимальная цена на услугу
    public int PriceMin { get; set; } = priceMin;


    // максимальная цена на услугу
    public int PriceMax { get; set; } = priceMax;


    // длительность услуги, по умолчанию равна 3600 секундам(1 час)
    public int Duration { get; set; } = duration;


    // доступность для онлайн записи
    // (1 - доступна для онлайн записи, 0 - не доступна)
    //public int ServiceType { get; set; } = serviceType;


    // комментарий к услуге
    public string? Comment { get; set; } = comment;


    // дата и время удаления записи об услуге
    public DateTime? Deleted { get; set; } = deleted;


    // навигационные свойства для связи "многие ко многим" Services <--> Employees

    // связное свойство для таблицы "СОТРУДНИКИ_УСЛУГИ", связь 1:M
    // (одна услуга может быть во многих связях)
    public virtual List<EmployeeService> EmployeesServices { get; set; } = [];

    // связное свойство для таблицы "СОТРУДНИКИ", связь M:M
    // (многие услуги могут быть оказаны множеством сотрудников)
    public virtual List<Employee> Employees { get; set; } = [];


    // навигационные свойства для связи "многие ко многим" Services <--> Records

    // связное свойство для таблицы "ЗАПИСИ_УСЛУГИ", связь 1:M
    // (одна услуга может быть во многих связях)
    public virtual List<RecordService> RecordsServices { get; set; } = [];

    // связное свойство для таблицы "ЗАПИСИ_НА_СЕАНС", связь M:M
    // (многие услуги могут быть во многих записях)
    public virtual List<Record> Records { get; set; } = [];


    // ?
    // api_service_id  Integer Внешний идентификатор услуги


    // ? вес категории(используется для сортировки категорий при отображении)
    //public int Weight { get; set; } = weight;


    // ?
    // staff array   Список сотрудников, оказывающих услугу и длительность сеанса


    // список имён файлов изображений услуги
    //public List<string> ImageGroup { get; set; } = imageGroup;


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
    public Service() : this("", 0, 0, 0, 0, 3600, null, null/*0, "", 0, []*/) {
    } // Service


    // статический метод, возвращающий новый объект-копию
    public static Service NewService(Service srcService) =>
        new(srcService.Name,
            srcService.ServicesCategoryId,
            srcService.CompanyId,
            srcService.PriceMin,
            srcService.PriceMax,
            srcService.Duration,
            srcService.Comment,
            srcService.Deleted) {
            Id = srcService.Id,
            ServicesCategory = srcService.ServicesCategory,
            Company = srcService.Company,
            EmployeesServices = srcService.EmployeesServices,
            Employees = srcService.Employees,
            RecordsServices = srcService.RecordsServices,
            Records = srcService.Records
        };


    // статический метод, возвращающий объект-DTO
    public static ServiceDto ServiceToDto(Service srcService) =>
        new(srcService.Id,
            srcService.Name,
            ServicesCategory.ServicesCategoryToDto(srcService.ServicesCategory),
            srcService.CompanyId,
            srcService.PriceMin,
            srcService.PriceMax,
            srcService.Duration,
            srcService.Comment,
            srcService.Deleted
        );


    // статический метод, возвращающий список объектов-DTO
    public static List<ServiceDto> ServicesToDto(List<Service> srcServices) =>
        srcServices.Select(ServiceToDto).ToList();

} // class Service