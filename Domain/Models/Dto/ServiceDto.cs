namespace Domain.Models.Dto;

// DTO: Data Transfer Object - объект для переноса данных
// Данные для отображения сведений о сущности таблицы "УСЛУГИ" (Services)
public record ServiceDto(

    // идентификатор записи об услуге
    int Id,

    // название услуги
    string Name,

    // категория услуг, в которой состоит услуга
    ServicesCategoryDto ServicesCategory,

    // минимальная цена на услугу
    int PriceMin,

    // максимальная цена на услугу
    int PriceMax,

    // длительность услуги (по умолчанию равна 3600 секундам(1 час))
    int Duration,

    // доступность для онлайн записи
    // (1 - доступна для онлайн записи, 0 - не доступна)
    int ServiceType,

    // комментарий к услуге
    string Comment,

    // ?
    // api_service_id  Integer Внешний идентификатор услуги

    // ? вес категории(используется для сортировки категорий при отображении)
    int Weight,

    // ?
    // staff array   Список сотрудников, оказывающих услугу и длительность сеанса

    // список имён файлов изображений услуги
    List<string> ImageGroup

    );
// record ServiceDto