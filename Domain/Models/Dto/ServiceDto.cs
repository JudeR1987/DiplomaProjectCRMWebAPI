namespace Domain.Models.Dto;

// DTO: Data Transfer Object - объект для переноса данных
// Данные для отображения сведений о сущности таблицы "УСЛУГИ" (Services)
public record ServiceDto(

    // идентификатор записи об услуге
    int Id,

    // наименование услуги
    string Name,

    // категория услуг, в которой состоит услуга
    ServicesCategoryDto ServicesCategory,

    // идентификатор записи о компании, для которой услуга определена
    int CompanyId,

    // минимальная цена на услугу
    int PriceMin,

    // максимальная цена на услугу
    int PriceMax,

    // длительность услуги (по умолчанию равна 3600 секундам(1 час))
    int Duration,

    // комментарий к услуге
    string? Comment,

    // дата и время удаления записи об услуге
    DateTime? Deleted

    );
// record ServiceDto