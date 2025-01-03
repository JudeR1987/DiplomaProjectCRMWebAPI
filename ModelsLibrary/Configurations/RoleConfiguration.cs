using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models.Entities;

namespace Domain.Configurations;

// конфигурация для сущности Role, задаётся атрибутом в классе сущности
public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    void IEntityTypeConfiguration<Role>.Configure(EntityTypeBuilder<Role> builder) {

        #region Задание ограничений полей таблицы "РОЛИ" при помощи Fluent API

        // настроить ограничение поля Name для Role:
        // задать ограничение максимальной длины строкового поля наименования
        // роли пользователя
        // nvarchar(50) not null
        builder
            .Property(role => role.Name)
            .HasMaxLength(50)
            .IsRequired()
            .IsUnicode();

        // настроить ограничение поля Name для Role:
        // задать уникальность строкового поля наименования роли пользователя
        builder
            .HasIndex(role => role.Name)
            .IsUnique();

        #endregion


        #region Задание отношений между таблицами при помощи Fluent API

        #endregion


        #region Инициализация таблицы "РОЛИ"

        var roles = new List<Role> {
            new() { Id = 1, Name = "owner"         },
            new() { Id = 2, Name = "worker"        },
            new() { Id = 3, Name = "administrator" },
            new() { Id = 4, Name = "accountant"    },
            new() { Id = 5, Name = "manager"       },
            new() { Id = 6, Name = "master"        }
        };

        // инициализация таблицы "РОЛИ"
        builder.HasData(roles);

        #endregion

    } // Configure

} // class RoleConfiguration