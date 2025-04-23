using Domain.Models.Entities;
using Domain.Models.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations;

// конфигурация для сущности WorkDay, задаётся атрибутом в классе сущности
public class WorkDayConfiguration : IEntityTypeConfiguration<WorkDay>
{
    void IEntityTypeConfiguration<WorkDay>.Configure(EntityTypeBuilder<WorkDay> builder) {

        #region Задание ограничений полей таблицы "РАСПИСАНИЕ" при помощи Fluent API

        #endregion


        #region Задание отношений между таблицами при помощи Fluent API

        // связь с таблицей "СОТРУДНИКИ"
        builder
            .HasOne(workDay => workDay.Employee)
            .WithMany(employee => employee.Schedule)
            .HasForeignKey(workDay => workDay.EmployeeId);


        // Настройка отношения "многие ко многим"
        // Schedule <- WorkDaysFreeSlots -> Slots
        builder
            .HasMany(workDay => workDay.FreeSlots)
            .WithMany(slot => slot.WorkDaysByFree)
            .UsingEntity<WorkDayFreeSlot>(
                workDayFreeSlot => workDayFreeSlot
                    .HasOne(wDFS => wDFS.Slot)
                    .WithMany(slot => slot.WorkDaysFreeSlots)
                    .HasForeignKey(wDFS => wDFS.SlotId),
                workDayFreeSlot => workDayFreeSlot
                    .HasOne(wDFS => wDFS.WorkDay)
                    .WithMany(workDay => workDay.WorkDaysFreeSlots)
                    .HasForeignKey(wDFS => wDFS.WorkDayId)
            );


        // Настройка отношения "многие ко многим"
        // Schedule <- WorkDaysBreakSlots -> Slots
        builder
            .HasMany(workDay => workDay.BreakSlots)
            .WithMany(slot => slot.WorkDaysByBreak)
            .UsingEntity<WorkDayBreakSlot>(
                workDayBreakSlot => workDayBreakSlot
                    .HasOne(wDFS => wDFS.Slot)
                    .WithMany(slot => slot.WorkDaysBreakSlots)
                    .HasForeignKey(wDFS => wDFS.SlotId),
                workDayBreakSlot => workDayBreakSlot
                    .HasOne(wDFS => wDFS.WorkDay)
                    .WithMany(workDay => workDay.WorkDaysBreakSlots)
                    .HasForeignKey(wDFS => wDFS.WorkDayId)
            );

        #endregion


        #region Инициализация таблицы "РАСПИСАНИЕ"

        var schedule = new List<WorkDay>{
            new() { Id =  1, EmployeeId = 1, Date = DateTime.Now.AddDays(1).Date, IsWorking = true,  StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(18, 0), Deleted = null },
            new() { Id =  2, EmployeeId = 1, Date = DateTime.Now.AddDays(2).Date, IsWorking = true,  StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(18, 0), Deleted = null },
            new() { Id =  3, EmployeeId = 1, Date = DateTime.Now.AddDays(3).Date, IsWorking = false, StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(18, 0), Deleted = null },
            new() { Id =  4, EmployeeId = 1, Date = DateTime.Now.AddDays(4).Date, IsWorking = true,  StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(18, 0), Deleted = null },
            new() { Id =  5, EmployeeId = 1, Date = DateTime.Now.AddDays(5).Date, IsWorking = true,  StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(18, 0), Deleted = null },
            new() { Id =  6, EmployeeId = 1, Date = DateTime.Now.AddDays(6).Date, IsWorking = false, StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(18, 0), Deleted = null },
            new() { Id =  7, EmployeeId = 1, Date = DateTime.Now.AddDays(7).Date, IsWorking = true,  StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(18, 0), Deleted = null },
            new() { Id =  8, EmployeeId = 2, Date = DateTime.Now.AddDays(1).Date, IsWorking = true,  StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(18, 0), Deleted = null },
            new() { Id =  9, EmployeeId = 2, Date = DateTime.Now.AddDays(2).Date, IsWorking = false, StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(18, 0), Deleted = null },
            new() { Id = 10, EmployeeId = 2, Date = DateTime.Now.AddDays(3).Date, IsWorking = true,  StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(18, 0), Deleted = null },
            new() { Id = 11, EmployeeId = 2, Date = DateTime.Now.AddDays(4).Date, IsWorking = false, StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(18, 0), Deleted = null },
            new() { Id = 12, EmployeeId = 2, Date = DateTime.Now.AddDays(5).Date, IsWorking = true,  StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(18, 0), Deleted = null },
            new() { Id = 13, EmployeeId = 2, Date = DateTime.Now.AddDays(6).Date, IsWorking = true,  StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(18, 0), Deleted = null },
            new() { Id = 14, EmployeeId = 2, Date = DateTime.Now.AddDays(7).Date, IsWorking = false, StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(18, 0), Deleted = null },
            new() { Id = 15, EmployeeId = 3, Date = DateTime.Now.AddDays(1).Date, IsWorking = false, StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(18, 0), Deleted = null },
            new() { Id = 16, EmployeeId = 3, Date = DateTime.Now.AddDays(2).Date, IsWorking = false, StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(18, 0), Deleted = null },
            new() { Id = 17, EmployeeId = 3, Date = DateTime.Now.AddDays(3).Date, IsWorking = true,  StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(18, 0), Deleted = null },
            new() { Id = 18, EmployeeId = 3, Date = DateTime.Now.AddDays(4).Date, IsWorking = true,  StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(18, 0), Deleted = null },
            new() { Id = 19, EmployeeId = 3, Date = DateTime.Now.AddDays(5).Date, IsWorking = true,  StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(18, 0), Deleted = null },
            new() { Id = 20, EmployeeId = 3, Date = DateTime.Now.AddDays(6).Date, IsWorking = true,  StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(18, 0), Deleted = null },
            new() { Id = 21, EmployeeId = 3, Date = DateTime.Now.AddDays(7).Date, IsWorking = false, StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(18, 0), Deleted = Utils.GetRandomDateTime() },
            new() { Id = 22, EmployeeId = 3, Date = DateTime.Now.AddDays(7).Date, IsWorking = true,  StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(18, 0), Deleted = null }
        };

        // инициализация таблицы "РАСПИСАНИЕ"
        builder.HasData(schedule);

        #endregion

    } // Configure

} // class WorkDayConfiguration