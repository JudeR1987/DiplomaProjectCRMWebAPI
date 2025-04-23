using Domain.Configurations;
using Domain.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models.Entities;

// сущность для таблицы "СОТРУДНИКИ_УСЛУГИ" (EmployeesServices)
// связь "многие ко многим" между таблицами
// "СОТРУДНИКИ" (Employees) и "УСЛУГИ" (Services)

// Атрибут задания класса конфигурирования сущности
[EntityTypeConfiguration(typeof(EmployeeServiceConfiguration))]
public class EmployeeService(int employeeId, int serviceId, DateTime? deleted)
{
    // первичный ключ - идентификатор связи
    public int Id { get; set; }


    // данные о сотруднике
    // свойство внешнего ключа
    public int EmployeeId { get; set; } = employeeId;

    // связное свойство для таблицы "СОТРУДНИКИ", связь М:1
    // (во многих связях может быть только один сотрудник)
    public virtual Employee Employee { get; set; } = null!;


    // данные об услуге
    // свойство внешнего ключа
    public int ServiceId { get; set; } = serviceId;

    // связное свойство для таблицы "УСЛУГИ", связь М:1
    // (во многих связях может быть только одна услуга)
    public virtual Service Service { get; set; } = null!;


    // дата и время удаления записи о связи
    public DateTime? Deleted { get; set; } = deleted;


    // конструктор по умолчанию
    public EmployeeService() : this(0, 0, null) {
    } // EmployeeService


    // статический метод, возвращающий новый объект-копию
    public static EmployeeService NewEmployeeService(EmployeeService srcEmployeeService) =>
        new(srcEmployeeService.EmployeeId,
            srcEmployeeService.ServiceId,
            srcEmployeeService.Deleted) {
            Id = srcEmployeeService.Id,
            Employee = srcEmployeeService.Employee,
            Service = srcEmployeeService.Service
        };


    // статический метод, возвращающий объект-DTO
    public static EmployeeServiceDto EmployeeServiceToDto(EmployeeService srcEmployeeService) =>
        new(srcEmployeeService.Id,
            srcEmployeeService.EmployeeId,
            srcEmployeeService.ServiceId,
            srcEmployeeService.Deleted
        );

} // class EmployeeService