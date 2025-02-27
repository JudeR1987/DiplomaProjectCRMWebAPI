﻿using Application.Interfaces;
using Domain.Context;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories;

// репозиторий, поставляющий данные из базы данных
public class DbRepository(CrmContext db) : IDbRepository
{
    // ссылка на базу данных
    private CrmContext _db = db;


    // 1. таблица "РОЛИ"
    // 1.1.1. получить все записи таблицы "РОЛИ" из БД
    public async Task<List<Role>> GetAllRolesAsync() =>
        await _db.Roles.AsNoTracking()
        .Where(role => role.Deleted == null)
        .ToListAsync();

    // 1.1.2. получить все(включая удалённые) записи таблицы "РОЛИ" из БД
    public async Task<List<Role>> GetAllRolesWithDeletedAsync() =>
        await _db.Roles.AsNoTracking().ToListAsync();

    // 1.1.3. получить все удалённые записи таблицы "РОЛИ" из БД
    public async Task<List<Role>> GetAllDeletedRolesAsync() =>
        await _db.Roles.AsNoTracking()
        .Where(role => role.Deleted != null)
        .ToListAsync();



    // 2. таблица "ПОЛЬЗОВАТЕЛИ"
    // 2.1.1. получить все записи таблицы "ПОЛЬЗОВАТЕЛИ" из БД
    public async Task<List<User>> GetAllUsersAsync() =>
        await _db.Users
        .Where(user => user.Deleted == null)
        .ToListAsync();

    // 2.1.2. получить все(включая удалённые) записи таблицы "ПОЛЬЗОВАТЕЛИ" из БД
    public async Task<List<User>> GetAllUsersWithDeletedAsync() =>
        await _db.Users.ToListAsync();

    // 2.1.3. получить все удалённые записи таблицы "ПОЛЬЗОВАТЕЛИ" из БД
    public async Task<List<User>> GetAllDeletedUsersAsync() =>
        await _db.Users
        .Where(user => user.Deleted != null)
        .ToListAsync();

    // 2.2. добавить новую запись о пользователе в БД
    public async Task CreateUserAsync(User user) {

        // добавление записи в БД
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();

    } // CreateUserAsync

    // 2.3. изменить данные пользователя в БД
    public async Task UpdateUserAsync(User user) {

        // изменение записи в БД
        _db.Users.Update(user);
        await _db.SaveChangesAsync();

    } // UpdateUserAsync



    // 3. таблица "ПОЛЬЗОВАТЕЛИ_РОЛИ"



    // 4. таблица "СТРАНЫ"
    // 4.1.1 получить все записи таблицы "СТРАНЫ" из БД
    public async Task<List<Country>> GetAllCountriesAsync() =>
        await _db.Countries.AsNoTracking()
        .Where(country => country.Deleted == null)
        .ToListAsync();

    // 4.1.2. получить все(включая удалённые) записи таблицы "СТРАНЫ" из БД
    public async Task<List<Country>> GetAllCountriesWithDeletedAsync() =>
        await _db.Countries.AsNoTracking().ToListAsync();

    // 4.1.3. получить все удалённые записи таблицы "СТРАНЫ" из БД
    public async Task<List<Country>> GetAllDeletedCountriesAsync() =>
        await _db.Countries.AsNoTracking()
        .Where(country => country.Deleted != null)
        .ToListAsync();



    // 5. таблица "ГОРОДА"
    // 5.1.1 получить все записи таблицы "ГОРОДА" из БД
    public async Task<List<City>> GetAllCitiesAsync() =>
        await _db.Cities.AsNoTracking()
        .Where(city => city.Deleted == null)
        .ToListAsync();

    // 5.1.2. получить все(включая удалённые) записи таблицы "ГОРОДА" из БД
    public async Task<List<City>> GetAllCitiesWithDeletedAsync() =>
        await _db.Cities.AsNoTracking().ToListAsync();

