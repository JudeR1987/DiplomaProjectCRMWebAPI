using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models.Entities;
using Domain.Models.Infrastructure;

namespace Domain.Configurations;

// конфигурация для сущности WorkDayBreakSlot, задаётся атрибутом в классе сущности
public class WorkDayBreakSlotConfiguration : IEntityTypeConfiguration<WorkDayBreakSlot>
{
    void IEntityTypeConfiguration<WorkDayBreakSlot>.Configure(EntityTypeBuilder<WorkDayBreakSlot> builder) {

        #region Задание ограничений полей таблицы "РАСПИСАНИЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ_ДЛЯ_ПЕРЕРЫВОВ" при помощи Fluent API

        #endregion


        #region Задание отношений между таблицами при помощи Fluent API

        #endregion


        #region Инициализация таблицы "РАСПИСАНИЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ_ДЛЯ_ПЕРЕРЫВОВ"

        var workDaysBreakSlots = new List<WorkDayBreakSlot> {
            // промежутки для перерывов сотрудника с Id=1 для одного дня:
            // 1) с 11:30 до 12:00
            new() { Id = 1, WorkDayId = 1, SlotId = 15, Deleted = null },
            new() { Id = 2, WorkDayId = 1, SlotId = 16, Deleted = null },
            // 2) с 15:00 до 15:30
            new() { Id = 3, WorkDayId = 1, SlotId = 30, Deleted = null },
            new() { Id = 4, WorkDayId = 1, SlotId = 31, Deleted = null },

            // промежутки для перерывов сотрудника с Id=2 для одного дня:
            // 1) с 13:00 до 14:00
            new() { Id = 5, WorkDayId = 8, SlotId = 22, Deleted = null },
            new() { Id = 6, WorkDayId = 8, SlotId = 23, Deleted = null },
            new() { Id = 7, WorkDayId = 8, SlotId = 24, Deleted = null },
            new() { Id = 8, WorkDayId = 8, SlotId = 20, Deleted = Utils.GetRandomDateTime() },
            new() { Id = 9, WorkDayId = 8, SlotId = 25, Deleted = null }
        };

        // инициализация таблицы "РАСПИСАНИЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ_ДЛЯ_ПЕРЕРЫВОВ"
        builder.HasData(workDaysBreakSlots);

        #endregion

    } // Configure

} // class WorkDayBreakSlotConfiguration