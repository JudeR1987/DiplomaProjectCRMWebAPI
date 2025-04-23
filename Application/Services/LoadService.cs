using Application.Interfaces;
using Domain.Models.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Application.Services;

// сервис для работы с загрузкой/выгрузкой файлов
public class LoadService : ILoadService
{
    // корневой каталог
    public const string APP_DATA = "App_Data";


    // имена папок для хранения файлов с фотографиями пользователей
    public const string USERS = "users";
    public const string PHOTOS = "photos";
    public const string TEMP_PHOTO = "tempPhoto";


    // имена папок для хранения файлов с фотографиями сотрудников
    // (PHOTOS="photos" и TEMP_PHOTO="tempPhoto" такие же как и у пользователей)
    public const string EMPLOYEES = "employees";


    // имена папок для хранения файлов с изображениями
    // логотипов компаний и их основных изображений
    public const string COMPANIES = "companies";
    public const string LOGOS = "logos";
    public const string IMAGES = "images";
    public const string TEMP_LOGO = "tempLogo";
    public const string TEMP_IMAGE = "tempImage";


    // имя файла с изображением по умолчанию
    // для персон
    public const string DEFAULT_PHOTO = "photo.ico";

    // для логотипов
    public const string DEFAULT_LOGO = "logo.ico";

    // для компаний
    public const string DEFAULT_COMPANY = "company.jpg";


    // тип контента
    // text/plain для текста или универсальный тип application/octet-stream
    public const string OCTET_STREAM = "application/octet-stream";


    // контроллер для загрузки данных
    public const string UPLOAD = "upload";


    // контроллер для выгрузки данных
    public const string DOWNLOAD = "download";


    // метод действия для выгрузки изображений из основной папки
    public const string GET_IMAGE = "getImage";


    // метод действия для выгрузки изображений из временной папки
    public const string GET_TEMP_IMAGE = "getTempImage";


    // ссылка на серверное окружение - для получения папки хоста
    private IHostEnvironment _environment;


    // конструктор
    public LoadService(IHostEnvironment environment) {

        // ссылка на серверное окружение
        _environment = environment;

        // создание каталогов при их отсутствии
        CreateDirectories();

    } // LoadService



    // добавление уникального суффикса к имени файла изображения
    public string GetUniqueFileName(string fileName) =>
        $"{Path.GetFileNameWithoutExtension(fileName)}" +
        $"_{Utils.GetRandomString()}" +
        $"{Path.GetExtension(fileName)}";



    // получить путь к файлу аватарки пользователя
    public string GetPathToUserAvatar(string fileName) =>
        $"{AuthOptions.ISSUER}/{DOWNLOAD}/{GET_IMAGE}/" +
        $"{USERS}/{PHOTOS}/{fileName}";

    // получить путь к временному файлу аватарки пользователя
    public string GetPathToTempUserAvatar(int userId, string fileName) =>
        $"{AuthOptions.ISSUER}/{DOWNLOAD}/{GET_TEMP_IMAGE}/" +
        $"{USERS}/{PHOTOS}/{TEMP_PHOTO}_{userId}/{fileName}";



    // получить путь к файлу изображения компании
    public string GetPathToCompanyImage(string imageType, string fileName) =>
        $"{AuthOptions.ISSUER}/{DOWNLOAD}/{GET_IMAGE}/{COMPANIES}/" +
        $"{(imageType == "logo" ? LOGOS : IMAGES)}/{fileName}";

    // получить путь к временному файлу изображения компании
    public string GetPathToTempCompanyImage(
        string imageType, string tempDir, string fileName) =>
        $"{AuthOptions.ISSUER}/{DOWNLOAD}/{GET_TEMP_IMAGE}/{COMPANIES}/" +
        $"{(imageType == "logo" ? LOGOS : IMAGES)}/" +
        $"{tempDir}/{fileName}";



    // получить путь к файлу аватарки сотрудника
    public string GetPathToEmployeeAvatar(string fileName) =>
        $"{AuthOptions.ISSUER}/{DOWNLOAD}/{GET_IMAGE}/" +
        $"{EMPLOYEES}/{PHOTOS}/{fileName}";

