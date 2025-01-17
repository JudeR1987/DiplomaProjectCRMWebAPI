using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Domain.Models.Infrastructure;
using Domain.Models.Entities;

namespace DiplomaProjectCRMWebAPI.Controllers;

// контроллер для аутентификации и авторизации пользователей
[ApiController]
[Route("api/{controller}/{action}")]
public class AuthController(IDbService dbService, IJwtService jwtService) : ControllerBase
{
    // получение ссылки на сервис-поставщик данных из базы данных
    // при помощи внедрения зависимости - через конструктор
    private readonly IDbService _dbService = dbService;

    // получение ссылки на сервис-поставщик jwt-токенов
    // при помощи внедрения зависимости - через конструктор
    private readonly IJwtService _jwtService = jwtService;


    // по POST-запросу при успешной аутентификации вернуть
    // клиенту Ok с JWT-токеном, или сообщение об ошибке аутентификации
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginModel loginModel) {

        // имитация временной задержки
        Task.Delay(1_500).Wait();

        // если данных о пользователе нет - вернуть сообщение об ошибке
        if (loginModel == null || string.IsNullOrEmpty(loginModel.Login) ||
            string.IsNullOrEmpty(loginModel.Password))
            return BadRequest("Данные о пользователе отсутствуют"); // потом убрать сообщение(валидация на клиенте)


        // поиск пользователя по его логину и паролю
        var user = await _dbService
            .GetUserToLoginAsync(loginModel.Login, loginModel.Password);

        // если пользователь не найден(Id=0) - вернуть сообщение
        // об ошибке 401(НЕ АВТОРИЗОВАН)
        if (user.Id == 0)
            return Unauthorized("Invalid username or password " +
                "(Неверный логин или пароль, или пользователь не зарегистрирован)");


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


        // вернуть Ok с JWT-токеном
        return Ok(new { Token = jwtTokenString, User = displayUser });

    } // Login


    // по POST-запросу выйти из учётной записи и вернуть
    // клиенту Ok, или сообщение об ошибке
    [HttpPost]
    public async Task<IActionResult> LogOut([FromBody] LogOutModel logOutModel) {

        // имитация временной задержки
        Task.Delay(1_500).Wait();

        // если данных о пользователе нет - вернуть сообщение об ошибке
        if (logOutModel == null || logOutModel.UserId == 0 ||
            string.IsNullOrEmpty(logOutModel.UserToken))
            return BadRequest();


        // поиск пользователя по Id
        var user = await _dbService.GetUserByIdAsync(logOutModel.UserId);

        // если пользователь не найден(Id=0) - вернуть сообщение
        // об ошибке 401(НЕ АВТОРИЗОВАН)
        if (user.Id == 0)
            return Unauthorized("Invalid user (Пользователь не зарегистрирован)");


        // если пользователь не входил в учётную запись или его
        // параметры не соответствует - вернуть сообщение об ошибке
        if (!user.IsLogin || user.IsLogin != logOutModel.IsLogin ||
            user.UserToken != logOutModel.UserToken)
            return Unauthorized("Invalid token (Требуется повторный вход)");


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
        Task.Delay(1_500).Wait();

        // если данных о пользователе нет - вернуть сообщение об ошибке
        if (refreshModel == null || refreshModel.UserId == 0 ||
            string.IsNullOrEmpty(refreshModel.UserToken))
            return BadRequest();


        // поиск пользователя по Id
        var user = await _dbService.GetUserByIdAsync(refreshModel.UserId);

        // если пользователь не найден(Id=0) - вернуть сообщение
        // об ошибке 401(НЕ АВТОРИЗОВАН)
        if (user.Id == 0)
            return Unauthorized("Invalid user (Пользователь не зарегистрирован)");


        // если пользователь не входил в учётную запись или его токен
        // обновления не соответствует - вернуть сообщение об ошибке
        if (!user.IsLogin || user.UserToken != refreshModel.UserToken)
            return Unauthorized("Invalid token (Требуется повторный вход)");


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


        // вернуть Ok с JWT-токеном
        return Ok(new { Token = jwtTokenString, User = displayUser });

    } // Refresh


    // по POST-запросу зарегистрировать пользователя в системе и вернуть
    // клиенту Ok, или сообщение об ошибке регистрации
    [HttpPost]
    public async Task<IActionResult> Registration([FromBody] LoginModel loginModel) {

        // имитация временной задержки
        Task.Delay(1_500).Wait();

        // если данных о пользователе нет - вернуть сообщение об ошибке
        if (loginModel == null || string.IsNullOrEmpty(loginModel.Phone) ||
            string.IsNullOrEmpty(loginModel.Email))
            return BadRequest("Данные для регистрации отсутствуют"); // потом убрать сообщение(валидация на клиенте)


        // поиск совпадений пользовательских данных
        var regUser = await _dbService
            .GetRegisteredUserAsync(loginModel.Phone, loginModel.Email);

        // если пользователь найден(Id != 0) - вернуть сообщение об ошибке
        if (regUser.Id != 0)
            return BadRequest("Invalid data (Такой пользователь уже зарегистрирован)");


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
        var password = "123";

        // 6 - путь к файлу аватарки пользователя
        // TODO: проработать download
        var avatar = "http://...";

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
        

        // TODO: возможно, потребуется добавить новому
        // пользователю роль типа "НОВИЧЁК"


        // вернуть Ok
        return Ok();

    } // Registration

} // class AuthController