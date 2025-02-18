using Microsoft.AspNetCore.Mvc;
using Domain.Models.Dto;
using Application.Interfaces;

namespace DiplomaProjectCRMWebAPI.Controllers;

// ClientsController - передаёт данные таблицы "КЛИЕНТЫ" в JSON-формате
[ApiController]
[Route("api/{controller}/{action}")]
public class ClientsController(IDbService dbService) : ControllerBase
{
    // получение ссылки на сервис-поставщик данных из базы данных
    // при помощи внедрения зависимости - через конструктор
    private readonly IDbService _dbService = dbService;


    // 1. по GET-запросу вернуть клиенту данные
    // о коллекции записей о клиентах из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllAsync() {

        // имитация временной задержки
        //Task.Delay(1_500).Wait();

        // все записи таблицы
        var source = (await _dbService.GetAllClientsAsync())
            .Select(client => new ClientDto(
                client.Id, client.Surname, client.Name, client.Patronymic,
                client.Phone, client.Email, client.Gender, client.ImportanceId,
                client.Discount, client.Card, client.BirthDate, client.Comment,
                client.Spent, client.Balance, client.SmsBirthday, client.SmsNot))
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllAsync

} // class ClientsController