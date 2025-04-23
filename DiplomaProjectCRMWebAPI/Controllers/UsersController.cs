using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaProjectCRMWebAPI.Controllers;

// UsersController - передаёт данные таблицы "ПОЛЬЗОВАТЕЛИ" в JSON-формате
[ApiController]
[Route("api/{controller}/{action}")]
public class UsersController(IDbService dbService) : ControllerBase
{
    // ссылка на сервис-поставщик данных из базы данных
    private readonly IDbService _dbService = dbService;


    // 1. по GET-запросу вернуть клиенту данные
    // о коллекции записей о пользователях из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllAsync() {

        // все записи таблицы
        var source = (await _dbService.GetAllUsersAsync())
            .Select(Domain.Models.Entities.User.UserToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllAsync


    // 2. по GET-запросу вернуть клиенту данные о коллекции записей
    // о пользователях(включая удалённые) из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllWithDeletedAsync() {

        // все записи таблицы
        var source = (await _dbService.GetAllUsersWithDeletedAsync())
            .Select(Domain.Models.Entities.User.UserToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllWithDeletedAsync


    // 3. по GET-запросу вернуть клиенту данные о коллекции
    // удалённых записей о пользователях из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllDeletedAsync() {

        // все записи таблицы
        var source = (await _dbService.GetAllDeletedUsersAsync())
            .Select(Domain.Models.Entities.User.UserToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllDeletedAsync


    // 4. по GET-запросу вернуть клиенту данные о записи о пользователе
    // по его номеру телефона из БД в JSON-формате
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetByPhoneAsync([FromQuery] string phone) {

        // если данных о номере телефона пользователя нет - вернуть некорректные данные
        if (string.IsNullOrEmpty(phone))
            return BadRequest(new { Phone = phone ?? "" });


        // поиск записи о пользователе по номеру телефона
        var user = await _dbService.GetUserByPhoneAsync(phone);


        // получить отображаемые данные о пользователе(DTO)
        var displayUser = Domain.Models.Entities.User.UserToDto(user);

        // вернуть данные в JSON-формате
        return new JsonResult(new { User = displayUser });

    } // GetByPhoneAsync


    // 5. по GET-запросу вернуть клиенту данные о записи о пользователе
    // по его email из БД в JSON-формате
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetByEmailAsync([FromQuery] string email) {

        // если данных о email пользователя нет - вернуть некорректные данные
        if (string.IsNullOrEmpty(email))
            return BadRequest(new { Email = email ?? "" });


        // поиск записи о пользователе по email
        var user = await _dbService.GetUserByEmailAsync(email);


        // получить отображаемые данные о пользователе(DTO)
        var displayUser = Domain.Models.Entities.User.UserToDto(user);

        // вернуть данные в JSON-формате
        return new JsonResult(new { User = displayUser });

    } // GetByEmailAsync

} // class UsersController