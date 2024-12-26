using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiplomaProjectCRMWebAPI.Context;
using ModelsLibrary.Models.Dto;

namespace DiplomaProjectCRMWebAPI.Controllers;

// ServicesController - передаёт данные таблицы "УСЛУГИ" в JSON-формате
[ApiController]
[Route("api/{controller}/{action}")]
public class ServicesController(CrmContext crmContext) : ControllerBase
{
    private readonly CrmContext _crmContext = crmContext;

    // 1. по GET-запросу вернуть клиенту данные
    // о коллекции записей об услугах из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllAsync() {

        // имитация временной задержки
        Task.Delay(1_500).Wait();

        // все записи таблицы
        var source = (await _crmContext
            .Services.AsNoTracking().ToListAsync())
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