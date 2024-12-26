using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiplomaProjectCRMWebAPI.Context;
using ModelsLibrary.Models.Dto;

namespace DiplomaProjectCRMWebAPI.Controllers;

// ServicesCategoriesController - передаёт данные
// таблицы "КАТЕГОРИИ_УСЛУГ" в JSON-формате
[ApiController]
[Route("api/{controller}/{action}")]
public class ServicesCategoriesController(CrmContext crmContext) : ControllerBase
{
    private readonly CrmContext _crmContext = crmContext;

    // 1. по GET-запросу вернуть клиенту данные
    // о коллекции записей о категориях услуг из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllAsync() {

        // имитация временной задержки
        Task.Delay(1_500).Wait();

        // все записи таблицы
        var source = (await _crmContext
            .ServicesCategories.AsNoTracking().ToListAsync())
            .Select(servicesCategory => new ServicesCategoryDto(
                servicesCategory.Id,
                servicesCategory.Name,
                servicesCategory.Weight))
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllAsync

} // class ServicesCategoriesController