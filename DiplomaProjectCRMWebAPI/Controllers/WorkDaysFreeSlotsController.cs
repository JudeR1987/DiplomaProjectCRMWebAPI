using Application.Interfaces;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaProjectCRMWebAPI.Controllers;

// WorkDaysFreeSlotsController - передаёт данные таблицы
// "РАСПИСАНИЕ_СВОБОДНЫЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ" в JSON-формате
[ApiController]
[Route("api/{controller}/{action}")]
public class WorkDaysFreeSlotsController(IDbService dbService) : ControllerBase
{
    // ссылка на сервис-поставщик данных из базы данных
    private readonly IDbService _dbService = dbService;


    // 1. по GET-запросу вернуть клиенту данные
    // о коллекции записей о связях рабочих дней и свободных
    // промежутков времени из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllAsync() {

        // все записи таблицы
        var source = (await _dbService.GetAllWorkDaysFreeSlotsAsync())
            .Select(WorkDayFreeSlot.WorkDayFreeSlotToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllAsync


    // 2. по GET-запросу вернуть клиенту данные о коллекции записей
    // о связях рабочих дней и свободных промежутков времени(включая
    // удалённые) из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllWithDeletedAsync() {

        // все записи таблицы
        var source = (await _dbService.GetAllWorkDaysFreeSlotsWithDeletedAsync())
            .Select(WorkDayFreeSlot.WorkDayFreeSlotToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllWithDeletedAsync


    // 3. по GET-запросу вернуть клиенту данные о коллекции
    // удалённых записей о связях рабочих дней и свободных
    // промежутков времени из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllDeletedAsync() {

        // все записи таблицы
        var source = (await _dbService.GetAllDeletedWorkDaysFreeSlotsAsync())
            .Select(WorkDayFreeSlot.WorkDayFreeSlotToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllDeletedAsync

} // class WorkDaysFreeSlotsController