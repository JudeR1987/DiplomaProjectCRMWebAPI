using Application.Interfaces;
using Domain.Models.Entities;

namespace Application.Services;

// сервис-поставщик данных из базы данных,
// использующего бизнес-логику
public class DbService(IDbRepository dbRepository) : IDbService
{
    // ссылка на репозиторий базы данных
    private readonly IDbRepository _dbRepository = dbRepository;


    // 1. получить все записи таблицы "КЛИЕНТЫ" из БД
    public async Task<List<Client>> GetAllClientsAsync() =>
        await _dbRepository.GetAllClientsAsync();


    // 2. получить все записи таблицы "УСЛУГИ" из БД
    public async Task<List<Service>> GetAllServicesAsync() =>
        await _dbRepository.GetAllServicesAsync();


    // 3. получить все записи таблицы "КАТЕГОРИИ_УСЛУГ" из БД
    public async Task<List<ServicesCategory>> GetAllServicesCategoriesAsync() =>
        await _dbRepository.GetAllServicesCategoriesAsync();

} // class DbService