using Domain.Models.Entities;

namespace Application.Interfaces;

// интерфейс сервиса-поставщика данных из базы данных,
// использующего бизнес-логику
public interface IDbService
{
    // 1. получить все записи таблицы "РОЛИ" из БД
    Task<List<Role>> GetAllRolesAsync();



    // 2.1.1. получить все записи таблицы "ПОЛЬЗОВАТЕЛИ" из БД
    Task<List<User>> GetAllUsersAsync();

    // 2.1.2. получить все(включая удалённые) записи таблицы "ПОЛЬЗОВАТЕЛИ" из БД
    Task<List<User>> GetAllUsersWithDeletedAsync();

    // 2.1.3. получить все удалённые записи таблицы "ПОЛЬЗОВАТЕЛИ" из БД
    Task<List<User>> GetAllDeletedUsersAsync();

    // 2.2. получить запись о пользователе из БД по Id
    // (если запись не найдена - вернуть new User() с Id=0)
    Task<User> GetUserByIdAsync(int userId);

    // 2.3. получить пользователя по логину и паролю
    // (если пользователь не найден - вернуть new User() с Id=0)
    Task<User> GetUserToLoginAsync(string login, string password);

    // 2.4. получить зарегистрированного пользователя с совпадающим логином
    // (если пользователь не найден - вернуть new User() с Id=0)
    Task<User> GetUserByLoginAsync(string login);

    // 2.5. получить зарегистрированного пользователя с совпадающим email
    // (если пользователь не найден - вернуть new User() с Id=0)
    Task<User> GetUserByEmailAsync(string email);

    // 2.6. добавить новую запись о пользователе в БД
    Task CreateUserAsync(User user);

    // 2.7. изменить данные пользователя в БД
    Task UpdateUserAsync(User user);






    // 3. получить все записи таблицы "КЛИЕНТЫ" из БД
    Task<List<Client>> GetAllClientsAsync();


    // 4. получить все записи таблицы "УСЛУГИ" из БД
    Task<List<Service>> GetAllServicesAsync();


    // 5. получить все записи таблицы "КАТЕГОРИИ_УСЛУГ" из БД
    Task<List<ServicesCategory>> GetAllServicesCategoriesAsync();

} // interface IDbService