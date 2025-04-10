using Application.Interfaces;
using Domain.Models.Dto;
using Domain.Models.Entities;

namespace Application.Services;

// сервис-поставщик данных из базы данных,
// использующего бизнес-логику
public class DbService(IDbRepository dbRepository) : IDbService
{
    // ссылка на репозиторий базы данных
    private readonly IDbRepository _dbRepository = dbRepository;


    // 1. таблица "РОЛИ"
    // 1.1.1. получить все записи таблицы "РОЛИ" из БД
    public async Task<List<Role>> GetAllRolesAsync() =>
        await _dbRepository.GetAllRolesAsync();

    // 1.1.2. получить все(включая удалённые) записи таблицы "РОЛИ" из БД
    public async Task<List<Role>> GetAllRolesWithDeletedAsync() =>
        await _dbRepository.GetAllRolesWithDeletedAsync();

    // 1.1.3. получить все удалённые записи таблицы "РОЛИ" из БД
    public async Task<List<Role>> GetAllDeletedRolesAsync() =>
        await _dbRepository.GetAllDeletedRolesAsync();



    // 2. таблица "ПОЛЬЗОВАТЕЛИ"
    // 2.1.1. получить все записи таблицы "ПОЛЬЗОВАТЕЛИ" из БД
    public async Task<List<User>> GetAllUsersAsync() =>
        await _dbRepository.GetAllUsersAsync();

    // 2.1.2. получить все(включая удалённые) записи таблицы "ПОЛЬЗОВАТЕЛИ" из БД
    public async Task<List<User>> GetAllUsersWithDeletedAsync() =>
        await _dbRepository.GetAllUsersWithDeletedAsync();

    // 2.1.3. получить все удалённые записи таблицы "ПОЛЬЗОВАТЕЛИ" из БД
    public async Task<List<User>> GetAllDeletedUsersAsync() =>
        await _dbRepository.GetAllDeletedUsersAsync();

    // 2.2. получить запись о пользователе из БД по Id
    // (если запись не найдена - вернуть new User() с Id=0)
    public async Task<User> GetUserByIdAsync(int userId) =>
        (await GetAllUsersAsync())
        .FirstOrDefault(user => user.Id == userId)
        ?? new User() { Id = 0 };

    // 2.3. получить пользователя по логину и паролю
    // (если пользователь не найден - вернуть new User() с Id=0)
    /*public async Task<User> GetUserToLoginAsync(string login, string password) =>
        (await GetAllUsersAsync())
        .Find(user => user.Login == login && user.Password == password)
        ?? new User() { Id = 0 };*/

    // 2.4. получить зарегистрированного пользователя с совпадающим логином
    // (если пользователь не найден - вернуть new User() с Id=0)
    /*public async Task<User> GetUserByLoginAsync(string login) =>
        (await GetAllUsersAsync())
        .Find(user => user.Login == login)
        ?? new User() { Id = 0 };*/

    // 2.4. получить зарегистрированного пользователя с совпадающим телефоном
    // (если пользователь не найден - вернуть new User() с Id=0)
    public async Task<User> GetUserByPhoneAsync(string phone) =>
        (await GetAllUsersAsync())
        .Find(user => user.Phone == phone)
        ?? new User() { Id = 0 };

    // 2.5. получить зарегистрированного пользователя с совпадающим email
    // (если пользователь не найден - вернуть new User() с Id=0)
    public async Task<User> GetUserByEmailAsync(string email) =>
        (await GetAllUsersAsync())
        .Find(user => user.Email == email)
        ?? new User() { Id = 0 };

    // 2.6. добавить новую запись о пользователе в БД
    public async Task CreateUserAsync(User newUser) =>
        await _dbRepository.CreateUserAsync(newUser);

    // 2.7. изменить данные пользователя в БД
    public async Task UpdateUserAsync(User user) =>
        await _dbRepository.UpdateUserAsync(user);



    // 3. таблица "ПОЛЬЗОВАТЕЛИ_РОЛИ"



    // 4. таблица "СТРАНЫ"
    // 4.1.1 получить все записи таблицы "СТРАНЫ" из БД
    public async Task<List<Country>> GetAllCountriesAsync() =>
        await _dbRepository.GetAllCountriesAsync();

