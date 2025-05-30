﻿namespace Domain.Models.Dto;

// DTO: Data Transfer Object - объект для переноса данных
// Данные для отображения сведений о сущности таблицы "РАСПИСАНИЕ" (Schedule)
public record WorkDayDto(

    // идентификатор записи о рабочем дне
    int Id,

    // идентификатор записи о сотруднике
    int EmployeeId,

    // дата рабочего дня
    string Date,

    // признак того, что день рабочий
    // (true - рабочий, false - выходной)
    bool IsWorking,

    // время начала рабочего дня
    string StartTime,

    // время окончания рабочего дня
    string EndTime,

    // дата и время удаления записи о рабочем дне
    DateTime? Deleted

    );
// record WorkDayDto