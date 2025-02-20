using Application.Interfaces;
using Domain.Models.Entities;

namespace Application.Services;

// сервис-поставщик данных из базы данных,
// использующего бизнес-логику
public class DbService(IDbRepository dbRepository) : IDbService
{
    // ссылка на репозиторий базы данных
    private readonly IDbRepository _dbRepository = dbRepository;


    // 1. получить все записи таблицы "РОЛИ" из БД
    public async Task<List<Role>> GetAllRolesAsync() =>
        await _dbRepository.GetAllRolesAsync();



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
    public async Task<User> GetUserToLoginAsync(string login, string password) =>
        (await GetAllUsersAsync())
        .Find(user => user.Login == login && user.Password == password)
        ?? new User() { Id = 0 };

    // 2.4. получить зарегистрированного пользователя с совпадающим логином
    // (если пользователь не найден - вернуть new User() с Id=0)
    public async Task<User> GetUserByLoginAsync(string login) =>
        (await GetAllUsersAsync())
        .Find(user => user.Login == login)
        ?? new User() { Id = 0 };

    // 2.5. получить зарегистрированного пользователя с совпадающим email
    // (если пользователь не найден - вернуть new User() с Id=0)
    public async Task<User> GetUserByEmailAsync(string email) =>
        (await GetAllUsersAsync())
        .Find(user => user.Email == email)
        ?? new User() { Id = 0 };

    // 2.6. добавить новую запись о пользователе в БД
    public async Task CreateUserAsync(User user) =>
        await _dbRepository.CreateUserAsync(user);

    // 2.7. изменить данные пользователя в БД
    public async Task UpdateUserAsync(User user) =>
        await _dbRepository.UpdateUserAsync(user);




    // 3. получить все записи таблицы "КЛИЕНТЫ" из БД
    public async Task<List<Client>> GetAllClientsAsync() =>
        await _dbRepository.GetAllClientsAsync();


    // 4. получить все записи таблицы "УСЛУГИ" из БД
    public async Task<List<Service>> GetAllServicesAsync() =>
        await _dbRepository.GetAllServicesAsync();


    // 5. получить все записи таблицы "КАТЕГОРИИ_УСЛУГ" из БД
    public async Task<List<ServicesCategory>> GetAllServicesCategoriesAsync() =>
        await _dbRepository.GetAllServicesCategoriesAsync();

} // class DbService