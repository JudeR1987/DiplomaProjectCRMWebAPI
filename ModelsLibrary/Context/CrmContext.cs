using Microsoft.EntityFrameworkCore;
using Domain.Models.Entities;

namespace Domain.Context;

// контекст отображения на базу данных
public class CrmContext : DbContext
{
    #region таблицы базы данных

    // 1. таблица "РОЛИ"
    public DbSet<Role> Roles => Set<Role>();


    // 2. таблица "ПОЛЬЗОВАТЕЛИ"
    public DbSet<User> Users => Set<User>();


    // 3. таблица "ЛОГИНЫ"
    public DbSet<Login> Logins => Set<Login>();


    // 4. таблица "ПОЛЬЗОВАТЕЛИ_РОЛИ"
    //public DbSet<UsersRoles> UsersRoles => Set<UsersRoles>();


    // 4. таблица "КАТЕГОРИИ_УСЛУГ"
    public DbSet<ServicesCategory> ServicesCategories => Set<ServicesCategory>();


    // 5. таблица "УСЛУГИ"
    public DbSet<Service> Services => Set<Service>();


    // 6. таблица "КЛИЕНТЫ"
    public DbSet<Client> Clients => Set<Client>();


    // 3. таблица "ПЕРСОНЫ"
    //public DbSet<Person> People => Set<Person>();


    // 5. таблица "МАРШРУТЫ"
    //public DbSet<Route> Routes => Set<Route>();


    // 6. таблица "ПОЕЗДКИ"
    //public DbSet<Trip> Trips => Set<Trip>();

    #endregion


    // конструктор по умолчанию
    public CrmContext(DbContextOptions<CrmContext> options) : base(options) {

        // гарантированное удаление БД, если она есть
        Database.EnsureDeleted();

        // гарантированное создание БД, если её нет
        Database.EnsureCreated();

    } // CrmContext


    // инициализация таблиц
    protected override void OnModelCreating(ModelBuilder modelBuilder) {

        base.OnModelCreating(modelBuilder);

        // конфигурирование сущностей задаем неявно т.к. класс-конфигуратор задан атрибутом
        // [EntityTypeConfiguration(typeof(XxxConfiguration))] в классах сущностей

    } // OnModelCreating

} // class CrmContext