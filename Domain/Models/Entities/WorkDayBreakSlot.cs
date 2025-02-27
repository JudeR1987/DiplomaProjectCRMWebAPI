using Domain.Configurations;
using Domain.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models.Entities;

// сущность для таблицы "РАСПИСАНИЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ_ДЛЯ_ПЕРЕРЫВОВ" (WorkDaysBreakSlots)
// связь "многие ко многим" между таблицами
// "РАСПИСАНИЕ" (Schedule) и "ПРОМЕЖУТКИ_ВРЕМЕНИ" (Slots)

// Атрибут задания класса конфигурирования сущности
[EntityTypeConfiguration(typeof(WorkDayBreakSlotConfiguration))]
public class WorkDayBreakSlot(int workDayId, int slotId, DateTime? deleted)
{
    // первичный ключ - идентификатор связи(промежутка времени для перерыва)
    public int Id { get; set; }


    // данные о рабочем дне
    // свойство внешнего ключа
    public int WorkDayId { get; set; } = workDayId;

    // связное свойство для таблицы "РАСПИСАНИЕ", связь М:1
    // (во многих связях могут быть данные только об одном рабочем дне)
    public virtual WorkDay WorkDay { get; set; } = null!;


    // данные о промежутке времени
    // свойство внешнего ключа
    public int SlotId { get; set; } = slotId;

    // связное свойство для таблицы "ПРОМЕЖУТКИ_ВРЕМЕНИ", связь М:1
    // (во многих связях может быть только один промежуток времени)
    public virtual Slot Slot { get; set; } = null!;


    // дата и время удаления записи о связи
    public DateTime? Deleted { get; set; } = deleted;


    // конструктор по умолчанию
    public WorkDayBreakSlot() : this(0, 0, null) {
    } // WorkDayBreakSlot()


    // статический метод, возвращающий новый объект-копию
    public static WorkDayBreakSlot NewWorkDayBreakSlot(WorkDayBreakSlot srcWorkDayBreakSlot) =>
        new(srcWorkDayBreakSlot.WorkDayId,
            srcWorkDayBreakSlot.SlotId,
            srcWorkDayBreakSlot.Deleted) {
            Id = srcWorkDayBreakSlot.Id,
            WorkDay = srcWorkDayBreakSlot.WorkDay,
            Slot = srcWorkDayBreakSlot.Slot
        };


    // статический метод, возвращающий объект-DTO
    public static WorkDayBreakSlotDto WorkDayBreakSlotToDto(WorkDayBreakSlot srcWorkDayBreakSlot) =>
        new(srcWorkDayBreakSlot.Id,
            srcWorkDayBreakSlot.WorkDayId,
            srcWorkDayBreakSlot.SlotId,
            srcWorkDayBreakSlot.Deleted
        );


    // статический метод, возвращающий список объектов-DTO
    /*public static List<RecordServiceDto> RecordsServicesToDto(
        List<RecordService> srcRecordsServices) =>
        srcRecordsServices.Select(RecordServiceToDto).ToList();*/

} // class BreakSlot