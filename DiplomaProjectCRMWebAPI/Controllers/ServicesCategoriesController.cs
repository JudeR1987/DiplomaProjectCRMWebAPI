using Microsoft.AspNetCore.Mvc;
using Domain.Models.Dto;
using Application.Interfaces;

namespace DiplomaProjectCRMWebAPI.Controllers;

// ServicesCategoriesController - передаёт данные
// таблицы "КАТЕГОРИИ_УСЛУГ" в JSON-формате
[ApiController]
[Route("api/{controller}/{action}")]
public class ServicesCategoriesController(IDbService dbService) : ControllerBase
{
    // получение ссылки на сервис-поставщик данных из базы данных
    // при помощи внедрения зависимости - через конструктор
    private readonly IDbService _dbService = dbService;


    // 1. по GET-запросу вернуть клиенту данные
    // о коллекции записей о категориях услуг из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllAsync() {

        // имитация временной задержки
        //Task.Delay(1_500).Wait();

        // все записи таблицы
        var source = (await _dbService.GetAllServicesCategoriesAsync())
            .Select(servicesCategory => new ServicesCategoryDto(
                servicesCategory.Id,
                servicesCategory.Name,
                servicesCategory.Weight))
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllAsync

} // class ServicesCategoriesController