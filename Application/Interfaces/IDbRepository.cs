using Domain.Models.Entities;

namespace Application.Interfaces;

// интерфейс репозитория данных базы данных
public interface IDbRepository
{
    // 1. таблица "РОЛИ"
    // 1.1.1. получить все записи таблицы "РОЛИ" из БД
    Task<List<Role>> GetAllRolesAsync();

    // 1.1.2. получить все(включая удалённые) записи таблицы "РОЛИ" из БД
    Task<List<Role>> GetAllRolesWithDeletedAsync();

    // 1.1.3. получить все удалённые записи таблицы "РОЛИ" из БД
    Task<List<Role>> GetAllDeletedRolesAsync();



    // 2. таблица "ПОЛЬЗОВАТЕЛИ"
    // 2.1.1 получить все записи таблицы "ПОЛЬЗОВАТЕЛИ" из БД
    Task<List<User>> GetAllUsersAsync();

    // 2.1.2. получить все(включая удалённые) записи таблицы "ПОЛЬЗОВАТЕЛИ" из БД
    Task<List<User>> GetAllUsersWithDeletedAsync();

    // 2.1.3. получить все удалённые записи таблицы "ПОЛЬЗОВАТЕЛИ" из БД
    Task<List<User>> GetAllDeletedUsersAsync();

    // 2.2. добавить новую запись о пользователе в БД
    Task CreateUserAsync(User newUser);

    // 2.3. изменить данные пользователя в БД
    Task UpdateUserAsync(User user);



    // 3. таблица "ПОЛЬЗОВАТЕЛИ_РОЛИ"



    // 4. таблица "СТРАНЫ"
    // 4.1.1 получить все записи таблицы "СТРАНЫ" из БД
    Task<List<Country>> GetAllCountriesAsync();

    // 4.1.2. получить все(включая удалённые) записи таблицы "СТРАНЫ" из БД
    Task<List<Country>> GetAllCountriesWithDeletedAsync();

    // 4.1.3. получить все удалённые записи таблицы "СТРАНЫ" из БД
    Task<List<Country>> GetAllDeletedCountriesAsync();



    // 5. таблица "ГОРОДА"
    // 5.1.1 получить все записи таблицы "ГОРОДА" из БД
    Task<List<City>> GetAllCitiesAsync();

    // 5.1.2. получить все(включая удалённые) записи таблицы "ГОРОДА" из БД
    Task<List<City>> GetAllCitiesWithDeletedAsync();

    // 5.1.3. получить все удалённые записи таблицы "ГОРОДА" из БД
    Task<List<City>> GetAllDeletedCitiesAsync();

    // 5.2. добавить новую запись о городе в БД
    Task<(bool, string)> CreateCityAsync(City newCity);



    // 6. таблица "УЛИЦЫ"
    // 6.1.1 получить все записи таблицы "УЛИЦЫ" из БД
    Task<List<Street>> GetAllStreetsAsync();

    // 6.1.2. получить все(включая удалённые) записи таблицы "УЛИЦЫ" из БД
    Task<List<Street>> GetAllStreetsWithDeletedAsync();

    // 6.1.3. получить все удалённые записи таблицы "УЛИЦЫ" из БД
    Task<List<Street>> GetAllDeletedStreetsAsync();

    // 6.2. добавить новую запись об улице в БД
    Task<(bool, string)> CreateStreetAsync(Street newStreet);



    // 7. таблица "АДРЕСА"
    // 7.1.1 получить все записи таблицы "АДРЕСА" из БД
    Task<List<Address>> GetAllAddressesAsync();

    // 7.1.2. получить все(включая удалённые) записи таблицы "АДРЕСА" из БД
    Task<List<Address>> GetAllAddressesWithDeletedAsync();

    // 7.1.3. получить все удалённые записи таблицы "АДРЕСА" из БД
    Task<List<Address>> GetAllDeletedAddressesAsync();

    // 7.2. добавить новую запись об адресе в БД
    Task<(bool, string)> CreateAddressAsync(Address newAddress);



    // 8. таблица "КОМПАНИИ"
    // 8.1.1 получить все записи таблицы "КОМПАНИИ" из БД
    Task<List<Company>> GetAllCompaniesAsync();

    // 8.1.2. получить все(включая удалённые) записи таблицы "КОМПАНИИ" из БД
    Task<List<Company>> GetAllCompaniesWithDeletedAsync();

    // 8.1.3. получить все удалённые записи таблицы "КОМПАНИИ" из БД
    Task<List<Company>> GetAllDeletedCompaniesAsync();

    // 8.2. получить все записи о компаниях из БД с заданным
    // пользователем-владельцем
    Task<List<Company>> GetAllCompaniesByUserIdAsync(int userId);

    // 8.3. добавить новую запись о компании в БД
    Task<(bool, string)> CreateCompanyAsync(Company newCompany);

    // 8.4. изменить данные о компании в БД
    Task<(bool, string)> UpdateCompanyAsync(Company companyEdt);



    // 9. таблица "СПЕЦИАЛЬНОСТИ"
    // 9.1.1 получить все записи таблицы "СПЕЦИАЛЬНОСТИ" из БД
    Task<List<Specialization>> GetAllSpecializationsAsync();

