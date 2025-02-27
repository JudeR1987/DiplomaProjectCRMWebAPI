using Domain.Configurations;
using Domain.Models.Dto;
using Domain.Models.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models.Entities;

// сущность для таблицы "РАСПИСАНИЕ" (Schedule)

// Атрибут задания класса конфигурирования сущности
[EntityTypeConfiguration(typeof(WorkDayConfiguration))]
public class WorkDay(int employeeId, DateTime date, bool isWorking,
    TimeOnly startTime, TimeOnly endTime, DateTime? deleted)
{
    // первичный ключ - идентификатор записи о рабочем дне
    public int Id { get; set; }


    // данные о сотруднике
    // свойство внешнего ключа
    public int EmployeeId { get; set; } = employeeId;

    // связное свойство для таблицы "СОТРУДНИКИ", связь М:1
    // (многие рабочие дни могут быть только для одного сотрудника)
    public virtual Employee Employee { get; set; } = null!;


    // дата рабочего дня
    public DateTime Date { get; set; } = date;


    // признак того, что день рабочий
    // (true - рабочий, false - выходной)
    public bool IsWorking { get; set; } = isWorking;


    // время начала рабочего дня
    public TimeOnly StartTime { get; set; } = startTime;


    // время окончания рабочего дня
    public TimeOnly EndTime { get; set; } = endTime;


    // дата и время удаления записи о рабочем дне
    public DateTime? Deleted { get; set; } = deleted;


    // вычисляемые свойства

    // длительность по времени всех промежутков для перерыва
    public TimeSpan BreakTime =>
        new(0, 0, BreakSlots.Sum(breakSlot => breakSlot.Length));


    // длительность по времени рабочего дня
    public TimeSpan WorkTime => IsWorking
        ? EndTime - StartTime - BreakTime
        : new TimeSpan(0, 0, 0);


    // навигационные свойства для связи "многие ко многим" Schedule <--> Slots

    // связное свойство для таблицы "РАСПИСАНИЕ_СВОБОДНЫЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ", связь 1:M
    // (данные об одном рабочем дне могут быть во многих связях)
    public virtual List<WorkDayFreeSlot> WorkDaysFreeSlots { get; set; } = [];

    // связное свойство для таблицы "ПРОМЕЖУТКИ_ВРЕМЕНИ", связь M:M
    // (во многих рабочих днях может быть множество
    // !СВОБОДНЫХ_ДЛЯ_ЗАПИСИ! промежутков времени)
    public virtual List<Slot> FreeSlots { get; set; } = [];


    // навигационные свойства для связи "многие ко многим" Schedule <--> Slots

    // связное свойство для таблицы "РАСПИСАНИЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ_ДЛЯ_ПЕРЕРЫВОВ", связь 1:M
    // (данные об одном рабочем дне могут быть во многих связях)
    public virtual List<WorkDayBreakSlot> WorkDaysBreakSlots { get; set; } = [];

    // связное свойство для таблицы "ПРОМЕЖУТКИ_ВРЕМЕНИ", связь M:M
    // (во многих рабочих днях может быть множество промежутков времени !ДЛЯ_ПЕРЕРЫВОВ!)
    public virtual List<Slot> BreakSlots { get; set; } = [];


    // конструктор по умолчанию
    public WorkDay() : this(0, DateTime.Now.Date, false,
        new TimeOnly(0, 0), new TimeOnly(0, 0), null) {
    } // WorkDay()


    // статический метод, возвращающий новый объект-копию
    public static WorkDay NewWorkDay(WorkDay srcWorkDay) =>
        new(srcWorkDay.EmployeeId,
            srcWorkDay.Date,
            srcWorkDay.IsWorking,
            srcWorkDay.StartTime,
            srcWorkDay.EndTime,
            srcWorkDay.Deleted) {
            Id = srcWorkDay.Id,
            Employee = srcWorkDay.Employee,
            WorkDaysFreeSlots = srcWorkDay.WorkDaysFreeSlots,
            FreeSlots = srcWorkDay.FreeSlots,
            WorkDaysBreakSlots = srcWorkDay.WorkDaysBreakSlots,
            BreakSlots = srcWorkDay.BreakSlots
        };


    // статический метод, возвращающий объект-DTO
    public static WorkDayDto WorkDayToDto(WorkDay srcWorkDay) =>
        new(srcWorkDay.Id,
            srcWorkDay.EmployeeId,
            Utils.DateToYYYYMMDD(srcWorkDay.Date),
            srcWorkDay.IsWorking,
            srcWorkDay.StartTime.ToString(),
            srcWorkDay.EndTime.ToString(),
            srcWorkDay.BreakTime,
            srcWorkDay.WorkTime,
            Slot.SlotsToDto(srcWorkDay.FreeSlots),
            Slot.SlotsToDto(srcWorkDay.BreakSlots),
            srcWorkDay.Deleted
        );


    // статический метод, возвращающий список объектов-DTO
    /*public static List<CityDto> CitiesToDto(List<City> srcCities) =>
        //srcCities.Select(CityToDto).ToList();
        [.. srcCities.Select(CityToDto)];*/

} // class WorkDay