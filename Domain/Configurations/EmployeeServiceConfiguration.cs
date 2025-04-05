using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models.Entities;
using Domain.Models.Infrastructure;

namespace Domain.Configurations;

// конфигурация для сущности EmployeeService, задаётся атрибутом в классе сущности
public class EmployeeServiceConfiguration : IEntityTypeConfiguration<EmployeeService>
{
    void IEntityTypeConfiguration<EmployeeService>.Configure(EntityTypeBuilder<EmployeeService> builder) {

        #region Задание ограничений полей таблицы "СОТРУДНИКИ_УСЛУГИ" при помощи Fluent API

        #endregion


        #region Задание отношений между таблицами при помощи Fluent API

        #endregion


        #region Инициализация таблицы "СОТРУДНИКИ_УСЛУГИ"

        var employeesServices = new List<EmployeeService> {
            new() { Id =  1, EmployeeId = 1, ServiceId = 21, Deleted = null },
            new() { Id =  2, EmployeeId = 1, ServiceId = 22, Deleted = null },
            new() { Id =  3, EmployeeId = 1, ServiceId = 23, Deleted = null },
            new() { Id =  4, EmployeeId = 2, ServiceId =  1, Deleted = null },
            new() { Id =  5, EmployeeId = 2, ServiceId =  3, Deleted = null },
            new() { Id =  6, EmployeeId = 2, ServiceId =  4, Deleted = null },
            new() { Id =  7, EmployeeId = 3, ServiceId =  2, Deleted = null },
            new() { Id =  8, EmployeeId = 3, ServiceId =  6, Deleted = null },
            new() { Id =  9, EmployeeId = 3, ServiceId =  8, Deleted = null },
            new() { Id = 10, EmployeeId = 4, ServiceId = 29, Deleted = null },
            new() { Id = 11, EmployeeId = 5, ServiceId = 31, Deleted = null },
            new() { Id = 12, EmployeeId = 5, ServiceId = 32, Deleted = null },
            new() { Id = 13, EmployeeId = 5, ServiceId = 34, Deleted = null },
            new() { Id = 14, EmployeeId = 6, ServiceId = 38, Deleted = null },
            new() { Id = 15, EmployeeId = 6, ServiceId = 39, Deleted = null },
            new() { Id = 16, EmployeeId = 7, ServiceId = 13, Deleted = Utils.GetRandomDateTime() },
            new() { Id = 17, EmployeeId = 1, ServiceId = 24, Deleted = Utils.GetRandomDateTime() }
        };

        // инициализация таблицы "СОТРУДНИКИ_УСЛУГИ"
        builder.HasData(employeesServices);

        #endregion

    } // Configure

} // class EmployeeServiceConfiguration