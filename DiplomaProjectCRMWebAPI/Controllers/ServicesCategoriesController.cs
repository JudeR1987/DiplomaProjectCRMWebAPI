using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Domain.Models.Entities;

namespace DiplomaProjectCRMWebAPI.Controllers;

// ServicesCategoriesController - передаёт данные
// таблицы "КАТЕГОРИИ_УСЛУГ" в JSON-формате
[ApiController]
[Route("api/{controller}/{action}")]
public class ServicesCategoriesController(IDbService dbService) : ControllerBase
{
    // ссылка на сервис-поставщик данных из базы данных
    private readonly IDbService _dbService = dbService;


    // 1. по GET-запросу вернуть клиенту данные
    // о коллекции записей о категориях услуг из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllAsync() {

        // все записи таблицы "КАТЕГОРИИ_УСЛУГ" из БД
        var allServicesCategories =
            (await _dbService.GetAllServicesCategoriesAsync())
            .Select(ServicesCategory.ServicesCategoryToDto)
            .OrderBy(servicesCategory => servicesCategory.Name)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(new { allServicesCategories });

    } // GetAllAsync


    // 2. по GET-запросу вернуть клиенту данные о коллекции записей
    // о категориях услуг(включая удалённые) из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllWithDeletedAsync() {

        // все записи таблицы
        var source = (await _dbService.GetAllServicesCategoriesWithDeletedAsync())
            .Select(ServicesCategory.ServicesCategoryToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllWithDeletedAsync


    // 3. по GET-запросу вернуть клиенту данные о коллекции
    // удалённых записей о категориях услуг из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllDeletedAsync() {

        // все записи таблицы
        var source = (await _dbService.GetAllDeletedServicesCategoriesAsync())
            .Select(ServicesCategory.ServicesCategoryToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllDeletedAsync

} // class ServicesCategoriesController