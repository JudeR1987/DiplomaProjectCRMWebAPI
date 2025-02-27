using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models.Entities;
using Domain.Models.Infrastructure;

namespace Domain.Configurations;

// конфигурация для сущности Position, задаётся атрибутом в классе сущности
public class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    void IEntityTypeConfiguration<Position>.Configure(EntityTypeBuilder<Position> builder) {

        #region Задание ограничений полей таблицы "ДОЛЖНОСТИ" при помощи Fluent API

        // настроить ограничение поля Name для Position:
        // задать ограничение максимальной длины строкового поля наименования должности
        // nvarchar(80) not null
        builder
            .Property(position => position.Name)
            .HasMaxLength(80)
            .IsRequired()
            .IsUnicode();

        // настроить ограничение поля Name для Position:
        // задать уникальность строкового поля наименования роли пользователя
        builder
            .HasIndex(position => position.Name)
            .IsUnique();

        #endregion


        #region Задание отношений между таблицами при помощи Fluent API

        #endregion


        #region Инициализация таблицы "ДОЛЖНОСТИ"

        var positions = new List<Position> {
            new() { Id = 1, Name = "владелец",         Deleted = null },
            new() { Id = 2, Name = "директор",         Deleted = null },
            new() { Id = 3, Name = "администратор",    Deleted = null },
            new() { Id = 4, Name = "бухгалтер",        Deleted = null },
            new() { Id = 5, Name = "менеджер",         Deleted = null },
            new() { Id = 6, Name = "мастер",           Deleted = null },
            new() { Id = 7, Name = "мастер по уборке", Deleted = null },
            new() { Id = 8, Name = "садовник",         Deleted = Utils.GetRandomDateTime() },
            new() { Id = 9, Name = "рабочий",          Deleted = null }
        };

        // инициализация таблицы "ДОЛЖНОСТИ"
        builder.HasData(positions);

        #endregion

    } // Configure

} // class PositionConfiguration