    // 4.1.2. получить все(включая удалённые) записи таблицы "СТРАНЫ" из БД
    public async Task<List<Country>> GetAllCountriesWithDeletedAsync() =>
        await _dbRepository.GetAllCountriesWithDeletedAsync();

    // 4.1.3. получить все удалённые записи таблицы "СТРАНЫ" из БД
    public async Task<List<Country>> GetAllDeletedCountriesAsync() =>
        await _dbRepository.GetAllDeletedCountriesAsync();

    // 4.2. получить запись о стране из БД по Id
    // (если запись не найдена - вернуть new Country() с Id=0)
    public async Task<Country> GetCountryByIdAsync(int countryId) =>
        (await GetAllCountriesAsync())
        .FirstOrDefault(country => country.Id == countryId)
        ?? new Country() { Id = 0 };



    // 5. таблица "ГОРОДА"
    // 5.1.1 получить все записи таблицы "ГОРОДА" из БД
    public async Task<List<City>> GetAllCitiesAsync() =>
        await _dbRepository.GetAllCitiesAsync();

    // 5.1.2. получить все(включая удалённые) записи таблицы "ГОРОДА" из БД
    public async Task<List<City>> GetAllCitiesWithDeletedAsync() =>
        await _dbRepository.GetAllCitiesWithDeletedAsync();

    // 5.1.3. получить все удалённые записи таблицы "ГОРОДА" из БД
    public async Task<List<City>> GetAllDeletedCitiesAsync() =>
        await _dbRepository.GetAllDeletedCitiesAsync();

    // 5.2. получить запись о городе из БД по Id
    // (если запись не найдена - вернуть new City() с Id=0)
    public async Task<City> GetCityByIdAsync(int cityId) =>
        (await GetAllCitiesAsync())
        .FirstOrDefault(city => city.Id == cityId)
        ?? new City() { Id = 0 };

    // 5.3. добавить новую запись о городе в БД
    public async Task<(bool, string)> CreateCityAsync(City newCity) =>
        await _dbRepository.CreateCityAsync(newCity);



    // 6. таблица "УЛИЦЫ"
    // 6.1.1 получить все записи таблицы "УЛИЦЫ" из БД
    public async Task<List<Street>> GetAllStreetsAsync() =>
        await _dbRepository.GetAllStreetsAsync();

    // 6.1.2. получить все(включая удалённые) записи таблицы "УЛИЦЫ" из БД
    public async Task<List<Street>> GetAllStreetsWithDeletedAsync() =>
        await _dbRepository.GetAllStreetsWithDeletedAsync();

    // 6.1.3. получить все удалённые записи таблицы "УЛИЦЫ" из БД
    public async Task<List<Street>> GetAllDeletedStreetsAsync() =>
        await _dbRepository.GetAllDeletedStreetsAsync();

    // 6.2. получить запись об улице из БД по Id
    // (если запись не найдена - вернуть new Street() с Id=0)
    public async Task<Street> GetStreetByIdAsync(int streetId) =>
        (await GetAllStreetsAsync())
        .FirstOrDefault(street => street.Id == streetId)
        ?? new Street() { Id = 0 };

    // 6.3. добавить новую запись об улице в БД
    public async Task<(bool, string)> CreateStreetAsync(Street newStreet) =>
        await _dbRepository.CreateStreetAsync(newStreet);



    // 7. таблица "АДРЕСА"
    // 7.1.1 получить все записи таблицы "АДРЕСА" из БД
    public async Task<List<Address>> GetAllAddressesAsync() =>
        await _dbRepository.GetAllAddressesAsync();

    // 7.1.2. получить все(включая удалённые) записи таблицы "АДРЕСА" из БД
    public async Task<List<Address>> GetAllAddressesWithDeletedAsync() =>
        await _dbRepository.GetAllAddressesWithDeletedAsync();

    // 7.1.3. получить все удалённые записи таблицы "АДРЕСА" из БД
    public async Task<List<Address>> GetAllDeletedAddressesAsync() =>
        await _dbRepository.GetAllDeletedAddressesAsync();

    // 7.2. получить запись об адресе из БД по всем параметрам
    // (если запись не найдена - вернуть new Address() с Id=0)
    public async Task<Address> GetAddressByParamsAsync(/*int countryId,*/
        int cityId, int streetId, string building, int? flat) =>
        (await GetAllAddressesAsync())
        .FirstOrDefault(
            address =>
            /*address.City.Country.Id == countryId &&*/
            address.City.Id         == cityId &&
            address.Street.Id       == streetId &&
            address.Building        == building &&
            address.Flat            == flat)
        ?? new Address() { Id = 0 };

