using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Domain.Models.Infrastructure;
using Domain.Models.Entities;

namespace DiplomaProjectCRMWebAPI.Controllers;

// контроллер для аутентификации и авторизации пользователей
[ApiController]
[Route("api/{controller}/{action}")]
public class AuthController(IDbService dbService, IJwtService jwtService,
    IMailService mailService) : ControllerBase
{
    // получение ссылки на сервис-поставщик данных из базы данных
    // при помощи внедрения зависимости - через конструктор
    private readonly IDbService _dbService = dbService;

    // получение ссылки на сервис-поставщик jwt-токенов
    // при помощи внедрения зависимости - через конструктор
    private readonly IJwtService _jwtService = jwtService;

    // получение ссылки на сервис отправки электронных писем на почту
    // при помощи внедрения зависимости - через конструктор
    private readonly IMailService _mailService = mailService;


    // по POST-запросу при успешной аутентификации вернуть
    // клиенту Ok с JWT-токеном, или сообщение об ошибке аутентификации
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginModel loginModel) {

        // имитация временной задержки
        //Task.Delay(1_500).Wait();

        // если данных о пользователе нет - вернуть некорректные данные
        if (loginModel == null || string.IsNullOrEmpty(loginModel.Password) ||
            (string.IsNullOrEmpty(loginModel.Login) &&
            string.IsNullOrEmpty(loginModel.Email)))
            return BadRequest(new { loginModel });


        // если в данных указан адрес почты, то найдём пользователя
        // по email и паролю
        var user = new User();
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


        // если в данных указан логин(телефон), то найдём пользователя
        // по логину и паролю
        if (!string.IsNullOrEmpty(loginModel.Login)) {
            
            user = await _dbService
                .GetUserByLoginAsync(loginModel.Login);

            // если пользователь не найден(Id=0) - вернуть сообщение
            // об ошибке 401(НЕ АВТОРИЗОВАН)
            if (user.Id == 0)
                return Unauthorized(new { loginModel.Login });

            // если пароль найденного пользователя не совпадает - вернуть сообщение
            // об ошибке 401(НЕ АВТОРИЗОВАН)
            if (user.Password != loginModel.Password)
                return Unauthorized(new { loginModel.Login, loginModel.Password });

        } // if


        // зададим найденному пользователю требуемые параметры

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


    // по POST-запросу выйти из учётной записи и вернуть
    // клиенту Ok, или сообщение об ошибке
    [HttpPost]
    public async Task<IActionResult> LogOut([FromBody] LogOutModel logOutModel) {

        // имитация временной задержки
        //Task.Delay(1_500).Wait();

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


    // по POST-запросу вернуть клиенту новый JWT-токен, или сообщение об ошибке
    [HttpPost]
    public async Task<IActionResult> Refresh([FromBody] RefreshModel refreshModel) {

        // имитация временной задержки
        //Task.Delay(1_500).Wait();

        //refreshModel = new RefreshModel(refreshModel.UserId, ""); // для проверки
        // если данных о пользователе нет - вернуть некорректные данные
        if (refreshModel == null || refreshModel.UserId <= 0 ||
            string.IsNullOrEmpty(refreshModel.UserToken))
            return BadRequest(new { refreshModel });


        // поиск пользователя по Id
        var user = await _dbService.GetUserByIdAsync(refreshModel.UserId);

        // если пользователь не найден(Id=0) - вернуть сообщение
        // об ошибке 401(НЕ АВТОРИЗОВАН)
        //user.Id = 0; // для проверки
        if (user.Id == 0)
            return Unauthorized(new { refreshModel.UserId });


        // если пользователь не входил в учётную запись или его токен
        // обновления не соответствует - вернуть сообщение об ошибке
        //user.IsLogin = false; // для проверки
        if (!user.IsLogin || user.UserToken != refreshModel.UserToken)
            return Unauthorized(new { refreshModel.UserId, refreshModel.UserToken });


        // создать jwt-токен безопасности для пользователя
        var jwtTokenString = _jwtService.CreateJwtToken(user);

        // создать токен обновления
        var newUserToken = _jwtService.CreateUserToken();

        // установить пользователю новое значение токена обновления
        user.UserToken = newUserToken;

        //user.Password = "1"; // для проверки
        // сохранить изменённые данные пользователя в базе данных
        await _dbService.UpdateUserAsync(user);
        
        // получить отображаемые данные о пользователе(DTO)
        var displayUser = Domain.Models.Entities.User.UserToDto(user);


        // вернуть Ok с Jwt-токеном
        return Ok(new { Token = jwtTokenString, User = displayUser });

    } // Refresh


    // по POST-запросу зарегистрировать пользователя в системе и вернуть
    // клиенту Ok, или сообщение об ошибке регистрации
    [HttpPost]
    public async Task<IActionResult> Registration([FromBody] LoginModel loginModel) {

        // имитация временной задержки
        //Task.Delay(1_500).Wait();

        // если данных о пользователе нет - вернуть некорректные данные
        if (loginModel == null ||
            string.IsNullOrEmpty(loginModel.Login) ||
            string.IsNullOrEmpty(loginModel.Phone) ||
            string.IsNullOrEmpty(loginModel.Email))
            return BadRequest(new { loginModel });


        // поиск пользователя по логину (логин=телефону)
        var registeredUserByLogin = await _dbService
            .GetUserByLoginAsync(loginModel.Login);

        // если пользователь найден(Id != 0),
        // вернуть объект с совпадающим параметром
        if (registeredUserByLogin.Id != 0)
            return BadRequest(new { loginModel.Phone });


        // поиск пользователя по email
        var registeredUserByEmail = await _dbService
            .GetUserByEmailAsync(loginModel.Email);

        // если пользователь найден(Id != 0),
        // вернуть объект с совпадающим параметром
        if (registeredUserByEmail.Id != 0)
            return BadRequest(new { loginModel.Email });


        // если хотя бы один пользователь найден(Id != 0),
        // вернуть объект с совпадающими параметрами
        /*if (regUserByLogin.Id != 0 || regUserByEmail.Id != 0)
            return new JsonResult(new { Phone = regPhone, Email = regEmail });*/


        // данные для регистрации

        // 1 - имя пользователя
        var userName = loginModel.Login;

        // 2 - логин пользователя
        var login = loginModel.Login;

        // 3 - номер телефона пользователя
        var phone = loginModel.Phone;

        // 4 - номер телефона пользователя
        var email = loginModel.Email;

        // 5 - пароль учётной записи пользователя
        // TODO: сформировать и отправить на email
        var password = Utils.GetRandomString(17);

        // 6 - путь к файлу аватарки пользователя
        // (значение по умолчанию)
        var avatar =
            "http://localhost:5297/download/userphoto/users/photos/photo.ico";

        // 7 - пользовательский токен обновления
        var userToken = _jwtService.CreateUserToken();

        // 8 - состояние входа пользователя в учётную запись
        // (true - вошёл, false - вышел)
        var isLogin = false;


        // создать нового пользователя
        var newUser = new User(
            userName, login, phone, email, password,
            avatar, userToken, isLogin
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