using Application.Interfaces;
using Domain.Models.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.IO;

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

    // имя файла с изображением по умолчанию
    public const string DEFAULT_PHOTO = "photo.ico";

    // тип контента
    // text/plain для текста или универсальный тип application/octet-stream
    public const string OCTET_STREAM = "application/octet-stream";

    // контроллер для загрузки данных
    public const string UPLOAD = "upload";

    // контроллер для выгрузки данных
    public const string DOWNLOAD = "download";

    // метод действия для выгрузки фотографий пользователя из временной папки
    public const string GET_TEMP_PHOTO = "getTempPhoto";


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


    // получить название временной папки для хранения
    // фотографий пользователя с учётом его идентификатора
    public string GetTempPhotoDirectoryById(int userId) =>
        $"{TEMP_PHOTO}_{userId}";


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


    // выгрузка файлов (Download)

    // 1. сформировать специальный объект ответа на запрос
    // для передачи файла как массива байтов
    /*public async Task<FileContentResult>
        DownloadFileAsBytes(string path, string fileName) {

        // массив байтов для отправки на клиента
        byte[] bytes = await File.ReadAllBytesAsync(path);

        // text/plain для текста или универсальный тип application/octet-stream
        var type = ContentType_OctetStream;

        // вернуть клиенту файл с фотографией пользователя, как массив байтов
        return ControllerBase.File(bytes, type, fileName);

    } // DownloadFileAsBytes*/


    // создание каталогов при их отсутствии
    private void CreateDirectories() {

        // путь к папке с фотографиями пользователей
        var path = Path.Combine(_environment.ContentRootPath,
            APP_DATA, USERS, PHOTOS);

        // создание каталогов при их отсутствии
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

    } // CreateDirectories

} // class LoadService