    // 9.1.2. получить все(включая удалённые) записи таблицы "СПЕЦИАЛЬНОСТИ" из БД
    Task<List<Specialization>> GetAllSpecializationsWithDeletedAsync();

    // 9.1.3. получить все удалённые записи таблицы "СПЕЦИАЛЬНОСТИ" из БД
    Task<List<Specialization>> GetAllDeletedSpecializationsAsync();

    // 9.2. добавить новую запись о специальности в БД
    Task<(bool, string)> CreateSpecializationAsync(Specialization newSpecialization);

    // 9.3. изменить данные о специальности в БД
    Task<(bool, string)> UpdateSpecializationAsync(Specialization specializationEdt);



    // 10. таблица "ДОЛЖНОСТИ"
    // 10.1.1 получить все записи таблицы "ДОЛЖНОСТИ" из БД
    Task<List<Position>> GetAllPositionsAsync();

    // 10.1.2. получить все(включая удалённые) записи таблицы "ДОЛЖНОСТИ" из БД
    Task<List<Position>> GetAllPositionsWithDeletedAsync();

    // 10.1.3. получить все удалённые записи таблицы "ДОЛЖНОСТИ" из БД
    Task<List<Position>> GetAllDeletedPositionsAsync();

    // 10.2. добавить новую запись о должности в БД
    Task<(bool, string)> CreatePositionAsync(Position newPosition);

    // 10.3. изменить данные о должности в БД
    Task<(bool, string)> UpdatePositionAsync(Position positionEdt);



    // 11. таблица "КАТЕГОРИИ_УСЛУГ"
    // 11.1.1 получить все записи таблицы "КАТЕГОРИИ_УСЛУГ" из БД
    Task<List<ServicesCategory>> GetAllServicesCategoriesAsync();

    // 11.1.2. получить все(включая удалённые) записи таблицы "КАТЕГОРИИ_УСЛУГ" из БД
    Task<List<ServicesCategory>> GetAllServicesCategoriesWithDeletedAsync();

    // 11.1.3. получить все удалённые записи таблицы "КАТЕГОРИИ_УСЛУГ" из БД
    Task<List<ServicesCategory>> GetAllDeletedServicesCategoriesAsync();

    // 11.2. добавить новую запись о категории услуг в БД
    Task<(bool, string)> CreateServicesCategoryAsync(ServicesCategory newServicesCategory);

    // 11.3. изменить данные о категории услуг в БД
    Task<(bool, string)> UpdateServicesCategoryAsync(ServicesCategory servicesCategoryEdt);



    // 12. таблица "УСЛУГИ"
    // 12.1.1 получить все записи таблицы "УСЛУГИ" из БД
    Task<List<Service>> GetAllServicesAsync();

    // 12.1.2. получить все(включая удалённые) записи таблицы "УСЛУГИ" из БД
    Task<List<Service>> GetAllServicesWithDeletedAsync();

    // 12.1.3. получить все удалённые записи таблицы "УСЛУГИ" из БД
    Task<List<Service>> GetAllDeletedServicesAsync();

    // 12.2. получить все записи об услугах для заданной компании из БД
    Task<List<Service>> GetAllServicesByCompanyIdAsync(int companyId);

    // 12.3. добавить новую запись об услуге в БД
    Task<(bool, string)> CreateServiceAsync(Service newService);

    // 12.4. изменить данные об услуге в БД
    Task<(bool, string)> UpdateServiceAsync(Service serviceEdt);



    // 13. таблица "СОТРУДНИКИ"
    // 13.1.1 получить все записи таблицы "СОТРУДНИКИ" из БД
    Task<List<Employee>> GetAllEmployeesAsync();

    // 13.1.2. получить все(включая удалённые) записи таблицы "СОТРУДНИКИ" из БД
    Task<List<Employee>> GetAllEmployeesWithDeletedAsync();

    // 13.1.3. получить все удалённые записи таблицы "СОТРУДНИКИ" из БД
    Task<List<Employee>> GetAllDeletedEmployeesAsync();

    // 13.2. получить все записи о сотрудниках для заданной компании из БД
    Task<List<Employee>> GetAllEmployeesByCompanyIdAsync(int companyId);

    // 13.3. добавить новую запись о сотруднике в БД
    Task<(bool, string)> CreateEmployeeAsync(Employee newEmployee);

    // 13.4. изменить данные о сотруднике в БД
    Task<(bool, string)> UpdateEmployeeAsync(Employee employeeEdt);



    // 14. таблица "СОТРУДНИКИ_УСЛУГИ"
    // 14.1.1 получить все записи таблицы "СОТРУДНИКИ_УСЛУГИ" из БД
    Task<List<EmployeeService>> GetAllEmployeesServicesAsync();

    // 14.1.2. получить все(включая удалённые) записи таблицы "СОТРУДНИКИ_УСЛУГИ" из БД
    Task<List<EmployeeService>> GetAllEmployeesServicesWithDeletedAsync();

