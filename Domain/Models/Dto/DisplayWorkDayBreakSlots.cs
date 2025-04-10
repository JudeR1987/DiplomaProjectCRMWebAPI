namespace Domain.Models.Dto;

// DTO: Data Transfer Object - объект для переноса данных
// Данные для отображения рабочего дня сотрудника с коллекцией
// промежутков времени перерывов сотрудника
public record DisplayWorkDayBreakSlots(

    // данные о рабочем дне сотрудника
    WorkDayDto WorkDay,

    // коллекция промежутков времени перерывов сотрудника
    List<SlotDto> Slots

); // record DisplayWorkDayBreakSlots