    // 5.1.3. получить все удалённые записи таблицы "ГОРОДА" из БД
    public async Task<List<City>> GetAllDeletedCitiesAsync() =>
        await _db.Cities.AsNoTracking()
        .Where(city => city.Deleted != null)
        .ToListAsync();



    // 6. таблица "УЛИЦЫ"
    // 6.1.1 получить все записи таблицы "УЛИЦЫ" из БД
    public async Task<List<Street>> GetAllStreetsAsync() =>
        await _db.Streets.AsNoTracking()
        .Where(street => street.Deleted == null)
        .ToListAsync();

    // 6.1.2. получить все(включая удалённые) записи таблицы "УЛИЦЫ" из БД
    public async Task<List<Street>> GetAllStreetsWithDeletedAsync() =>
        await _db.Streets.AsNoTracking().ToListAsync();

    // 6.1.3. получить все удалённые записи таблицы "УЛИЦЫ" из БД
    public async Task<List<Street>> GetAllDeletedStreetsAsync() =>
        await _db.Streets.AsNoTracking()
        .Where(street => street.Deleted != null)
        .ToListAsync();



    // 7. таблица "АДРЕСА"
    // 7.1.1 получить все записи таблицы "АДРЕСА" из БД
    public async Task<List<Address>> GetAllAddressesAsync() =>
        await _db.Addresses.AsNoTracking()
        .Where(address => address.Deleted == null)
        .ToListAsync();

    // 7.1.2. получить все(включая удалённые) записи таблицы "АДРЕСА" из БД
    public async Task<List<Address>> GetAllAddressesWithDeletedAsync() =>
        await _db.Addresses.AsNoTracking().ToListAsync();

    // 7.1.3. получить все удалённые записи таблицы "АДРЕСА" из БД
    public async Task<List<Address>> GetAllDeletedAddressesAsync() =>
        await _db.Addresses.AsNoTracking()
        .Where(address => address.Deleted != null)
        .ToListAsync();



    // 8. таблица "КОМПАНИИ"
    // 8.1.1 получить все записи таблицы "КОМПАНИИ" из БД
    public async Task<List<Company>> GetAllCompaniesAsync() =>
        await _db.Companies.AsNoTracking()
        .Where(company => company.Deleted == null)
        .ToListAsync();

    // 8.1.2. получить все(включая удалённые) записи таблицы "КОМПАНИИ" из БД
    public async Task<List<Company>> GetAllCompaniesWithDeletedAsync() =>
        await _db.Companies.AsNoTracking().ToListAsync();

    // 8.1.3. получить все удалённые записи таблицы "КОМПАНИИ" из БД
    public async Task<List<Company>> GetAllDeletedCompaniesAsync() =>
        await _db.Companies.AsNoTracking()
        .Where(company => company.Deleted != null)
        .ToListAsync();



    // 9. таблица "СПЕЦИАЛЬНОСТИ"
    // 9.1.1 получить все записи таблицы "СПЕЦИАЛЬНОСТИ" из БД
    public async Task<List<Specialization>> GetAllSpecializationsAsync() =>
        await _db.Specializations.AsNoTracking()
        .Where(specialization => specialization.Deleted == null)
        .ToListAsync();

    // 9.1.2. получить все(включая удалённые) записи таблицы "СПЕЦИАЛЬНОСТИ" из БД
    public async Task<List<Specialization>> GetAllSpecializationsWithDeletedAsync() =>
        await _db.Specializations.AsNoTracking().ToListAsync();

    // 9.1.3. получить все удалённые записи таблицы "СПЕЦИАЛЬНОСТИ" из БД
    public async Task<List<Specialization>> GetAllDeletedSpecializationsAsync() =>
        await _db.Specializations.AsNoTracking()
        .Where(specialization => specialization.Deleted != null)
        .ToListAsync();



    // 10. таблица "ДОЛЖНОСТИ"
    // 10.1.1 получить все записи таблицы "ДОЛЖНОСТИ" из БД
    public async Task<List<Position>> GetAllPositionsAsync() =>
        await _db.Positions.AsNoTracking()
        .Where(position => position.Deleted == null)
        .ToListAsync();

