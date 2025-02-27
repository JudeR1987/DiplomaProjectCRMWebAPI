﻿using Application.Interfaces;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaProjectCRMWebAPI.Controllers;

// WorkDaysBreakSlotsController - передаёт данные таблицы
// "РАСПИСАНИЕ_ПРОМЕЖУТКИ_ВРЕМЕНИ_ДЛЯ_ПЕРЕРЫВОВ" в JSON-формате
[ApiController]
[Route("api/{controller}/{action}")]
public class WorkDaysBreakSlotsController(IDbService dbService) : ControllerBase
{
    // получение ссылки на сервис-поставщик данных из базы данных
    // при помощи внедрения зависимости - через конструктор
    private readonly IDbService _dbService = dbService;


    // 1. по GET-запросу вернуть клиенту данные
    // о коллекции записей о связях рабочих дней и промежутков
    // времени для перерыва из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllAsync() {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();

        // все записи таблицы
        var source = (await _dbService.GetAllWorkDaysBreakSlotsAsync())
            .Select(WorkDayBreakSlot.WorkDayBreakSlotToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllAsync


    // 2. по GET-запросу вернуть клиенту данные о коллекции записей
    // о связях рабочих дней и промежутков времени для перерыва(включая
    // удалённые) из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllWithDeletedAsync() {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();

        // все записи таблицы
        var source = (await _dbService.GetAllWorkDaysBreakSlotsWithDeletedAsync())
            .Select(WorkDayBreakSlot.WorkDayBreakSlotToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllWithDeletedAsync


    // 3. по GET-запросу вернуть клиенту данные о коллекции
    // удалённых записей о связях рабочих дней и промежутков
    // времени для перерыва из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllDeletedAsync() {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();

        // все записи таблицы
        var source = (await _dbService.GetAllDeletedWorkDaysBreakSlotsAsync())
            .Select(WorkDayBreakSlot.WorkDayBreakSlotToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllDeletedAsync

} // class WorkDaysBreakSlotsController