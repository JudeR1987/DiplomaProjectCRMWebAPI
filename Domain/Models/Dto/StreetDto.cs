namespace Domain.Models.Dto;

// DTO: Data Transfer Object - объект для переноса данных
// Данные для отображения сведений о сущности таблицы "УЛИЦЫ" (Streets)
public record StreetDto(

    // идентификатор записи об улице
    int Id,

    // наименование улицы
    string Name,

    // дата и время удаления записи об улице
    DateTime? Deleted

    );
// record StreetDto