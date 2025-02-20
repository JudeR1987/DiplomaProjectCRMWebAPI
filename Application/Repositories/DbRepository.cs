using Application.Interfaces;
using Domain.Context;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories;

// репозиторий, поставляющий данные из базы данных
public class DbRepository(CrmContext db) : IDbRepository
{
    // ссылка на базу данных
    private CrmContext _db = db;



    // 1. получить все записи таблицы "РОЛИ" из БД
    public async Task<List<Role>> GetAllRolesAsync() =>
        await _db.Roles.AsNoTracking().ToListAsync();



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




    // 3. получить все записи таблицы "КЛИЕНТЫ" из БД
    public async Task<List<Client>> GetAllClientsAsync() =>
        await _db.Clients.AsNoTracking().ToListAsync();


    // 4. получить все записи таблицы "УСЛУГИ" из БД
    public async Task<List<Service>> GetAllServicesAsync() =>
        await _db.Services.AsNoTracking().ToListAsync();


    // 5. получить все записи таблицы "КАТЕГОРИИ_УСЛУГ" из БД
    public async Task<List<ServicesCategory>> GetAllServicesCategoriesAsync() =>
        await _db.ServicesCategories.AsNoTracking().ToListAsync();

} // class DbRepository