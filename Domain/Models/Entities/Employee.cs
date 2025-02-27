using Domain.Configurations;
using Domain.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System;

namespace Domain.Models.Entities;

// сущность для таблицы "СОТРУДНИКИ" (Employees)

// Атрибут задания класса конфигурирования сущности
[EntityTypeConfiguration(typeof(EmployeeConfiguration))]
public class Employee(string name, int userId, int companyId, int specializationId,
    int positionId, int rating, string avatar, DateTime? deleted)
{
    // первичный ключ - идентификатор записи о сотруднике
    public int Id { get; set; }


    // имя сотрудника
    public string Name { get; set; } = name;


    // данные о пользователе
    // свойство внешнего ключа
    public int UserId { get; set; } = userId;

    // связное свойство для таблицы "ПОЛЬЗОВАТЕЛИ", связь 1:1
    // (у одного сотрудника могут быть только одни данные о пользователе)
    public virtual User User { get; set; } = null!;


    // данные о компании
    // свойство внешнего ключа
    public int CompanyId { get; set; } = companyId;

    // связное свойство для таблицы "КОМПАНИИ", связь М:1
    // (многие сотрудники могут работать только в одной компании)
    public virtual Company Company { get; set; } = null!;


    // данные о специальности
    // свойство внешнего ключа
    public int SpecializationId { get; set; } = specializationId;

    // связное свойство для таблицы "СПЕЦИАЛЬНОСТИ", связь М:1
    // (многие сотрудники могут работать только по одной специальности)
    public virtual Specialization Specialization { get; set; } = null!;


    // данные о должности
    // свойство внешнего ключа
    public int PositionId { get; set; } = positionId;

    // связное свойство для таблицы "ДОЛЖНОСТИ", связь М:1
    // (многие сотрудники могут работать только на одной должности)
    public virtual Position Position { get; set; } = null!;


    // рейтинг сотрудника (от 0 до 5)
    public int Rating { get; set; } = rating;


    // путь к файлу аватарки сотрудника
    public string Avatar { get; set; } = avatar;


    // дата и время удаления записи о сотруднике
    public DateTime? Deleted { get; set; } = deleted;


    // навигационные свойства для связи "многие ко многим" Employees <--> Services

    // связное свойство для таблицы "СОТРУДНИКИ_УСЛУГИ", связь 1:M
    // (один сотрудник может быть во многих связях)
    public virtual List<EmployeeService> EmployeesServices { get; set; } = [];

    // связное свойство для таблицы "УСЛУГИ", связь M:M
    // (у многих сотрудников может быть множество услуг)
    public virtual List<Service> Services { get; set; } = [];


    // навигационные свойства для связи "многие ко многим" Employees <--> Clients

    // связное свойство для таблицы "ЗАПИСИ_НА_СЕАНС", связь 1:M
    // (один сотрудник может быть добавлен в множество записей на сеанс)
    public virtual List<Record> Records { get; set; } = [];

    // связное свойство для таблицы "КЛИЕНТЫ", связь M:M
    // (у многих сотрудников может быть множество услуг)
    // (многие сотрудники могут оказывать услуги множеству клиентов)
    public virtual List<Client> Clients { get; set; } = [];


    // связное свойство для таблицы "РАСПИСАНИЕ", связь 1:M
    // (одна страна может быть для многих городов)
    // (один сотрудник может иметь множество рабочих дней)
    public virtual List<WorkDay> Schedule { get; set; } = [];


    /*
     * show_rating boolean Показывать ли рейтинг сотрудника
     *
     * votes_count number Кол-во голосов, поставивших сотруднику оценку
     * 
     * comments_count number Кол-во комментариев сотруднику
     */


    // конструктор по умолчанию
    public Employee() : this("", 0, 0, 0, 0, 0, "", null) {
    } // Employee()


    // статический метод, возвращающий новый объект-копию
    public static Employee NewEmployee(Employee srcEmployee) =>
        new(srcEmployee.Name,
            srcEmployee.UserId,
            srcEmployee.CompanyId,
            srcEmployee.SpecializationId,
            srcEmployee.PositionId,
            srcEmployee.Rating,
            srcEmployee.Avatar,
            srcEmployee.Deleted) {
            Id = srcEmployee.Id,
            User = srcEmployee.User,
            Company = srcEmployee.Company,
            Specialization = srcEmployee.Specialization,
            Position = srcEmployee.Position,
            EmployeesServices = srcEmployee.EmployeesServices,
            Services = srcEmployee.Services,
            Records = srcEmployee.Records,
            Clients = srcEmployee.Clients,
            Schedule = srcEmployee.Schedule
        };


    // статический метод, возвращающий объект-DTO
    public static EmployeeDto EmployeeToDto(Employee srcEmployee) =>
        new(srcEmployee.Id,
            srcEmployee.Name,
            srcEmployee.UserId,
            Company.CompanyToDto(srcEmployee.Company),
            Specialization.SpecializationToDto(srcEmployee.Specialization),
            Position.PositionToDto(srcEmployee.Position),
            srcEmployee.Rating,
            srcEmployee.Avatar,
            Service.ServicesToDto(srcEmployee.Services),
            srcEmployee.Deleted
        );

} // class Employee