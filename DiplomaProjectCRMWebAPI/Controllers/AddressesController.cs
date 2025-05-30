﻿using Application.Interfaces;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaProjectCRMWebAPI.Controllers;

// AddressesController - передаёт данные таблицы "АДРЕСА" в JSON-формате
[ApiController]
[Route("api/{controller}/{action}")]
public class AddressesController(IDbService dbService) : ControllerBase
{
    // ссылка на сервис-поставщик данных из базы данных
    private readonly IDbService _dbService = dbService;


    // 1. по GET-запросу вернуть клиенту данные
    // о коллекции записей об адресах из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllAsync() {

        // все записи таблицы
        var source = (await _dbService.GetAllAddressesAsync())
            .Select(Address.AddressToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllAsync


    // 2. по GET-запросу вернуть клиенту данные о коллекции записей
    // об адресах(включая удалённые) из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllWithDeletedAsync() {

        // все записи таблицы
        var source = (await _dbService.GetAllAddressesWithDeletedAsync())
            .Select(Address.AddressToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllWithDeletedAsync


    // 3. по GET-запросу вернуть клиенту данные о коллекции
    // удалённых записей об адресах из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllDeletedAsync() {

        // все записи таблицы
        var source = (await _dbService.GetAllDeletedAddressesAsync())
            .Select(Address.AddressToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllDeletedAsync

} // class AddressesController