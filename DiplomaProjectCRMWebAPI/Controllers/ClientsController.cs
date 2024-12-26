using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiplomaProjectCRMWebAPI.Context;
using ModelsLibrary.Models.Dto;

namespace DiplomaProjectCRMWebAPI.Controllers;

// ClientsController - передаёт данные таблицы "КЛИЕНТЫ" в JSON-формате
[ApiController]
[Route("api/{controller}/{action}")]
public class ClientsController(CrmContext crmContext) : ControllerBase
{
    private readonly CrmContext _crmContext = crmContext;

    // 1. по GET-запросу вернуть клиенту данные
    // о коллекции записей о клиентах из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllAsync() {

        // имитация временной задержки
        Task.Delay(1_500).Wait();

        // все записи таблицы
        var source = (await _crmContext
            .Clients.AsNoTracking().ToListAsync())
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