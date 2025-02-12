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

        // путь расположения фотографии
        var path = Path.Combine(_environment.ContentRootPath,
            "App_Data", "Users", "photos", fileName);

        // если файла нет - заменить его на файл с изображением по умолчанию
        if (!System.IO.File.Exists(path))
            path = Path.Combine(_environment.ContentRootPath,
                "App_Data", "Users", "photos", "photo.ico");

        // массив байтов для отправки на клиента
        byte[] bytes = await System.IO.File.ReadAllBytesAsync(path);

        // text/plain для текста или универсальный тип application/octet-stream
        var type = "application/octet-stream";

        // вернуть клиенту файл с фотографией пользователя, как массив байтов
        return File(bytes, type, fileName);

    } // UserPhoto

} // class DownloadController