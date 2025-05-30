﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models.Entities;

namespace Domain.Configurations;

// конфигурация для сущности Record, задаётся атрибутом в классе сущности
public class RecordConfiguration : IEntityTypeConfiguration<Record>
{
    void IEntityTypeConfiguration<Record>.Configure(EntityTypeBuilder<Record> builder) {

        #region Задание ограничений полей таблицы "ЗАПИСИ_НА_СЕАНС" при помощи Fluent API

        // настроить SQL-ограничение поля Date для Record:
        // задать ограничение минимального значения даты и времени
        // начала записи на сеанс
        /*builder
            .ToTable(record => record
            .HasCheckConstraint("Date", "Date >= GetDate()"));*/

        // настроить SQL-ограничение поля CreateDate для Record:
        // задать ограничение максимального значения даты и времени
        // создания записи на сеанс
        builder
            .ToTable(record => record
            .HasCheckConstraint("CreateDate", "CreateDate <= GetDate()"));

        // настроить SQL-ограничение поля Length для Record:
        // задать ограничение минимального значения длительности сеанса
        builder
            .ToTable(record => record
            .HasCheckConstraint("Length", "Length > 0"));

        // настроить ограничение поля Comment для Record:
        // задать ограничение максимальной длины строкового поля
        // комментария к записи на сеанс
        // nvarchar(300)
        builder
            .Property(record => record.Comment)
            .HasMaxLength(300)
            .IsUnicode();

        // настроить SQL-ограничение поля Attendance для Record:
        // задать ограничения минимального и максимального значений статуса
        // посещения клиентом сеанса
        builder
            .ToTable(record => record
            .HasCheckConstraint("Attendance", "Attendance between -1 and 2"));

        #endregion


        #region Задание отношений между таблицами при помощи Fluent API

        // Настройка отношения "многие ко многим"
        // Records <- RecordsServices -> Services
        builder
            .HasMany(record => record.Services)
            .WithMany(service => service.Records)
            .UsingEntity<RecordService>(
                recordService => recordService
                    .HasOne(rS => rS.Service)
                    .WithMany(service => service.RecordsServices)
                    .HasForeignKey(rS => rS.ServiceId),
                recordService => recordService
                    .HasOne(rS => rS.Record)
                    .WithMany(record => record.RecordsServices)
                    .HasForeignKey(rS => rS.RecordId)
            );

        #endregion


        #region Инициализация таблицы "ЗАПИСИ_НА_СЕАНС"

        var records = new List<Record> {
            new() { Id =  1, EmployeeId = 1, ClientId =  4, Date = DateTime.Now.AddDays(1), CreateDate = DateTime.Now.AddDays(-1), Length = 3600, Comment = "Какой-то комментарий №1",  Attendance =  2, IsOnline = false, IsPaid = true,  Deleted = null },
            new() { Id =  2, EmployeeId = 2, ClientId =  6, Date = DateTime.Now.AddDays(2), CreateDate = DateTime.Now.AddDays(-2), Length = 3600, Comment = "Какой-то комментарий №2",  Attendance =  2, IsOnline = false, IsPaid = true,  Deleted = null },
            new() { Id =  3, EmployeeId = 2, ClientId =  8, Date = DateTime.Now.AddDays(3), CreateDate = DateTime.Now.AddDays(-2), Length = 3600, Comment = "Какой-то комментарий №3",  Attendance =  0, IsOnline = true,  IsPaid = true,  Deleted = null },
            new() { Id =  4, EmployeeId = 1, ClientId =  8, Date = DateTime.Now.AddDays(2), CreateDate = DateTime.Now.AddDays(-3), Length = 3600, Comment = "Какой-то комментарий №4",  Attendance =  2, IsOnline = false, IsPaid = false, Deleted = null },
            new() { Id =  5, EmployeeId = 1, ClientId = 10, Date = DateTime.Now.AddDays(3), CreateDate = DateTime.Now.AddDays(-3), Length = 3600, Comment = "Какой-то комментарий №5",  Attendance =  2, IsOnline = false, IsPaid = false, Deleted = null },
            new() { Id =  6, EmployeeId = 1, ClientId = 11, Date = DateTime.Now,            CreateDate = DateTime.Now.AddDays(-1), Length = 3600, Comment = "Какой-то комментарий №6",  Attendance = -1, IsOnline = true,  IsPaid = false, Deleted = null },
            new() { Id =  7, EmployeeId = 2, ClientId =  8, Date = DateTime.Now.AddDays(3), CreateDate = DateTime.Now.AddDays(-2), Length = 3600, Comment = "Какой-то комментарий №7",  Attendance =  0, IsOnline = false, IsPaid = false, Deleted = null },
            new() { Id =  8, EmployeeId = 1, ClientId =  6, Date = DateTime.Now,            CreateDate = DateTime.Now.AddDays(-3), Length = 3600, Comment = "Какой-то комментарий №8",  Attendance =  1, IsOnline = true,  IsPaid = false, Deleted = null },
            new() { Id =  9, EmployeeId = 3, ClientId =  6, Date = DateTime.Now.AddDays(2), CreateDate = DateTime.Now.AddDays(-1), Length = 3600, Comment = "Какой-то комментарий №9",  Attendance =  0, IsOnline = false, IsPaid = false, Deleted = null },
            new() { Id = 10, EmployeeId = 1, ClientId =  2, Date = DateTime.Now.AddDays(1), CreateDate = DateTime.Now.AddDays(-3), Length = 3600, Comment = "Какой-то комментарий №10", Attendance =  2, IsOnline = true,  IsPaid = true,  Deleted = null },
            new() { Id = 11, EmployeeId = 2, ClientId = 14, Date = DateTime.Now.AddDays(1), CreateDate = DateTime.Now.AddDays(-2), Length = 3600, Comment = "Какой-то комментарий №11", Attendance =  2, IsOnline = true,  IsPaid = true,  Deleted = null },
            new() { Id = 12, EmployeeId = 2, ClientId = 11, Date = DateTime.Now.AddDays(3), CreateDate = DateTime.Now.AddDays(0),  Length = 3600, Comment = "Какой-то комментарий №12", Attendance =  2, IsOnline = true,  IsPaid = true,  Deleted = null },
            new() { Id = 13, EmployeeId = 1, ClientId = 10, Date = DateTime.Now,            CreateDate = DateTime.Now.AddDays(-3), Length = 3600, Comment = "Какой-то комментарий №13", Attendance =  1, IsOnline = false, IsPaid = true,  Deleted = null }
        };

        // инициализация таблицы "ЗАПИСИ_НА_СЕАНС"
        builder.HasData(records);

        #endregion

    } // Configure

} // class RecordConfiguration