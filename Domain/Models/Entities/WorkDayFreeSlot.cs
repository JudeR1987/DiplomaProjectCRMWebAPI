using Domain.Configurations;
using Domain.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models.Entities;

// сущность для таблицы "РАСПИСАНИЕ_СВОБОДНЫЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ" (WorkDaysFreeSlots)
// связь "многие ко многим" между таблицами
// "РАСПИСАНИЕ" (Schedule) и "ПРОМЕЖУТКИ_ВРЕМЕНИ" (Slots)

// Атрибут задания класса конфигурирования сущности
[EntityTypeConfiguration(typeof(WorkDayFreeSlotConfiguration))]
public class WorkDayFreeSlot(int workDayId, int slotId, DateTime? deleted)
{
    // первичный ключ - идентификатор связи(свободного промежутка времени)
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
    public WorkDayFreeSlot() : this(0, 0, null) {
    } // WorkDayFreeSlot()


    // статический метод, возвращающий новый объект-копию
    public static WorkDayFreeSlot NewWorkDayFreeSlot(WorkDayFreeSlot srcWorkDayFreeSlot) =>
        new(srcWorkDayFreeSlot.WorkDayId,
            srcWorkDayFreeSlot.SlotId,
            srcWorkDayFreeSlot.Deleted) {
            Id = srcWorkDayFreeSlot.Id,
            WorkDay = srcWorkDayFreeSlot.WorkDay,
            Slot = srcWorkDayFreeSlot.Slot
        };


    // статический метод, возвращающий объект-DTO
    public static WorkDayFreeSlotDto WorkDayFreeSlotToDto(WorkDayFreeSlot srcWorkDayFreeSlot) =>
        new(srcWorkDayFreeSlot.Id,
            srcWorkDayFreeSlot.WorkDayId,
            srcWorkDayFreeSlot.SlotId,
            srcWorkDayFreeSlot.Deleted
        );


    // статический метод, возвращающий список объектов-DTO
    /*public static List<RecordServiceDto> RecordsServicesToDto(
        List<RecordService> srcRecordsServices) =>
        srcRecordsServices.Select(RecordServiceToDto).ToList();*/

} // class FreeSlot