    // 10.1.2. получить все(включая удалённые) записи таблицы "ДОЛЖНОСТИ" из БД
    public async Task<List<Position>> GetAllPositionsWithDeletedAsync() =>
        await _db.Positions.AsNoTracking().ToListAsync();

    // 10.1.3. получить все удалённые записи таблицы "ДОЛЖНОСТИ" из БД
    public async Task<List<Position>> GetAllDeletedPositionsAsync() =>
        await _db.Positions.AsNoTracking()
        .Where(position => position.Deleted != null)
        .ToListAsync();



    // 11. таблица "КАТЕГОРИИ_УСЛУГ"
    // 11.1.1 получить все записи таблицы "КАТЕГОРИИ_УСЛУГ" из БД
    public async Task<List<ServicesCategory>> GetAllServicesCategoriesAsync() =>
        await _db.ServicesCategories.AsNoTracking()
        .Where(servicesCategory => servicesCategory.Deleted == null)
        .ToListAsync();

    // 11.1.2. получить все(включая удалённые) записи таблицы "КАТЕГОРИИ_УСЛУГ" из БД
    public async Task<List<ServicesCategory>> GetAllServicesCategoriesWithDeletedAsync() =>
        await _db.ServicesCategories.AsNoTracking().ToListAsync();

    // 11.1.3. получить все удалённые записи таблицы "КАТЕГОРИИ_УСЛУГ" из БД
    public async Task<List<ServicesCategory>> GetAllDeletedServicesCategoriesAsync() =>
        await _db.ServicesCategories.AsNoTracking()
        .Where(servicesCategory => servicesCategory.Deleted != null)
        .ToListAsync();



    // 12. таблица "УСЛУГИ"
    // 12.1.1 получить все записи таблицы "УСЛУГИ" из БД
    public async Task<List<Service>> GetAllServicesAsync() =>
        await _db.Services.AsNoTracking()
        .Where(service => service.Deleted == null)
        .ToListAsync();

    // 12.1.2. получить все(включая удалённые) записи таблицы "УСЛУГИ" из БД
    public async Task<List<Service>> GetAllServicesWithDeletedAsync() =>
        await _db.Services.AsNoTracking().ToListAsync();

    // 12.1.3. получить все удалённые записи таблицы "УСЛУГИ" из БД
    public async Task<List<Service>> GetAllDeletedServicesAsync() =>
        await _db.Services.AsNoTracking()
        .Where(service => service.Deleted != null)
        .ToListAsync();



    // 13. таблица "СОТРУДНИКИ"
    // 13.1.1 получить все записи таблицы "СОТРУДНИКИ" из БД
    public async Task<List<Employee>> GetAllEmployeesAsync() =>
        await _db.Employees.AsNoTracking()
        .Where(employee => employee.Deleted == null)
        .ToListAsync();

    // 13.1.2. получить все(включая удалённые) записи таблицы "СОТРУДНИКИ" из БД
    public async Task<List<Employee>> GetAllEmployeesWithDeletedAsync() =>
        await _db.Employees.AsNoTracking().ToListAsync();

    // 13.1.3. получить все удалённые записи таблицы "СОТРУДНИКИ" из БД
    public async Task<List<Employee>> GetAllDeletedEmployeesAsync() =>
        await _db.Employees.AsNoTracking()
        .Where(employee => employee.Deleted != null)
        .ToListAsync();



    // 14. таблица "СОТРУДНИКИ_УСЛУГИ"
    // 14.1.1 получить все записи таблицы "СОТРУДНИКИ_УСЛУГИ" из БД
    public async Task<List<EmployeeService>> GetAllEmployeesServicesAsync() =>
        await _db.EmployeesServices.AsNoTracking()
        .Where(employeeService => employeeService.Deleted == null)
        .ToListAsync();

    // 14.1.2. получить все(включая удалённые) записи таблицы "СОТРУДНИКИ_УСЛУГИ" из БД
    public async Task<List<EmployeeService>> GetAllEmployeesServicesWithDeletedAsync() =>
        await _db.EmployeesServices.AsNoTracking().ToListAsync();

