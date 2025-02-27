using Domain.Configurations;
using Domain.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models.Entities;

// класс, представляющий промежуток времени

// сущность для таблицы "ПРОМЕЖУТКИ_ВРЕМЕНИ" (Slots)

// Атрибут задания класса конфигурирования сущности
[EntityTypeConfiguration(typeof(SlotConfiguration))]
public class Slot(TimeOnly from, int length, DateTime? deleted)
{
    // первичный ключ - идентификатор записи о промежутке времени
    public int Id { get; set; }


    // начало промежутка времени
    public TimeOnly From { get; set; } = from;


    // длина промежутка времени в секундах
    public int Length { get; set; } = length;


    // дата и время удаления записи о промежутке времени
    public DateTime? Deleted { get; set; } = deleted;


    // вычисляемые свойства

    // конец промежутка времени
    public TimeOnly To => From.AddMinutes(Length / 60d);


    // навигационные свойства для связи "многие ко многим" Slots <--> Schedule

    // связное свойство для таблицы "РАСПИСАНИЕ_СВОБОДНЫЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ", связь 1:M
    // (один промежуток времени может быть во многих связях)
    public virtual List<WorkDayFreeSlot> WorkDaysFreeSlots { get; set; } = [];

    // связное свойство для таблицы "РАСПИСАНИЕ", связь M:M
    // (многие промежутки времени могут быть в множестве рабочих дней)
    public virtual List<WorkDay> WorkDaysByFree { get; set; } = [];


    // навигационные свойства для связи "многие ко многим" Slots <--> Schedule

    // связное свойство для таблицы "РАСПИСАНИЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ_ДЛЯ_ПЕРЕРЫВОВ", связь 1:M
    // (один промежуток времени может быть во многих связях)
    public virtual List<WorkDayBreakSlot> WorkDaysBreakSlots { get; set; } = [];

    // связное свойство для таблицы "РАСПИСАНИЕ", связь M:M
    // (многие промежутки времени могут быть в множестве рабочих дней)
    public virtual List<WorkDay> WorkDaysByBreak { get; set; } = [];


    // конструктор по умолчанию
    public Slot() : this(new TimeOnly(0, 0), 0, null) {
    } // Slot()


    // статический метод, возвращающий новый объект-копию
    public static Slot NewSlot(Slot srcSlot) =>
        new(srcSlot.From,
            srcSlot.Length,
            srcSlot.Deleted) {
            Id = srcSlot.Id,
            WorkDaysFreeSlots = srcSlot.WorkDaysFreeSlots,
            WorkDaysByFree = srcSlot.WorkDaysByFree,
            WorkDaysBreakSlots = srcSlot.WorkDaysBreakSlots,
            WorkDaysByBreak = srcSlot.WorkDaysByBreak
        };


    // статический метод, возвращающий объект-DTO
    public static SlotDto SlotToDto(Slot srcSlot) =>
        new(srcSlot.Id,
            srcSlot.From.ToString(),
            srcSlot.Length,
            srcSlot.To.ToString(),
            srcSlot.Deleted
        );


    // статический метод, возвращающий список объектов-DTO
    public static List<SlotDto> SlotsToDto(List<Slot> srcSlots) =>
        srcSlots.Select(SlotToDto).ToList();

} // class Slot