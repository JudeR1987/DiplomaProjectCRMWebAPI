using Application.Interfaces;
using Application.Services;
using Domain.Models.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaProjectCRMWebAPI.Controllers;

// контроллер для передачи ресурсов на клиента
[Route("{controller}/{action}/users/photos/{fileName}")]
public class DownloadController(IHostEnvironment environment/*,
    IDbService dbService*/) : Controller
{

    // ссылка на серверное окружение - для получения папки хоста
    private IHostEnvironment _environment = environment;

    // получение ссылки на сервис-поставщик данных из базы данных
    // при помощи внедрения зависимости - через конструктор
    //private readonly IDbService _dbService = dbService;


    // по GET-запросу вернуть клиенту файл с фотографией пользователя
    [HttpGet]
    public async Task<IActionResult> UserPhoto(string fileName) {

        // имитация временной задержки
        //Task.Delay(1_500).Wait();

        // поиск пользователя по Id
        //var user = await _dbService.GetUserByIdAsync(userId);

        // если пользователь не найден(Id=0) - вернуть сообщение
        // об ошибке 401(НЕ АВТОРИЗОВАН)
        /*if (user.Id == 0)
            return Unauthorized("Invalid user (Пользователь не зарегистрирован)");*/

        //var name = "devs1.jpg";
        var path = Path.Combine(_environment.ContentRootPath,
            "App_Data", "Users", "photos", fileName);
        byte[] bytes = await System.IO.File.ReadAllBytesAsync(path);

        // text/plain для текста или универсальный тип application/octet-stream
        var type = "application/octet-stream";

        // вернуть клиенту файл с фотографией пользователя, как массив байтов
        return File(bytes, type, fileName);

    } // UserPhoto

} // class DownloadController