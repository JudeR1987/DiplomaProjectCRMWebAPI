using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Domain.Models.Infrastructure;
using Domain.Models.Entities;
using Application.Services;

namespace DiplomaProjectCRMWebAPI.Controllers;

// контроллер для аутентификации и авторизации пользователей
[ApiController]
[Route("api/{controller}/{action}")]
public class AuthController(
    IDbService dbService,
    IJwtService jwtService,
    IMailService mailService,
    ILoadService loadService) : ControllerBase
{
    // ссылка на сервис-поставщик данных из базы данных
    private readonly IDbService _dbService = dbService;

    // ссылка на сервис-поставщик jwt-токенов
    private readonly IJwtService _jwtService = jwtService;

    // ссылка на сервис отправки электронных писем на почту
    private readonly IMailService _mailService = mailService;

    // ссылка на сервис для работы с загрузкой/выгрузкой файлов
    private readonly ILoadService _loadService = loadService;


    // 1. по POST-запросу при успешной аутентификации вернуть
    // клиенту Ok с JWT-токеном, или сообщение об ошибке аутентификации
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginModel loginModel) {

        // если данных о пользователе нет - вернуть некорректные данные
        if (loginModel == null || string.IsNullOrEmpty(loginModel.Password) ||
            (string.IsNullOrEmpty(loginModel.Phone) &&
            string.IsNullOrEmpty(loginModel.Email)))
            return BadRequest(new { loginModel });


        // если в данных указан телефон, то найдём пользователя
        // по телефону и паролю
        var user = new User();
        if (!string.IsNullOrEmpty(loginModel.Phone)) {
            
            user = await _dbService
                .GetUserByPhoneAsync(loginModel.Phone);

            // если пользователь не найден(Id=0) - вернуть сообщение
            // об ошибке 401(НЕ АВТОРИЗОВАН)
            if (user.Id == 0)
                return Unauthorized(new { loginModel.Phone });

            // если пароль найденного пользователя не совпадает - вернуть сообщение
            // об ошибке 401(НЕ АВТОРИЗОВАН)
            if (user.Password != loginModel.Password)
                return Unauthorized(new { loginModel.Phone, loginModel.Password });

        } // if


        // если в данных указан адрес почты, то найдём пользователя
        // по email и паролю
        if (!string.IsNullOrEmpty(loginModel.Email)) {
            
            user = await _dbService
                .GetUserByEmailAsync(loginModel.Email);

            // если пользователь не найден(Id=0) - вернуть сообщение
            // об ошибке 401(НЕ АВТОРИЗОВАН)
            if (user.Id == 0)
                return Unauthorized(new { loginModel.Email });

            // если пароль найденного пользователя не совпадает - вернуть сообщение
            // об ошибке 401(НЕ АВТОРИЗОВАН)
            if (user.Password != loginModel.Password)
                return Unauthorized(new { loginModel.Email, loginModel.Password });

        } // if


        // задание найденному пользователю требуемых параметров

        // создать jwt-токен безопасности для пользователя
        var jwtTokenString = _jwtService.CreateJwtToken(user);

        // создать токен обновления
        var userToken = _jwtService.CreateUserToken();

        // установить пользователю новое значение токена обновления
        user.UserToken = userToken;

        // установить пользователю активное состояние входа в учётную запись
        user.IsLogin = true;

        // сохранить изменённые данные пользователя в базе данных
        await _dbService.UpdateUserAsync(user);

        // получить отображаемые данные о пользователе(DTO)
        var displayUser = Domain.Models.Entities.User.UserToDto(user);


        // вернуть Ok с Jwt-токеном
        return Ok(new { Token = jwtTokenString, User = displayUser });

    } // Login


    // 2. по POST-запросу выйти из учётной записи и вернуть
    // клиенту Ok, или сообщение об ошибке
    [HttpPost]
    public async Task<IActionResult> LogOut([FromBody] LogOutModel logOutModel) {

        // если данных о пользователе нет - вернуть некорректные данные
        if (logOutModel == null || logOutModel.UserId <= 0 ||
            string.IsNullOrEmpty(logOutModel.UserToken))
            return BadRequest(new { logOutModel });


        // поиск пользователя по Id
        var user = await _dbService.GetUserByIdAsync(logOutModel.UserId);

        // если пользователь не найден(Id=0) - вернуть сообщение
        // об ошибке 401(НЕ АВТОРИЗОВАН)
        if (user.Id == 0)
            return Unauthorized(new { logOutModel.UserId });


        // если пользователь не входил в учётную запись или его параметры
        // не соответствуют - вернуть сообщение об ошибке 401(НЕ АВТОРИЗОВАН)
        if (!user.IsLogin || user.IsLogin != logOutModel.IsLogin ||
            user.UserToken != logOutModel.UserToken)
            return Unauthorized(new { logOutModel.IsLogin, logOutModel.UserToken });


        // создать токен обновления
        var userToken = _jwtService.CreateUserToken();

        // установить пользователю новое значение токена обновления
        user.UserToken = userToken;

        // установить пользователю состояние выхода из учётной записи
        user.IsLogin = false;

        // сохранить изменённые данные пользователя в базе данных
        await _dbService.UpdateUserAsync(user);


        // вернуть Ok
        return Ok();

    } // LogOut


    // 3. по POST-запросу вернуть клиенту новый JWT-токен, или сообщение об ошибке
    [HttpPost]
    public async Task<IActionResult> Refresh([FromBody] RefreshModel refreshModel) {

        // если данных о пользователе нет - вернуть некорректные данные
        if (refreshModel == null || refreshModel.UserId <= 0 ||
            string.IsNullOrEmpty(refreshModel.UserToken))
            return BadRequest(new { refreshModel });


        // поиск пользователя по Id
        var user = await _dbService.GetUserByIdAsync(refreshModel.UserId);

        // если пользователь не найден(Id=0) - вернуть сообщение
        // об ошибке 401(НЕ АВТОРИЗОВАН)
        if (user.Id == 0)
            return Unauthorized(new { refreshModel.UserId });


        // если пользователь не входил в учётную запись или его токен
        // обновления не соответствует - вернуть сообщение об ошибке
        if (!user.IsLogin || user.UserToken != refreshModel.UserToken)
            return Unauthorized(new { refreshModel.UserId, refreshModel.UserToken });


        // создать jwt-токен безопасности для пользователя
        var jwtTokenString = _jwtService.CreateJwtToken(user);

        // создать токен обновления
        var newUserToken = _jwtService.CreateUserToken();

        // установить пользователю новое значение токена обновления
        user.UserToken = newUserToken;

        // сохранить изменённые данные пользователя в базе данных
        await _dbService.UpdateUserAsync(user);
        
        // получить отображаемые данные о пользователе(DTO)
        var displayUser = Domain.Models.Entities.User.UserToDto(user);


        // вернуть Ok с Jwt-токеном
        return Ok(new { Token = jwtTokenString, User = displayUser });

    } // Refresh


    // 4. по POST-запросу зарегистрировать пользователя в системе и вернуть
    // клиенту Ok, или сообщение об ошибке регистрации
    [HttpPost]
    public async Task<IActionResult> Registration([FromBody] LoginModel loginModel) {

        // если данных о пользователе нет - вернуть некорректные данные
        if (loginModel == null ||
            string.IsNullOrEmpty(loginModel.Phone) ||
            string.IsNullOrEmpty(loginModel.Email))
            return BadRequest(new { loginModel });


        // поиск пользователя по телефону
        var registeredUserByPhone = await _dbService
            .GetUserByPhoneAsync(loginModel.Phone);

        // если пользователь найден(Id != 0),
        // вернуть объект с совпадающим параметром
        if (registeredUserByPhone.Id != 0)
            return BadRequest(new { loginModel.Phone });


        // поиск пользователя по email
        var registeredUserByEmail = await _dbService
            .GetUserByEmailAsync(loginModel.Email);

        // если пользователь найден(Id != 0),
        // вернуть объект с совпадающим параметром
        if (registeredUserByEmail.Id != 0)
            return BadRequest(new { loginModel.Email });


        // данные для регистрации

        // 1 - имя пользователя (если не указано, то
        // вместо имени используем номер телефона)
        var userName = string.IsNullOrEmpty(loginModel.UserName)
            ? loginModel.Phone
            : loginModel.UserName;

        // 2 - номер телефона пользователя
        var phone = loginModel.Phone;

        // 3 - номер телефона пользователя
        var email = loginModel.Email;

        // 4 - пароль учётной записи пользователя
        var password = Utils.GetRandomString(17);

        // 5 - путь к файлу аватарки пользователя (значение по умолчанию)
        /*var avatar = "http://localhost:5297/download/getimage/users/photos/photo.ico";*/
        var avatar = _loadService.GetPathToUserAvatar(LoadService.DEFAULT_PHOTO);

        // 6 - пользовательский токен обновления
        var userToken = _jwtService.CreateUserToken();

        // 7 - состояние входа пользователя в учётную запись
        // (true - вошёл, false - вышел)
        var isLogin = false;


        // создать нового пользователя
        var newUser = new User(
            userName, phone, email, password,
            avatar, userToken, isLogin, null
        );


        // добавить данные о пользователе в базу данных
        await _dbService.CreateUserAsync(newUser);


        // отправить пароль на почту !!!
        (bool isOk, string message) = await _mailService
            .SendMailAsync(newUser, MailOptions.SUBJECT_PASSWORD);


        // TODO: возможно, потребуется добавить новому
        // пользователю роль типа "НОВИЧЁК"


        // вернуть Ok
        return Ok();

    } // Registration

} // class AuthController