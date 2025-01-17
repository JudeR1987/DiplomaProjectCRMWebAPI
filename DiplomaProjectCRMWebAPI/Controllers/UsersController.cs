using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaProjectCRMWebAPI.Controllers;

// UsersController - передаёт данные таблицы "ПОЛЬЗОВАТЕЛИ" в JSON-формате
[ApiController]
[Route("api/{controller}/{action}")]
public class UsersController(IDbService dbService) : ControllerBase
{
    // получение ссылки на сервис-поставщик данных из базы данных
    // при помощи внедрения зависимости - через конструктор
    private readonly IDbService _dbService = dbService;


    // 1. по GET-запросу вернуть клиенту данные
    // о коллекции записей о пользователях из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllAsync() {

        // имитация временной задержки
        Task.Delay(1_500).Wait();

        // все записи таблицы
        var source = (await _dbService.GetAllUsersAsync())
            .Select(Domain.Models.Entities.User.UserToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllAsync

} // class UsersController