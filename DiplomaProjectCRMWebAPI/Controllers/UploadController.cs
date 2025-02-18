using Application.Interfaces;
using Application.Services;
using Domain.Models.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaProjectCRMWebAPI.Controllers;

// контроллер для получения ресурсов от клиента
[Route("{controller}/{action}")]
public class UploadController(
    IHostEnvironment environment, ILoadService loadService) : Controller
{

    // ссылка на серверное окружение - для получения папки хоста
    private IHostEnvironment _environment = environment;

    // получение ссылки на сервис для работы с загрузкой файлов
    // при помощи внедрения зависимости - через конструктор
    private readonly ILoadService _loadService = loadService;


    // корневой каталог
    //public const string APP_DATA = "App_Data";

    // имена папок для временного хранения файла с фотографией пользователя
    //public const string USERS      = "users";
    //public const string PHOTOS     = "photos";
    //public const string TEMP_PHOTO = "tempPhoto";


    // по POST-запросу принять от клиента файл с фотографией пользователя
    // и сохранить её во временной папке, вернуть клиенту Ok с адресом
    // расположения загруженной фотографии, или сообщение об ошибке
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> TempUserPhoto(
        [FromForm] IFormFile tempPhoto, [FromQuery] int userId) {

        // имитация временной задержки
        //Task.Delay(1_500).Wait();


        // если файл не был выбран - вернуть некорректные данные
        //tempPhoto = null!; // для проверки
        if (tempPhoto == null)
            return BadRequest(new { Avatar = "" });

        // если данных о пользователе нет - вернуть некорректные данные
        //userId = 0; // для проверки
        if (userId <= 0)
            return BadRequest(new { UserId = 0 });


        // путь расположения временной фотографии
        var path = Path.Combine(_environment.ContentRootPath,
            LoadService.APP_DATA, LoadService.USERS,
            LoadService.PHOTOS, $"{LoadService.TEMP_PHOTO}_{userId}");

        // создание каталогов при их отсутствии
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);


        // удалить ранее сохранённые файлы
        /*var files = Directory.GetFiles(path).ToList();
        files.ForEach(System.IO.File.Delete);*/
        _loadService.DeleteFilesByPath(path);


        // сделать имя файла более уникальным для исключения перезаписи
        var uniqueFileName = _loadService.GetUniqueFileName(tempPhoto.FileName);

        // загрузить файл фотографии пользователя во временную папку
        await _loadService.UploadFileAsync(path, uniqueFileName, tempPhoto);


        // формирование адреса размещения временной фотографии пользователя
        var downloadController = LoadService.DOWNLOAD;
        var downloadMethod = LoadService.GET_TEMP_PHOTO;
        var tempAvatar =
            $"{AuthOptions.ISSUER}/{downloadController}/{downloadMethod}" +
            $"/{LoadService.USERS}/{LoadService.PHOTOS}" +
            $"/{LoadService.TEMP_PHOTO}_{userId}/{uniqueFileName}";


        // вернуть Ok с адресом фотографии
        return Ok(new { Avatar = tempAvatar });

    } // TempUserPhoto

} // class UploadController