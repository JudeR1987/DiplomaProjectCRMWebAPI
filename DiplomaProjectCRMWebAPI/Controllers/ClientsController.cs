using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Domain.Models.ViewModels;

namespace DiplomaProjectCRMWebAPI.Controllers;

// ClientsController - передаёт данные таблицы "КЛИЕНТЫ" в JSON-формате
[ApiController]
[Route("api/{controller}/{action}")]
public class ClientsController(IDbService dbService) : ControllerBase
{
    // получение ссылки на сервис-поставщик данных из базы данных
    // при помощи внедрения зависимости - через конструктор
    private readonly IDbService _dbService = dbService;

    // количество элементов на странице
    private readonly int _pageSize = 10;


    // 1. по GET-запросу вернуть клиенту данные
    // о коллекции записей о клиентах из БД в JSON-формате
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page) {

        // если данных о запрашиваемой странице нет - вернуть некорректные данные
        // page = 0; // для проверки
        if (page <= 0)
            return BadRequest(new { page });


        // все записи таблицы
        var source = await _dbService.GetAllClientsAsync();

        // часть коллекции для заданной страницы
        var items = source
            .Skip((page - 1) * _pageSize)
            .Take(_pageSize)
            .Select(Client.ClientToDto)
            .ToList();

        // данные для пагинации
        var pageViewModel = new PageViewModel(page, source.Count, _pageSize);

        // данные для вывода части коллекции
        var viewModel = new GetAllClientsViewModel(items, pageViewModel);

        // вернуть данные в JSON-формате
        return new JsonResult(viewModel);

    } // GetAllAsync


    // 2. по GET-запросу вернуть клиенту данные о коллекции записей
    // о клиентах(включая удалённые) из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllWithDeletedAsync() {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();

        // все записи таблицы
        var source = (await _dbService.GetAllClientsWithDeletedAsync())
            .Select(Client.ClientToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllWithDeletedAsync


    // 3. по GET-запросу вернуть клиенту данные о коллекции
    // удалённых записей о клиентах из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllDeletedAsync() {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();

        // все записи таблицы
        var source = (await _dbService.GetAllDeletedClientsAsync())
            .Select(Client.ClientToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllDeletedAsync


    // 4. по GET-запросу вернуть клиенту данные о коллекции записей
    // о клиентах из БД для заданной компании в JSON-формате
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllByCompanyIdAsync(
        [FromQuery] int id, [FromQuery] int page) {

        // если данных о компании нет - вернуть некорректные данные
        // id = 0; // для проверки
        if (id <= 0)
            return BadRequest(new { CompanyId = 0 });

        // если данных о запрашиваемой странице нет - вернуть некорректные данные
        // page = 0; // для проверки
        if (page <= 0)
            return BadRequest(new { page });


        // все записи таблицы
        var source = await _dbService.GetAllClientsByCompanyIdAsync(id);

        // часть коллекции для заданной страницы
        var items = source
            .Skip((page - 1) * _pageSize)
            .Take(_pageSize)
            .Select(Client.ClientToDto)
            .ToList();

        // данные для пагинации
        var pageViewModel = new PageViewModel(page, source.Count, _pageSize);

        // данные для вывода части коллекции
        var viewModel = new GetAllClientsViewModel(items, pageViewModel);

        // вернуть данные в JSON-формате
        return new JsonResult(viewModel);

    } // GetAllByCompanyIdAsync

} // class ClientsController