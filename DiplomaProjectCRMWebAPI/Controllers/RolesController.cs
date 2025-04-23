using Application.Interfaces;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaProjectCRMWebAPI.Controllers;

// RolesController - передаёт данные таблицы "РОЛИ" в JSON-формате
[ApiController]
[Route("api/{controller}/{action}")]
public class RolesController(IDbService dbService) : ControllerBase
{
    // ссылка на сервис-поставщик данных из базы данных
    private readonly IDbService _dbService = dbService;


    // 1. по GET-запросу вернуть клиенту данные
    // о коллекции записей о ролях пользователя из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllAsync() {

        // все записи таблицы
        var source = (await _dbService.GetAllRolesAsync())
            .Select(Role.RoleToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllAsync


    // 2. по GET-запросу вернуть клиенту данные о коллекции записей
    // о ролях пользователя(включая удалённые) из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllWithDeletedAsync() {

        // все записи таблицы
        var source = (await _dbService.GetAllRolesWithDeletedAsync())
            .Select(Role.RoleToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllWithDeletedAsync


    // 3. по GET-запросу вернуть клиенту данные о коллекции
    // удалённых записей о ролях пользователя из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllDeletedAsync() {

        // все записи таблицы
        var source = (await _dbService.GetAllDeletedRolesAsync())
            .Select(Role.RoleToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllDeletedAsync

} // class RolesController