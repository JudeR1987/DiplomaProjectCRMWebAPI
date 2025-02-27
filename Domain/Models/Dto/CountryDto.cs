namespace Domain.Models.Dto;

// DTO: Data Transfer Object - объект для переноса данных
// Данные для отображения сведений о сущности таблицы "СТРАНЫ" (Countries)
public record CountryDto(

    // идентификатор записи о стране
    int Id,

    // наименование страны
    string Name,

    // дата и время удаления записи о стране
    DateTime? Deleted

    );
// record CountryDto