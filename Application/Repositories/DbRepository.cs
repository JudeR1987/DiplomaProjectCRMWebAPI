using Application.Interfaces;
using Domain.Context;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories;

// репозиторий, поставляющий данные из базы данных
public class DbRepository(CrmContext db) : IDbRepository
{
    // ссылка на базу данных
    private readonly CrmContext _db = db;


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
    public async Task CreateUserAsync(User newUser) {

        // добавление записи в БД
        await _db.Users.AddAsync(newUser);
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

    // 5.2. добавить новую запись о городе в БД
    public async Task<(bool, string)> CreateCityAsync(City newCity) {
        
        // вернём из метода результаты операции
        bool isOk;
        string message;

        try {

            // добавление записи в БД
            await _db.Cities.AddAsync(newCity);
            await _db.SaveChangesAsync();

            isOk = true;
            message = "Ok";

        } catch (Exception ex) {

            isOk = false;
            message = ex.Message;

        } // try-catch

        return (isOk, message);

    } // CreateCityAsync



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

    // 6.2. добавить новую запись об улице в БД
    public async Task<(bool, string)> CreateStreetAsync(Street newStreet) {
        
        // вернём из метода результаты операции
        bool isOk;
        string message;

        try {

            // добавление записи в БД
            await _db.Streets.AddAsync(newStreet);
            await _db.SaveChangesAsync();

            isOk = true;
            message = "Ok";

        } catch (Exception ex) {

            isOk = false;
            message = ex.Message;

        } // try-catch

        return (isOk, message);

    } // CreateStreetAsync



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

    // 7.2. добавить новую запись об адресе в БД
    public async Task<(bool, string)> CreateAddressAsync(Address newAddress) {

        // вернём из метода результаты операции
        bool isOk;
        string message;

        try {

            // добавление записи в БД
            await _db.Addresses.AddAsync(newAddress);
            await _db.SaveChangesAsync();

            isOk = true;
            message = "Ok";

        } catch (Exception ex) {

            isOk = false;
            message = ex.Message;

        } // try-catch

        return (isOk, message);

    } // CreateAddressAsync



    // 8. таблица "КОМПАНИИ"
    // 8.1.1 получить все записи таблицы "КОМПАНИИ" из БД
    public async Task<List<Company>> GetAllCompaniesAsync() =>
        await _db.Companies
        .Where(company => company.Deleted == null)
        .ToListAsync();

    // 8.1.2. получить все(включая удалённые) записи таблицы "КОМПАНИИ" из БД
    public async Task<List<Company>> GetAllCompaniesWithDeletedAsync() =>
        await _db.Companies.ToListAsync();

    // 8.1.3. получить все удалённые записи таблицы "КОМПАНИИ" из БД
    public async Task<List<Company>> GetAllDeletedCompaniesAsync() =>
        await _db.Companies
        .Where(company => company.Deleted != null)
        .ToListAsync();

    // 8.2. получить все записи о компаниях из БД с заданным
    // пользователем-владельцем
    public async Task<List<Company>> GetAllCompaniesByUserIdAsync(int userId) =>
        await _db.Companies
        .Where(company => company.UserOwnerId == userId && company.Deleted == null)
        .ToListAsync();

    // 8.3. добавить новую запись о компании в БД
    public async Task<(bool, string)> CreateCompanyAsync(Company newCompany) {

        // вернём из метода результаты операции
        bool isOk;
        string message;

        try {

            // добавление записи в БД
            await _db.Companies.AddAsync(newCompany);
            await _db.SaveChangesAsync();

            isOk = true;
            message = "Ok";

        } catch (Exception ex) {

            isOk = false;
            message = ex.Message;

        } // try-catch

        return (isOk, message);

    } // CreateCompanyAsync

    // 8.4. изменить данные о компании в БД
    public async Task<(bool, string)> UpdateCompanyAsync(Company companyEdt) {

        // вернём из метода результаты операции
        bool isOk;
        string message;

        try {

            // изменение записи в БД
            _db.Companies.Update(companyEdt);
            await _db.SaveChangesAsync();

            isOk = true;
            message = "Ok";

        } catch (Exception ex) {

            isOk = false;
            message = ex.Message;

        } // try-catch

        return (isOk, message);

    } // UpdateCompanyAsync



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

    // 9.2. добавить новую запись о специальности в БД
    public async Task<(bool, string)> CreateSpecializationAsync(Specialization newSpecialization) {

        // вернём из метода результаты операции
        bool isOk;
        string message;

        try {

            // добавление записи в БД
            await _db.Specializations.AddAsync(newSpecialization);
            await _db.SaveChangesAsync();

            isOk = true;
            message = "Ok";

        } catch (Exception ex) {

            isOk = false;
            message = ex.Message;

        } // try-catch

        return (isOk, message);

    } // CreateSpecializationAsync

    // 9.3. изменить данные о специальности в БД
    public async Task<(bool, string)> UpdateSpecializationAsync(Specialization specializationEdt) {

        // вернём из метода результаты операции
        bool isOk;
        string message;

        try {

            // изменение записи в БД
            _db.Specializations.Update(specializationEdt);
            await _db.SaveChangesAsync();

            isOk = true;
            message = "Ok";

        } catch (Exception ex) {

            isOk = false;
            message = ex.Message;

        } // try-catch

        return (isOk, message);

    } // UpdateSpecializationAsync



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

    // 10.2. добавить новую запись о должности в БД
    public async Task<(bool, string)> CreatePositionAsync(Position newPosition) {

        // вернём из метода результаты операции
        bool isOk;
        string message;

        try {

            // добавление записи в БД
            await _db.Positions.AddAsync(newPosition);
            await _db.SaveChangesAsync();

            isOk = true;
            message = "Ok";

        } catch (Exception ex) {

            isOk = false;
            message = ex.Message;

        } // try-catch

        return (isOk, message);

    } // CreatePositionAsync

    // 10.3. изменить данные о должности в БД
    public async Task<(bool, string)> UpdatePositionAsync(Position positionEdt) {

        // вернём из метода результаты операции
        bool isOk;
        string message;

        try {

            // изменение записи в БД
            _db.Positions.Update(positionEdt);
            await _db.SaveChangesAsync();

            isOk = true;
            message = "Ok";

        } catch (Exception ex) {

            isOk = false;
            message = ex.Message;

        } // try-catch

        return (isOk, message);

    } // UpdatePositionAsync



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

    // 11.2. добавить новую запись о категории услуг в БД
    public async Task<(bool, string)> CreateServicesCategoryAsync(ServicesCategory newServicesCategory) {

        // вернём из метода результаты операции
        bool isOk;
        string message;

        try {

            // добавление записи в БД
            await _db.ServicesCategories.AddAsync(newServicesCategory);
            await _db.SaveChangesAsync();

            isOk = true;
            message = "Ok";

        } catch (Exception ex) {

            isOk = false;
            message = ex.Message;

        } // try-catch

        return (isOk, message);

    } // CreateServicesCategoryAsync

    // 11.3. изменить данные о категории услуг в БД
    public async Task<(bool, string)> UpdateServicesCategoryAsync(ServicesCategory servicesCategoryEdt) {

        // вернём из метода результаты операции
        bool isOk;
        string message;

        try {

            // изменение записи в БД
            _db.ServicesCategories.Update(servicesCategoryEdt);
            await _db.SaveChangesAsync();

            isOk = true;
            message = "Ok";

        } catch (Exception ex) {

            isOk = false;
            message = ex.Message;

        } // try-catch

        return (isOk, message);

    } // UpdateServicesCategoryAsync



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

    // 12.2. получить все записи об услугах для заданной компании из БД
    public async Task<List<Service>> GetAllServicesByCompanyIdAsync(int companyId) =>
        await _db.Services
        .Where(service => service.CompanyId == companyId && service.Deleted == null)
        .ToListAsync();

    // 12.3. добавить новую запись об услуге в БД
    public async Task<(bool, string)> CreateServiceAsync(Service newService) {

        // вернём из метода результаты операции
        bool isOk;
        string message;

        try {

            // добавление записи в БД
            await _db.Services.AddAsync(newService);
            await _db.SaveChangesAsync();

            isOk = true;
            message = "Ok";

        } catch (Exception ex) {

            isOk = false;
            message = ex.Message;

        } // try-catch

        return (isOk, message);

    } // CreateServiceAsync

    // 12.4. изменить данные об услуге в БД
    public async Task<(bool, string)> UpdateServiceAsync(Service serviceEdt) {

        // вернём из метода результаты операции
        bool isOk;
        string message;

        try {

            // изменение записи в БД
            _db.Services.Update(serviceEdt);
            await _db.SaveChangesAsync();

            isOk = true;
            message = "Ok";

        } catch (Exception ex) {

            isOk = false;
            message = ex.Message;

        } // try-catch

        return (isOk, message);

    } // UpdateServiceAsync



    // 13. таблица "СОТРУДНИКИ"
    // 13.1.1 получить все записи таблицы "СОТРУДНИКИ" из БД
    public async Task<List<Employee>> GetAllEmployeesAsync() =>
        await _db.Employees
        .Where(employee => employee.Deleted == null)
        .ToListAsync();

    // 13.1.2. получить все(включая удалённые) записи таблицы "СОТРУДНИКИ" из БД
    public async Task<List<Employee>> GetAllEmployeesWithDeletedAsync() =>
        await _db.Employees.ToListAsync();

    // 13.1.3. получить все удалённые записи таблицы "СОТРУДНИКИ" из БД
    public async Task<List<Employee>> GetAllDeletedEmployeesAsync() =>
        await _db.Employees
        .Where(employee => employee.Deleted != null)
        .ToListAsync();

    // 13.2. получить все записи о сотрудниках для заданной компании из БД
    public async Task<List<Employee>> GetAllEmployeesByCompanyIdAsync(int companyId) =>
        await _db.Employees
        .Where(employee => employee.CompanyId == companyId && employee.Deleted == null)
        .ToListAsync();

    // 13.3. добавить новую запись о сотруднике в БД
    public async Task<(bool, string)> CreateEmployeeAsync(Employee newEmployee) {

        // вернём из метода результаты операции
        bool isOk;
        string message;

        try {

            // добавление записи в БД
            await _db.Employees.AddAsync(newEmployee);
            await _db.SaveChangesAsync();

            isOk = true;
            message = "Ok";

        } catch (Exception ex) {

            isOk = false;
            message = ex.Message;

        } // try-catch

        return (isOk, message);

    } // CreateEmployeeAsync

    // 13.4. изменить данные о сотруднике в БД
    public async Task<(bool, string)> UpdateEmployeeAsync(Employee employeeEdt) {

        // вернём из метода результаты операции
        bool isOk;
        string message;

        try {

            // изменение записи в БД
            _db.Employees.Update(employeeEdt);
            await _db.SaveChangesAsync();

            isOk = true;
            message = "Ok";

        } catch (Exception ex) {

            isOk = false;
            message = ex.Message;

        } // try-catch

        return (isOk, message);

    } // UpdateEmployeeAsync



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

    // 14.2. добавить новую запись об услуге сотрудника в БД
    public async Task<(bool, string)> CreateEmployeeServiceAsync(
        EmployeeService newEmployeeService) {

        // вернём из метода результаты операции
        bool isOk;
        string message;

        try {

            // добавление записи в БД
            await _db.EmployeesServices.AddAsync(newEmployeeService);
            await _db.SaveChangesAsync();

            isOk = true;
            message = "Ok";

        } catch (Exception ex) {

            isOk = false;
            message = ex.Message;

        } // try-catch

        return (isOk, message);

    } // CreateEmployeeServiceAsync

    // 14.3. изменить данные об услуге сотрудника в БД
    public async Task<(bool, string)> UpdateEmployeeServiceAsync(
        EmployeeService employeeServiceEdt) {

        // вернём из метода результаты операции
        bool isOk;
        string message;

        try {

            // изменение записи в БД
            _db.EmployeesServices.Update(employeeServiceEdt);
            await _db.SaveChangesAsync();

            isOk = true;
            message = "Ok";

        } catch (Exception ex) {

            isOk = false;
            message = ex.Message;

        } // try-catch

        return (isOk, message);

    } // UpdateEmployeeServiceAsync



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

    // 15.2. получить все записи о клиентах заданной компании из БД
    public async Task<List<Client>> GetAllClientsByCompanyIdAsync(int companyId) {

        var employees = await _db.Employees
        .Where(employee => employee.CompanyId == companyId)
        .ToListAsync();

        List<Client> clients = [];

        employees.ForEach(employee => clients.AddRange(employee.Clients));

        clients = clients
            .Distinct()
            .OrderBy(client => client.Surname)
            .ToList();

        return clients;

    } // GetAllClientsByCompanyIdAsync

    // 15.3. добавить новую запись о клиенте в БД
    public async Task<(bool, string)> CreateClientAsync(Client newClient) {

        // вернём из метода результаты операции
        bool isOk;
        string message;

        try {

            // добавление записи в БД
            await _db.Clients.AddAsync(newClient);
            await _db.SaveChangesAsync();

            isOk = true;
            message = "Ok";

        } catch (Exception ex) {

            isOk = false;
            message = ex.Message;

        } // try-catch

        return (isOk, message);

    } // CreateClientAsync



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

    // 16.2. получить все записи о записях на сеанс заданной компании из БД
    public async Task<List<Record>> GetAllRecordsByCompanyIdAsync(int companyId) {

        var employees = await _db.Employees
        .Where(employee => employee.CompanyId == companyId)
        .ToListAsync();

        List<Record> records = [];

        employees.ForEach(employee => records.AddRange(employee.Records));

        records = [.. records
            .OrderBy(record => record.Date)];

        return records;

    } // GetAllRecordsByCompanyIdAsync

    // 16.3. добавить новую запись о записи на сеанс в БД
    public async Task<(bool, string)> CreateRecordAsync(Record newRecord) {

        // вернём из метода результаты операции
        bool isOk;
        string message;

        try {

            // добавление записи в БД
            await _db.Records.AddAsync(newRecord);
            await _db.SaveChangesAsync();

            isOk = true;
            message = "Ok";

        } catch (Exception ex) {

            isOk = false;
            message = ex.Message;

        } // try-catch

        return (isOk, message);

    } // CreateRecordAsync



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
        await _db.Schedule
        .Where(workDay => workDay.Deleted == null)
        .ToListAsync();

    // 18.1.2. получить все(включая удалённые) записи таблицы "РАСПИСАНИЕ" из БД
    public async Task<List<WorkDay>> GetAllScheduleWithDeletedAsync() =>
        await _db.Schedule.ToListAsync();

    // 18.1.3. получить все удалённые записи таблицы "РАСПИСАНИЕ" из БД
    public async Task<List<WorkDay>> GetAllDeletedScheduleAsync() =>
        await _db.Schedule
        .Where(workDay => workDay.Deleted != null)
        .ToListAsync();

    // 18.2. получить все записи о рабочих днях
    // заданного сотрудника за заданный период из БД
    public async Task<List<WorkDay>> GetAllScheduleByEmployeeIdFromToAsync(
        int employeeId, DateTime firstDay, DateTime lastDay) =>
        await _db.Schedule
        .Where(workDay => workDay.EmployeeId == employeeId &&
               workDay.Deleted == null &&
               workDay.Date >= firstDay &&
               workDay.Date <= lastDay)
        .ToListAsync();

    // 18.3. добавить новую запись о рабочем дне сотрудника в БД
    public async Task<(bool, string)> CreateWorkDayAsync(WorkDay newWorkDay) {

        // вернём из метода результаты операции
        bool isOk;
        string message;

        try {

            // добавление записи в БД
            await _db.Schedule.AddAsync(newWorkDay);
            await _db.SaveChangesAsync();

            isOk = true;
            message = "Ok";

        } catch (Exception ex) {

            isOk = false;
            message = ex.Message;

        } // try-catch

        return (isOk, message);

    } // CreateWorkDayAsync

    // 18.4. изменить данные о рабочем дне сотрудника в БД
    public async Task<(bool, string)> UpdateWorkDayAsync(WorkDay workDayEdt) {

        // вернём из метода результаты операции
        bool isOk;
        string message;

        try {

            // изменение записи в БД
            _db.Schedule.Update(workDayEdt);
            await _db.SaveChangesAsync();

            isOk = true;
            message = "Ok";

        } catch (Exception ex) {

            isOk = false;
            message = ex.Message;

        } // try-catch

        return (isOk, message);

    } // UpdateWorkDayAsync



    // 19. таблица "ПРОМЕЖУТКИ_ВРЕМЕНИ"
    // 19.1.1 получить все записи таблицы "ПРОМЕЖУТКИ_ВРЕМЕНИ" из БД
    public async Task<List<Slot>> GetAllSlotsAsync() =>
        await _db.Slots
        .Where(slot => slot.Deleted == null)
        .ToListAsync();

    // 19.1.2. получить все(включая удалённые) записи таблицы "ПРОМЕЖУТКИ_ВРЕМЕНИ" из БД
    public async Task<List<Slot>> GetAllSlotsWithDeletedAsync() =>
        await _db.Slots.ToListAsync();

    // 19.1.3. получить все удалённые записи таблицы "ПРОМЕЖУТКИ_ВРЕМЕНИ" из БД
    public async Task<List<Slot>> GetAllDeletedSlotsAsync() =>
        await _db.Slots
        .Where(slot => slot.Deleted != null)
        .ToListAsync();

    // 19.2. добавить новую запись о промежутке времени в БД
    public async Task<(bool, string)> CreateSlotAsync(Slot newSlot) {

        // вернём из метода результаты операции
        bool isOk;
        string message;

        try {

            // добавление записи в БД
            await _db.Slots.AddAsync(newSlot);
            await _db.SaveChangesAsync();

            isOk = true;
            message = "Ok";

        } catch (Exception ex) {

            isOk = false;
            message = ex.Message;

        } // try-catch

        return (isOk, message);

    } // CreateSlotAsync



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

    // 20.2. добавить новую запись о промежутке времени свободного
    // для записи клиентов конкретного рабочего дня сотрудника в БД
    public async Task<(bool, string)> CreateWorkDayFreeSlotAsync(
        WorkDayFreeSlot newWorkDayFreeSlot) {

        // вернём из метода результаты операции
        bool isOk;
        string message;

        try {

            // добавление записи в БД
            await _db.WorkDaysFreeSlots.AddAsync(newWorkDayFreeSlot);
            await _db.SaveChangesAsync();

            isOk = true;
            message = "Ok";

        } catch (Exception ex) {

            isOk = false;
            message = ex.Message;

        } // try-catch

        return (isOk, message);

    } // CreateWorkDayFreeSlotAsync

    // 20.3. изменить запись о промежутке времени свободного
    // для записи клиентов конкретного рабочего дня сотрудника в БД
    public async Task<(bool, string)> UpdateWorkDayFreeSlotAsync(
        WorkDayFreeSlot workDayFreeSlotEdt) {

        // вернём из метода результаты операции
        bool isOk;
        string message;

        try {

            // изменение записи в БД
            _db.WorkDaysFreeSlots.Update(workDayFreeSlotEdt);
            await _db.SaveChangesAsync();

            isOk = true;
            message = "Ok";

        } catch (Exception ex) {

            isOk = false;
            message = ex.Message;

        } // try-catch

        return (isOk, message);

    } // UpdateWorkDayFreeSlotAsync

    // 20.4. получить коллекцию промежутков времени свободного
    // для записи клиентов конкретного рабочего дня заданного сотрудника в БД
    public async Task<List<Slot>> GetAllFreeSlotsByEmployeeIdByDateAsync(
        int employeeId, DateTime date) =>
        (await _db.Schedule
        .FirstOrDefaultAsync(workDay => workDay.EmployeeId == employeeId &&
                             workDay.Deleted == null &&
                             workDay.Date == date)
        ?? new WorkDay())
        .WorkDaysFreeSlots
        .Where(workDayFreeSlot => workDayFreeSlot.Deleted == null)
        .Select(workDayFreeSlot => workDayFreeSlot.Slot)
        .ToList();



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

    // 21.2. добавить новую запись о промежутке времени
    // для перерыва конкретного рабочего дня сотрудника в БД
    public async Task<(bool, string)> CreateWorkDayBreakSlotAsync(
        WorkDayBreakSlot newWorkDayBreakSlot) {

        // вернём из метода результаты операции
        bool isOk;
        string message;

        try {

            // добавление записи в БД
            await _db.WorkDaysBreakSlots.AddAsync(newWorkDayBreakSlot);
            await _db.SaveChangesAsync();

            isOk = true;
            message = "Ok";

        } catch (Exception ex) {

            isOk = false;
            message = ex.Message;

        } // try-catch

        return (isOk, message);

    } // CreateWorkDayBreakSlotAsync

    // 21.3. изменить запись о промежутке времени
    // для перерыва конкретного рабочего дня сотрудника в БД
    public async Task<(bool, string)> UpdateWorkDayBreakSlotAsync(
        WorkDayBreakSlot workDayBreakSlotEdt) {

        // вернём из метода результаты операции
        bool isOk;
        string message;

        try {

            // изменение записи в БД
            _db.WorkDaysBreakSlots.Update(workDayBreakSlotEdt);
            await _db.SaveChangesAsync();

            isOk = true;
            message = "Ok";

        } catch (Exception ex) {

            isOk = false;
            message = ex.Message;

        } // try-catch

        return (isOk, message);

    } // UpdateWorkDayBreakSlotAsync

} // class DbRepository