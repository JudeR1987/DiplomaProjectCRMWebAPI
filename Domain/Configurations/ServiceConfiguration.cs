using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models.Entities;
using Domain.Models.Infrastructure;

namespace Domain.Configurations;

// конфигурация для сущности Service, задаётся атрибутом в классе сущности
public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    void IEntityTypeConfiguration<Service>.Configure(EntityTypeBuilder<Service> builder) {

        #region Задание ограничений полей таблицы "УСЛУГИ" при помощи Fluent API

        // настроить ограничение поля Name для Service:
        // задать ограничение максимальной длины строкового поля наименования услуги
        // nvarchar(50) not null
        builder
            .Property(service => service.Name)
            .HasMaxLength(50)
            .IsRequired()
            .IsUnicode();

        // настроить SQL-ограничение поля PriceMin для Service:
        // задать ограничение минимального значения минимальной цены на услугу
        builder
            .ToTable(service => service.HasCheckConstraint("PriceMin", "PriceMin >= 0"));

        // настроить SQL-ограничение поля PriceMax для Service:
        // задать ограничение минимального значения максимальной цены на услугу
        builder
            .ToTable(service => service.HasCheckConstraint("PriceMax", "PriceMax >= 0"));

        // настроить SQL-ограничение поля Duration для Service:
        // задать ограничение минимального значения длительности услуги
        builder
            .ToTable(service => service.HasCheckConstraint("Duration", "Duration >= 0"));

        // настроить ограничение поля Comment для Service:
        // задать ограничение максимальной длины строкового поля комментария к услуге
        // nvarchar(500)
        builder
            .Property(service => service.Comment)
            .HasMaxLength(500)
            .IsUnicode();

        #endregion


        #region Задание отношений между таблицами при помощи Fluent API

        // Настройка отношения "многие к одному"

        // связь с таблицей "КАТЕГОРИИ_УСЛУГ"
        builder
            .HasOne(service => service.ServicesCategory)
            .WithMany(servicesCategory => servicesCategory.Services)
            .HasForeignKey(service => service.ServicesCategoryId);

        // связь с таблицей "КОМПАНИИ"
        builder
            .HasOne(service => service.Company)
            .WithMany(company => company.Services)
            .HasForeignKey(service => service.CompanyId)
            // !!! чтобы не было зацикливания !!!
            .OnDelete(DeleteBehavior.NoAction);

        #endregion


        #region Инициализация таблицы "УСЛУГИ"

        var services = new List<Service> {
            // категория "Стрижки, укладки"
            new() { Id =  1, Name = "Стрижка женская 1",                             ServicesCategoryId = 1, CompanyId = 1, PriceMin =  2_000, PriceMax =  2_000, Comment = "Описание 1",  Deleted = null },
            new() { Id =  2, Name = "Стрижка женская 2",                             ServicesCategoryId = 1, CompanyId = 2, PriceMin =  2_500, PriceMax =  2_500, Comment = "Описание 2",  Deleted = null },
            new() { Id =  3, Name = "Стрижка женская 3",                             ServicesCategoryId = 1, CompanyId = 1, PriceMin =  2_800, PriceMax =  2_800, Comment = "Описание 3",  Deleted = null },
            new() { Id =  4, Name = "Стрижка чёлки",                                 ServicesCategoryId = 1, CompanyId = 1, PriceMin =  1_000, PriceMax =  1_000, Comment = "Описание 4",  Deleted = null },
            new() { Id =  5, Name = "Укладка(брашинг) 1",                            ServicesCategoryId = 1, CompanyId = 2, PriceMin =  1_500, PriceMax =  1_500, Comment = "Описание 5",  Deleted = null },
            new() { Id =  6, Name = "Укладка(брашинг) 2",                            ServicesCategoryId = 1, CompanyId = 1, PriceMin =  2_000, PriceMax =  2_000, Comment = "Описание 6",  Deleted = null },
            new() { Id =  7, Name = "Укладка стайлерами(локоны/прямые)",             ServicesCategoryId = 1, CompanyId = 1, PriceMin =  2_400, PriceMax =  2_400, Comment = "Описание 7",  Deleted = null },
            new() { Id =  8, Name = "Причёска 1",                                    ServicesCategoryId = 1, CompanyId = 3, PriceMin =  3_000, PriceMax =  3_000, Comment = "Описание 8",  Deleted = null },
            new() { Id =  9, Name = "Причёска 2",                                    ServicesCategoryId = 1, CompanyId = 4, PriceMin =  4_000, PriceMax =  4_000, Comment = "Описание 9",  Deleted = null },
            new() { Id = 10, Name = "Стрижка мужская",                               ServicesCategoryId = 1, CompanyId = 1, PriceMin =  1_600, PriceMax =  1_600, Comment = "Описание 10", Deleted = null },

            // категория "Окрашивание волос"
            new() { Id = 11, Name = "Сложное окрашивание 1",                         ServicesCategoryId = 2, CompanyId = 1, PriceMin = 10_000, PriceMax = 10_000, Comment = "Описание 11", Deleted = null },
            new() { Id = 12, Name = "Сложное окрашивание 2",                         ServicesCategoryId = 2, CompanyId = 2, PriceMin = 14_000, PriceMax = 14_000, Comment = "Описание 12", Deleted = null },
            new() { Id = 13, Name = "Мелирование 1",                                 ServicesCategoryId = 2, CompanyId = 4, PriceMin =  4_700, PriceMax =  4_700, Comment = "Описание 13", Deleted = null },
            new() { Id = 14, Name = "Мелирование 2",                                 ServicesCategoryId = 2, CompanyId = 1, PriceMin =  6_400, PriceMax =  6_400, Comment = "Описание 14", Deleted = null },

            // категория "Уход за волосами"
            new() { Id = 15, Name = "Маска DAVINES'",                                ServicesCategoryId = 3, CompanyId = 1, PriceMin =  1_000, PriceMax =  1_000, Comment = "Описание 15", Deleted = null },
            new() { Id = 16, Name = "Полезное мытьё Lebel 1",                        ServicesCategoryId = 3, CompanyId = 3, PriceMin =  2_200, PriceMax =  2_200, Comment = "Описание 16", Deleted = null },
            new() { Id = 17, Name = "Полезное мытьё Lebel 2",                        ServicesCategoryId = 3, CompanyId = 1, PriceMin =  3_300, PriceMax =  3_300, Comment = "Описание 17", Deleted = null },
            new() { Id = 18, Name = "Абсолютное счастье для волос",                  ServicesCategoryId = 3, CompanyId = 2, PriceMin =  5_500, PriceMax =  5_500, Comment = "Описание 18", Deleted = null },
            new() { Id = 19, Name = "Блеск и сила",                                  ServicesCategoryId = 3, CompanyId = 2, PriceMin =  4_000, PriceMax =  4_000, Comment = "Описание 19", Deleted = null },
            new() { Id = 20, Name = "Жизненная сила Lebel",                          ServicesCategoryId = 3, CompanyId = 1, PriceMin =  3_500, PriceMax =  3_500, Comment = "Описание 20", Deleted = null },

            // категория "Маникюр"
            new() { Id = 21, Name = "Маникюр + покрытие гель-лак",                   ServicesCategoryId = 4, CompanyId = 1, PriceMin =  2_700, PriceMax =  2_700, Comment = "Описание 21", Deleted = null },
            new() { Id = 22, Name = "Снятие + маникюр + покрытие гель-лак",          ServicesCategoryId = 4, CompanyId = 1, PriceMin =  3_100, PriceMax =  3_100, Comment = "Описание 22", Deleted = null },
            new() { Id = 23, Name = "Маникюр + покрытие лак",                        ServicesCategoryId = 4, CompanyId = 1, PriceMin =  2_100, PriceMax =  2_100, Comment = "Описание 23", Deleted = null },
            new() { Id = 24, Name = "Снятие + маникюр",                              ServicesCategoryId = 4, CompanyId = 1, PriceMin =  1_800, PriceMax =  1_800, Comment = "Описание 24", Deleted = null },
            new() { Id = 25, Name = "Пилочный маникюр + покрытие гель-лак",          ServicesCategoryId = 4, CompanyId = 1, PriceMin =  3_100, PriceMax =  3_100, Comment = "Описание 25", Deleted = null },
            new() { Id = 26, Name = "Маникюр + наращивание + покрытие гель-лак",     ServicesCategoryId = 4, CompanyId = 1, PriceMin =  5_500, PriceMax =  5_500, Comment = "Описание 26", Deleted = null },
            new() { Id = 27, Name = "Маникюр мужской",                               ServicesCategoryId = 4, CompanyId = 3, PriceMin =  1_800, PriceMax =  1_800, Comment = "Описание 27", Deleted = null },
            new() { Id = 28, Name = "Укрепление",                                    ServicesCategoryId = 4, CompanyId = 1, PriceMin =    500, PriceMax =    500, Comment = "Описание 28", Deleted = null },
            new() { Id = 29, Name = "Дизайн ногтя",                                  ServicesCategoryId = 4, CompanyId = 2, PriceMin =     50, PriceMax =    300, Comment = "Описание 29", Deleted = null },

            // категория "Педикюр"
            new() { Id = 30, Name = "Педикюр + покрытие гель-лак",                   ServicesCategoryId = 5, CompanyId = 2, PriceMin =  3_300, PriceMax =  3_300, Comment = "Описание 30", Deleted = null },
            new() { Id = 31, Name = "Снятие + педикюр + покрытие гель-лак",          ServicesCategoryId = 5, CompanyId = 1, PriceMin =  3_600, PriceMax =  3_600, Comment = "Описание 31", Deleted = null },
            new() { Id = 32, Name = "Педикюр + покрытие лак",                        ServicesCategoryId = 5, CompanyId = 3, PriceMin =  2_600, PriceMax =  2_600, Comment = "Описание 32", Deleted = null },
            new() { Id = 33, Name = "Снятие + педикюр",                              ServicesCategoryId = 5, CompanyId = 1, PriceMin =  2_300, PriceMax =  2_300, Comment = "Описание 33", Deleted = null },
            new() { Id = 34, Name = "Smart-педикюр + покрытие гель-лак",             ServicesCategoryId = 5, CompanyId = 2, PriceMin =  3_700, PriceMax =  3_700, Comment = "Описание 34", Deleted = null },
            new() { Id = 35, Name = "Педикюр мужской",                               ServicesCategoryId = 5, CompanyId = 1, PriceMin =  2_300, PriceMax =  2_300, Comment = "Описание 35", Deleted = null },

            // категория "Брови, ресницы, макияж"
            new() { Id = 36, Name = "Ламинирование бровей",                          ServicesCategoryId = 6, CompanyId = 1, PriceMin =  3_000, PriceMax =  3_000, Comment = "Описание 36", Deleted = null },
            new() { Id = 37, Name = "Коррекция бровей",                              ServicesCategoryId = 6, CompanyId = 2, PriceMin =  1_000, PriceMax =  1_000, Comment = "Описание 37", Deleted = null },
            new() { Id = 38, Name = "Окрашивание бровей",                            ServicesCategoryId = 6, CompanyId = 1, PriceMin =  1_000, PriceMax =  1_000, Comment = "Описание 38", Deleted = null },
            new() { Id = 39, Name = "Ламинирование ресниц",                          ServicesCategoryId = 6, CompanyId = 2, PriceMin =  3_000, PriceMax =  3_000, Comment = "Описание 39", Deleted = null },
            new() { Id = 40, Name = "Окрашивание ресниц",                            ServicesCategoryId = 6, CompanyId = 1, PriceMin =  1_000, PriceMax =  1_000, Comment = "Описание 40", Deleted = null },
            new() { Id = 41, Name = "Макияж",                                        ServicesCategoryId = 6, CompanyId = 3, PriceMin =  3_500, PriceMax =  3_500, Comment = "Описание 41", Deleted = null },

            // категория "Перманентный макияж"
            new() { Id = 42, Name = "Перманентный макияж Брови",                     ServicesCategoryId = 7, CompanyId = 1, PriceMin =  9_000, PriceMax =  9_000, Comment = "Описание 42", Deleted = null },
            new() { Id = 43, Name = "Перманентный макияж Губы",                      ServicesCategoryId = 7, CompanyId = 2, PriceMin =  9_000, PriceMax =  9_000, Comment = "Описание 43", Deleted = null },
            new() { Id = 44, Name = "Перманентный макияж Межресничное пространство", ServicesCategoryId = 7, CompanyId = 1, PriceMin =  7_000, PriceMax =  7_000, Comment = "Описание 44", Deleted = null },
            new() { Id = 45, Name = "Коррекция перманентного макияжа",               ServicesCategoryId = 7, CompanyId = 3, PriceMin =  5_000, PriceMax =  7_000, Comment = "Описание 45", Deleted = null },

            // категория "Макияж"
            new() { Id = 46, Name = "Свадебный макияж",                              ServicesCategoryId = 8, CompanyId = 4, PriceMin =  4_500, PriceMax =  4_500, Comment = "Описание 46", Deleted = Utils.GetRandomDateTime() },
            new() { Id = 47, Name = "Праздничный макияж",                            ServicesCategoryId = 8, CompanyId = 1, PriceMin =  5_000, PriceMax =  5_000, Comment = "Описание 47", Deleted = null, Duration = 5_400 },
            new() { Id = 48, Name = "Вечерний макияж",                               ServicesCategoryId = 8, CompanyId = 2, PriceMin =  5_500, PriceMax =  5_500, Comment = "Описание 48", Deleted = null, Duration = 7_200 },

            // категория "Эстетическая косметология"
            new() { Id = 49, Name = "Атравматичная чистка лица",                     ServicesCategoryId = 9, CompanyId = 4, PriceMin =  5_000, PriceMax =  5_000, Comment = "Описание 49", Deleted = null },
            new() { Id = 50, Name = "Пилинг Alpha-Beta Retinol (лицо, шея)",         ServicesCategoryId = 9, CompanyId = 1, PriceMin =  6_000, PriceMax =  6_000, Comment = "Описание 50", Deleted = null, Duration = 5_400 },
            new() { Id = 51, Name = "Уход для лица по типу кожи (120 мин.)",         ServicesCategoryId = 9, CompanyId = 2, PriceMin =  5_500, PriceMax =  5_500, Comment = "Описание 51", Deleted = null, Duration = 7_200 },
            new() { Id = 52, Name = "Массаж лица, шеи и декольте (40 мин.)",         ServicesCategoryId = 9, CompanyId = 1, PriceMin =  2_500, PriceMax =  2_500, Comment = "Описание 52", Deleted = null, Duration = 2_400 },
            new() { Id = 53, Name = "Комбинированная чистка лица",                   ServicesCategoryId = 9, CompanyId = 3, PriceMin =  6_000, PriceMax =  6_000, Comment = "Описание 53", Deleted = null }
        };

        // инициализация таблицы "УСЛУГИ"
        builder.HasData(services);

        #endregion

    } // Configure

} // class ServiceConfiguration