using Application.Interfaces;
using Domain.Models.Entities;
using Domain.Models.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaProjectCRMWebAPI.Controllers;

// ProfileController - производит изменения в профиле пользователя
[ApiController]
[Route("{controller}/{action}")]
public class ProfileController(IDbService dbService,
    IMailService mailService) : ControllerBase
{
    // получение ссылки на сервис-поставщик данных из базы данных
    // при помощи внедрения зависимости - через конструктор
    private readonly IDbService _dbService = dbService;

    // получение ссылки на сервис отправки электронных писем на почту
    // при помощи внедрения зависимости - через конструктор
    private readonly IMailService _mailService = mailService;


    // 1. по POST-запросу получить сведения об изменении пароля пользователя
    // и вернуть клиенту Ok, или сообщение об ошибке
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> EditPasswordAsync(
        [FromForm] int userId, [FromForm] string newPassword) {

        // имитация временной задержки
        Task.Delay(1_500).Wait();

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
    public async Task<IActionResult> EditUserAsync(
        [FromForm] int userId, [FromForm] string userName,
        [FromForm] string phone, [FromForm] string email) {

        // имитация временной задержки
        Task.Delay(1_500).Wait();
        
        // если требуемых данных о пользователе нет - вернуть некорректные данные
        if (userId <= 0)
            return BadRequest(new { UserId = 0 });
        
        if (string.IsNullOrEmpty(userName))
            return BadRequest(new { UserName = userName ?? "" });

        if (string.IsNullOrEmpty(phone))
            return BadRequest(new { Phone = phone ?? "" });

        if (string.IsNullOrEmpty(email))
            return BadRequest(new { Email = email ?? "" });


        // поиск пользователя по Id
        var user = await _dbService.GetUserByIdAsync(userId);

        // если пользователь не найден(Id=0) - вернуть сообщение
        // об ошибке 401(НЕ АВТОРИЗОВАН)
        if (user.Id == 0)
            return Unauthorized(new { userId });


        // установить новое значение имени пользователя
        if (userName != user.UserName) user.UserName = userName;


        // проверка зарегистрированных номеров телефонов,
        // если номер не совпадает с номером пользователя
        if (phone != user.Phone) {

            // поиск пользователя по логину (логин=телефону)
            var registeredUserByLogin =
                await _dbService.GetUserByLoginAsync(phone);

            // если пользователь найден(Id != 0),
            // вернуть объект с совпадающим параметром
            if (registeredUserByLogin.Id != 0)
                return BadRequest(new { phone });

            // установить новое значение номера телефона пользователя
            // ( и логина т.к. логин=телефону)
            user.Phone = phone;
            user.Login = phone;

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


        // сохранить изменённые данные пользователя в базе данных
        await _dbService.UpdateUserAsync(user);

        // получить отображаемые данные о пользователе(DTO)
        var displayUser = Domain.Models.Entities.User.UserToDto(user);

        // вернуть данные в JSON-формате
        return Ok(new { User = displayUser });

    } // EditUserAsync

} // class ProfileController