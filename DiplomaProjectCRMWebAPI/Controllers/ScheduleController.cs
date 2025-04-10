using Application.Interfaces;
using Domain.Models.Dto;
using Domain.Models.Entities;
using Domain.Models.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DiplomaProjectCRMWebAPI.Controllers;

// ScheduleController - передаёт данные таблицы "РАСПИСАНИЕ" в JSON-формате
[ApiController]
[Route("api/{controller}/{action}")]
public class ScheduleController(IDbService dbService) : ControllerBase
{
    // получение ссылки на сервис-поставщик данных из базы данных
    // при помощи внедрения зависимости - через конструктор
    private readonly IDbService _dbService = dbService;


    // 1. по GET-запросу вернуть клиенту данные
    // о коллекции записей о рабочих днях из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllAsync() {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();

        // все записи таблицы
        var source = (await _dbService.GetAllScheduleAsync())
            .Select(WorkDay.WorkDayToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllAsync


    // 2. по GET-запросу вернуть клиенту данные о коллекции записей
    // о рабочих днях(включая удалённые) из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllWithDeletedAsync() {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();

        // все записи таблицы
        var source = (await _dbService.GetAllScheduleWithDeletedAsync())
            .Select(WorkDay.WorkDayToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllWithDeletedAsync


    // 3. по GET-запросу вернуть клиенту данные о коллекции
    // удалённых записей о рабочих днях из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllDeletedAsync() {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();

        // все записи таблицы
        var source = (await _dbService.GetAllDeletedScheduleAsync())
            .Select(WorkDay.WorkDayToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllDeletedAsync


    // 4. по GET-запросу вернуть клиенту данные о коллекции записей о рабочих
    // днях заданного сотрудника с соответствующими коллекциями промежутков
    // времени перерывов сотрудника за заданный период из БД в JSON-формате
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllWorkDaysBreakSlotsByEmployeeIdFromToAsync(
        int id, string firstDateString, string secondDateString) {

        // id -> employeeId

        // параметры типа DateTime приходят в виде строки,
        // поэтому используем дополнительные преобразования
        /*var year = int.Parse(firstDateString.Split("-")[0]);
        var month = int.Parse(firstDateString.Split("-")[1]);
        var day = int.Parse(firstDateString.Split("-")[2]);*/

        // дата первого дня периода
        //var firstDay = new DateTime(year, month, day);
        var firstDay = Utils.StringToDate(firstDateString);

        // дополнительные преобразования
        /*year = int.Parse(secondDateString.Split("-")[0]);
        month = int.Parse(secondDateString.Split("-")[1]);
        day = int.Parse(secondDateString.Split("-")[2]);*/

        // дата последнего дня периода
        //var lastDay = new DateTime(year, month, day);
        var lastDay = Utils.StringToDate(secondDateString);

        // если данных об идентификаторе сотрудника нет - вернуть некорректные данные
        // id = 0; // для проверки
        if (id <= 0)
            return BadRequest(new { EmployeeId = 0 });


        /*// все записи таблицы с указанным параметром за период
        var workDays =
            (await _dbService.GetAllScheduleByEmployeeIdFromToAsync(id, firstDay, lastDay))
            .Select(WorkDay.WorkDayToDto)
            .ToList();*/

        // все записи таблицы с указанным параметром за период
        var workDays = await _dbService
            .GetAllScheduleByEmployeeIdFromToAsync(id, firstDay, lastDay);

        // получить отображаемые данные(DTO)
        var displayWorkDaysBreakSlots = workDays
            .Select(workDay => new DisplayWorkDayBreakSlots(

                // DTO данных о рабочем дне
                WorkDay.WorkDayToDto(workDay),

                // коллекция DTO о промежутках времени для перерыва сотрудника
                Slot.SlotsToDto(
                    
                    // связи рабочего дня и промежутков для перерыва
                    workDay.WorkDaysBreakSlots
                     // выбрать НЕ_удалённые связи с НЕ_удалёнными промежутками
                    .Where(workDayBreakSlots => workDayBreakSlots.Deleted == null &&
                                                workDayBreakSlots.Slot.Deleted == null)
                    // выбрать промежутки
                    .Select(workDayBreakSlots => workDayBreakSlots.Slot)
                    // сортировка по времени начала промежутка
                    .OrderBy(slot => slot.From)
                    .ToList()

                ) // SlotsToDto
            ))
            // сортировка по возрастанию даты
            .OrderBy(displayWorkDayBreakSlots => displayWorkDayBreakSlots.WorkDay.Date)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(new { displayWorkDaysBreakSlots });

    } // GetAllWorkDaysBreakSlotsByEmployeeIdFromToAsync


    // 5. по PUT-запросу получить новые данные о рабочем дне сотрудника для создания
    // новой записи в таблице БД, вернуть клиенту Ok или сообщение об ошибке
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> CreateWorkDayAsync(
        [FromBody] DisplayWorkDayBreakSlots displayWorkDayBreakSlots) {

        // если данных о рабочем дне нет или Id некорректный - вернуть некорректные данные
        // if (true) // для проверки
        if (displayWorkDayBreakSlots == null || displayWorkDayBreakSlots.WorkDay.Id != 0)
            return BadRequest(new { WorkDayId = 0 });


        // 1) добавить в таблицу новую запись о рабочем дне сотрудника

        // 1. данные о сотруднике
        var employeeId = displayWorkDayBreakSlots.WorkDay.EmployeeId;


        // 2. дата рабочего дня
        var date = Utils.StringToDate(displayWorkDayBreakSlots.WorkDay.Date);


        // 3. признак рабочий/выходной
        var isWorking = displayWorkDayBreakSlots.WorkDay.IsWorking;


        // 4. время начала рабочего дня
        var startTime = Utils.StringToTime(displayWorkDayBreakSlots.WorkDay.StartTime);


        // 5. время окончания рабочего дня
        var endTime = Utils.StringToTime(displayWorkDayBreakSlots.WorkDay.EndTime);


        // 6. дата и время удаления записи о рабочем дне
        DateTime? deleted = null;


        // создание новой записи о рабочем дне
        var newWorkDay = new WorkDay(
            employeeId, date.Date, isWorking, startTime, endTime, deleted
        );

        // добавление записи в БД
        (bool isOk, string message) =
            await _dbService.CreateWorkDayAsync(newWorkDay);

        // если при добавлении была ошибка - передать ошибку
        // (bool isOk, string message) = (false, "привет-123!!!"); // для проверки
        if (!isOk)
            return BadRequest(new { CreateMessage = message });


        // 2) связать данную запись о рабочем дне с промежутками ДЛЯ ПЕРЕРЫВОВ

        // коллекция промежутков времени ДЛЯ ПЕРЕРЫВОВ сотрудника
        var breakSlots = displayWorkDayBreakSlots.Slots
            .Select(Slot.NewSlotFromDto).ToList();

        // связь записи о рабочем дне с каждым промежутком времени ДЛЯ ПЕРЕРЫВОВ сотрудника
        foreach (var slot in breakSlots) {

            // данные о промежутке времени
            // !!!(в данном случае все промежутки времени будут с Id=0)

            // поиск промежутка времени в таблице БД по параметрам промежутка времени
            var newSlot = await _dbService.GetSlotByParamsAsync(slot);

            // если newSlot.Id=0 - добавление записи в БД о новом промежутке времени
            if (newSlot.Id == 0) {

                // создание новой записи о промежутке времени
                newSlot = new Slot(slot.From, slot.Length, null);

                // добавление записи в БД
                (bool isOkCreateSlot, string messageCreateSlot) =
                    await _dbService.CreateSlotAsync(newSlot);

                // если при добавлении была ошибка - передать ошибку
                /*(bool isOkCreateSlot, string messageCreateSlot) =
                    (false, "привет-123!!!"); // для проверки*/
                if (!isOkCreateSlot)
                    return BadRequest(new { CreateMessage = messageCreateSlot });

            } // if

            // создание новой записи о связи нового рабочего дня
            // с найденным/добавленным промежутком времени для перерыва сотрудника
            var newWorkDayBreakSlot = new WorkDayBreakSlot(newWorkDay.Id, newSlot.Id, null);

            // добавление записи в БД
            (bool isOkCreateWorkDayBreakSlot, string messageCreateWorkDayBreakSlot) =
                await _dbService.CreateWorkDayBreakSlotAsync(newWorkDayBreakSlot);

            // если при добавлении была ошибка - передать ошибку
            /*(bool isOkCreateWorkDayBreakSlot, string messageCreateWorkDayBreakSlot) =
                (false, "привет-123!!!"); // для проверки*/
            if (!isOkCreateWorkDayBreakSlot)
                return BadRequest(new { CreateMessage = messageCreateWorkDayBreakSlot });

        } // foreach slot


        // 3) связать данную запись о рабочем дне с промежутками ДЛЯ ЗАПИСИ
        // !!! только если день РАБОЧИЙ!!!
        if (newWorkDay.IsWorking) {

            // 1. получить коллекцию часовых промежутков времени СВОБОДНЫХ ДЛЯ ЗАПИСИ
            //  для удобства отображения и записи клиентов
            var freeSlots = GetOneHourRegularIntervals(newWorkDay.StartTime, newWorkDay.EndTime);


            // 2. из полученных промежутков времени удалить промежутки ДЛЯ ПЕРЕРЫВОВ
            freeSlots = RemoveAllBreakSlots(freeSlots, breakSlots);


            // 3. связь записи о рабочем дне с каждым СВОБОДНЫМ ДЛЯ ЗАПИСИ промежутком времени
            foreach (var slot in freeSlots) {

                // данные о промежутке времени
                // !!!(в данном случае все промежутки времени будут с Id=0)

                // поиск промежутка времени в таблице БД по параметрам промежутка времени
                var newSlot = await _dbService.GetSlotByParamsAsync(slot);

                // если newSlot.Id=0 - добавление записи в БД о новом промежутке времени
                if (newSlot.Id == 0) {

                    // создание новой записи о промежутке времени
                    newSlot = new Slot(slot.From, slot.Length, null);

                    // добавление записи в БД
                    (bool isOkCreateSlot, string messageCreateSlot) =
                        await _dbService.CreateSlotAsync(newSlot);

                    // если при добавлении была ошибка - передать ошибку
                    /*(bool isOkCreateSlot, string messageCreateSlot) =
                        (false, "привет-123!!!"); // для проверки*/
                    if (!isOkCreateSlot)
                        return BadRequest(new { CreateMessage = messageCreateSlot });

                } // if


                // создание новой записи о связи нового рабочего дня
                // с найденным/добавленным промежутком времени СВОБОДНОГО ДЛЯ ЗАПИСИ
                var newWorkDayFreeSlot = new WorkDayFreeSlot(
                    newWorkDay.Id, newSlot.Id, null
                );

                // добавление записи в БД
                (bool isOkCreateWorkDayFreeSlot, string messageCreateWorkDayFreeSlot) =
                    await _dbService.CreateWorkDayFreeSlotAsync(newWorkDayFreeSlot);

                // если при добавлении была ошибка - передать ошибку
                /*(bool isOkCreateWorkDayFreeSlot, string messageCreateWorkDayFreeSlot) =
                    (false, "привет-123!!!"); // для проверки*/
                if (!isOkCreateWorkDayFreeSlot)
                    return BadRequest(new { CreateMessage = messageCreateWorkDayFreeSlot });

            } // foreach slot

        } // if


        // вернуть Ok
        return Ok();

    } // CreateWorkDayAsync


    // 6. по POST-запросу получить данные о рабочем дне сотрудника для изменения
    // записи в таблице БД, вернуть клиенту Ok или сообщение об ошибке
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> EditWorkDayAsync(
        [FromBody] DisplayWorkDayBreakSlots displayWorkDayBreakSlots) {

        // если данных о рабочем дне нет или Id некорректный - вернуть некорректные данные
        // if (true) // для проверки
        if (displayWorkDayBreakSlots == null || displayWorkDayBreakSlots.WorkDay.Id <= 0)
            return BadRequest(new { WorkDayId = 0 });


        // 1) данные для изменения записи в БД о рабочем дне:
        // (меняем данные и ссылки при наличии изменений)

        // получить изменяемую запись о рабочем дне по Id
        var workDayEdt = await _dbService
            .GetWorkDayByIdAsync(displayWorkDayBreakSlots.WorkDay.Id);

        // если запись не найдена(Id=0) - вернуть сообщение
        // об ошибке 401(НЕ АВТОРИЗОВАН)
        // workDayEdt.Id = 0; // для проверки
        if (workDayEdt.Id == 0)
            return Unauthorized(new { WorkDayId = displayWorkDayBreakSlots.WorkDay.Id });


        // 1. данные о сотруднике
        // workDayEdt.EmployeeId = 999; // для проверки
        if (workDayEdt.EmployeeId != displayWorkDayBreakSlots.WorkDay.EmployeeId) {

            // получить запись в БД о сотруднике по Id
            var employee = await _dbService
                .GetEmployeeByIdAsync(displayWorkDayBreakSlots.WorkDay.EmployeeId);

            // если данных о сотруднике нет - вернуть некорректные данные
            // employee.Id = 0; // для проверки
            if (employee.Id == 0)
                return BadRequest(new { EmployeeId = 0 });


            // изменить данные
            workDayEdt.EmployeeId = employee.Id;

            // изменить ссылку
            workDayEdt.Employee = employee;

        } // if


        // 2. дата рабочего дня
        // если данные о дате рабочего дня не соответствуют - вернуть некорректные данные
        var srcDate = Utils.StringToDate(displayWorkDayBreakSlots.WorkDay.Date);
        // workDayEdt.Date = DateTime.Now.AddDays(-100); // для проверки
        if (workDayEdt.Date.Date != srcDate.Date)
            return BadRequest(new { Date = 0 });


        // 3. признак рабочий/выходной
        if (workDayEdt.IsWorking != displayWorkDayBreakSlots.WorkDay.IsWorking)
            workDayEdt.IsWorking = displayWorkDayBreakSlots.WorkDay.IsWorking;


        // 4. время начала рабочего дня
        var srcStartTime = Utils.StringToTime(displayWorkDayBreakSlots.WorkDay.StartTime);
        if (workDayEdt.StartTime != srcStartTime)
            workDayEdt.StartTime = srcStartTime;


        // 5. время окончания рабочего дня
        var srcEndTime = Utils.StringToTime(displayWorkDayBreakSlots.WorkDay.EndTime);
        if (workDayEdt.EndTime != srcEndTime)
            workDayEdt.EndTime = srcEndTime;


        // изменение записи в БД
        (bool isOk, string message) =
            await _dbService.UpdateWorkDayAsync(workDayEdt);

        // если при изменении была ошибка - передать ошибку
        // (bool isOk, string message) = (false, "привет-123!!!"); // для проверки
        if (!isOk)
            return BadRequest(new { UpdateMessage = message });



        // 2) изменение связей данной записи о рабочем дне с промежутками ДЛЯ ПЕРЕРЫВОВ

        // коллекция промежутков времени ДЛЯ ПЕРЕРЫВОВ сотрудника, полученная от клиента
        var breakSlots = displayWorkDayBreakSlots.Slots
            .Select(Slot.NewSlotFromDto).ToList();

        // выбрать "старые" промежутки с идентификаторами
        var oldBreakSlots = breakSlots.Where(slot => slot.Id > 0).ToList();

        // связи рабочего дня с промежутками ДЛЯ ПЕРЕРЫВОВ
        var workDaysBreakSlots = workDayEdt.WorkDaysBreakSlots
            .Where(workDayBreakSlot => workDayBreakSlot.Deleted == null &&
                                       workDayBreakSlot.Slot.Deleted == null).ToList();

        // если день "РАБОЧИЙ" - выбрать все связи, которые НЕ содержат в себе
        // идентификаторов пришедших с клиента промежутков времени для перерыва
        // (эти связи удаляем)
        if (workDayEdt.IsWorking) {

            // выбрать связи, которые НЕ содержат идентификаторы "старых" промежутков
            //var selectedWorkDaysBreakSlots = workDaysBreakSlots;
            oldBreakSlots.ForEach(slot => {

                /*selectedWorkDaysBreakSlots = [.. selectedWorkDaysBreakSlots
                    .Where(workDayBreakSlot => workDayBreakSlot.SlotId != slot.Id)];*/

                workDaysBreakSlots.RemoveAll(workDayBreakSlot =>
                    workDayBreakSlot.SlotId == slot.Id);

            }); // ForEach slot

            // остались только те, которые не содержат Id "старых" промежутков
            // (их удалим)
            //workDaysBreakSlots = selectedWorkDaysBreakSlots;

        } // if


        // удаление каждой связи:
        // "ВЫХОДНОЙ" день - все связи, "РАБОЧИЙ" - выбранные, без "старых"
        foreach (var workDayBreakSlot in workDaysBreakSlots) {

            // установить в записи данных о связи время и дату удаления записи
            workDayBreakSlot.Deleted = DateTime.Now;

            // изменение записи в БД
            (bool isOkUpdateWorkDayBreakSlot, string messageUpdateWorkDayBreakSlot) =
                await _dbService.UpdateWorkDayBreakSlotAsync(workDayBreakSlot);

            // если при изменении была ошибка - передать ошибку
            /*(bool isOkUpdateWorkDayBreakSlot, string messageUpdateWorkDayBreakSlot) =
                (false, "привет-123!!!"); // для проверки*/
            if (!isOkUpdateWorkDayBreakSlot)
                return BadRequest(new { UpdateMessage = messageUpdateWorkDayBreakSlot });

        } // foreach workDayBreakSlot


        // для "РАБОЧЕГО" дня - изменить все связи
        if (workDayEdt.IsWorking) {

            // 1. изменение связей со "старыми" промежутками

            // связи рабочего дня со "старыми" промежутками ДЛЯ ПЕРЕРЫВОВ
            workDaysBreakSlots = [.. workDayEdt.WorkDaysBreakSlots
                .Where(workDayBreakSlot => workDayBreakSlot.Deleted == null &&
                                           workDayBreakSlot.Slot.Deleted == null)];

            // на каждой из оставшихся связей, если изменения были,
            // !!!заменить промежуток!!!(!!!промежуток изменять нельзя!!!)
            foreach (var workDayBreakSlot in workDaysBreakSlots) {

                // поиск промежутка времени в таблице БД по Id
                //var oldSlot = await _dbService.GetSlotByIdAsync(workDayBreakSlot.SlotId);

                // данные о промежутке из виртуального свойства связи

                // выбрать "старый" промежуток по Id из пришедших с клиента
                var oldBreakSlot = oldBreakSlots
                    .Find(slot => slot.Id == workDayBreakSlot.SlotId) ?? new Slot();

                // если промежуток не найден, выходим из цикла
                if (oldBreakSlot.Id == 0) break;

                // заменить, если были изменения
                if (workDayBreakSlot.Slot.From != oldBreakSlot.From ||
                    workDayBreakSlot.Slot.Length != oldBreakSlot.Length) {

                    // поиск промежутка времени в таблице БД по параметрам промежутка времени
                    var newSlot = await _dbService.GetSlotByParamsAsync(oldBreakSlot);

                    // если newSlot.Id=0 - добавление записи в БД о новом промежутке времени
                    if (newSlot.Id == 0) {

                        // создание новой записи о промежутке времени
                        newSlot = new Slot(oldBreakSlot.From, oldBreakSlot.Length, null);

                        // добавление записи в БД
                        (bool isOkCreateSlot, string messageCreateSlot) =
                            await _dbService.CreateSlotAsync(newSlot);

                        // если при добавлении была ошибка - передать ошибку
                        /*(bool isOkCreateSlot, string messageCreateSlot) =
                            (false, "привет-123!!!"); // для проверки*/
                        if (!isOkCreateSlot)
                            return BadRequest(new { CreateMessage = messageCreateSlot });

                    } // if

                    // изменить данные
                    workDayBreakSlot.SlotId = newSlot.Id;

                    // изменить ссылку
                    workDayBreakSlot.Slot = newSlot;

                    // изменение записи в БД
                    (bool isOkUpdateWorkDayBreakSlot, string messageUpdateWorkDayBreakSlot) =
                        await _dbService.UpdateWorkDayBreakSlotAsync(workDayBreakSlot);

                    // если при изменении была ошибка - передать ошибку
                    /*(bool isOkUpdateWorkDayBreakSlot, string messageUpdateWorkDayBreakSlot) =
                        (false, "привет-123!!!"); // для проверки*/
                    if (!isOkUpdateWorkDayBreakSlot)
                        return BadRequest(new { UpdateMessage = messageUpdateWorkDayBreakSlot });

                } // if

            } // foreach workDayBreakSlot


            // 2. добавление связей с "новыми" промежутками

            // выбрать "новые" промежутки с нулевыми идентификаторами
            var newBreakSlots = breakSlots.Where(slot => slot.Id == 0).ToList();

            // связь записи о рабочем дне с "новыми" промежутками времени ДЛЯ ПЕРЕРЫВОВ сотрудника
            foreach (var slot in newBreakSlots) {

                // поиск промежутка времени в таблице БД по параметрам промежутка времени
                var newSlot = await _dbService.GetSlotByParamsAsync(slot);

                // если newSlot.Id=0 - добавление записи в БД о новом промежутке времени
                if (newSlot.Id == 0) {

                    // создание новой записи о промежутке времени
                    newSlot = new Slot(slot.From, slot.Length, null);

                    // добавление записи в БД
                    (bool isOkCreateSlot, string messageCreateSlot) =
                        await _dbService.CreateSlotAsync(newSlot);

                    // если при добавлении была ошибка - передать ошибку
                    /*(bool isOkCreateSlot, string messageCreateSlot) =
                        (false, "привет-123!!!"); // для проверки*/
                    if (!isOkCreateSlot)
                        return BadRequest(new { CreateMessage = messageCreateSlot });

                } // if

                // создание новой записи о связи нового рабочего дня
                // с найденным/добавленным промежутком времени для перерыва сотрудника
                var newWorkDayBreakSlot = new WorkDayBreakSlot(workDayEdt.Id, newSlot.Id, null);

                // добавление записи в БД
                (bool isOkCreateWorkDayBreakSlot, string messageCreateWorkDayBreakSlot) =
                    await _dbService.CreateWorkDayBreakSlotAsync(newWorkDayBreakSlot);

                // если при добавлении была ошибка - передать ошибку
                /*(bool isOkCreateWorkDayBreakSlot, string messageCreateWorkDayBreakSlot) =
                    (false, "привет-123!!!"); // для проверки*/
                if (!isOkCreateWorkDayBreakSlot)
                    return BadRequest(new { CreateMessage = messageCreateWorkDayBreakSlot });

            } // foreach slot

        } // if



        // 3) изменение связей данной записи о рабочем дне с промежутками ДЛЯ ЗАПИСИ

        // связи рабочего дня с промежутками ДЛЯ ЗАПИСИ
        var workDaysFreeSlots = workDayEdt.WorkDaysFreeSlots
            .Where(workDayFreeSlot => workDayFreeSlot.Deleted == null &&
                                      workDayFreeSlot.Slot.Deleted == null).ToList();

        // удаляем все связи
        foreach (var workDayFreeSlot in workDaysFreeSlots) {

            // установить в записи данных о связи время и дату удаления записи
            workDayFreeSlot.Deleted = DateTime.Now;

            // изменение записи в БД
            (bool isOkUpdateWorkDayFreeSlot, string messageUpdateWorkDayFreeSlot) =
                await _dbService.UpdateWorkDayFreeSlotAsync(workDayFreeSlot);

            // если при изменении была ошибка - передать ошибку
            /*(bool isOkUpdateWorkDayFreeSlot, string messageUpdateWorkDayFreeSlot) =
                (false, "привет-123!!!"); // для проверки*/
            if (!isOkUpdateWorkDayFreeSlot)
                return BadRequest(new { UpdateMessage = messageUpdateWorkDayFreeSlot });

        } // foreach workDayFreeSlot


        // если рабочий день "РАБОЧИЙ"!!! - связать данную запись о рабочем дне
        // с промежутками ДЛЯ ЗАПИСИ так, как и при создании записи о рабочем дне
        if (workDayEdt.IsWorking) {

            // 1. получить коллекцию часовых промежутков времени СВОБОДНЫХ ДЛЯ ЗАПИСИ
            //  для удобства отображения и записи клиентов
            var freeSlots = GetOneHourRegularIntervals(workDayEdt.StartTime, workDayEdt.EndTime);


            // 2. из полученных промежутков времени удалить промежутки ДЛЯ ПЕРЕРЫВОВ
            freeSlots = RemoveAllBreakSlots(freeSlots, breakSlots);


            // 3. связь записи о рабочем дне с каждым СВОБОДНЫМ ДЛЯ ЗАПИСИ промежутком времени
            foreach (var slot in freeSlots) {

                // поиск промежутка времени в таблице БД по параметрам промежутка времени
                var newSlot = await _dbService.GetSlotByParamsAsync(slot);

                // если newSlot.Id=0 - добавление записи в БД о новом промежутке времени
                if (newSlot.Id == 0) {

                    // создание новой записи о промежутке времени
                    newSlot = new Slot(slot.From, slot.Length, null);

                    // добавление записи в БД
                    (bool isOkCreateSlot, string messageCreateSlot) =
                        await _dbService.CreateSlotAsync(newSlot);

                    // если при добавлении была ошибка - передать ошибку
                    /*(bool isOkCreateSlot, string messageCreateSlot) =
                        (false, "привет-123!!!"); // для проверки*/
                    if (!isOkCreateSlot)
                        return BadRequest(new { CreateMessage = messageCreateSlot });

                } // if


                // создание новой записи о связи изменяемого рабочего дня
                // с найденным/добавленным промежутком времени СВОБОДНОГО ДЛЯ ЗАПИСИ
                var newWorkDayFreeSlot = new WorkDayFreeSlot(workDayEdt.Id, newSlot.Id, null);

                // добавление записи в БД
                (bool isOkCreateWorkDayFreeSlot, string messageCreateWorkDayFreeSlot) =
                    await _dbService.CreateWorkDayFreeSlotAsync(newWorkDayFreeSlot);

                // если при добавлении была ошибка - передать ошибку
                /*(bool isOkCreateWorkDayFreeSlot, string messageCreateWorkDayFreeSlot) =
                    (false, "привет-123!!!"); // для проверки*/
                if (!isOkCreateWorkDayFreeSlot)
                    return BadRequest(new { CreateMessage = messageCreateWorkDayFreeSlot });

            } // foreach slot

        } // if


        // вернуть Ok
        return Ok();

    } // EditWorkDayAsync


    // метод "нарезки" промежутка времени на равномерные часовые промежутки
    private List<Slot> GetOneHourRegularIntervals(TimeOnly startTime, TimeOnly endTime) {

        // промежуток времени всего рабочего дня
        var allWorkDaySlot = new Slot(startTime, (int)(endTime - startTime).TotalSeconds, null);

        // коллекция промежутков времени ДЛЯ ЗАПИСИ
        List<Slot> freeSlots = [];


        // если начало промежутка времени всего рабочего дня начинается не с нуля минут,
        // то сдвинем промежуток до ближайшего времени с нулями в минутах
        var hours = allWorkDaySlot.From.Hour;
        var minutes = allWorkDaySlot.From.Minute;

        if (minutes != 0) {

            // новое начало промежутка всего дня
            var newStartTime = new TimeOnly(hours + 1, 0);

            // промежуток от старого начала до нового начала
            var startSlot = new Slot(allWorkDaySlot.From,
                (int)(newStartTime - allWorkDaySlot.From).TotalSeconds, null);

            // сместим промежуток всего дня
            allWorkDaySlot.From = newStartTime;
            allWorkDaySlot.Length -= startSlot.Length;

            // добавим промежуток от старого начала до нового начала первым
            // элементом в коллекцию промежутков времени ДЛЯ ЗАПИСИ
            freeSlots.Add(startSlot);

        } // if

        // "нарежем" промежуток времени всего рабочего дня на часовые промежутки
        while (allWorkDaySlot.Length >= 3600) { // пока НЕ меньше часа

            // новый часовой промежуток
            var newSlot = new Slot(allWorkDaySlot.From, 3600, null);

            // добавим новый часовой промежуток в коллекцию промежутков времени ДЛЯ ЗАПИСИ
            freeSlots.Add(newSlot);

            // сместим промежуток всего дня на 1 час
            allWorkDaySlot.From = new TimeOnly(allWorkDaySlot.From.Hour + 1, 0);
            allWorkDaySlot.Length -= 3600;

        } // while

        // оставшийся после цикла промежуток, меньший часа, также
        // добавим в коллекцию промежутков времени ДЛЯ ЗАПИСИ
        if (allWorkDaySlot.Length > 0)
            freeSlots.Add(allWorkDaySlot);

        return freeSlots;

    } // GetOneHourRegularIntervals


    // метод удаления промежутков времени из коллекции промежутков времени
    private List<Slot> RemoveAllBreakSlots(List<Slot> freeSlots, List<Slot> breakSlots) {

        breakSlots.ForEach(slot => {

            // самое раннее время рабочих промежутков
            var minTime = freeSlots.Min(slot => slot.From);

            // самое позднее время рабочих промежутков
            var maxTime = freeSlots.Max(slot => slot.To);

            // полное время всех промежутков ДЛЯ ЗАПИСИ
            var allFreeSlotsLength = freeSlots.Sum(slot => slot.Length);

            // удаляем промежуток, если он находится в пределах граничных значений
            if (slot.From >= minTime && slot.To <= maxTime) {

                // точка начала промежутка перерыва
                var startPoint = slot.From;

                // точка окончания промежутка перерыва
                var endPoint = slot.To;

                // 1. удалить промежутки, которые полностью
                // находятся внутри промежутка перерыва
                freeSlots.RemoveAll(slot => slot.From >= startPoint && slot.To <= endPoint);

                // если было удалено всё время промежутка перерыва - выходим из цикла
                if (allFreeSlotsLength - freeSlots.Sum(slot => slot.Length) != slot.Length) {

                    // 2. удаление на раннем крае(левом)

                    // найти индекс элемента, содержащего точку начала промежутка перерыва
                    var index = freeSlots.FindIndex(slot =>
                        slot.From <= startPoint && slot.To > startPoint);

                    // если найден - удаление
                    if (index != -1) {

                        // 2.1 если начало совпадает, то перерыв внутри
                        if (freeSlots[index].From == startPoint) {

                            // сдвинем начало промежутка к концу перерыва
                            freeSlots[index].From = endPoint;
                            freeSlots[index].Length -= slot.Length;

                        }
                        else {

                            // 2.2 окончание перерыва в пределах найденного промежутка
                            if (freeSlots[index].To > endPoint) {

                                // а) получим новый промежуток от окончания перерыва до
                                // окончания рабочего промежутка
                                var tempSlot = new Slot(
                                    endPoint,
                                    (int)(freeSlots[index].To - endPoint).TotalSeconds,
                                    null
                                );

                                // б) окончание промежутка сдвигаем в начало перерыва
                                freeSlots[index].Length =
                                    (int)(startPoint - freeSlots[index].From).TotalSeconds;

                                // в) вставить в коллекцию новый промежуток
                                freeSlots.Insert(index + 1, tempSlot);

                            }
                            else {

                                // 2.3 окончание перерыва выходит за пределы найденного промежутка

                                // а) окончание промежутка сдвигаем в начало перерыва
                                freeSlots[index].Length =
                                    (int)(startPoint - freeSlots[index].From).TotalSeconds;

                                // б) если перерыв захватывает следующий промежуток
                                if (freeSlots[index + 1].From < endPoint) {

                                    // сдвинем начало следующего промежутка к концу перерыва
                                    freeSlots[index + 1].Length -=
                                        (int)(endPoint - freeSlots[index + 1].From).TotalSeconds;
                                    freeSlots[index + 1].From = endPoint;

                                } // if

                            } // if

                        } // if

                    } // if


                    // 3. удаление на позднем крае(правом)

                    // найти индекс элемента, содержащего точку окончания промежутка перерыва
                    index = freeSlots.FindIndex(slot =>
                        slot.From < endPoint && slot.To >= endPoint);

                    // если найден - удаление
                    if (index != -1) {

                        // 3.1 если окончание совпадает, то перерыв внутри
                        if (freeSlots[index].To == endPoint) {

                            // сократим длительность промежутка на значение
                            // длительности перерыва
                            freeSlots[index].Length -= slot.Length;

                        }
                        else {

                            // 3.2 начало перерыва в пределах найденного промежутка
                            if (freeSlots[index].From < startPoint) {

                                // а) получим новый промежуток от окончания перерыва до
                                // окончания рабочего промежутка
                                var tempSlot = new Slot(
                                    endPoint, (int)(freeSlots[index].To - endPoint).TotalSeconds, null
                                );

                                // б) окончание промежутка сдвигаем в начало перерыва
                                freeSlots[index].Length =
                                    (int)(startPoint - freeSlots[index].From).TotalSeconds;

                                // в) вставить в коллекцию новый промежуток
                                freeSlots.Insert(index + 1, tempSlot);

                            }
                            else {

                                // 3.3 начало перерыва выходит за пределы найденного промежутка

                                // а) начало промежутка сдвигаем в конец перерыва
                                freeSlots[index].Length -=
                                    (int)(endPoint - freeSlots[index].From).TotalSeconds;
                                freeSlots[index].From = endPoint;

                                // б) если перерыв захватывает предыдущий промежуток
                                if (freeSlots[index - 1].To > startPoint) {

                                    // сдвинем конец предыдущего промежутка в начало перерыва
                                    freeSlots[index - 1].Length -=
                                        (int)(freeSlots[index - 1].To - startPoint).TotalSeconds;

                                } // if

                            } // if

                        } // if

                    } // if

                } // if

            } // if

        }); // ForEach slot

        return freeSlots;

    } // RemoveAllBreakSlots


} // class ScheduleController