    // 7.3. добавить новую запись об адресе в БД
    public async Task<(bool, string)> CreateAddressAsync(Address newAddress) =>
        await _dbRepository.CreateAddressAsync(newAddress);



    // 8. таблица "КОМПАНИИ"
    // 8.1.1 получить все записи таблицы "КОМПАНИИ" из БД
    public async Task<List<Company>> GetAllCompaniesAsync() =>
        await _dbRepository.GetAllCompaniesAsync();

    // 8.1.2. получить все(включая удалённые) записи таблицы "КОМПАНИИ" из БД
    public async Task<List<Company>> GetAllCompaniesWithDeletedAsync() =>
        await _dbRepository.GetAllCompaniesWithDeletedAsync();

    // 8.1.3. получить все удалённые записи таблицы "КОМПАНИИ" из БД
    public async Task<List<Company>> GetAllDeletedCompaniesAsync() =>
        await _dbRepository.GetAllDeletedCompaniesAsync();

    // 8.2. получить все записи о компаниях из БД с заданным
    // пользователем-владельцем
    public async Task<List<Company>> GetAllCompaniesByUserIdAsync(int userId) =>
        await _dbRepository.GetAllCompaniesByUserIdAsync(userId);

    // 8.3. получить запись о компании из БД по Id
    // (если запись не найдена - вернуть new Company() с Id=0)
    public async Task<Company> GetCompanyByIdAsync(int companyId) =>
        (await GetAllCompaniesAsync())
        .FirstOrDefault(company => company.Id == companyId)
        ?? new Company() { Id = 0 };

    // 8.4. добавить новую запись о компании в БД
    public async Task<(bool, string)> CreateCompanyAsync(Company newCompany) =>
        await _dbRepository.CreateCompanyAsync(newCompany);

    // 8.5. изменить данные о компании в БД
    public async Task<(bool, string)> UpdateCompanyAsync(Company companyEdt) =>
        await _dbRepository.UpdateCompanyAsync(companyEdt);



    // 9. таблица "СПЕЦИАЛЬНОСТИ"
    // 9.1.1 получить все записи таблицы "СПЕЦИАЛЬНОСТИ" из БД
    public async Task<List<Specialization>> GetAllSpecializationsAsync() =>
        await _dbRepository.GetAllSpecializationsAsync();

    // 9.1.2. получить все(включая удалённые) записи таблицы "СПЕЦИАЛЬНОСТИ" из БД
    public async Task<List<Specialization>> GetAllSpecializationsWithDeletedAsync() =>
        await _dbRepository.GetAllSpecializationsWithDeletedAsync();

    // 9.1.3. получить все удалённые записи таблицы "СПЕЦИАЛЬНОСТИ" из БД
    public async Task<List<Specialization>> GetAllDeletedSpecializationsAsync() =>
        await _dbRepository.GetAllDeletedSpecializationsAsync();

    // 9.2. получить запись о специальности из БД по Id
    // (если запись не найдена - вернуть new Specialization() с Id=0)
    public async Task<Specialization> GetSpecializationByIdAsync(int specializationId) =>
        (await GetAllSpecializationsAsync())
        .FirstOrDefault(specialization => specialization.Id == specializationId)
        ?? new Specialization() { Id = 0 };

    // 9.3. добавить новую запись о специальности в БД
    public async Task<(bool, string)> CreateSpecializationAsync(Specialization newSpecialization) =>
        await _dbRepository.CreateSpecializationAsync(newSpecialization);

    // 9.4. изменить данные о специальности в БД
    public async Task<(bool, string)> UpdateSpecializationAsync(Specialization specializationEdt) =>
        await _dbRepository.UpdateSpecializationAsync(specializationEdt);



    // 10. таблица "ДОЛЖНОСТИ"
    // 10.1.1 получить все записи таблицы "ДОЛЖНОСТИ" из БД
    public async Task<List<Position>> GetAllPositionsAsync() =>
        await _dbRepository.GetAllPositionsAsync();

