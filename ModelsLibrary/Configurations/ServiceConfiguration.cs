using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModelsLibrary.Models.Entities;

namespace ModelsLibrary.Configurations;

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

        // настроить SQL-ограничение поля ServiceType для Service:
        // задать ограничение минимального и максимального значений
        // доступности для онлайн записи услуги
        builder
            .ToTable(service => service
            .HasCheckConstraint("ServiceType", "ServiceType between 0 and 1"));

        // настроить ограничение поля Comment для Service:
        // задать ограничение максимальной длины строкового поля комментария к услуге
        // nvarchar(500)
        builder
            .Property(service => service.Comment)
            .HasMaxLength(500)
            .IsUnicode();

        // настроить SQL-ограничение поля Weight для Service:
        // задать ограничение минимального значения веса услуги для сортировки
        builder
            .ToTable(service => service.HasCheckConstraint("Weight", "Weight >= 0"));

        #endregion


        #region Задание отношений между таблицами при помощи Fluent API

        // Настройка отношения "многие к одному"

        // связь с таблицей "КАТЕГОРИИ_УСЛУГ"
        builder
            .HasOne(service => service.ServicesCategory)
            .WithMany(servicesCategory => servicesCategory.Services)
            .HasForeignKey(service => service.ServicesCategoryId);

        // связь с таблицей "ЦЕЛИ"
        /*builder
            .HasOne(route => route.Purpose)
            .WithMany(purpose => purpose.Routes)
            .HasForeignKey(route => route.PurposeId);*/

        #endregion


        #region Инициализация таблицы "УСЛУГИ"

        var services = new List<Service> {
            // категория "Стрижки, укладки"
            new() { Id =  1, Name = "Стрижка женская 1",                             ServicesCategoryId = 1, PriceMin =  2_000, PriceMax =  2_000, ServiceType = 1, Comment = "Описание 1",  Weight =  1, ImageGroup = ["img01.png", "img02.png"] },
            new() { Id =  2, Name = "Стрижка женская 2",                             ServicesCategoryId = 1, PriceMin =  2_500, PriceMax =  2_500, ServiceType = 1, Comment = "Описание 2",  Weight =  2 },
            new() { Id =  3, Name = "Стрижка женская 3",                             ServicesCategoryId = 1, PriceMin =  2_800, PriceMax =  2_800, ServiceType = 1, Comment = "Описание 3",  Weight =  3 },
            new() { Id =  4, Name = "Стрижка чёлки",                                 ServicesCategoryId = 1, PriceMin =  1_000, PriceMax =  1_000, ServiceType = 1, Comment = "Описание 4",  Weight =  4 },
            new() { Id =  5, Name = "Укладка(брашинг) 1",                            ServicesCategoryId = 1, PriceMin =  1_500, PriceMax =  1_500, ServiceType = 1, Comment = "Описание 5",  Weight =  5 },
            new() { Id =  6, Name = "Укладка(брашинг) 2",                            ServicesCategoryId = 1, PriceMin =  2_000, PriceMax =  2_000, ServiceType = 1, Comment = "Описание 6",  Weight =  6 },
            new() { Id =  7, Name = "Укладка стайлерами(локоны/прямые)",             ServicesCategoryId = 1, PriceMin =  2_400, PriceMax =  2_400, ServiceType = 1, Comment = "Описание 7",  Weight =  7 },
            new() { Id =  8, Name = "Причёска 1",                                    ServicesCategoryId = 1, PriceMin =  3_000, PriceMax =  3_000, ServiceType = 1, Comment = "Описание 8",  Weight =  8 },
            new() { Id =  9, Name = "Причёска 2",                                    ServicesCategoryId = 1, PriceMin =  4_000, PriceMax =  4_000, ServiceType = 1, Comment = "Описание 9",  Weight =  9 },
            new() { Id = 10, Name = "Стрижка мужская",                               ServicesCategoryId = 1, PriceMin =  1_600, PriceMax =  1_600, ServiceType = 1, Comment = "Описание 10", Weight = 10 },

            // категория "Окрашивание волос"
            new() { Id = 11, Name = "Сложное окрашивание 1",                         ServicesCategoryId = 2, PriceMin = 10_000, PriceMax = 10_000, ServiceType = 1, Comment = "Описание 11", Weight = 11 },
            new() { Id = 12, Name = "Сложное окрашивание 2",                         ServicesCategoryId = 2, PriceMin = 14_000, PriceMax = 14_000, ServiceType = 1, Comment = "Описание 12", Weight = 12 },
            new() { Id = 13, Name = "Мелирование 1",                                 ServicesCategoryId = 2, PriceMin =  4_700, PriceMax =  4_700, ServiceType = 1, Comment = "Описание 13", Weight = 13 },
            new() { Id = 14, Name = "Мелирование 2",                                 ServicesCategoryId = 2, PriceMin =  6_400, PriceMax =  6_400, ServiceType = 1, Comment = "Описание 14", Weight = 14 },

            // категория "Уход за волосами"
            new() { Id = 15, Name = "Маска DAVINES'",                                ServicesCategoryId = 3, PriceMin =  1_000, PriceMax =  1_000, ServiceType = 1, Comment = "Описание 15", Weight = 15 },
            new() { Id = 16, Name = "Полезное мытьё Lebel 1",                        ServicesCategoryId = 3, PriceMin =  2_200, PriceMax =  2_200, ServiceType = 1, Comment = "Описание 16", Weight = 16 },
            new() { Id = 17, Name = "Полезное мытьё Lebel 2",                        ServicesCategoryId = 3, PriceMin =  3_300, PriceMax =  3_300, ServiceType = 1, Comment = "Описание 17", Weight = 17 },
            new() { Id = 18, Name = "Абсолютное счастье для волос",                  ServicesCategoryId = 3, PriceMin =  5_500, PriceMax =  5_500, ServiceType = 1, Comment = "Описание 18", Weight = 18 },
            new() { Id = 19, Name = "Блеск и сила",                                  ServicesCategoryId = 3, PriceMin =  4_000, PriceMax =  4_000, ServiceType = 1, Comment = "Описание 19", Weight = 19 },
            new() { Id = 20, Name = "Жизненная сила Lebel",                          ServicesCategoryId = 3, PriceMin =  3_500, PriceMax =  3_500, ServiceType = 1, Comment = "Описание 20", Weight = 20 },

            // категория "Маникюр"
            new() { Id = 21, Name = "Маникюр + покрытие гель-лак",                   ServicesCategoryId = 4, PriceMin =  2_700, PriceMax =  2_700, ServiceType = 1, Comment = "Описание 21", Weight = 21 },
            new() { Id = 22, Name = "Снятие + маникюр + покрытие гель-лак",          ServicesCategoryId = 4, PriceMin =  3_100, PriceMax =  3_100, ServiceType = 1, Comment = "Описание 22", Weight = 22 },
            new() { Id = 23, Name = "Маникюр + покрытие лак",                        ServicesCategoryId = 4, PriceMin =  2_100, PriceMax =  2_100, ServiceType = 1, Comment = "Описание 23", Weight = 23 },
            new() { Id = 24, Name = "Снятие + маникюр",                              ServicesCategoryId = 4, PriceMin =  1_800, PriceMax =  1_800, ServiceType = 1, Comment = "Описание 24", Weight = 24 },
            new() { Id = 25, Name = "Пилочный маникюр + покрытие гель-лак",          ServicesCategoryId = 4, PriceMin =  3_100, PriceMax =  3_100, ServiceType = 1, Comment = "Описание 25", Weight = 25 },
            new() { Id = 26, Name = "Маникюр + наращивание + покрытие гель-лак",     ServicesCategoryId = 4, PriceMin =  5_500, PriceMax =  5_500, ServiceType = 1, Comment = "Описание 26", Weight = 26 },
            new() { Id = 27, Name = "Маникюр мужской",                               ServicesCategoryId = 4, PriceMin =  1_800, PriceMax =  1_800, ServiceType = 1, Comment = "Описание 27", Weight = 27 },
            new() { Id = 28, Name = "Укрепление",                                    ServicesCategoryId = 4, PriceMin =    500, PriceMax =    500, ServiceType = 1, Comment = "Описание 28", Weight = 28 },
            new() { Id = 29, Name = "Дизайн ногтя",                                  ServicesCategoryId = 4, PriceMin =     50, PriceMax =    300, ServiceType = 1, Comment = "Описание 29", Weight = 29 },

            // категория "Педикюр"
            new() { Id = 30, Name = "Педикюр + покрытие гель-лак",                   ServicesCategoryId = 5, PriceMin =  3_300, PriceMax =  3_300, ServiceType = 1, Comment = "Описание 30", Weight = 30 },
            new() { Id = 31, Name = "Снятие + педикюр + покрытие гель-лак",          ServicesCategoryId = 5, PriceMin =  3_600, PriceMax =  3_600, ServiceType = 1, Comment = "Описание 31", Weight = 31 },
            new() { Id = 32, Name = "Педикюр + покрытие лак",                        ServicesCategoryId = 5, PriceMin =  2_600, PriceMax =  2_600, ServiceType = 1, Comment = "Описание 32", Weight = 32 },
            new() { Id = 33, Name = "Снятие + педикюр",                              ServicesCategoryId = 5, PriceMin =  2_300, PriceMax =  2_300, ServiceType = 1, Comment = "Описание 33", Weight = 33 },
            new() { Id = 34, Name = "Smart-педикюр + покрытие гель-лак",             ServicesCategoryId = 5, PriceMin =  3_700, PriceMax =  3_700, ServiceType = 1, Comment = "Описание 34", Weight = 34 },
            new() { Id = 35, Name = "Педикюр мужской",                               ServicesCategoryId = 5, PriceMin =  2_300, PriceMax =  2_300, ServiceType = 1, Comment = "Описание 35", Weight = 35 },

            // категория "Брови, ресницы, макияж"
            new() { Id = 36, Name = "Ламинирование бровей",                          ServicesCategoryId = 6, PriceMin =  3_000, PriceMax =  3_000, ServiceType = 1, Comment = "Описание 36", Weight = 36 },
            new() { Id = 37, Name = "Коррекция бровей",                              ServicesCategoryId = 6, PriceMin =  1_000, PriceMax =  1_000, ServiceType = 1, Comment = "Описание 37", Weight = 37 },
            new() { Id = 38, Name = "Окрашивание бровей",                            ServicesCategoryId = 6, PriceMin =  1_000, PriceMax =  1_000, ServiceType = 1, Comment = "Описание 38", Weight = 38 },
            new() { Id = 39, Name = "Ламинирование ресниц",                          ServicesCategoryId = 6, PriceMin =  3_000, PriceMax =  3_000, ServiceType = 1, Comment = "Описание 39", Weight = 39 },
            new() { Id = 40, Name = "Окрашивание ресниц",                            ServicesCategoryId = 6, PriceMin =  1_000, PriceMax =  1_000, ServiceType = 1, Comment = "Описание 40", Weight = 40 },
            new() { Id = 41, Name = "Макияж",                                        ServicesCategoryId = 6, PriceMin =  3_500, PriceMax =  3_500, ServiceType = 1, Comment = "Описание 41", Weight = 41 },

            // категория "Перманентный макияж"
            new() { Id = 42, Name = "Перманентный макияж Брови",                     ServicesCategoryId = 7, PriceMin =  9_000, PriceMax =  9_000, ServiceType = 1, Comment = "Описание 42", Weight = 42 },
            new() { Id = 43, Name = "Перманентный макияж Губы",                      ServicesCategoryId = 7, PriceMin =  9_000, PriceMax =  9_000, ServiceType = 1, Comment = "Описание 43", Weight = 43 },
            new() { Id = 44, Name = "Перманентный макияж Межресничное пространство", ServicesCategoryId = 7, PriceMin =  7_000, PriceMax =  7_000, ServiceType = 1, Comment = "Описание 44", Weight = 44 },
            new() { Id = 45, Name = "Коррекция перманентного макияжа",               ServicesCategoryId = 7, PriceMin =  5_000, PriceMax =  7_000, ServiceType = 1, Comment = "Описание 45", Weight = 45 },

            // категория "Эстетическая косметология"
            new() { Id = 46, Name = "Уход для лица по типу кожи (60 мин.)",          ServicesCategoryId = 8, PriceMin =  4_500, PriceMax =  4_500, ServiceType = 1, Comment = "Описание 46", Weight = 46 },
            new() { Id = 47, Name = "Уход для лица по типу кожи (90 мин.)",          ServicesCategoryId = 8, PriceMin =  5_000, PriceMax =  5_000, ServiceType = 1, Comment = "Описание 47", Weight = 47, Duration = 5_400 },
            new() { Id = 48, Name = "Уход для лица по типу кожи (120 мин.)",         ServicesCategoryId = 8, PriceMin =  5_500, PriceMax =  5_500, ServiceType = 1, Comment = "Описание 48", Weight = 48, Duration = 7_200 },
            new() { Id = 49, Name = "Массаж лица, шеи и декольте (40 мин.)",         ServicesCategoryId = 8, PriceMin =  2_500, PriceMax =  2_500, ServiceType = 1, Comment = "Описание 49", Weight = 49, Duration = 2_400 },
            new() { Id = 50, Name = "Комбинированная чистка лица",                   ServicesCategoryId = 8, PriceMin =  6_000, PriceMax =  6_000, ServiceType = 1, Comment = "Описание 50", Weight = 50 },
        };

        // инициализация таблицы "УСЛУГИ"
        builder.HasData(services);

        #endregion

    } // Configure

} // class ServiceConfiguration