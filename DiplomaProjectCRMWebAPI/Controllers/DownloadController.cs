using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaProjectCRMWebAPI.Controllers;

// контроллер для передачи ресурсов на клиента
[Route("{controller}/{action}")]
public class DownloadController(
    IHostEnvironment environment/*, ILoadService loadService*/) : Controller
{

    // ссылка на серверное окружение - для получения папки хоста
    private IHostEnvironment _environment = environment;

    // получение ссылки на сервис для работы с выгрузкой файлов
    // при помощи внедрения зависимости - через конструктор
    //private readonly ILoadService _loadService = loadService;


    // корневой каталог
    //private const string APP_DATA = "App_Data";

    // имя файла с изображением по умолчанию
    //private const string DEFAULT_PHOTO = "photo.ico";

    // тип контента
    // text/plain для текста или универсальный тип application/octet-stream
    //private const string OCTET_STREAM = "application/octet-stream";


    // по GET-запросу вернуть клиенту файл с фотографией пользователя
    [HttpGet]
    [Route("{directory1}/{directory2}/{fileName}")]
    // .../users/photos/fileName
    public async Task<IActionResult> GetPhoto(
        string directory1, string directory2, string fileName) {

        // путь расположения фотографии
        var path = Path.Combine(_environment.ContentRootPath,
            LoadService.APP_DATA, directory1, directory2, fileName);


        // если файла нет - заменить его на файл с изображением по умолчанию
        if (!System.IO.File.Exists(path))
            path = Path.Combine(
                _environment.ContentRootPath, LoadService.APP_DATA,
                directory1, directory2, LoadService.DEFAULT_PHOTO);


        // массив байтов для отправки на клиента
        byte[] bytes = await System.IO.File.ReadAllBytesAsync(path);

        // text/plain для текста или универсальный тип application/octet-stream
        var type = LoadService.OCTET_STREAM;


        // вернуть клиенту файл с фотографией пользователя, как массив байтов
        return File(bytes, type, fileName);

    } // GetPhoto


    // по GET-запросу вернуть клиенту файл с временной фотографией пользователя
    [HttpGet]
    [Route("{directory1}/{directory2}/{directory3}/{fileName}")]
    // .../users/photos/tempPhoto/fileName
    public async Task<IActionResult> GetTempPhoto(string directory1,
        string directory2, string directory3, string fileName) {

        // путь расположения временной фотографии
        var path = Path.Combine(
            _environment.ContentRootPath, LoadService.APP_DATA,
            directory1, directory2, directory3, fileName);


        // если файла нет - заменить его на файл с изображением по умолчанию
        if (!System.IO.File.Exists(path))
            path = Path.Combine(
                _environment.ContentRootPath, LoadService.APP_DATA,
                directory1, directory2, LoadService.DEFAULT_PHOTO);


        // массив байтов для отправки на клиента
        byte[] bytes = await System.IO.File.ReadAllBytesAsync(path);

        // text/plain для текста или универсальный тип application/octet-stream
        var type = LoadService.OCTET_STREAM;


        // вернуть клиенту файл с фотографией пользователя, как массив байтов
        return File(bytes, type, fileName);

    } // GetTempPhoto

} // class DownloadController