    // 10.1.2. получить все(включая удалённые) записи таблицы "ДОЛЖНОСТИ" из БД
    public async Task<List<Position>> GetAllPositionsWithDeletedAsync() =>
        await _dbRepository.GetAllPositionsWithDeletedAsync();

    // 10.1.3. получить все удалённые записи таблицы "ДОЛЖНОСТИ" из БД
    public async Task<List<Position>> GetAllDeletedPositionsAsync() =>
        await _dbRepository.GetAllDeletedPositionsAsync();

    // 10.2. получить запись о должности из БД по Id
    // (если запись не найдена - вернуть new Position() с Id=0)
    public async Task<Position> GetPositionByIdAsync(int positionId) =>
        (await GetAllPositionsAsync())
        .FirstOrDefault(position => position.Id == positionId)
        ?? new Position() { Id = 0 };

    // 10.3. добавить новую запись о должности в БД
    public async Task<(bool, string)> CreatePositionAsync(Position newPosition) =>
        await _dbRepository.CreatePositionAsync(newPosition);

    // 10.4. изменить данные о должности в БД
    public async Task<(bool, string)> UpdatePositionAsync(Position positionEdt) =>
        await _dbRepository.UpdatePositionAsync(positionEdt);



    // 11. таблица "КАТЕГОРИИ_УСЛУГ"
    // 11.1.1 получить все записи таблицы "КАТЕГОРИИ_УСЛУГ" из БД
    public async Task<List<ServicesCategory>> GetAllServicesCategoriesAsync() =>
        await _dbRepository.GetAllServicesCategoriesAsync();

    // 11.1.2. получить все(включая удалённые) записи таблицы "КАТЕГОРИИ_УСЛУГ" из БД
    public async Task<List<ServicesCategory>> GetAllServicesCategoriesWithDeletedAsync() =>
        await _dbRepository.GetAllServicesCategoriesWithDeletedAsync();

    // 11.1.3. получить все удалённые записи таблицы "КАТЕГОРИИ_УСЛУГ" из БД
    public async Task<List<ServicesCategory>> GetAllDeletedServicesCategoriesAsync() =>
        await _dbRepository.GetAllDeletedServicesCategoriesAsync();

    // 11.2. получить запись о категории услуг из БД по Id
    // (если запись не найдена - вернуть new ServicesCategory() с Id=0)
    public async Task<ServicesCategory> GetServicesCategoryByIdAsync(int servicesCategoryId) =>
        (await GetAllServicesCategoriesAsync())
        .FirstOrDefault(servicesCategory => servicesCategory.Id == servicesCategoryId)
        ?? new ServicesCategory() { Id = 0 };

    // 11.3. добавить новую запись о категории услуг в БД
    public async Task<(bool, string)> CreateServicesCategoryAsync(ServicesCategory newServicesCategory) =>
        await _dbRepository.CreateServicesCategoryAsync(newServicesCategory);

    // 11.4. изменить данные о категории услуг в БД
    public async Task<(bool, string)> UpdateServicesCategoryAsync(ServicesCategory servicesCategoryEdt) =>
        await _dbRepository.UpdateServicesCategoryAsync(servicesCategoryEdt);



    // 12. таблица "УСЛУГИ"
    // 12.1.1 получить все записи таблицы "УСЛУГИ" из БД
    public async Task<List<Service>> GetAllServicesAsync() =>
        await _dbRepository.GetAllServicesAsync();

    // 12.1.2. получить все(включая удалённые) записи таблицы "УСЛУГИ" из БД
    public async Task<List<Service>> GetAllServicesWithDeletedAsync() =>
        await _dbRepository.GetAllServicesWithDeletedAsync();

    // 12.1.3. получить все удалённые записи таблицы "УСЛУГИ" из БД
    public async Task<List<Service>> GetAllDeletedServicesAsync() =>
        await _dbRepository.GetAllDeletedServicesAsync();

    // 12.2. получить все записи об услугах для заданной компании из БД
    public async Task<List<Service>> GetAllServicesByCompanyIdAsync(int companyId) =>
        await _dbRepository.GetAllServicesByCompanyIdAsync(companyId);

