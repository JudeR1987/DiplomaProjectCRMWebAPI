using Microsoft.AspNetCore.Mvc;
using Domain.Models.Dto;
using Application.Interfaces;

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
        //Task.Delay(1_500).Wait();

        // все записи таблицы
        var source = (await _dbService.GetAllServicesAsync())
            .Select(service => new ServiceDto(
                service.Id, service.Name,
                new ServicesCategoryDto(
                    service.ServicesCategory.Id,
                    service.ServicesCategory.Name,
                    service.ServicesCategory.Weight),
                service.PriceMin, service.PriceMax, service.Duration,
                service.ServiceType, service.Comment, service.Weight,
                new List<string>(service.ImageGroup)))
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllAsync

} // class ServicesController