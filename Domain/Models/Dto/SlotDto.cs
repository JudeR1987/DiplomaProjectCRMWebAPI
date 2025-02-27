namespace Domain.Models.Dto;

// DTO: Data Transfer Object - объект для переноса данных
// Данные для отображения сведений о сущности таблицы "ПРОМЕЖУТКИ_ВРЕМЕНИ" (Slots)
public record SlotDto(

    // идентификатор записи о промежутке времени
    int Id,

    // начало промежутка времени
    string From,

    // длина промежутка времени в секундах
    int Length,

    // конец промежутка времени
    string To,

    // дата и время удаления записи о промежутке времени
    DateTime? Deleted

    );
// record SlotDto