    // 12.3. получить все записи об услугах для заданной
    // компании из БД, сгруппированные по категориям услуг (DTO)
    public async Task<List<DisplayServicesCategory>>
        GetAllServicesByCompanyIdGroupByCategoriesAsync(int companyId) =>
        (await GetAllServicesByCompanyIdAsync(companyId))
        .GroupBy(service => service.ServicesCategory,
            (key, group) => new {
                ServicesCategory = key,
                Services = group
                    .OrderBy(service => service.Name)
                    .ToList()
            })
        .Where(group => group.ServicesCategory.Deleted == null)
        .Select(group => new DisplayServicesCategory(
            ServicesCategory.ServicesCategoryToDto(group.ServicesCategory),
            Service.ServicesToDto(group.Services)
            ))
        .OrderBy(displayServicesCategory => displayServicesCategory.ServicesCategory.Name)
        .ToList();

    // 12.4. получить запись об услуге из БД по Id
    // (если запись не найдена - вернуть new Service() с Id=0)
    public async Task<Service> GetServiceByIdAsync(int serviceId) =>
        (await GetAllServicesAsync())
        .FirstOrDefault(service => service.Id == serviceId)
        ?? new Service() { Id = 0 };

    // 12.5. добавить новую запись об услуге в БД
    public async Task<(bool, string)> CreateServiceAsync(Service newService) =>
        await _dbRepository.CreateServiceAsync(newService);

    // 12.6. изменить данные об услуге в БД
    public async Task<(bool, string)> UpdateServiceAsync(Service serviceEdt) =>
        await _dbRepository.UpdateServiceAsync(serviceEdt);



    // 13. таблица "СОТРУДНИКИ"
    // 13.1.1 получить все записи таблицы "СОТРУДНИКИ" из БД
    public async Task<List<Employee>> GetAllEmployeesAsync() =>
        await _dbRepository.GetAllEmployeesAsync();

    // 13.1.2. получить все(включая удалённые) записи таблицы "СОТРУДНИКИ" из БД
    public async Task<List<Employee>> GetAllEmployeesWithDeletedAsync() =>
        await _dbRepository.GetAllEmployeesWithDeletedAsync();

    // 13.1.3. получить все удалённые записи таблицы "СОТРУДНИКИ" из БД
    public async Task<List<Employee>> GetAllDeletedEmployeesAsync() =>
        await _dbRepository.GetAllDeletedEmployeesAsync();

    // 13.2. получить все записи о сотрудниках для заданной компании из БД
    public async Task<List<Employee>> GetAllEmployeesByCompanyIdAsync(int companyId) =>
        await _dbRepository.GetAllEmployeesByCompanyIdAsync(companyId);

    // 13.3. получить запись о сотруднике из БД по Id
    // (если запись не найдена - вернуть new Employee() с Id=0)
    public async Task<Employee> GetEmployeeByIdAsync(int employeeId) =>
        (await GetAllEmployeesAsync())
        .FirstOrDefault(employee => employee.Id == employeeId)
        ?? new Employee() { Id = 0 };

    // 13.4. получить запись о сотруднике из БД по Id пользователя
    // (если запись не найдена - вернуть new Employee() с Id=0)
    public async Task<Employee> GetEmployeeByUserIdAsync(int userId) =>
        (await GetAllEmployeesAsync())
        .FirstOrDefault(employee => employee.UserId == userId)
        ?? new Employee() { Id = 0 };

    // 13.5. получить все записи об услугах заданного сотрудника из БД
    public async Task<List<Service>> GetAllServicesByEmployeeIdAsync(int employeeId) =>
        (await GetEmployeeByIdAsync(employeeId)).EmployeesServices
        .Where(employeeService => employeeService.Deleted == null &&
                                  employeeService.Service.Deleted == null)
        .Select(employeeService => employeeService.Service)
        .ToList();

    // 13.5. получить все записи об услугах заданного сотрудника из БД,
    // сгруппированные по категориям услуг (DTO)
    /*public async Task<List<DisplayServicesCategory>>
        GetAllServicesByEmployeeIdGroupByCategoriesAsync(int employeeId) =>
        (await GetEmployeeByIdAsync(employeeId)).EmployeesServices
        .Where(employeeService => employeeService.Deleted == null &&
                                  employeeService.Service.Deleted == null)
        .Select(employeeService => employeeService.Service)
        .GroupBy(service => service.ServicesCategory,
            (key, group) => new {
                ServicesCategory = key,
                Services = group.ToList()
            })
        .Where(group => group.ServicesCategory.Deleted == null)
        .Select(group => new DisplayServicesCategory(
            ServicesCategory.ServicesCategoryToDto(group.ServicesCategory),
            Service.ServicesToDto(group.Services)
            ))
        .ToList();*/

