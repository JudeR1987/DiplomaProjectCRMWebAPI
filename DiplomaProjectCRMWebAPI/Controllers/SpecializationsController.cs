﻿using Application.Interfaces;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaProjectCRMWebAPI.Controllers;

// SpecializationsController - передаёт данные таблицы "СПЕЦИАЛЬНОСТИ" в JSON-формате
[ApiController]
[Route("api/{controller}/{action}")]
public class SpecializationsController(IDbService dbService) : ControllerBase
{
    // получение ссылки на сервис-поставщик данных из базы данных
    // при помощи внедрения зависимости - через конструктор
    private readonly IDbService _dbService = dbService;


    // 1. по GET-запросу вернуть клиенту данные
    // о коллекции записей о специальностях из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllAsync() {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();

        // все записи таблицы
        var source = (await _dbService.GetAllSpecializationsAsync())
            .Select(Specialization.SpecializationToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllAsync


    // 2. по GET-запросу вернуть клиенту данные о коллекции записей
    // о специальностях(включая удалённые) из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllWithDeletedAsync() {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();

        // все записи таблицы
        var source = (await _dbService.GetAllSpecializationsWithDeletedAsync())
            .Select(Specialization.SpecializationToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllWithDeletedAsync


    // 3. по GET-запросу вернуть клиенту данные о коллекции
    // удалённых записей о специальностях из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllDeletedAsync() {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();

        // все записи таблицы
        var source = (await _dbService.GetAllDeletedSpecializationsAsync())
            .Select(Specialization.SpecializationToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllDeletedAsync

} // class SpecializationsController