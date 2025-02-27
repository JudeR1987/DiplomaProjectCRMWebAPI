using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models.Entities;
using Domain.Models.Infrastructure;

namespace Domain.Configurations;

// конфигурация для сущности WorkDayFreeSlot, задаётся атрибутом в классе сущности
public class WorkDayFreeSlotConfiguration : IEntityTypeConfiguration<WorkDayFreeSlot>
{
    void IEntityTypeConfiguration<WorkDayFreeSlot>.Configure(EntityTypeBuilder<WorkDayFreeSlot> builder) {

        #region Задание ограничений полей таблицы "РАСПИСАНИЕ_СВОБОДНЫЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ" при помощи Fluent API

        #endregion


        #region Задание отношений между таблицами при помощи Fluent API

        #endregion


        #region Инициализация таблицы "РАСПИСАНИЕ_СВОБОДНЫЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ"

        var workDaysFreeSlots = new List<WorkDayFreeSlot> {
            // промежутки с 9:00 до 18:00 для одного дня сотрудника с Id=1
            // (учтены перерывы)
            new() { Id =  1, WorkDayId = 1, SlotId =  5, Deleted = null },
            new() { Id =  2, WorkDayId = 1, SlotId =  6, Deleted = null },
            new() { Id =  3, WorkDayId = 1, SlotId =  7, Deleted = null },
            new() { Id =  4, WorkDayId = 1, SlotId =  8, Deleted = null },
            new() { Id =  5, WorkDayId = 1, SlotId =  9, Deleted = null },
            new() { Id =  6, WorkDayId = 1, SlotId = 10, Deleted = null },
            new() { Id =  7, WorkDayId = 1, SlotId = 11, Deleted = null },
            new() { Id =  8, WorkDayId = 1, SlotId = 12, Deleted = null },
            new() { Id =  9, WorkDayId = 1, SlotId = 13, Deleted = null },
            new() { Id = 10, WorkDayId = 1, SlotId = 14, Deleted = null },
            new() { Id = 11, WorkDayId = 1, SlotId = 17, Deleted = null },
            new() { Id = 12, WorkDayId = 1, SlotId = 18, Deleted = null },
            new() { Id = 13, WorkDayId = 1, SlotId = 19, Deleted = null },
            new() { Id = 14, WorkDayId = 1, SlotId = 20, Deleted = null },
            new() { Id = 15, WorkDayId = 1, SlotId = 22, Deleted = null },
            new() { Id = 16, WorkDayId = 1, SlotId = 23, Deleted = null },
            new() { Id = 17, WorkDayId = 1, SlotId = 24, Deleted = null },
            new() { Id = 18, WorkDayId = 1, SlotId = 25, Deleted = null },
            new() { Id = 19, WorkDayId = 1, SlotId = 26, Deleted = null },
            new() { Id = 20, WorkDayId = 1, SlotId = 27, Deleted = null },
            new() { Id = 21, WorkDayId = 1, SlotId = 28, Deleted = null },
            new() { Id = 22, WorkDayId = 1, SlotId = 29, Deleted = null },
            new() { Id = 23, WorkDayId = 1, SlotId = 32, Deleted = null },
            new() { Id = 24, WorkDayId = 1, SlotId = 33, Deleted = null },
            new() { Id = 25, WorkDayId = 1, SlotId = 34, Deleted = null },
            new() { Id = 26, WorkDayId = 1, SlotId = 35, Deleted = null },
            new() { Id = 27, WorkDayId = 1, SlotId = 36, Deleted = null },
            new() { Id = 28, WorkDayId = 1, SlotId = 37, Deleted = null },
            new() { Id = 29, WorkDayId = 1, SlotId = 38, Deleted = null },
            new() { Id = 30, WorkDayId = 1, SlotId = 39, Deleted = null },
            new() { Id = 31, WorkDayId = 1, SlotId = 40, Deleted = null },
            new() { Id = 32, WorkDayId = 1, SlotId = 41, Deleted = null },

            // промежутки с 9:00 до 18:00 для одного дня сотрудника с Id=2
            // (учтены перерывы)
            new() { Id = 33, WorkDayId = 8, SlotId =  5, Deleted = null },
            new() { Id = 34, WorkDayId = 8, SlotId =  6, Deleted = null },
            new() { Id = 35, WorkDayId = 8, SlotId =  7, Deleted = null },
            new() { Id = 36, WorkDayId = 8, SlotId =  8, Deleted = null },
            new() { Id = 37, WorkDayId = 8, SlotId =  9, Deleted = null },
            new() { Id = 38, WorkDayId = 8, SlotId = 10, Deleted = null },
            new() { Id = 39, WorkDayId = 8, SlotId = 11, Deleted = null },
            new() { Id = 40, WorkDayId = 8, SlotId = 12, Deleted = null },
            new() { Id = 41, WorkDayId = 8, SlotId = 13, Deleted = null },
            new() { Id = 42, WorkDayId = 8, SlotId = 14, Deleted = null },
            new() { Id = 43, WorkDayId = 8, SlotId = 15, Deleted = null },
            new() { Id = 44, WorkDayId = 8, SlotId = 16, Deleted = null },
            new() { Id = 45, WorkDayId = 8, SlotId = 17, Deleted = null },
            new() { Id = 46, WorkDayId = 8, SlotId = 18, Deleted = null },
            new() { Id = 47, WorkDayId = 8, SlotId = 19, Deleted = null },
            new() { Id = 48, WorkDayId = 8, SlotId = 20, Deleted = null },
            new() { Id = 49, WorkDayId = 8, SlotId = 26, Deleted = null },
            new() { Id = 50, WorkDayId = 8, SlotId = 27, Deleted = null },
            new() { Id = 51, WorkDayId = 8, SlotId = 28, Deleted = null },
            new() { Id = 52, WorkDayId = 8, SlotId = 29, Deleted = null },
            new() { Id = 53, WorkDayId = 8, SlotId = 30, Deleted = null },
            new() { Id = 54, WorkDayId = 8, SlotId = 31, Deleted = null },
            new() { Id = 55, WorkDayId = 8, SlotId = 32, Deleted = null },
            new() { Id = 56, WorkDayId = 8, SlotId = 33, Deleted = null },
            new() { Id = 57, WorkDayId = 8, SlotId = 34, Deleted = null },
            new() { Id = 58, WorkDayId = 8, SlotId = 35, Deleted = null },
            new() { Id = 59, WorkDayId = 8, SlotId = 36, Deleted = null },
            new() { Id = 60, WorkDayId = 8, SlotId = 37, Deleted = null },
            new() { Id = 61, WorkDayId = 8, SlotId = 38, Deleted = null },
            new() { Id = 62, WorkDayId = 8, SlotId = 39, Deleted = null },
            new() { Id = 63, WorkDayId = 8, SlotId = 52, Deleted = Utils.GetRandomDateTime() },
            new() { Id = 64, WorkDayId = 8, SlotId = 40, Deleted = null },
            new() { Id = 65, WorkDayId = 8, SlotId = 41, Deleted = null }
        };

        // инициализация таблицы "РАСПИСАНИЕ_СВОБОДНЫЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ"
        builder.HasData(workDaysFreeSlots);

        #endregion

    } // Configure

} // class WorkDayFreeSlotConfiguration