    // 13.6. добавить новую запись о сотруднике в БД
    public async Task<(bool, string)> CreateEmployeeAsync(Employee newEmployee) =>
        await _dbRepository.CreateEmployeeAsync(newEmployee);

    // 13.7. изменить данные о сотруднике в БД
    public async Task<(bool, string)> UpdateEmployeeAsync(Employee employeeEdt) =>
        await _dbRepository.UpdateEmployeeAsync(employeeEdt);



    // 14. таблица "СОТРУДНИКИ_УСЛУГИ"
    // 14.1.1 получить все записи таблицы "СОТРУДНИКИ_УСЛУГИ" из БД
    public async Task<List<EmployeeService>> GetAllEmployeesServicesAsync() =>
        await _dbRepository.GetAllEmployeesServicesAsync();

    // 14.1.2. получить все(включая удалённые) записи таблицы "СОТРУДНИКИ_УСЛУГИ" из БД
    public async Task<List<EmployeeService>> GetAllEmployeesServicesWithDeletedAsync() =>
        await _dbRepository.GetAllEmployeesServicesWithDeletedAsync();

    // 14.1.3. получить все удалённые записи таблицы "СОТРУДНИКИ_УСЛУГИ" из БД
    public async Task<List<EmployeeService>> GetAllDeletedEmployeesServicesAsync() =>
        await _dbRepository.GetAllDeletedEmployeesServicesAsync();

    // 14.2. получить запись об услуге сотрудника из БД по Id сотрудника и Id услуги
    // (если запись не найдена - вернуть new EmployeeService() с Id=0)
    public async Task<EmployeeService> GetEmployeeServiceByEmployeeIdServiceIdAsync(
        int employeeId, int serviceId) =>
        (await GetAllEmployeesServicesAsync())
        .FirstOrDefault(employeeService => employeeService.EmployeeId == employeeId &&
                                           employeeService.ServiceId == serviceId)
        ?? new EmployeeService() { Id = 0 };

    // 14.3. добавить новую запись об услуге сотрудника в БД
    public async Task<(bool, string)> CreateEmployeeServiceAsync(
        EmployeeService newEmployeeService) =>
        await _dbRepository.CreateEmployeeServiceAsync(newEmployeeService);

    // 14.4. изменить данные об услуге сотрудника в БД
    public async Task<(bool, string)> UpdateEmployeeServiceAsync(
        EmployeeService employeeServiceEdt) =>
        await _dbRepository.UpdateEmployeeServiceAsync(employeeServiceEdt);



    // 15. таблица "КЛИЕНТЫ"
    // 15.1.1 получить все записи таблицы "КЛИЕНТЫ" из БД
    public async Task<List<Client>> GetAllClientsAsync() =>
        await _dbRepository.GetAllClientsAsync();

    // 15.1.2. получить все(включая удалённые) записи таблицы "КЛИЕНТЫ" из БД
    public async Task<List<Client>> GetAllClientsWithDeletedAsync() =>
        await _dbRepository.GetAllClientsWithDeletedAsync();

    // 15.1.3. получить все удалённые записи таблицы "КЛИЕНТЫ" из БД
    public async Task<List<Client>> GetAllDeletedClientsAsync() =>
        await _dbRepository.GetAllDeletedClientsAsync();



    // 16. таблица "ЗАПИСИ_НА_СЕАНС"
    // 16.1.1 получить все записи таблицы "ЗАПИСИ_НА_СЕАНС" из БД
    public async Task<List<Record>> GetAllRecordsAsync() =>
        await _dbRepository.GetAllRecordsAsync();

    // 16.1.2. получить все(включая удалённые) записи таблицы "ЗАПИСИ_НА_СЕАНС" из БД
    public async Task<List<Record>> GetAllRecordsWithDeletedAsync() =>
        await _dbRepository.GetAllRecordsWithDeletedAsync();

    // 16.1.3. получить все удалённые записи таблицы "ЗАПИСИ_НА_СЕАНС" из БД
    public async Task<List<Record>> GetAllDeletedRecordsAsync() =>
        await _dbRepository.GetAllDeletedRecordsAsync();



