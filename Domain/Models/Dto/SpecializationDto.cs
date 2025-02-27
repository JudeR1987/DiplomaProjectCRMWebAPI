namespace Domain.Models.Dto;

// DTO: Data Transfer Object - объект для переноса данных
// Данные для отображения сведений о сущности
// таблицы "СПЕЦИАЛЬНОСТИ" (Specializations)
public record SpecializationDto(

    // идентификатор записи о специальности
    int Id,

    // наименование специальности
    string Name,

    // дата и время удаления записи о специальности
    DateTime? Deleted

    );
// record SpecializationDto