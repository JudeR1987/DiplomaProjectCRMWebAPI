using Domain.Models.Entities;
using Domain.Models.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations;

// конфигурация для сущности Employee, задаётся атрибутом в классе сущности
public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    void IEntityTypeConfiguration<Employee>.Configure(EntityTypeBuilder<Employee> builder) {

        #region Задание ограничений полей таблицы "СОТРУДНИКИ" при помощи Fluent API

        // настроить ограничение поля Name для Employee:
        // задать ограничение максимальной длины строкового поля имени сотрудника
        // nvarchar(50) not null
        /*builder
            .Property(employee => employee.Name)
            .HasMaxLength(50)
            .IsRequired()
            .IsUnicode();*/

        // настроить SQL-ограничение поля Rating для Employee:
        // задать ограничения минимального и максимального значений рейтинга сотрудника
        builder
            .ToTable(employee => employee
            .HasCheckConstraint("Rating", "Rating between 0 and 5"));

        #endregion


        #region Задание отношений между таблицами при помощи Fluent API

        // Настройка отношения "один к одному"

        // связь с таблицей "ПОЛЬЗОВАТЕЛИ"
        builder
            .HasOne(employee => employee.User)
            .WithOne(user => user.Employee)
            .HasForeignKey<Employee>(employee => employee.UserId)
            // !!! чтобы не было зацикливания !!!
            .OnDelete(DeleteBehavior.NoAction);


        // Настройка отношения "многие к одному"

        // связь с таблицей "СПЕЦИАЛЬНОСТИ"
        builder
            .HasOne(employee => employee.Specialization)
            .WithMany(specialization => specialization.Employees)
            .HasForeignKey(employee => employee.SpecializationId);


        // связь с таблицей "ДОЛЖНОСТИ"
        builder
            .HasOne(employee => employee.Position)
            .WithMany(position => position.Employees)
            .HasForeignKey(employee => employee.PositionId);


        // связь с таблицей "КОМПАНИИ"
        builder
            .HasOne(employee => employee.Company)
            .WithMany(company => company.Employees)
            .HasForeignKey(employee => employee.CompanyId);


        // Настройка отношения "многие ко многим"
        // Employees <- EmployeesServices -> Services
        builder
            .HasMany(employee => employee.Services)
            .WithMany(service => service.Employees)
            .UsingEntity<EmployeeService>(
                employeeService => employeeService
                    .HasOne(eS => eS.Service)
                    .WithMany(service => service.EmployeesServices)
                    .HasForeignKey(eS => eS.ServiceId),
                employeeService => employeeService
                    .HasOne(eS => eS.Employee)
                    .WithMany(employee => employee.EmployeesServices)
                    .HasForeignKey(eS => eS.EmployeeId)
            );

        // Настройка отношения "многие ко многим"
        // Employees <- Records -> Clients
        builder
            .HasMany(employee => employee.Clients)
            .WithMany(client => client.Employees)
            .UsingEntity<Record>(
                record => record
                    .HasOne(r => r.Client)
                    .WithMany(client => client.Records)
                    .HasForeignKey(r => r.ClientId),
                record => record
                    .HasOne(r => r.Employee)
                    .WithMany(employee => employee.Records)
                    .HasForeignKey(r => r.EmployeeId)
            );

        #endregion


        #region Инициализация таблицы "СОТРУДНИКИ"

        var employees = new List<Employee>{
            new() { Id =  1, /*Name = "Оксана",   */ UserId = 3, CompanyId = 1, SpecializationId = 1, PositionId = 6, Rating = 5, Avatar = "http://localhost:5297/download/getimage/employees/photos/employee001_31710.jpg", Deleted = null },
            new() { Id =  2, /*Name = "Марина",   */ UserId = 4, CompanyId = 1, SpecializationId = 3, PositionId = 6, Rating = 4, Avatar = "http://localhost:5297/download/getimage/employees/photos/employee002_31720.jpg", Deleted = null },
            new() { Id =  3, /*Name = "Тамара",   */ UserId = 5, CompanyId = 2, SpecializationId = 3, PositionId = 6, Rating = 5, Avatar = "http://localhost:5297/download/getimage/employees/photos/employee003_31730.jpg", Deleted = null },
            new() { Id =  4, /*Name = "Милана",   */ UserId = 6, CompanyId = 2, SpecializationId = 1, PositionId = 3, Rating = 0, Avatar = "http://localhost:5297/download/getimage/employees/photos/employee004_31740.jpg", Deleted = null },
            new() { Id =  5, /*Name = "Екатерина",*/ UserId = 7, CompanyId = 2, SpecializationId = 1, PositionId = 6, Rating = 4, Avatar = "http://localhost:5297/download/getimage/employees/photos/employee005_31750.jpg", Deleted = null },
            new() { Id =  6, /*Name = "Виктория", */ UserId = 8, CompanyId = 3, SpecializationId = 2, PositionId = 6, Rating = 5, Avatar = "http://localhost:5297/download/getimage/employees/photos/employee006_31760.jpg", Deleted = null },
            new() { Id =  7, /*Name = "Кристина", */ UserId = 2, CompanyId = 4, SpecializationId = 5, PositionId = 6, Rating = 3, Avatar = "http://localhost:5297/download/getimage/employees/photos/employee007_31770.jpg", Deleted = Utils.GetRandomDateTime() }
        };

        // инициализация таблицы "СОТРУДНИКИ"
        builder.HasData(employees);

        #endregion

    } // Configure

} // class EmployeeConfiguration