    // 17. таблица "ЗАПИСИ_УСЛУГИ"
    // 17.1.1 получить все записи таблицы "ЗАПИСИ_УСЛУГИ" из БД
    public async Task<List<RecordService>> GetAllRecordsServicesAsync() =>
        await _dbRepository.GetAllRecordsServicesAsync();

    // 17.1.2. получить все(включая удалённые) записи таблицы "ЗАПИСИ_УСЛУГИ" из БД
    public async Task<List<RecordService>> GetAllRecordsServicesWithDeletedAsync() =>
        await _dbRepository.GetAllRecordsServicesWithDeletedAsync();

    // 17.1.3. получить все удалённые записи таблицы "ЗАПИСИ_УСЛУГИ" из БД
    public async Task<List<RecordService>> GetAllDeletedRecordsServicesAsync() =>
        await _dbRepository.GetAllDeletedRecordsServicesAsync();



    // 18. таблица "РАСПИСАНИЕ"
    // 18.1.1 получить все записи таблицы "РАСПИСАНИЕ" из БД
    public async Task<List<WorkDay>> GetAllScheduleAsync() =>
        await _dbRepository.GetAllScheduleAsync();

    // 18.1.2. получить все(включая удалённые) записи таблицы "РАСПИСАНИЕ" из БД
    public async Task<List<WorkDay>> GetAllScheduleWithDeletedAsync() =>
        await _dbRepository.GetAllScheduleWithDeletedAsync();

    // 18.1.3. получить все удалённые записи таблицы "РАСПИСАНИЕ" из БД
    public async Task<List<WorkDay>> GetAllDeletedScheduleAsync() =>
        await _dbRepository.GetAllDeletedScheduleAsync();

    // 18.2. получить все записи о рабочих днях
    // заданного сотрудника за заданный период из БД
    public async Task<List<WorkDay>> GetAllScheduleByEmployeeIdFromToAsync(
        int employeeId, DateTime firstDay, DateTime lastDay) =>
        await _dbRepository.GetAllScheduleByEmployeeIdFromToAsync(
            employeeId, firstDay, lastDay);

    // 18.3. получить запись о рабочем дне из БД по Id
    // (если запись не найдена - вернуть new WorkDay() с Id=0)
    public async Task<WorkDay> GetWorkDayByIdAsync(int workDayId) =>
        (await GetAllScheduleAsync())
        .FirstOrDefault(workDay => workDay.Id == workDayId)
        ?? new WorkDay() { Id = 0 };

    // 18.4. добавить новую запись о рабочем дне сотрудника в БД
    public async Task<(bool, string)> CreateWorkDayAsync(WorkDay newWorkDay) =>
        await _dbRepository.CreateWorkDayAsync(newWorkDay);

    // 18.5. изменить данные о рабочем дне сотрудника в БД
    public async Task<(bool, string)> UpdateWorkDayAsync(WorkDay workDayEdt) =>
        await _dbRepository.UpdateWorkDayAsync(workDayEdt);



    // 19. таблица "ПРОМЕЖУТКИ_ВРЕМЕНИ"
    // 19.1.1 получить все записи таблицы "ПРОМЕЖУТКИ_ВРЕМЕНИ" из БД
    public async Task<List<Slot>> GetAllSlotsAsync() =>
        await _dbRepository.GetAllSlotsAsync();

    // 19.1.2. получить все(включая удалённые) записи таблицы "ПРОМЕЖУТКИ_ВРЕМЕНИ" из БД
    public async Task<List<Slot>> GetAllSlotsWithDeletedAsync() =>
        await _dbRepository.GetAllSlotsWithDeletedAsync();

    // 19.1.3. получить все удалённые записи таблицы "ПРОМЕЖУТКИ_ВРЕМЕНИ" из БД
    public async Task<List<Slot>> GetAllDeletedSlotsAsync() =>
        await _dbRepository.GetAllDeletedSlotsAsync();

    // 19.2. получить запись о промежутке времени из БД по всем параметрам
    // (если запись не найдена - вернуть new Slot() с Id=0)
    public async Task<Slot> GetSlotByParamsAsync(Slot srcSlot) =>
        (await GetAllSlotsAsync())
        .FirstOrDefault(slot =>
            slot.From == srcSlot.From && slot.Length == srcSlot.Length)
        ?? new Slot() { Id = 0 };