    // 14.1.3. получить все удалённые записи таблицы "СОТРУДНИКИ_УСЛУГИ" из БД
    public async Task<List<EmployeeService>> GetAllDeletedEmployeesServicesAsync() =>
        await _db.EmployeesServices.AsNoTracking()
        .Where(employeeService => employeeService.Deleted != null)
        .ToListAsync();



    // 15. таблица "КЛИЕНТЫ"
    // 15.1.1 получить все записи таблицы "КЛИЕНТЫ" из БД
    public async Task<List<Client>> GetAllClientsAsync() =>
        await _db.Clients.AsNoTracking()
        .Where(client => client.Deleted == null)
        .ToListAsync();

    // 15.1.2. получить все(включая удалённые) записи таблицы "КЛИЕНТЫ" из БД
    public async Task<List<Client>> GetAllClientsWithDeletedAsync() =>
        await _db.Clients.AsNoTracking().ToListAsync();

    // 15.1.3. получить все удалённые записи таблицы "КЛИЕНТЫ" из БД
    public async Task<List<Client>> GetAllDeletedClientsAsync() =>
        await _db.Clients.AsNoTracking()
        .Where(client => client.Deleted != null)
        .ToListAsync();



    // 16. таблица "ЗАПИСИ_НА_СЕАНС"
    // 16.1.1 получить все записи таблицы "ЗАПИСИ_НА_СЕАНС" из БД
    public async Task<List<Record>> GetAllRecordsAsync() =>
        await _db.Records.AsNoTracking()
        .Where(record => record.Deleted == null)
        .ToListAsync();

    // 16.1.2. получить все(включая удалённые) записи таблицы "ЗАПИСИ_НА_СЕАНС" из БД
    public async Task<List<Record>> GetAllRecordsWithDeletedAsync() =>
        await _db.Records.AsNoTracking().ToListAsync();

    // 16.1.3. получить все удалённые записи таблицы "ЗАПИСИ_НА_СЕАНС" из БД
    public async Task<List<Record>> GetAllDeletedRecordsAsync() =>
        await _db.Records.AsNoTracking()
        .Where(record => record.Deleted != null)
        .ToListAsync();



    // 17. таблица "ЗАПИСИ_УСЛУГИ"
    // 17.1.1 получить все записи таблицы "ЗАПИСИ_УСЛУГИ" из БД
    public async Task<List<RecordService>> GetAllRecordsServicesAsync() =>
        await _db.RecordsServices.AsNoTracking()
        .Where(recordService => recordService.Deleted == null)
        .ToListAsync();

    // 17.1.2. получить все(включая удалённые) записи таблицы "ЗАПИСИ_УСЛУГИ" из БД
    public async Task<List<RecordService>> GetAllRecordsServicesWithDeletedAsync() =>
        await _db.RecordsServices.AsNoTracking().ToListAsync();

    // 17.1.3. получить все удалённые записи таблицы "ЗАПИСИ_УСЛУГИ" из БД
    public async Task<List<RecordService>> GetAllDeletedRecordsServicesAsync() =>
        await _db.RecordsServices.AsNoTracking()
        .Where(recordService => recordService.Deleted != null)
        .ToListAsync();



    // 18. таблица "РАСПИСАНИЕ"
    // 18.1.1 получить все записи таблицы "РАСПИСАНИЕ" из БД
    public async Task<List<WorkDay>> GetAllScheduleAsync() =>
        await _db.Schedule.AsNoTracking()
        .Where(workDay => workDay.Deleted == null)
        .ToListAsync();

    // 18.1.2. получить все(включая удалённые) записи таблицы "РАСПИСАНИЕ" из БД
    public async Task<List<WorkDay>> GetAllScheduleWithDeletedAsync() =>
        await _db.Schedule.AsNoTracking().ToListAsync();

