using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Domain.Models.Dto;

namespace DiplomaProjectCRMWebAPI.Controllers;

// ServicesController - передаёт данные таблицы "УСЛУГИ" в JSON-формате
[ApiController]
[Route("api/{controller}/{action}")]
public class ServicesController(IDbService dbService) : ControllerBase
{
    // получение ссылки на сервис-поставщик данных из базы данных
    // при помощи внедрения зависимости - через конструктор
    private readonly IDbService _dbService = dbService;


    // 1. по GET-запросу вернуть клиенту данные
    // о коллекции записей об услугах из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllAsync() {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();

        // все записи таблицы
        var source = (await _dbService.GetAllServicesAsync())
            .Select(Service.ServiceToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllAsync


    // 2. по GET-запросу вернуть клиенту данные о коллекции записей
    // об услугах(включая удалённые) из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllWithDeletedAsync() {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();

        // все записи таблицы
        var source = (await _dbService.GetAllServicesWithDeletedAsync())
            .Select(Service.ServiceToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllWithDeletedAsync


    // 3. по GET-запросу вернуть клиенту данные о коллекции
    // удалённых записей об услугах из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllDeletedAsync() {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();

        // все записи таблицы
        var source = (await _dbService.GetAllDeletedServicesAsync())
            .Select(Service.ServiceToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllDeletedAsync


    // 4. по GET-запросу вернуть клиенту данные о коллекции записей
    // об услугах для заданной компании из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllByCompanyIdAsync(int id) {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();

        // все записи таблицы с указанным параметром
        var source = (await _dbService.GetAllServicesByCompanyIdAsync(id))
            .Select(Service.ServiceToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllByCompanyIdAsync


    // 5. по GET-запросу вернуть клиенту данные о коллекции записей об услугах
    // для заданной компании, сгруппированные по категориям услуг из БД в JSON-формате
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllByCompanyIdGroupByCategoriesAsync(
        [FromQuery] int id) {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();


        // если данных об идентификаторе компании нет - вернуть некорректные данные
        // id = 0; // для проверки
        if (id <= 0)
            return BadRequest(new { CompanyId = 0 });


        // получить отображаемые данные(DTO) об услугах
        // для заданной компании, сгруппированные по категориям услуг
        var displayServicesCategories = await _dbService
            .GetAllServicesByCompanyIdGroupByCategoriesAsync(id);
        
        /*var displayServicesCategories = await _db.Services
            .Where(service => service.CompanyId == companyId && service.Deleted == null)
            .GroupBy(service => service.ServicesCategory,
                (key, group) => new {
                    ServicesCategory = key,
                    Services = group.ToList()
                })
            .Select(group => new DisplayServicesCategory(
                ServicesCategory.ServicesCategoryToDto(group.ServicesCategory),
                Service.ServicesToDto(group.Services)
                ))
            .ToListAsync();*/

        // вернуть данные в JSON-формате
        return new JsonResult(new { displayServicesCategories });

    } // GetAllByCompanyIdGroupByCategoriesAsync


    // 6. по GET-запросу вернуть клиенту данные о записи об услуге
    // по её идентификатору из БД в JSON-формате
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetByIdAsync([FromQuery] int id) {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();


        // если данных об идентификаторе услуги нет - вернуть некорректные данные
        // id = 0; // для проверки
        if (id <= 0)
            return BadRequest(new { ServiceId = 0 });


        // поиск записи об услуге по Id
        var service = await _dbService.GetServiceByIdAsync(id);

        // если услуга не найдена(Id=0) - вернуть сообщение
        // об ошибке 401(НЕ АВТОРИЗОВАН)
        // service.Id = 0; // для проверки
        if (service.Id == 0)
            return Unauthorized(new { ServiceId = id });


        // получить отображаемые данные об услуге(DTO)
        var displayService = Service.ServiceToDto(service);

        // вернуть данные в JSON-формате
        return new JsonResult(new { Service = displayService });

    } // GetByIdAsync


    // 7. по PUT-запросу получить новые данные об услуге компании для создания
    // новой записи в таблице БД, вернуть клиенту Ok или сообщение об ошибке
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> CreateServiceAsync([FromBody] ServiceDto service) {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();
        //var temp = Request;


        // если данных об услуге компании нет - вернуть некорректные данные
        // if (true) // для проверки
        if (service == null || service.Id < 0)
            return BadRequest(new { ServiceId = 0 });


        // данные для создания новой записи в БД об услуге компании:

        // 1. наименование услуги
        var serviceName = service.Name;


        // 2. данные о категории услуг

        // получить данные о категории услуг компании
        var servicesCategoryId = service.ServicesCategory.Id;

        // --- проверка, потом удалить
        var allServicesCategories = await _dbService.GetAllServicesCategoriesAsync();
        // ---

        // если servicesCategoryId=0 - добавление записи в БД о новой категории услуг
        var servicesCategory = new ServicesCategory();
        if (servicesCategoryId == 0) {

            // создание новой записи о категории услуг
            servicesCategory = new ServicesCategory(service.ServicesCategory.Name, null);

            // добавление записи в БД
            (bool isOkCreateServicesCategory, string messageCreateServicesCategory) =
                await _dbService.CreateServicesCategoryAsync(servicesCategory);

            // если при добавлении была ошибка - передать ошибку
            /*(bool isOkCreateServicesCategory, string messageCreateServicesCategory) =
                (false, "привет-123!!!"); // для проверки*/
            if (!isOkCreateServicesCategory)
                return BadRequest(new { CreateMessage = messageCreateServicesCategory });

        } else {
            // иначе - получить запись по Id из БД
            servicesCategory = await _dbService.GetServicesCategoryByIdAsync(servicesCategoryId);

            // если запись о категории услуг не найдена - вернуть некорректные данные
            // servicesCategory.Id = 0; // для проверки
            if (servicesCategory.Id == 0)
                return BadRequest(new { ServicesCategoryId = 0 });

        } // if

        // --- проверка, потом удалить
        var allServicesCategories2 = await _dbService.GetAllServicesCategoriesAsync();
        var x = 999; // для точки останова
        // ---


        // 3. данные о компании, для которой услуга определена
        var companyId = service.CompanyId;

        // если данных о компании нет - вернуть некорректные данные
        // companyId = 0; // для проверки
        if (companyId <= 0)
            return BadRequest(new { CompanyId = 0 });


        // 4. минимальная цена на услугу
        var priceMin = service.PriceMin;


        // 5. максимальная цена на услугу
        var priceMax = service.PriceMax;


        // 6. длительность услуги
        var duration = service.Duration;


        // 7. комментарий к услуге
        var comment = service.Comment == "" ? null : service.Comment;


        // 8. дата и время удаления записи об услуге
        DateTime? deleted = null;


        // создание новой записи об услуге компании
        var newService = new Service(
            serviceName, servicesCategory.Id, companyId,
            priceMin, priceMax, duration, comment, deleted
        );

        // добавление записи в БД
        (bool isOk, string message) =
            await _dbService.CreateServiceAsync(newService);

        // если при добавлении была ошибка - передать ошибку
        // (bool isOk, string message) = (false, "привет-123!!!"); // для проверки
        if (!isOk)
            return BadRequest(new { CreateMessage = message });


        // вернуть Ok
        return Ok();

    } // CreateServiceAsync


    // 8. по POST-запросу получить данные об услуге компании для изменения
    // записи в таблице БД, вернуть клиенту Ok или сообщение об ошибке
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> EditServiceAsync([FromBody] ServiceDto service) {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();
        // var temp = Request;

        // если данных об услуге компании нет - вернуть некорректные данные
        //if (true) // для проверки
        if (service == null || service.Id <= 0)
            return BadRequest(new { ServiceId = 0 });


        // данные для изменения записи в БД об услуге компании:
        // (меняем данные и ссылки при наличии изменений)

        // получить изменяемую запись об услуге компании по Id
        var serviceEdt = await _dbService.GetServiceByIdAsync(service.Id);

        // если услуга не найдена(Id=0) - вернуть сообщение
        // об ошибке 401(НЕ АВТОРИЗОВАН)
        // serviceEdt.Id = 0; // для проверки
        if (serviceEdt.Id == 0)
            return Unauthorized(new { ServiceId = service.Id });


        // 1. наименование услуги
        if (serviceEdt.Name != service.Name)
            serviceEdt.Name = service.Name;


        // 2. данные о категории услуг
        if (serviceEdt.ServicesCategoryId != service.ServicesCategory.Id) {

            // получить данные о категории услуг компании
            var servicesCategoryId = service.ServicesCategory.Id;

            // --- проверка, потом удалить
            var allServicesCategories = await _dbService.GetAllServicesCategoriesAsync();
            // ---

            // если servicesCategoryId=0 - добавление записи в БД о новой категории услуг
            var servicesCategory = new ServicesCategory();
            if (servicesCategoryId == 0) {

                // создание новой записи о категории услуг
                servicesCategory = new ServicesCategory(service.ServicesCategory.Name, null);

                // добавление записи в БД
                (bool isOkCreateServicesCategory, string messageCreateServicesCategory) =
                    await _dbService.CreateServicesCategoryAsync(servicesCategory);

                // если при добавлении была ошибка - передать ошибку
                /*(bool isOkCreateServicesCategory, string messageCreateServicesCategory) =
                    (false, "привет-123!!!"); // для проверки*/
                if (!isOkCreateServicesCategory)
                    return BadRequest(new { CreateMessage = messageCreateServicesCategory });

            } else {
                // иначе - получить запись по Id из БД
                servicesCategory = await _dbService
                    .GetServicesCategoryByIdAsync(servicesCategoryId);

                // если запись о категории услуг не найдена - вернуть некорректные данные
                // servicesCategory.Id = 0; // для проверки
                if (servicesCategory.Id == 0)
                    return BadRequest(new { ServicesCategoryId = 0 });

            } // if

            // --- проверка, потом удалить
            var allServicesCategories2 = await _dbService.GetAllServicesCategoriesAsync();
            var x = 999; // для точки останова
            // ---


            // изменить данные
            serviceEdt.ServicesCategoryId = servicesCategory.Id;

            // изменить ссылку на запись в БД
            serviceEdt.ServicesCategory = servicesCategory;

        } // if


        // 3. данные о компании, для которой услуга определена
        // serviceEdt.CompanyId = 999; // для проверки
        if (serviceEdt.CompanyId != service.CompanyId) {
            
            // получить запись в БД о компании по Id
            var company = await _dbService.GetCompanyByIdAsync(service.CompanyId);

            // если данных о компании нет - вернуть некорректные данные
            // company.Id = 0; // для проверки
            if (company.Id == 0)
                return BadRequest(new { CompanyId = 0 });


            // изменить данные
            serviceEdt.CompanyId = company.Id;

            // изменить ссылку
            serviceEdt.Company = company;

        } // if


        // 4. минимальная цена на услугу
        if (serviceEdt.PriceMin != service.PriceMin)
            serviceEdt.PriceMin = service.PriceMin;


        // 5. максимальная цена на услугу
        if (serviceEdt.PriceMax != service.PriceMax)
            serviceEdt.PriceMax = service.PriceMax;


        // 6. длительность услуги
        if (serviceEdt.Duration != service.Duration)
            serviceEdt.Duration = service.Duration;


        // 7. комментарий к услуге
        var comment = service.Comment == "" ? null : service.Comment;
        
        if (serviceEdt.Comment != comment)
            serviceEdt.Comment = comment;


        // изменение записи в БД
        (bool isOk, string message) =
            await _dbService.UpdateServiceAsync(serviceEdt);

        // если при изменении была ошибка - передать ошибку
        // (bool isOk, string message) = (false, "привет-123!!!"); // для проверки
        if (!isOk)
            return BadRequest(new { UpdateMessage = message });


        // вернуть Ok
        return Ok();

    } // EditServiceAsync


    // 9. по DELETE-запросу удалить данные об услуге
    // и вернуть клиенту Ok, или сообщение об ошибке
    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteServiceAsync([FromQuery] int id) {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();

        // если данных об услуге нет - вернуть некорректные данные
        // id = 0; // для проверки
        if (id <= 0)
            return BadRequest(new { ServiceId = 0 });


        // поиск услуги по Id
        var service = await _dbService.GetServiceByIdAsync(id);

        // если услуга не найдена(Id=0) - вернуть сообщение
        // об ошибке 401(НЕ АВТОРИЗОВАН)
        // service.Id = 0; // для проверки
        if (service.Id == 0)
            return Unauthorized(new { ServiceId = id });


        // установить в записи данных об услуге
        // время и дату удаления записи
        service.Deleted = DateTime.Now;

        // изменение записи в БД
        (bool isOk, string message) =
            await _dbService.UpdateServiceAsync(service);

        // если при изменении была ошибка - передать ошибку
        // (bool isOk, string message) = (false, "привет-123!!!"); // для проверки
        if (!isOk)
            return BadRequest(new { UpdateMessage = message });


        // вернуть Ok
        return Ok();

    } // DeleteServiceAsync

} // class ServicesController