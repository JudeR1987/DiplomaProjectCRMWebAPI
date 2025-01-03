using Domain.Models.Entities;

namespace Application.Interfaces;

// интерфейс сервиса-поставщика данных из базы данных,
// использующего бизнес-логику
public interface IDbService
{
    // 1. получить все записи таблицы "КЛИЕНТЫ" из БД
    Task<List<Client>> GetAllClientsAsync();


    // 2. получить все записи таблицы "УСЛУГИ" из БД
    Task<List<Service>> GetAllServicesAsync();


    // 3. получить все записи таблицы "КАТЕГОРИИ_УСЛУГ" из БД
    Task<List<ServicesCategory>> GetAllServicesCategoriesAsync();

} // interface IDbService