namespace Domain.Models.Dto;

// DTO: Data Transfer Object - объект для переноса данных
// Данные для отображения сведений о сущности таблицы "ГОРОДА" (Cities)
public record CityDto(

    // идентификатор записи о городе
    int Id,

    // наименование города
    string Name,

    // данные о стране принадлежности
    CountryDto Country,

    // идентификатор записи о стране принадлежности
    //int CountryId,

    // дата и время удаления записи о городе
    DateTime? Deleted

    );
// record CityDto