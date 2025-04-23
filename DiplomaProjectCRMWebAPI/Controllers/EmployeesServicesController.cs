using Application.Interfaces;
using Domain.Models.Dto;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaProjectCRMWebAPI.Controllers;

// EmployeesServicesController - передаёт данные таблицы "СОТРУДНИКИ_УСЛУГИ" в JSON-формате
[ApiController]
[Route("api/{controller}/{action}")]
public class EmployeesServicesController(IDbService dbService) : ControllerBase
{
    // ссылка на сервис-поставщик данных из базы данных
    private readonly IDbService _dbService = dbService;


    // 1. по GET-запросу вернуть клиенту данные
    // о коллекции записей о связях сотрудников и услуг из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllAsync() {

        // все записи таблицы
        var source = (await _dbService.GetAllEmployeesServicesAsync())
            .Select(EmployeeService.EmployeeServiceToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllAsync


    // 2. по GET-запросу вернуть клиенту данные о коллекции записей
    // о связях сотрудников и услуг(включая удалённые) из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllWithDeletedAsync() {

        // все записи таблицы
        var source = (await _dbService.GetAllEmployeesServicesWithDeletedAsync())
            .Select(EmployeeService.EmployeeServiceToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllWithDeletedAsync


    // 3. по GET-запросу вернуть клиенту данные о коллекции
    // удалённых записей о связях сотрудников и услуг из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllDeletedAsync() {

        // все записи таблицы
        var source = (await _dbService.GetAllDeletedEmployeesServicesAsync())
            .Select(EmployeeService.EmployeeServiceToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllDeletedAsync


    // 4. по PUT-запросу получить новые данные об услуге сотрудника для создания
    // новой записи в таблице БД, вернуть клиенту Ok или сообщение об ошибке
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> CreateEmployeeServiceAsync(
        [FromBody] EmployeeServiceDto employeeService) {

        // если данных об услуге сотрудника нет или Id некорректный -
        // вернуть некорректные данные
        if (employeeService == null || employeeService.Id != 0)
            return BadRequest(new { EmployeeServiceId = 0 });


        // 1. данные о сотруднике, которому добавляем услугу
        var employeeId = employeeService.EmployeeId;

        // если данных о сотруднике нет - вернуть некорректные данные
        if (employeeId <= 0)
            return BadRequest(new { EmployeeId = 0 });


        // 2. данные об услуге, которая добавляется сотруднику
        var serviceId = employeeService.ServiceId;

        // если данных об услуге нет - вернуть некорректные данные
        if (serviceId <= 0)
            return BadRequest(new { ServiceId = 0 });


        // 3. дата и время удаления записи записи о связи
        DateTime? deleted = null;


        // создание новой записи об услуге сотрудника
        var newEmployeeService = new EmployeeService(
            employeeId, serviceId, deleted
        );

        // добавление записи в БД
        (bool isOk, string message) =
            await _dbService.CreateEmployeeServiceAsync(newEmployeeService);

        // если при добавлении была ошибка - передать ошибку
        if (!isOk)
            return BadRequest(new { CreateMessage = message });


        // вернуть Ok
        return Ok();

    } // CreateEmployeeServiceAsync


    // 5. по DELETE-запросу удалить данные об услуге сотрудника
    // и вернуть клиенту Ok, или сообщение об ошибке
    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteEmployeeServiceByEmployeeIdServiceIdAsync(
        [FromQuery] int firstId, [FromQuery] int secondId) {

        // получим данные запроса
        var employeeId = firstId;
        var serviceId = secondId;


        // если данных о сотруднике нет - вернуть некорректные данные
        if (employeeId <= 0)
            return BadRequest(new { EmployeeId = 0 });

        // если данных об услуге нет - вернуть некорректные данные
        if (serviceId <= 0)
            return BadRequest(new { ServiceId = 0 });


        // поиск записи об услуге сотрудника по идентификаторам сотрудника и услуги
        var employeeService = await _dbService
            .GetEmployeeServiceByEmployeeIdServiceIdAsync(employeeId, serviceId);

        // если запись не найдена(Id=0) - вернуть сообщение
        // об ошибке 401(НЕ АВТОРИЗОВАН)
        if (employeeService.Id == 0)
            return Unauthorized(new { EmployeeServiceId = 0 });


        // установить в записи данных об услуге сотрудника
        // время и дату удаления записи
        employeeService.Deleted = DateTime.Now;

        // изменение записи в БД
        (bool isOk, string message) =
            await _dbService.UpdateEmployeeServiceAsync(employeeService);

        // если при изменении была ошибка - передать ошибку
        if (!isOk)
            return BadRequest(new { UpdateMessage = message });


        // вернуть Ok
        return Ok();

    } // DeleteEmployeeServiceByEmployeeIdServiceIdAsync

} // class EmployeesServicesController