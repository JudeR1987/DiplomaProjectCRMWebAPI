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


    // 1. по POST-запросу принять от клиента файл с фотографией пользователя
    // и сохранить её во временной папке, вернуть клиенту Ok с адресом
    // расположения загруженной фотографии, или сообщение об ошибке
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> TempUserPhoto(
        [FromForm] IFormFile tempImage, [FromQuery] int userId) {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();


        // если файл не был выбран - вернуть некорректные данные
        //tempPhoto = null!; // для проверки
        if (tempImage == null)
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
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);


        // удалить ранее сохранённые файлы
        /*var files = Directory.GetFiles(path).ToList();
        files.ForEach(System.IO.File.Delete);*/
        _loadService.DeleteFilesByPath(path);


        // сделать имя файла более уникальным для исключения перезаписи
        var uniqueFileName = _loadService.GetUniqueFileName(tempImage.FileName);

        // загрузить файл фотографии пользователя во временную папку
        await _loadService.UploadFileAsync(path, uniqueFileName, tempImage);


        // формирование адреса размещения временной фотографии пользователя
        /*var downloadController = LoadService.DOWNLOAD;
        var downloadMethod = LoadService.GET_TEMP_IMAGE;
        var tempAvatar =
            $"{AuthOptions.ISSUER}/{downloadController}/{downloadMethod}" +
            $"/{LoadService.USERS}/{LoadService.PHOTOS}" +
            $"/{LoadService.TEMP_PHOTO}_{userId}/{uniqueFileName}";*/
        var tempAvatar = _loadService
            .GetPathToTempUserAvatar(userId, uniqueFileName);


        // вернуть Ok с адресом фотографии
        return Ok(new { Avatar = tempAvatar });

    } // TempUserPhoto


    // 2. по POST-запросу принять от клиента файл с изображением
    // компании и сохранить его во временной папке, вернуть клиенту Ok
    // с адресом расположения загруженного изображения, или сообщение об ошибке
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> TempCompanyImage(
        [FromForm] IFormFile tempImage, [FromQuery] int userId,
        [FromQuery] int companyId,      [FromQuery] string tempDir,
        [FromQuery] string imageType) {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();


        // если файл не был выбран - вернуть некорректные данные
        //tempImage = null!; // для проверки
        if (tempImage == null)
            return BadRequest(new { Image = "" });


        // если данных о пользователе нет - вернуть некорректные данные
        //userId = 0; // для проверки
        if (userId <= 0)
            return BadRequest(new { UserId = 0 });

        // если данные о компании неверные - вернуть некорректные данные
        // (companyId=0 - режим создания компании, иначе - режим изменения данных)
        //companyId = -1; // для проверки
        if (companyId < 0)
            return BadRequest(new { CompanyId = 0 });


        // при разных типах изображений применяем разные имена временных папок
        // (для логотипа и основного изображения компании)
        /*var tempDirName = imageType == "logo"
            ? LoadService.TEMP_LOGO
            : LoadService.TEMP_IMAGE;*/


        // при режиме создания использовать значение временной папки,
        // если значение временной папки отсутствует, то его нужно создать
        /*if (companyId == 0 && string.IsNullOrEmpty(tempDir)) {

            tempDir = $"{tempDirName}_{userId}_{companyId}_{Utils.GetRandomString()}";

        } // if


        tempDir = companyId == 0 ? tempDir : $"{tempDirName}_{userId}_{companyId}";*/
        tempDir = companyId == 0 && !string.IsNullOrEmpty(tempDir)
            ? tempDir
            : _loadService.GetTempCompanyImageDirectoryByParams(
                imageType, userId, companyId);


        // путь расположения временного изображения компании
        var path = Path.Combine(_environment.ContentRootPath,
            LoadService.APP_DATA, LoadService.COMPANIES,
            imageType == "logo" ? LoadService.LOGOS : LoadService.IMAGES, tempDir);

        // создание каталогов при их отсутствии
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);


        // удалить ранее сохранённые файлы
        /*var files = Directory.GetFiles(path).ToList();
        files.ForEach(System.IO.File.Delete);*/
        _loadService.DeleteFilesByPath(path);


        // сделать имя файла более уникальным для исключения перезаписи
        var uniqueFileName = _loadService.GetUniqueFileName(tempImage.FileName);

        // загрузить файл изображения компании во временную папку
        await _loadService.UploadFileAsync(path, uniqueFileName, tempImage);


        // формирование адреса размещения временного изображения компании
        /*var downloadController = LoadService.DOWNLOAD;
        var downloadMethod = LoadService.GET_TEMP_IMAGE;
        var tempImagePath =
            $"{AuthOptions.ISSUER}/{downloadController}/{downloadMethod}" +
            $"/{LoadService.COMPANIES}" +
            $"/{(imageType == "logo" ? LoadService.LOGOS : LoadService.IMAGES)}" +
            $"/{tempDir}/{uniqueFileName}";*/
        var tempImagePath = _loadService.GetPathToTempCompanyImage(
            imageType, tempDir, uniqueFileName);


        // вернуть Ok с адресом изображения
        return Ok(new { Image = tempImagePath });

    } // TempCompanyImage


    // 3. по POST-запросу принять от клиента файл с изображением компании
    // и сохранить её во временной папке, вернуть клиенту Ok с адресом
    // расположения загруженного изображения, или сообщение об ошибке
    /*[HttpPost]
    [Authorize]
    public async Task<IActionResult> TempCompanyTitleImage(
        [FromForm] IFormFile tempImage, [FromQuery] int id) {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();


        // если файл не был выбран - вернуть некорректные данные
        //tempPhoto = null!; // для проверки
        if (tempImage == null)
            return BadRequest(new { Avatar = "" });

        // если данных о пользователе нет - вернуть некорректные данные
        //userId = 0; // для проверки
        if (id <= 0)
            return BadRequest(new { UserId = 0 });


        // путь расположения временной фотографии
        var path = Path.Combine(_environment.ContentRootPath,
            LoadService.APP_DATA, LoadService.USERS,
            LoadService.PHOTOS, $"{LoadService.TEMP_PHOTO}_{id}");

        // создание каталогов при их отсутствии
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);


        // удалить ранее сохранённые файлы
        *//*var files = Directory.GetFiles(path).ToList();
        files.ForEach(System.IO.File.Delete);*//*
        _loadService.DeleteFilesByPath(path);


        // сделать имя файла более уникальным для исключения перезаписи
        var uniqueFileName = _loadService.GetUniqueFileName(tempImage.FileName);

        // загрузить файл фотографии пользователя во временную папку
        await _loadService.UploadFileAsync(path, uniqueFileName, tempImage);


        // формирование адреса размещения временной фотографии пользователя
        var downloadController = LoadService.DOWNLOAD;
        var downloadMethod = LoadService.GET_TEMP_IMAGE;
        var tempAvatar =
            $"{AuthOptions.ISSUER}/{downloadController}/{downloadMethod}" +
            $"/{LoadService.USERS}/{LoadService.PHOTOS}" +
            $"/{LoadService.TEMP_PHOTO}_{id}/{uniqueFileName}";


        // вернуть Ok с адресом фотографии
        return Ok(new { Avatar = tempAvatar });

    } // TempCompanyTitleImage*/

} // class UploadController