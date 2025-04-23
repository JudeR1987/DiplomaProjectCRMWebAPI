using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Domain.Models.ViewModels;
using Domain.Models.Dto;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Domain.Models.Infrastructure;
using System.IO;

namespace DiplomaProjectCRMWebAPI.Controllers;

// RecordsController - передаёт данные таблицы "ЗАПИСИ_НА_СЕАНС" в JSON-формате
[ApiController]
[Route("api/{controller}/{action}")]
public class RecordsController(IDbService dbService) : ControllerBase
{
    // получение ссылки на сервис-поставщик данных из базы данных
    // при помощи внедрения зависимости - через конструктор
    private readonly IDbService _dbService = dbService;

    // количество элементов на странице
    private readonly int _pageSize = 10;


    // 1. по GET-запросу вернуть клиенту данные
    // о коллекции записей на сеанс из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllAsync() {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();

        // все записи таблицы
        var source = (await _dbService.GetAllRecordsAsync())
            .Select(Record.RecordToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllAsync


    // 2. по GET-запросу вернуть клиенту данные о коллекции записей
    // на сеанс(включая удалённые) из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllWithDeletedAsync() {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();

        // все записи таблицы
        var source = (await _dbService.GetAllRecordsWithDeletedAsync())
            .Select(Record.RecordToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllWithDeletedAsync


    // 3. по GET-запросу вернуть клиенту данные о коллекции
    // удалённых записей на сеанс из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllDeletedAsync() {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();

        // все записи таблицы
        var source = (await _dbService.GetAllDeletedRecordsAsync())
            .Select(Record.RecordToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllDeletedAsync


    // 4. по GET-запросу вернуть клиенту данные о коллекции
    // записей на сеанс из БД для заданной компании в JSON-формате
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllByCompanyIdAsync(
        [FromQuery] int id, [FromQuery] int page) {

        // если данных о компании нет - вернуть некорректные данные
        // id = 0; // для проверки
        if (id <= 0)
            return BadRequest(new { CompanyId = 0 });

        // если данных о запрашиваемой странице нет - вернуть некорректные данные
        // page = 0; // для проверки
        if (page <= 0)
            return BadRequest(new { page });


        // все записи таблицы
        var source = await _dbService.GetAllRecordsByCompanyIdAsync(id);

        // часть коллекции для заданной страницы
        var items = source
            .Skip((page - 1) * _pageSize)
            .Take(_pageSize)
            .Select(Record.RecordToDto)
            .ToList();

        // данные для пагинации
        var pageViewModel = new PageViewModel(page, source.Count, _pageSize);

        // данные для вывода части коллекции
        var viewModel = new GetAllRecordsViewModel(items, pageViewModel);

        // вернуть данные в JSON-формате
        return new JsonResult(viewModel);

    } // GetAllByCompanyIdAsync


    // 5. по PUT-запросу получить новые данные об онлайн-записи для создания
    // новой записи в таблице БД, вернуть клиенту Ok или сообщение об ошибке
    [HttpPut]
    public async Task<IActionResult> CreateOnlineRecordAsync(
        [FromForm] int employeeId, [FromForm] string clientName, [FromForm] string clientPhone,
        [FromForm] string clientEmail, [FromForm] string dateString, [FromForm] string timeString,
        [FromForm] string createDateString, [FromForm] string createTimeString,
        [FromForm] int length, [FromForm] string? comment, [FromForm] int attendance,
        [FromForm] bool isOnline, [FromForm] bool isPaid) {

        // если данных о записи нет или Id некорректный - вернуть некорректные данные
        // if (true) // для проверки
        /*if (record == null || record.Id != 0)
            return BadRequest(new { RecordId = 0 });*/


        // 1) данные для создания новой записи в БД об онлайн-записи на сеанс:

        // 1. данные о сотруднике
        //var employeeId = record.Employee.Id;


        // 2. данные о клиенте
        // var clientId = record.Client.Id;

        // получить запись о клиенте по всем полученным параметрам
        var client = await _dbService.GetClientByParamsAsync(
            clientName, clientPhone, clientEmail
        );

        // если client.Id=0 - добавление записи в БД о новом клиенте
        if (client.Id == 0) {

            // создание новой записи о клиенте
            client = new Client() {
                Name = clientName, Phone = clientPhone, Email = clientEmail
            };

            // добавление записи в БД
            (bool isOkCreateClient, string messageCreateClient) =
                await _dbService.CreateClientAsync(client);

            // если при добавлении была ошибка - передать ошибку
            /*(bool isOkCreateClient, string messageCreateClient) =
                (false, "привет-123!!!"); // для проверки*/
            if (!isOkCreateClient)
                return BadRequest(new { CreateMessage = messageCreateClient });

        } // if


        // 3. дата и время начала записи на сеанс
        var date = Utils.StringToDateTime(dateString, timeString);


        // 4. дата и время создания записи
        var createDate = Utils.StringToDateTime(createDateString, createTimeString);


        // 5. длительность сеанса(в секундах)
        //var length = record.Length;


        // 6. комментарий к записи на сеанс
        //var comment = record.Comment == "" ? null : record.Comment;


        // 7. статус посещения клиентом записи на сеанс
        //var attendance = record.Attendance;


        // 8. принадлежность записи на сеанс к онлайн-записи
        //var isOnline = record.IsOnline;


        // 9. статус оплаты
        //var isPaid = record.IsPaid;


        // 10. дата и время удаления записи о сеансе
        DateTime? deleted = null;


        // создание новой записи об онлайн-записи на сеанс
        var newRecord = new Record(
            employeeId, client.Id, date, createDate, length,
            comment, attendance, isOnline, isPaid, deleted
        );

        // добавление записи в БД
        (bool isOk, string message) =
            await _dbService.CreateRecordAsync(newRecord);

        // если при добавлении была ошибка - передать ошибку
        // (bool isOk, string message) = (false, "привет-123!!!"); // для проверки
        if (!isOk)
            return BadRequest(new { CreateMessage = message });


        // 2) изменение промежутков времени свободных для записи:
        // TODO: не учтены промежутки времени для перерывов!!! требуется доработка!!!

        // 1. все свободные для записи промежутки времени на полученную дату
        var freeSlots = await _dbService
            .GetAllFreeSlotsByEmployeeIdByDateAsync(employeeId, date.Date);

        
        // 2. удаление промежутка времени для данной онлайн-записи
        // из коллекции свободных для записи промежутков времени

        // промежуток времени, соответствующий процедуре
        var recordSlot = new Slot(new TimeOnly(date.Hour, date.Minute), length, null);

        // удаление промежутков пока процедура не завершится
        while (recordSlot.Length > 0) {

            // 2.1 получить промежуток с совпадающим началом по времени
            var freeSlot = freeSlots
                .FirstOrDefault(slot => slot.From == recordSlot.From)
                ?? new Slot();

            // если промежуток не найден - выход из цикла
            // (возможно требуется промежуток для перерыва)
            if (freeSlot.Id == 0) break;

            // 2.2 получить связь рабочего дня данного сотрудника
            // за данную дату с изменяемым/удаляемым промежутком времени
            var workDayFreeSlot =
                (await _dbService.GetWorkDayByEmployeeIdByDateAsync(employeeId, date.Date))
                .WorkDaysFreeSlots
                .FirstOrDefault(workDayFreeSlot => workDayFreeSlot.SlotId == freeSlot.Id &&
                                                   workDayFreeSlot.Deleted == null &&
                                                   workDayFreeSlot.Slot.Deleted == null)
                ?? new WorkDayFreeSlot();


            // если длительность процедуры НЕ_меньше - удалить найденный промежуток
            // (связь, а не сам промежуток)
            if (recordSlot.Length >= freeSlot.Length) {

                // установить в записи данных о связи время и дату удаления записи
                workDayFreeSlot.Deleted = DateTime.Now;

                // сократить промежуток процедуры
                recordSlot.From = freeSlot.To;
                recordSlot.Length -= freeSlot.Length;

            }
            // если длительность процедуры меньше - изменить найденный промежуток
            // (связь, а не сам промежуток)
            else {

                // изменение найденного промежутка (новый промежуток)
                var newSlot = new Slot(recordSlot.To, freeSlot.Length - recordSlot.Length, null);

                // поиск промежутка времени в таблице БД по параметрам промежутка времени
                var slot = await _dbService.GetSlotByParamsAsync(newSlot);

                // если slot.Id=0 - добавление записи в БД о новом промежутке времени
                if (slot.Id == 0) {

                    // создание новой записи о промежутке времени
                    slot = newSlot;

                    // добавление записи в БД
                    (bool isOkCreateSlot, string messageCreateSlot) =
                        await _dbService.CreateSlotAsync(slot);

                    // если при добавлении была ошибка - передать ошибку
                    /*(bool isOkCreateSlot, string messageCreateSlot) =
                        (false, "привет-123!!!"); // для проверки*/
                    if (!isOkCreateSlot)
                        return BadRequest(new { CreateMessage = messageCreateSlot });

                } // if

                // изменить данные
                workDayFreeSlot.SlotId = slot.Id;

                // изменить ссылку
                workDayFreeSlot.Slot = slot;


                // сократить промежуток процедуры
                recordSlot.From = slot.From;
                recordSlot.Length = 0;

            } // if


            // изменение записи в БД
            (bool isOkUpdateWorkDayFreeSlot, string messageUpdateWorkDayFreeSlot) =
                await _dbService.UpdateWorkDayFreeSlotAsync(workDayFreeSlot);

            // если при изменении была ошибка - передать ошибку
            /*(bool isOkUpdateWorkDayFreeSlot, string messageUpdateWorkDayFreeSlot) =
                (false, "привет-123!!!"); // для проверки*/
            if (!isOkUpdateWorkDayFreeSlot)
                return BadRequest(new { UpdateMessage = messageUpdateWorkDayFreeSlot });

        } // while


        // вернуть Ok
        return Ok();

    } // CreateOnlineRecordAsync

} // class RecordsController