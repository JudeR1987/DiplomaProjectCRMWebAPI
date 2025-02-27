using Domain.Configurations;
using Domain.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models.Entities;

// сущность для таблицы "СПЕЦИАЛЬНОСТИ" (Specializations)

// Атрибут задания класса конфигурирования сущности
[EntityTypeConfiguration(typeof(SpecializationConfiguration))]
public class Specialization(string name, DateTime? deleted)
{
    // первичный ключ - идентификатор записи о специальности
    public int Id { get; set; }


    // наименование специальности
    public string Name { get; set; } = name;


    // дата и время удаления записи о специальности
    public DateTime? Deleted { get; set; } = deleted;


    // связное свойство для таблицы "СОТРУДНИКИ", связь 1:M
    // (по одной специальности могут работать многие сотрудники)
    public virtual List<Employee> Employees { get; set; } = [];


    // конструктор по умолчанию
    public Specialization() : this("", null) {
    } // Specialization()


    // статический метод, возвращающий новый объект-копию
    public static Specialization NewSpecialization(
        Specialization srcSpecialization) =>
        new(srcSpecialization.Name,
            srcSpecialization.Deleted) {
            Id = srcSpecialization.Id,
            Employees = srcSpecialization.Employees
        };


    // статический метод, возвращающий объект-DTO
    public static SpecializationDto SpecializationToDto(
        Specialization srcSpecialization) =>
        new(srcSpecialization.Id,
            srcSpecialization.Name,
            srcSpecialization.Deleted
        );

} // class Specialization