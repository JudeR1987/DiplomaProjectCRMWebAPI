using Application.Interfaces;
using Application.Services;
using Domain.Models.Entities;
using Domain.Models.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaProjectCRMWebAPI.Controllers;

// ProfileController - производит изменения в профиле пользователя
[ApiController]
[Route("{controller}/{action}")]
public class ProfileController(
    IHostEnvironment environment,
    IDbService dbService,
    IMailService mailService,
    ILoadService loadService) : ControllerBase
{
    // ссылка на серверное окружение - для получения папки хоста
    private IHostEnvironment _environment = environment;

    // ссылка на сервис-поставщик данных из базы данных
    private readonly IDbService _dbService = dbService;

    // ссылка на сервис отправки электронных писем на почту
    private readonly IMailService _mailService = mailService;

    // ссылка на сервис для работы с загрузкой/выгрузкой файлов
    private readonly ILoadService _loadService = loadService;


    // 1. по POST-запросу получить сведения об изменении пароля пользователя
    // и вернуть клиенту Ok, или сообщение об ошибке
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> EditPasswordAsync(
        [FromForm] int userId, [FromForm] string newPassword) {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();

        // если данных нет - вернуть некорректные данные
        if (userId <= 0)
            return BadRequest(new { UserId = 0 });

        if (string.IsNullOrEmpty(newPassword))
            return BadRequest(new { NewPassword = newPassword ?? "" });


        // поиск пользователя по Id
        var user = await _dbService.GetUserByIdAsync(userId);

        // если пользователь не найден(Id=0) - вернуть сообщение
        // об ошибке 401(НЕ АВТОРИЗОВАН)
        if (user.Id == 0)
            return Unauthorized(new { userId });


        // установить пользователю новое значение пароля
        user.Password = newPassword;
        //user.Password = "1";

        // сохранить изменённые данные пользователя в базе данных
        await _dbService.UpdateUserAsync(user);


        // отправить пароль на почту !!!
        (bool isOk, string message) = await _mailService
            .SendMailAsync(user, MailOptions.SUBJECT_NEW_PASSWORD);


        // вернуть данные в JSON-формате
        return Ok();

    } // EditPasswordAsync


    // 2. по POST-запросу получить данные о пользователе для изменения
    // и вернуть клиенту Ok с изменёнными данными, или сообщение об ошибке
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> EditUserAsync(
        [FromForm] int userId,[FromForm] string userName, [FromForm] string phone,
        [FromForm] string email, [FromForm] string avatar) {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();
        
        // если требуемых данных о пользователе нет - вернуть некорректные данные
        if (userId <= 0)
            return BadRequest(new { UserId = 0 });
        
        if (string.IsNullOrEmpty(userName))
            return BadRequest(new { UserName = userName ?? "" });

        if (string.IsNullOrEmpty(phone))
            return BadRequest(new { Phone = phone ?? "" });

        if (string.IsNullOrEmpty(email))
            return BadRequest(new { Email = email ?? "" });
        //avatar = ""; // для проверки
        if (string.IsNullOrEmpty(avatar))
            return BadRequest(new { Avatar = avatar ?? "" });


        // поиск пользователя по Id
        var user = await _dbService.GetUserByIdAsync(userId);

        // если пользователь не найден(Id=0) - вернуть сообщение
        // об ошибке 401(НЕ АВТОРИЗОВАН)
        //if (true) // для проверки
        if (user.Id == 0)
            return Unauthorized(new { userId });


        // установить новое значение имени пользователя
        if (userName != user.UserName) user.UserName = userName;


        // проверка зарегистрированных номеров телефонов,
        // если номер не совпадает с номером пользователя
        if (phone != user.Phone) {

            // поиск пользователя по логину (логин=телефону) // ИСПРАВИТЬ!!!
            /*var registeredUserByLogin =
                await _dbService.GetUserByLoginAsync(phone);*/

            // поиск пользователя по телефону
            var registeredUserByPhone =
                await _dbService.GetUserByPhoneAsync(phone);

            // если пользователь найден(Id != 0),
            // вернуть объект с совпадающим параметром
            /*if (registeredUserByLogin.Id != 0)
                return BadRequest(new { phone });*/
            if (registeredUserByPhone.Id != 0)
                return BadRequest(new { phone });

            // установить новое значение номера телефона пользователя
            // ( и логина т.к. логин=телефону)
            /*user.Phone = phone;
            user.Login = phone;*/

            // установить новое значение номера телефона пользователя
            user.Phone = phone;

        } // if


        // проверка зарегистрированных почтовых адресов,
        // если почта не совпадает с почтой пользователя
        if (email != user.Email) {

            // поиск пользователя по email
            var registeredUserByEmail =
                await _dbService.GetUserByEmailAsync(email);

            // если пользователь найден(Id != 0),
            // вернуть объект с совпадающим параметром
            if (registeredUserByEmail.Id != 0)
                return BadRequest(new { email });

            // установить новое значение email пользователя
            user.Email = email;

        } // if


        // сохранить данные о фотографии, если выбрана новая фотография
        if (avatar != user.Avatar) {

            // получить имена файла и папки его расположения
            var items = avatar.Split("/", StringSplitOptions.RemoveEmptyEntries);
            
            var fileName = items[items.Length - 1];

            var tempDirectory = items[items.Length - 2];


            // если файл находится не во временной папке,
            // вернуть объект и сообщение об ошибке
            var tempPhotoDirectoryById = _loadService.GetTempUserPhotoDirectoryById(userId);
            if (tempDirectory != tempPhotoDirectoryById)
                return BadRequest(new { avatar });


            // копировать файл из временной папки в рабочую

            // путь к папке с фотографиями пользователя
            var usersPhotosDirectoryPath = Path.Combine(
                _environment.ContentRootPath, LoadService.APP_DATA,
                LoadService.USERS, LoadService.PHOTOS);

            // путь к папке с временными фотографиями пользователя
            var tempDirectoryPath = Path.Combine(
                usersPhotosDirectoryPath, tempDirectory);

            // полный путь к копируемому файлу
            var tempPath = Path.Combine(tempDirectoryPath, fileName);

            // полный путь к копии файла
            var copyPath = Path.Combine(usersPhotosDirectoryPath, fileName);

            // скопировать файл фотографии
            _loadService.CopyFile(tempPath, copyPath);

            // удалить временную папку со всеми временными фотографиями
            (bool isOk, string message) =
                _loadService.DeleteDirectory(tempDirectoryPath);


            // удалить файл со старой фотографией

            // имя файла старой фотографии
            items = user.Avatar.Split("/", StringSplitOptions.RemoveEmptyEntries);
            var oldFileName = items[items.Length - 1];

            // полный путь к удаляемому файлу
            var oldFileNamePath = Path.Combine(usersPhotosDirectoryPath, oldFileName);

            // удаление файла (если он не является файлом по умолчанию)
            if (oldFileName != LoadService.DEFAULT_PHOTO)
                System.IO.File.Delete(oldFileNamePath);


            // установить новое значение avatar пользователя, удалив имя
            // временной папки и подстроку "Temp" метода действия из пути
            /* var avatar1 = avatar
                 .Replace($"{LoadService.TEMP_PHOTO}_{userId}/", "")
                 .Replace("Temp", "");
             var avatar2 = _loadService.GetPathToUserAvatar(fileName);*/
            user.Avatar = _loadService.GetPathToUserAvatar(fileName);

        } // if


        // сохранить изменённые данные пользователя в базе данных
        await _dbService.UpdateUserAsync(user);

        // получить отображаемые данные о пользователе(DTO)
        var displayUser = Domain.Models.Entities.User.UserToDto(user);

        // вернуть данные в JSON-формате
        return Ok(new { User = displayUser });

    } // EditUserAsync


    // 3. по DELETE-запросу удалить временную папку
    // со всеми временными фотографиями пользователя
    [HttpDelete]
    [Authorize]
    public IActionResult DeleteTempUserPhotos([FromQuery] int userId) {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();
        var temp = Request;

        // если данных о пользователе нет - вернуть некорректные данные
        //userId = 0; // для проверки
        if (userId <= 0)
            return BadRequest(new { UserId = 0 });


        // путь к папке с временными фотографиями пользователя
        var tempDirectoryPath = Path.Combine(_environment.ContentRootPath,
            LoadService.APP_DATA, LoadService.USERS, LoadService.PHOTOS,
            $"{LoadService.TEMP_PHOTO}_{userId}");

        // если папка не найдена - вернуть сообщение об ошибке
        if (!Directory.Exists(tempDirectoryPath))
            return BadRequest(new { directory = false });

        // удалить временную папку со всеми временными фотографиями
        (bool isOk, string message) =
            _loadService.DeleteDirectory(tempDirectoryPath);

        // если при удалении была ошибка - передать ошибку
        if (!isOk)
            return BadRequest(new { DeleteMessage = message });

        // вернуть Ok
        return Ok();

    } // DeleteTempUserPhotos


    // 4. по DELETE-запросу удалить данные о пользователе (учётную запись)
    // и вернуть клиенту Ok, или сообщение об ошибке
    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteUserAsync([FromQuery] int id) {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();

        // если данных о пользователе нет - вернуть некорректные данные
        // id = 0; // для проверки
        if (id <= 0)
            return BadRequest(new { UserId = 0 });


        // поиск пользователя по Id
        var user = await _dbService.GetUserByIdAsync(id);

        // если пользователь не найден(Id=0) - вернуть сообщение
        // об ошибке 401(НЕ АВТОРИЗОВАН)
        //user.Id = 0; // для проверки
        if (user.Id == 0)
            return Unauthorized(new { UserId = id });


        // если пользователь не входил в учётную запись - вернуть
        // сообщение об ошибке 401(НЕ АВТОРИЗОВАН)
        //user.IsLogin = false; // для проверки
        if (!user.IsLogin)
            return Unauthorized(new { user.IsLogin });


        // установить в записи данных о пользователе
        // время и дату удаления записи
        user.Deleted = DateTime.Now;

        // установить пользователю состояние выхода из учётной записи
        user.IsLogin = false;

        // сохранить изменённые данные пользователя в базе данных
        await _dbService.UpdateUserAsync(user);


        // вернуть Ok
        return Ok();

    } // DeleteUserAsync

} // class ProfileController