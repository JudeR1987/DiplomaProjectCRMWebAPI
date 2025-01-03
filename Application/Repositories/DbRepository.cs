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


    // 1. получить все записи таблицы "КЛИЕНТЫ" из БД
    public async Task<List<Client>> GetAllClientsAsync() =>
        await _db.Clients.AsNoTracking().ToListAsync();


    // 2. получить все записи таблицы "УСЛУГИ" из БД
    public async Task<List<Service>> GetAllServicesAsync() =>
        await _db.Services.AsNoTracking().ToListAsync();


    // 3. получить все записи таблицы "КАТЕГОРИИ_УСЛУГ" из БД
    public async Task<List<ServicesCategory>> GetAllServicesCategoriesAsync() =>
        await _db.ServicesCategories.AsNoTracking().ToListAsync();

} // class DbRepository