    // получить путь к временному файлу аватарки сотрудника
    public string GetPathToTempEmployeeAvatar(string tempDir, string fileName) =>
        $"{AuthOptions.ISSUER}/{DOWNLOAD}/{GET_TEMP_IMAGE}/" +
        $"{EMPLOYEES}/{PHOTOS}/{tempDir}/{fileName}";



    // получить имя временной папки для хранения
    // фотографий пользователя с учётом его идентификатора
    public string GetTempUserPhotoDirectoryById(int userId) =>
        $"{TEMP_PHOTO}_{userId}";



    // получить имя временной папки для хранения
    // изображений компании с учётом типа изображения
    // и идентификаторов пользователя и компании
    public string GetTempCompanyImageDirectoryByParams(
        string imageType, int userId, int companyId) {

        // при разных типах изображений применяем разные имена временных папок
        // (для логотипа и основного изображения компании)
        var tempDirName = imageType == "logo"
            ? TEMP_LOGO
            : TEMP_IMAGE;

        // при режиме создания компании (companyId == 0)
        // добавляем уникальный суффикс
        var randomString = companyId == 0
            ? $"_{Utils.GetRandomString()}"
            : "";
        
        var tempDir = $"{tempDirName}_{userId}_{companyId}{randomString}";

        return tempDir;

    } // GetTempCompanyImageDirectoryByParams



    // получить имя временной папки для хранения фотографий сотрудника
    // с учётом идентификаторов пользователя и сотрудника
    public string GetTempEmployeePhotoDirectoryByParams(int userId, int employeeId) {

        // при режиме добавления сотрудника (employeeId == 0)
        // добавляем уникальный суффикс
        var randomString = employeeId == 0
            ? $"_{Utils.GetRandomString()}"
            : "";
        
        return $"{TEMP_PHOTO}_{userId}_{employeeId}{randomString}";

    } // GetTempCompanyImageDirectoryByParams



    // копирование файла
    public void CopyFile(string srcFilePath, string dscFilePath) =>
        File.Copy(srcFilePath, dscFilePath, true);


    // удаление всех файлов в заданной папке
    public void DeleteFilesByPath(string path) {

        // список файлов(полные пути к файлам)
        var files = Directory.GetFiles(path).ToList();
        
        // удалить каждый файл
        files.ForEach(File.Delete);

    } // DeleteFilesByPath


    // удаление папки с содержимым
    public (bool, string) DeleteDirectory(string directoryPath) {

        // вернём из метода результаты операции
        bool isOk;
        string message;

        try {

            Directory.Delete(directoryPath, true);

            isOk = true;
            message = "Ok";

        } catch (Exception ex) {

            isOk = false;
            message = ex.Message;

        } // try-catch

        return (isOk, message);

    } // DeleteDirectory


    // загрузка файлов (Upload)

    // 1. загрузка файла на сервер
    async public Task UploadFileAsync(string path, string fileName, IFormFile file) {

        // путь расположения загружаемого файла
        var pathFile = Path.Combine(path, fileName);

        // копировать локальный файл на сервер
        await using var fileStream = new FileStream(pathFile, FileMode.Create);
        await file.CopyToAsync(fileStream);

    } // UploadFileAsync



    // создание каталогов при их отсутствии
    private void CreateDirectories() {

        // путь к папке с фотографиями пользователей
        var path = Path.Combine(_environment.ContentRootPath,
            APP_DATA, USERS, PHOTOS);

        // создание каталогов при их отсутствии
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        // путь к папкам с изображениями логотипов компаний
        path = Path.Combine(_environment.ContentRootPath,
            APP_DATA, COMPANIES, LOGOS);

        // создание каталогов при их отсутствии
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        // путь к папкам с основными изображениями компаний
        path = Path.Combine(_environment.ContentRootPath,
            APP_DATA, COMPANIES, IMAGES);

        // создание каталогов при их отсутствии
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        // путь к папке с фотографиями сотрудников
        path = Path.Combine(_environment.ContentRootPath,
            APP_DATA, EMPLOYEES, PHOTOS);

        // создание каталогов при их отсутствии
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

    } // CreateDirectories

} // class LoadService