    // 19.3. добавить новую запись о промежутке времени в БД
    public async Task<(bool, string)> CreateSlotAsync(Slot newSlot) =>
        await _dbRepository.CreateSlotAsync(newSlot);



    // 20. таблица "РАСПИСАНИЕ_СВОБОДНЫЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ"
    // 20.1.1 получить все записи таблицы
    // "РАСПИСАНИЕ_СВОБОДНЫЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ" из БД
    public async Task<List<WorkDayFreeSlot>> GetAllWorkDaysFreeSlotsAsync() =>
        await _dbRepository.GetAllWorkDaysFreeSlotsAsync();

    // 20.1.2. получить все(включая удалённые) записи таблицы
    // "РАСПИСАНИЕ_СВОБОДНЫЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ" из БД
    public async Task<List<WorkDayFreeSlot>> GetAllWorkDaysFreeSlotsWithDeletedAsync() =>
        await _dbRepository.GetAllWorkDaysFreeSlotsWithDeletedAsync();

    // 20.1.3. получить все удалённые записи таблицы
    // "РАСПИСАНИЕ_СВОБОДНЫЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ" из БД
    public async Task<List<WorkDayFreeSlot>> GetAllDeletedWorkDaysFreeSlotsAsync() =>
        await _dbRepository.GetAllDeletedWorkDaysFreeSlotsAsync();

    // 20.2. добавить новую запись о промежутке времени свободного
    // для записи клиентов конкретного рабочего дня сотрудника в БД
    public async Task<(bool, string)> CreateWorkDayFreeSlotAsync(
        WorkDayFreeSlot newWorkDayFreeSlot) =>
        await _dbRepository
        .CreateWorkDayFreeSlotAsync(newWorkDayFreeSlot);

    // 20.3. изменить запись о промежутке времени свободного
    // для записи клиентов конкретного рабочего дня сотрудника в БД
    public async Task<(bool, string)> UpdateWorkDayFreeSlotAsync(
        WorkDayFreeSlot workDayFreeSlotEdt) =>
        await _dbRepository
        .UpdateWorkDayFreeSlotAsync(workDayFreeSlotEdt);



    // 21. таблица "РАСПИСАНИЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ_ДЛЯ_ПЕРЕРЫВОВ"
    // 21.1.1 получить все записи таблицы
    // "РАСПИСАНИЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ_ДЛЯ_ПЕРЕРЫВОВ" из БД
    public async Task<List<WorkDayBreakSlot>> GetAllWorkDaysBreakSlotsAsync() =>
        await _dbRepository.GetAllWorkDaysBreakSlotsAsync();

    // 21.1.2. получить все(включая удалённые) записи таблицы
    // "РАСПИСАНИЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ_ДЛЯ_ПЕРЕРЫВОВ" из БД
    public async Task<List<WorkDayBreakSlot>> GetAllWorkDaysBreakSlotsWithDeletedAsync() =>
        await _dbRepository.GetAllWorkDaysBreakSlotsWithDeletedAsync();

    // 21.1.3. получить все удалённые записи таблицы
    // "РАСПИСАНИЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ_ДЛЯ_ПЕРЕРЫВОВ" из БД
    public async Task<List<WorkDayBreakSlot>> GetAllDeletedWorkDaysBreakSlotsAsync() =>
        await _dbRepository.GetAllDeletedWorkDaysBreakSlotsAsync();

    // 21.2. добавить новую запись о промежутке времени
    // для перерыва конкретного рабочего дня сотрудника в БД
    public async Task<(bool, string)> CreateWorkDayBreakSlotAsync(
        WorkDayBreakSlot newWorkDayBreakSlot) =>
        await _dbRepository
        .CreateWorkDayBreakSlotAsync(newWorkDayBreakSlot);

    // 21.3. изменить запись о промежутке времени
    // для перерыва конкретного рабочего дня сотрудника в БД
    public async Task<(bool, string)> UpdateWorkDayBreakSlotAsync(
        WorkDayBreakSlot workDayBreakSlotEdt) =>
        await _dbRepository
        .UpdateWorkDayBreakSlotAsync(workDayBreakSlotEdt);

} // class DbService