    // 14.1.3. получить все удалённые записи таблицы "СОТРУДНИКИ_УСЛУГИ" из БД
    Task<List<EmployeeService>> GetAllDeletedEmployeesServicesAsync();



    // 15. таблица "КЛИЕНТЫ"
    // 15.1.1 получить все записи таблицы "КЛИЕНТЫ" из БД
    Task<List<Client>> GetAllClientsAsync();

    // 15.1.2. получить все(включая удалённые) записи таблицы "КЛИЕНТЫ" из БД
    Task<List<Client>> GetAllClientsWithDeletedAsync();

    // 15.1.3. получить все удалённые записи таблицы "КЛИЕНТЫ" из БД
    Task<List<Client>> GetAllDeletedClientsAsync();



    // 16. таблица "ЗАПИСИ_НА_СЕАНС"
    // 16.1.1 получить все записи таблицы "ЗАПИСИ_НА_СЕАНС" из БД
    Task<List<Record>> GetAllRecordsAsync();

    // 16.1.2. получить все(включая удалённые) записи таблицы "ЗАПИСИ_НА_СЕАНС" из БД
    Task<List<Record>> GetAllRecordsWithDeletedAsync();

    // 16.1.3. получить все удалённые записи таблицы "ЗАПИСИ_НА_СЕАНС" из БД
    Task<List<Record>> GetAllDeletedRecordsAsync();



    // 17. таблица "ЗАПИСИ_УСЛУГИ"
    // 17.1.1 получить все записи таблицы "ЗАПИСИ_УСЛУГИ" из БД
    Task<List<RecordService>> GetAllRecordsServicesAsync();

    // 17.1.2. получить все(включая удалённые) записи таблицы "ЗАПИСИ_УСЛУГИ" из БД
    Task<List<RecordService>> GetAllRecordsServicesWithDeletedAsync();

    // 17.1.3. получить все удалённые записи таблицы "ЗАПИСИ_УСЛУГИ" из БД
    Task<List<RecordService>> GetAllDeletedRecordsServicesAsync();



    // 18. таблица "РАСПИСАНИЕ"
    // 18.1.1 получить все записи таблицы "РАСПИСАНИЕ" из БД
    Task<List<WorkDay>> GetAllScheduleAsync();

    // 18.1.2. получить все(включая удалённые) записи таблицы "РАСПИСАНИЕ" из БД
    Task<List<WorkDay>> GetAllScheduleWithDeletedAsync();

    // 18.1.3. получить все удалённые записи таблицы "РАСПИСАНИЕ" из БД
    Task<List<WorkDay>> GetAllDeletedScheduleAsync();



    // 19. таблица "ПРОМЕЖУТКИ_ВРЕМЕНИ"
    // 19.1.1 получить все записи таблицы "ПРОМЕЖУТКИ_ВРЕМЕНИ" из БД
    Task<List<Slot>> GetAllSlotsAsync();

    // 19.1.2. получить все(включая удалённые) записи таблицы "ПРОМЕЖУТКИ_ВРЕМЕНИ" из БД
    Task<List<Slot>> GetAllSlotsWithDeletedAsync();

    // 19.1.3. получить все удалённые записи таблицы "ПРОМЕЖУТКИ_ВРЕМЕНИ" из БД
    Task<List<Slot>> GetAllDeletedSlotsAsync();



    // 20. таблица "РАСПИСАНИЕ_СВОБОДНЫЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ"
    // 20.1.1 получить все записи таблицы
    // "РАСПИСАНИЕ_СВОБОДНЫЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ" из БД
    Task<List<WorkDayFreeSlot>> GetAllWorkDaysFreeSlotsAsync();

    // 20.1.2. получить все(включая удалённые) записи таблицы
    // "РАСПИСАНИЕ_СВОБОДНЫЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ" из БД
    Task<List<WorkDayFreeSlot>> GetAllWorkDaysFreeSlotsWithDeletedAsync();

    // 20.1.3. получить все удалённые записи таблицы
    // "РАСПИСАНИЕ_СВОБОДНЫЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ" из БД
    Task<List<WorkDayFreeSlot>> GetAllDeletedWorkDaysFreeSlotsAsync();



    // 21. таблица "РАСПИСАНИЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ_ДЛЯ_ПЕРЕРЫВОВ"
    // 21.1.1 получить все записи таблицы
    // "РАСПИСАНИЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ_ДЛЯ_ПЕРЕРЫВОВ" из БД
    Task<List<WorkDayBreakSlot>> GetAllWorkDaysBreakSlotsAsync();

    // 21.1.2. получить все(включая удалённые) записи таблицы
    // "РАСПИСАНИЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ_ДЛЯ_ПЕРЕРЫВОВ" из БД
    Task<List<WorkDayBreakSlot>> GetAllWorkDaysBreakSlotsWithDeletedAsync();

    // 21.1.3. получить все удалённые записи таблицы
    // "РАСПИСАНИЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ_ДЛЯ_ПЕРЕРЫВОВ" из БД
    Task<List<WorkDayBreakSlot>> GetAllDeletedWorkDaysBreakSlotsAsync();

} // interface IDbRepository