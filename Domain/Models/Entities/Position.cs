using Microsoft.EntityFrameworkCore;
using Domain.Configurations;
using Domain.Models.Dto;

namespace Domain.Models.Entities;

// сущность для таблицы "ДОЛЖНОСТИ" (Positions)

// Атрибут задания класса конфигурирования сущности
[EntityTypeConfiguration(typeof(PositionConfiguration))]
public class Position(string name, DateTime? deleted)
{
    // первичный ключ - идентификатор записи о должности
    public int Id { get; set; }


    // наименование должности
    public string Name { get; set; } = name;


    // дата и время удаления записи о должности
    public DateTime? Deleted { get; set; } = deleted;


    // связное свойство для таблицы "СОТРУДНИКИ", связь 1:M
    // (на одной должности могут работать многие сотрудники)
    public virtual List<Employee> Employees { get; set; } = [];


    // конструктор по умолчанию
    public Position() : this("", null) {
    } // Position


    // статический метод, возвращающий новый объект-копию
    public static Position NewPosition(Position srcPosition) =>
        new(srcPosition.Name,
            srcPosition.Deleted) {
            Id = srcPosition.Id,
            Employees = srcPosition.Employees
        };


    // статический метод, возвращающий объект-DTO
    public static PositionDto PositionToDto(Position srcPosition) =>
        new(srcPosition.Id,
            srcPosition.Name,
            srcPosition.Deleted
        );

} // class Position