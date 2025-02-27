using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Domain.Models.Entities;

namespace DiplomaProjectCRMWebAPI.Controllers;

// ServicesController - передаёт данные таблицы "УСЛУГИ" в JSON-формате
[ApiController]
[Route("api/{controller}/{action}")]
public class ServicesController(IDbService dbService) : ControllerBase
{
    // получение ссылки на сервис-поставщик данных из базы данных
    // при помощи внедрения зависимости - через конструктор
    private readonly IDbService _dbService = dbService;


    // 1. по GET-запросу вернуть клиенту данные
    // о коллекции записей об услугах из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllAsync() {

        // имитация временной задержки
        //Task.Delay(1_500).Wait();

        // все записи таблицы
        var source = (await _dbService.GetAllServicesAsync())
            .Select(Service.ServiceToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllAsync


    // 2. по GET-запросу вернуть клиенту данные о коллекции записей
    // об услугах(включая удалённые) из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllWithDeletedAsync() {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();

        // все записи таблицы
        var source = (await _dbService.GetAllServicesWithDeletedAsync())
            .Select(Service.ServiceToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllWithDeletedAsync


    // 3. по GET-запросу вернуть клиенту данные о коллекции
    // удалённых записей об услугах из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllDeletedAsync() {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();

        // все записи таблицы
        var source = (await _dbService.GetAllDeletedServicesAsync())
            .Select(Service.ServiceToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllDeletedAsync

} // class ServicesController