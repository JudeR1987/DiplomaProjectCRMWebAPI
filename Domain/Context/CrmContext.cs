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

    // 3. таблица связей "ПОЛЬЗОВАТЕЛИ_РОЛИ"
    public DbSet<UserRole> UsersRoles => Set<UserRole>();



    // 4. таблица "СТРАНЫ"
    public DbSet<Country> Countries => Set<Country>();
    
    
    // 5. таблица "ГОРОДА"
    public DbSet<City> Cities => Set<City>();


    // 6. таблица "УЛИЦЫ"
    public DbSet<Street> Streets => Set<Street>();


    // 7. таблица "АДРЕСА"
    public DbSet<Address> Addresses => Set<Address>();



    // 8. таблица "КОМПАНИИ"
    public DbSet<Company> Companies => Set<Company>();



    // 9. таблица "СПЕЦИАЛЬНОСТИ"
    public DbSet<Specialization> Specializations => Set<Specialization>();


    // 10. таблица "ДОЛЖНОСТИ"
    public DbSet<Position> Positions => Set<Position>();



    // 11. таблица "КАТЕГОРИИ_УСЛУГ"
    public DbSet<ServicesCategory> ServicesCategories => Set<ServicesCategory>();


    // 12. таблица "УСЛУГИ"
    public DbSet<Service> Services => Set<Service>();



    // 13. таблица "СОТРУДНИКИ"
    public DbSet<Employee> Employees => Set<Employee>();

    // 14. таблица связей "СОТРУДНИКИ_УСЛУГИ"
    public DbSet<EmployeeService> EmployeesServices => Set<EmployeeService>();



    // 15. таблица "КЛИЕНТЫ"
    public DbSet<Client> Clients => Set<Client>();



    // 16. таблица "ЗАПИСИ_НА_СЕАНС"
    public DbSet<Record> Records => Set<Record>();


    // 17. таблица связей "ЗАПИСИ_УСЛУГИ"
    public DbSet<RecordService> RecordsServices => Set<RecordService>();



    // 18. таблица "РАСПИСАНИЕ"
    public DbSet<WorkDay> Schedule => Set<WorkDay>();


    // 19. таблица "ПРОМЕЖУТКИ_ВРЕМЕНИ"
    public DbSet<Slot> Slots => Set<Slot>();


    // 20. таблица связей "РАСПИСАНИЕ_СВОБОДНЫЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ"
    public DbSet<WorkDayFreeSlot> WorkDaysFreeSlots => Set<WorkDayFreeSlot>();


    // 21. таблица связей "РАСПИСАНИЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ_ДЛЯ_ПЕРЕРЫВОВ"
    public DbSet<WorkDayBreakSlot> WorkDaysBreakSlots => Set<WorkDayBreakSlot>();

    #endregion


    // конструктор по умолчанию
    public CrmContext(DbContextOptions<CrmContext> options) : base(options) {

        // гарантированное удаление БД, если она есть
        //Database.EnsureDeleted();

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