    // 18.1.3. получить все удалённые записи таблицы "РАСПИСАНИЕ" из БД
    public async Task<List<WorkDay>> GetAllDeletedScheduleAsync() =>
        await _db.Schedule.AsNoTracking()
        .Where(workDay => workDay.Deleted != null)
        .ToListAsync();



    // 19. таблица "ПРОМЕЖУТКИ_ВРЕМЕНИ"
    // 19.1.1 получить все записи таблицы "ПРОМЕЖУТКИ_ВРЕМЕНИ" из БД
    public async Task<List<Slot>> GetAllSlotsAsync() =>
        await _db.Slots.AsNoTracking()
        .Where(slot => slot.Deleted == null)
        .ToListAsync();

    // 19.1.2. получить все(включая удалённые) записи таблицы "ПРОМЕЖУТКИ_ВРЕМЕНИ" из БД
    public async Task<List<Slot>> GetAllSlotsWithDeletedAsync() =>
        await _db.Slots.AsNoTracking().ToListAsync();

    // 19.1.3. получить все удалённые записи таблицы "ПРОМЕЖУТКИ_ВРЕМЕНИ" из БД
    public async Task<List<Slot>> GetAllDeletedSlotsAsync() =>
        await _db.Slots.AsNoTracking()
        .Where(slot => slot.Deleted != null)
        .ToListAsync();



    // 20. таблица "РАСПИСАНИЕ_СВОБОДНЫЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ"
    // 20.1.1 получить все записи таблицы
    // "РАСПИСАНИЕ_СВОБОДНЫЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ" из БД
    public async Task<List<WorkDayFreeSlot>> GetAllWorkDaysFreeSlotsAsync() =>
        await _db.WorkDaysFreeSlots.AsNoTracking()
        .Where(workDayFreeSlot => workDayFreeSlot.Deleted == null)
        .ToListAsync();

    // 20.1.2. получить все(включая удалённые) записи таблицы
    // "РАСПИСАНИЕ_СВОБОДНЫЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ" из БД
    public async Task<List<WorkDayFreeSlot>> GetAllWorkDaysFreeSlotsWithDeletedAsync() =>
        await _db.WorkDaysFreeSlots.AsNoTracking().ToListAsync();

    // 20.1.3. получить все удалённые записи таблицы
    // "РАСПИСАНИЕ_СВОБОДНЫЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ" из БД
    public async Task<List<WorkDayFreeSlot>> GetAllDeletedWorkDaysFreeSlotsAsync() =>
        await _db.WorkDaysFreeSlots.AsNoTracking()
        .Where(workDayFreeSlot => workDayFreeSlot.Deleted != null)
        .ToListAsync();



    // 21. таблица "РАСПИСАНИЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ_ДЛЯ_ПЕРЕРЫВОВ"
    // 21.1.1 получить все записи таблицы
    // "РАСПИСАНИЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ_ДЛЯ_ПЕРЕРЫВОВ" из БД
    public async Task<List<WorkDayBreakSlot>> GetAllWorkDaysBreakSlotsAsync() =>
        await _db.WorkDaysBreakSlots.AsNoTracking()
        .Where(workDayBreakSlot => workDayBreakSlot.Deleted == null)
        .ToListAsync();

    // 21.1.2. получить все(включая удалённые) записи таблицы
    // "РАСПИСАНИЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ_ДЛЯ_ПЕРЕРЫВОВ" из БД
    public async Task<List<WorkDayBreakSlot>> GetAllWorkDaysBreakSlotsWithDeletedAsync() =>
        await _db.WorkDaysBreakSlots.AsNoTracking().ToListAsync();

    // 21.1.3. получить все удалённые записи таблицы
    // "РАСПИСАНИЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ_ДЛЯ_ПЕРЕРЫВОВ" из БД
    public async Task<List<WorkDayBreakSlot>> GetAllDeletedWorkDaysBreakSlotsAsync() =>
        await _db.WorkDaysBreakSlots.AsNoTracking()
        .Where(workDayBreakSlot => workDayBreakSlot.Deleted != null)
        .ToListAsync();

} // class DbRepository