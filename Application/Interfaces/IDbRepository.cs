using Domain.Models.Entities;

namespace Application.Interfaces;

// интерфейс репозитория данных базы данных
public interface IDbRepository
{
    // 1. получить все записи таблицы "РОЛИ" из БД
    Task<List<Role>> GetAllRolesAsync();


    // 2. получить все записи таблицы "ПОЛЬЗОВАТЕЛИ" из БД
    Task<List<User>> GetAllUsersAsync();

    // 2.1. добавить новую запись о пользователе в БД
    Task CreateUserAsync(User user);

    // 2.2. изменить данные пользователя в БД
    Task UpdateUserAsync(User user);





    // 3. получить все записи таблицы "КЛИЕНТЫ" из БД
    Task<List<Client>> GetAllClientsAsync();


    // 4. получить все записи таблицы "УСЛУГИ" из БД
    Task<List<Service>> GetAllServicesAsync();


    // 5. получить все записи таблицы "КАТЕГОРИИ_УСЛУГ" из БД
    Task<List<ServicesCategory>> GetAllServicesCategoriesAsync();

} // interface IDbRepository