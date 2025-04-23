using Application.Interfaces;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaProjectCRMWebAPI.Controllers;

// RecordsServicesController - передаёт данные таблицы "ЗАПИСИ_УСЛУГИ" в JSON-формате
[ApiController]
[Route("api/{controller}/{action}")]
public class RecordsServicesController(IDbService dbService) : ControllerBase
{
    // ссылка на сервис-поставщик данных из базы данных
    private readonly IDbService _dbService = dbService;


    // 1. по GET-запросу вернуть клиенту данные
    // о коллекции записей о связях записей на сеанс и услуг из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllAsync() {

        // все записи таблицы
        var source = (await _dbService.GetAllRecordsServicesAsync())
            .Select(RecordService.RecordServiceToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllAsync


    // 2. по GET-запросу вернуть клиенту данные о коллекции записей
    // о связях записей на сеанс и услуг(включая удалённые) из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllWithDeletedAsync() {

        // все записи таблицы
        var source = (await _dbService.GetAllRecordsServicesWithDeletedAsync())
            .Select(RecordService.RecordServiceToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllWithDeletedAsync


    // 3. по GET-запросу вернуть клиенту данные о коллекции
    // удалённых записей о связях записей на сеанс и услуг из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllDeletedAsync() {

        // все записи таблицы
        var source = (await _dbService.GetAllDeletedRecordsServicesAsync())
            .Select(RecordService.RecordServiceToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllDeletedAsync

} // class RecordsServicesController