namespace Domain.Models.Dto;

// DTO: Data Transfer Object - объект для переноса данных
// Данные для отображения сведений о сущности таблицы
// "РАСПИСАНИЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ_ДЛЯ_ПЕРЕРЫВОВ" (WorkDaysBreakSlots)
public record WorkDayBreakSlotDto(

    // идентификатор записи о связи
    int Id,

    // идентификатор записи о рабочем дне
    int WorkDayId,

    // идентификатор записи о промежутке времени
    int SlotId,

    // дата и время удаления записи о связи
    DateTime? Deleted

    );
// record WorkDayBreakSlotDto