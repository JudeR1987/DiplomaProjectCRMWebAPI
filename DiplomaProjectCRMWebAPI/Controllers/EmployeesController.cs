using Application.Interfaces;
using Application.Services;
using Domain.Models.Dto;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaProjectCRMWebAPI.Controllers;

// EmployeesController - передаёт данные таблицы "СОТРУДНИКИ" в JSON-формате
[ApiController]
[Route("api/{controller}/{action}")]
public class EmployeesController(
    IHostEnvironment environment,
    IDbService dbService,
    ILoadService loadService) : ControllerBase
{
    // ссылка на серверное окружение - для получения папки хоста
    private IHostEnvironment _environment = environment;
    
    // ссылка на сервис-поставщик данных из базы данных
    private readonly IDbService _dbService = dbService;

    // ссылка на сервис для работы с загрузкой/выгрузкой файлов
    private readonly ILoadService _loadService = loadService;


    // 1. по GET-запросу вернуть клиенту данные
    // о коллекции записей о сотрудниках из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllAsync() {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();

        // все записи таблицы
        var source = (await _dbService.GetAllEmployeesAsync())
            .Select(Employee.EmployeeToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllAsync


    // 2. по GET-запросу вернуть клиенту данные о коллекции записей
    // о сотрудниках(включая удалённые) из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllWithDeletedAsync() {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();

        // все записи таблицы
        var source = (await _dbService.GetAllEmployeesWithDeletedAsync())
            .Select(Employee.EmployeeToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllWithDeletedAsync


    // 3. по GET-запросу вернуть клиенту данные о коллекции
    // удалённых записей о сотрудниках из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllDeletedAsync() {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();

        // все записи таблицы
        var source = (await _dbService.GetAllDeletedEmployeesAsync())
            .Select(Employee.EmployeeToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllDeletedAsync


    // 4. по GET-запросу вернуть клиенту данные о коллекции записей
    // о сотрудниках для заданной компании из БД в JSON-формате
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllByCompanyIdAsync(int id) {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();

        // если данных об идентификаторе компании нет - вернуть некорректные данные
        // id = 0; // для проверки
        if (id <= 0)
            return BadRequest(new { CompanyId = 0 });


        // все записи таблицы с указанным параметром
        var employees = (await _dbService.GetAllEmployeesByCompanyIdAsync(id))
            .Select(Employee.EmployeeToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(new { employees });

    } // GetAllByCompanyIdAsync


    // 5. по GET-запросу вернуть клиенту данные о записи о сотруднике
    // по его идентификатору из БД в JSON-формате
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetByIdAsync([FromQuery] int id) {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();

        var temp = DateTime.Now;


        // если данных об идентификаторе сотрудника нет - вернуть некорректные данные
        // id = 0; // для проверки
        if (id <= 0)
            return BadRequest(new { EmployeeId = 0 });


        // поиск записи о сотруднике по Id
        var employee = await _dbService.GetEmployeeByIdAsync(id);

        // если сотрудник не найден(Id=0) - вернуть сообщение
        // об ошибке 401(НЕ АВТОРИЗОВАН)
        // employee.Id = 0; // для проверки
        if (employee.Id == 0)
            return Unauthorized(new { EmployeeId = id });


        // получить отображаемые данные о сотруднике(DTO)
        var displayEmployee = Employee.EmployeeToDto(employee);

        // вернуть данные в JSON-формате
        return new JsonResult(new { Employee = displayEmployee });

    } // GetByIdAsync


    // 6. по DELETE-запросу удалить временную папку
    // со всеми временными фотографиями сотрудника
    [HttpDelete]
    [Authorize]
    public IActionResult DeleteTempEmployeePhotos(
        [FromQuery] int userId, [FromQuery] int id) {

        // id -> employeeId

        // если данных о пользователе нет - вернуть некорректные данные
        // userId = 0; // для проверки
        if (userId <= 0)
            return BadRequest(new { UserId = 0 });

        // если данные о сотруднике неверные - вернуть некорректные данные
        // (id=0 - режим добавления данных, иначе - режим изменения данных)
        // id = -1; // для проверки
        if (id < 0)
            return BadRequest(new { EmployeeId = 0 });


        // путь к папке с временными фотографиями сотрудника
        var tempDirectoryPath = Path.Combine(_environment.ContentRootPath,
            LoadService.APP_DATA, LoadService.EMPLOYEES, LoadService.PHOTOS);

        // получить все папки с временными фотографиями для удаления
        var tempDirectories =
            Directory.GetDirectories(tempDirectoryPath).ToList();


        // если папки не найдены - вернуть сообщение об ошибке
        if (tempDirectories.Count == 0)
            return BadRequest(new { directory = false });


        // удалить все папки данного пользователя
        foreach (var dir in tempDirectories) {

            // получить имя временной папки
            var dirName = new DirectoryInfo(dir).Name;

            // массив элементов в имени
            var items = dirName.Split("_");

            // второй элемент соответствует идентификатору пользователя
            int.TryParse(items[1], out int itemUserId);
            if (items[0] == LoadService.TEMP_PHOTO && itemUserId == userId) {

                // удалить временную папку со всеми временными изображениями
                (bool isOk, string message) = _loadService.DeleteDirectory(dir);

                // если при удалении была ошибка - передать ошибку
                // if (true)
                if (!isOk)
                    return BadRequest(new { DeleteMessage = message });

            } // if

        } // foreach dir


        // вернуть Ok
        return Ok();

    } // DeleteTempEmployeePhotos


    // 7. по GET-запросу вернуть клиенту данные для формы
    // добавления/изменения данных о сотруднике из БД в JSON-формате
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetEmployeeFormParamsAsync() {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();


        // все записи таблицы "СПЕЦИАЛЬНОСТИ" из БД
        var allSpecializations = (await _dbService.GetAllSpecializationsAsync())
            .Select(Specialization.SpecializationToDto)
            .OrderBy(specialization => specialization.Name)
            .ToList();

        // все записи таблицы "ДОЛЖНОСТИ" из БД
        var allPositions = (await _dbService.GetAllPositionsAsync())
            .Select(Position.PositionToDto)
            .OrderBy(position => position.Name)
            .ToList();


        // вернуть данные в JSON-формате
        return new JsonResult(new { allSpecializations, allPositions });

    } // GetEmployeeFormParamsAsync


    // 8. по GET-запросу вернуть клиенту данные о записи о сотруднике
    // по идентификатору пользователя из БД в JSON-формате
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetByUserIdAsync([FromQuery] int id) {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();


        // если данных об идентификаторе пользователя нет - вернуть некорректные данные
        // id = 0; // для проверки
        if (id <= 0)
            return BadRequest(new { UserId = 0 });


        // поиск записи о сотруднике по Id пользователя
        var employee = await _dbService.GetEmployeeByUserIdAsync(id);


        // получить отображаемые данные о сотруднике(DTO)
        var displayEmployee = Employee.EmployeeToDto(employee);

        // вернуть данные в JSON-формате
        return new JsonResult(new { Employee = displayEmployee });

    } // GetByUserIdAsync


    // 9. по PUT-запросу получить новые данные о сотруднике для создания
    // новой записи в таблице БД, вернуть клиенту Ok или сообщение об ошибке
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> CreateEmployeeAsync([FromBody] EmployeeDto employee) {

        // если данных о сотруднике нет или Id некорректный - вернуть некорректные данные
        // if (true) // для проверки
        if (employee == null || employee.Id != 0)
            return BadRequest(new { EmployeeId = 0 });


        // данные для создания новой записи в БД о сотруднике:

        // 1. данные о пользователе
        var userId = employee.User.Id;


        // 2. данные о компании
        var companyId = employee.Company.Id;


        // 3. данные о специальности
        var specializationId = employee.Specialization.Id;

        // --- проверка, потом удалить
        var allSpecializations = await _dbService.GetAllSpecializationsAsync();
        // ---

        // если specializationId=0 - добавление записи в БД о новой специальности
        var specialization = new Specialization();
        if (specializationId == 0) {

            // создание новой записи о специальности
            specialization = new Specialization(employee.Specialization.Name, null);

            // добавление записи в БД
            (bool isOkCreateSpecialization, string messageCreateSpecialization) =
                await _dbService.CreateSpecializationAsync(specialization);

            // если при добавлении была ошибка - передать ошибку
            /*(bool isOkCreateSpecialization, string messageCreateSpecialization) =
                (false, "привет-123!!!"); // для проверки*/
            if (!isOkCreateSpecialization)
                return BadRequest(new { CreateMessage = messageCreateSpecialization });

        } else {
            // иначе - получить запись по Id из БД
            specialization = await _dbService.GetSpecializationByIdAsync(specializationId);

            // если запись о специальности не найдена - вернуть некорректные данные
            // specialization.Id = 0; // для проверки
            if (specialization.Id == 0)
                return BadRequest(new { SpecializationId = 0 });

        } // if

        // --- проверка, потом удалить
        var allSpecializations2 = await _dbService.GetAllSpecializationsAsync();
        var x = 999; // для точки останова
        // ---


        // 4. данные о должности
        var positionId = employee.Position.Id;

        // --- проверка, потом удалить
        var allPositions = await _dbService.GetAllPositionsAsync();
        // ---

        // если positionId=0 - добавление записи в БД о новой должности
        var position = new Position();
        if (positionId == 0) {

            // создание новой записи о должности
            position = new Position(employee.Position.Name, null);

            // добавление записи в БД
            (bool isOkCreatePosition, string messageCreatePosition) =
                await _dbService.CreatePositionAsync(position);

            // если при добавлении была ошибка - передать ошибку
            /*(bool isOkCreatePosition, string messageCreatePosition) =
                (false, "привет-123!!!"); // для проверки*/
            if (!isOkCreatePosition)
                return BadRequest(new { CreateMessage = messageCreatePosition });

        } else {
            // иначе - получить запись по Id из БД
            position = await _dbService.GetPositionByIdAsync(positionId);

            // если запись о должности не найдена - вернуть некорректные данные
            // position.Id = 0; // для проверки
            if (position.Id == 0)
                return BadRequest(new { PositionId = 0 });

        } // if

        // --- проверка, потом удалить
        var allPositions2 = await _dbService.GetAllPositionsAsync();
        var x2 = 999; // для точки останова
        // ---


        // 5. рейтинг сотрудника (от 0 до 5)
        var rating = employee.Rating == 0 ? employee.Rating : 0;


        // 6. путь к файлу аватарки сотрудника
        var employeePhoto = employee.Avatar;

        // получить имена файла и папки расположения фотографии
        // http://localhost:5297/download/getimage/employees/photos/photo.ico
        // http://localhost:5297/download/getTempImage/employees/photos/tempPhoto_1_0_kokokok/test_11_69...jpg
        var items = employeePhoto.Split("/", StringSplitOptions.RemoveEmptyEntries);

        var fileName = items[items.Length - 1];

        var fileDirectoryName = items[items.Length - 2];

        // если имя папки соответствует имени временной папки -
        // фотографию нужно скопировать из временной папки в рабочую
        var tempDirName = LoadService.TEMP_PHOTO;
        if (fileDirectoryName.StartsWith(tempDirName)) {

            // копировать файл из временной папки в рабочую
            CopyPhoto(fileDirectoryName, fileName);

            // установить новое значение пути фотографии сотрудника
            employeePhoto = _loadService.GetPathToEmployeeAvatar(fileName);

        } // if


        // 7. дата и время удаления записи о сотруднике
        DateTime? deleted = null;


        // создание новой записи о сотруднике
        var newEmployee = new Employee(
            userId, companyId, specialization.Id,
            position.Id, rating, employeePhoto, deleted
        );

        // добавление записи в БД
        (bool isOk, string message) =
            await _dbService.CreateEmployeeAsync(newEmployee);

        // если при добавлении была ошибка - передать ошибку
        // (bool isOk, string message) = (false, "привет-123!!!"); // для проверки
        if (!isOk)
            return BadRequest(new { CreateMessage = message });


        // вернуть Ok
        return Ok();

    } // CreateEmployeeAsync


    // 10. по POST-запросу получить данные о сотруднике для изменения
    // записи в таблице БД, вернуть клиенту Ok или сообщение об ошибке
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> EditEmployeeAsync([FromBody] EmployeeDto employee) {

        // если данных о сотруднике нет или Id некорректный - вернуть некорректные данные
        // if (true) // для проверки
        if (employee == null || employee.Id <= 0)
            return BadRequest(new { EmployeeId = 0 });


        // данные для изменения записи в БД о сотруднике:
        // (меняем данные и ссылки при наличии изменений)

        // получить изменяемую запись о сотруднике по Id
        var employeeEdt = await _dbService.GetEmployeeByIdAsync(employee.Id);

        // если сотрудник не найден(Id=0) - вернуть сообщение
        // об ошибке 401(НЕ АВТОРИЗОВАН)
        // employeeEdt.Id = 0; // для проверки
        if (employeeEdt.Id == 0)
            return Unauthorized(new { EmployeeId = employee.Id });


        // 1. данные о пользователе
        // employeeEdt.UserId = 999; // для проверки
        if (employeeEdt.UserId != employee.User.Id) {

            // получить запись в БД о пользователе по Id
            var user = await _dbService.GetUserByIdAsync(employee.User.Id);

            // если данных о пользователе нет - вернуть некорректные данные
            // user.Id = 0; // для проверки
            if (user.Id == 0)
                return BadRequest(new { UserId = 0 });


            // изменить данные
            employeeEdt.UserId = user.Id;

            // изменить ссылку
            employeeEdt.User = user;

        } // if


        // 2. данные о компании
        // employeeEdt.CompanyId = 999; // для проверки
        if (employeeEdt.CompanyId != employee.Company.Id) {

            // получить запись в БД о компании по Id
            var company = await _dbService.GetCompanyByIdAsync(employee.Company.Id);

            // если данных о компании нет - вернуть некорректные данные
            // company.Id = 0; // для проверки
            if (company.Id == 0)
                return BadRequest(new { CompanyId = 0 });


            // изменить данные
            employeeEdt.CompanyId = company.Id;

            // изменить ссылку
            employeeEdt.Company = company;

        } // if


        // 3. данные о специальности
        if (employeeEdt.SpecializationId != employee.Specialization.Id) {

            // получить данные о специальности сотрудника
            var specializationId = employee.Specialization.Id;

            // --- проверка, потом удалить
            var allSpecializations = await _dbService.GetAllSpecializationsAsync();
            // ---

            // если specializationId=0 - добавление записи в БД о новой специальности
            var specialization = new Specialization();
            if (specializationId == 0) {

                // создание новой записи о специальности
                specialization = new Specialization(employee.Specialization.Name, null);

                // добавление записи в БД
                (bool isOkCreateSpecialization, string messageCreateSpecialization) =
                    await _dbService.CreateSpecializationAsync(specialization);

                // если при добавлении была ошибка - передать ошибку
                /*(bool isOkCreateSpecialization, string messageCreateSpecialization) =
                    (false, "привет-123!!!"); // для проверки*/
                if (!isOkCreateSpecialization)
                    return BadRequest(new { CreateMessage = messageCreateSpecialization });

            } else {
                // иначе - получить запись по Id из БД
                specialization = await _dbService.GetSpecializationByIdAsync(specializationId);

                // если запись о специальности не найдена - вернуть некорректные данные
                // specialization.Id = 0; // для проверки
                if (specialization.Id == 0)
                    return BadRequest(new { SpecializationId = 0 });

            } // if

            // --- проверка, потом удалить
            var allSpecializations2 = await _dbService.GetAllSpecializationsAsync();
            var x = 999; // для точки останова
            // ---


            // изменить данные
            employeeEdt.SpecializationId = specialization.Id;

            // изменить ссылку на запись в БД
            employeeEdt.Specialization = specialization;

        } // if


        // 4. данные о должности
        if (employeeEdt.PositionId != employee.Position.Id) {

            // получить данные о должности сотрудника
            var positionId = employee.Position.Id;

            // --- проверка, потом удалить
            var allPositions = await _dbService.GetAllPositionsAsync();
            // ---

            // если positionId=0 - добавление записи в БД о новой должности
            var position = new Position();
            if (positionId == 0) {

                // создание новой записи о должности
                position = new Position(employee.Position.Name, null);

                // добавление записи в БД
                (bool isOkCreatePosition, string messageCreatePosition) =
                    await _dbService.CreatePositionAsync(position);

                // если при добавлении была ошибка - передать ошибку
                /*(bool isOkCreatePosition, string messageCreatePosition) =
                    (false, "привет-123!!!"); // для проверки*/
                if (!isOkCreatePosition)
                    return BadRequest(new { CreateMessage = messageCreatePosition });

            } else {
                // иначе - получить запись по Id из БД
                position = await _dbService.GetPositionByIdAsync(positionId);

                // если запись о должности не найдена - вернуть некорректные данные
                // position.Id = 0; // для проверки
                if (position.Id == 0)
                    return BadRequest(new { PositionId = 0 });

            } // if

            // --- проверка, потом удалить
            var allPositions2 = await _dbService.GetAllPositionsAsync();
            var x2 = 999; // для точки останова
            // ---


            // изменить данные
            employeeEdt.PositionId = position.Id;

            // изменить ссылку на запись в БД
            employeeEdt.Position = position;

        } // if


        // 5. рейтинг сотрудника (от 0 до 5) - без изменений


        // 6. путь к файлу аватарки сотрудника
        if (employeeEdt.Avatar != employee.Avatar) {

            // получить имена файла и папки расположения фотографии
            // http://localhost:5297/download/getTempImage/employees/photos/tempPhoto_1_0_kokokok/test_11_69...jpg
            var items = employee.Avatar.Split("/", StringSplitOptions.RemoveEmptyEntries);

            var fileName = items[items.Length - 1];

            var fileDirectoryName = items[items.Length - 2];

            // если файл находится не во временной папке,
            // вернуть объект и сообщение об ошибке
            var tempDirName = LoadService.TEMP_PHOTO;
            // if (fileDirectoryName.StartsWith(tempDirName)) // для проверки
            if (!fileDirectoryName.StartsWith(tempDirName))
                return BadRequest(new { employee.Avatar });


            // копировать файл из временной папки в рабочую
            CopyPhoto(fileDirectoryName, fileName);

            // удалить файл со старой фотографией сотрудника
            DeleteOldPhoto(employeeEdt.Avatar);


            // установить новое значение пути фотографии сотрудника
            employeeEdt.Avatar = _loadService.GetPathToEmployeeAvatar(fileName);

        } // if


        // изменение записи в БД
        (bool isOk, string message) =
            await _dbService.UpdateEmployeeAsync(employeeEdt);

        // если при изменении была ошибка - передать ошибку
        // (bool isOk, string message) = (false, "привет-123!!!"); // для проверки
        if (!isOk)
            return BadRequest(new { UpdateMessage = message });


        // вернуть Ok
        return Ok();

    } // EditEmployeeAsync


    // 11. по DELETE-запросу удалить данные о сотруднике
    // и вернуть клиенту Ok, или сообщение об ошибке
    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteEmployeeAsync([FromQuery] int id) {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();

        // если данных о сотруднике нет - вернуть некорректные данные
        // id = 0; // для проверки
        if (id <= 0)
            return BadRequest(new { EmployeeId = 0 });


        // поиск сотрудника по Id
        var employee = await _dbService.GetEmployeeByIdAsync(id);

        // если сотрудник не найден(Id=0) - вернуть сообщение
        // об ошибке 401(НЕ АВТОРИЗОВАН)
        // employee.Id = 0; // для проверки
        if (employee.Id == 0)
            return Unauthorized(new { EmployeeId = id });


        // установить в записи данных о сотруднике
        // время и дату удаления записи
        employee.Deleted = DateTime.Now;

        // изменение записи в БД
        (bool isOk, string message) =
            await _dbService.UpdateEmployeeAsync(employee);

        // если при изменении была ошибка - передать ошибку
        // (bool isOk, string message) = (false, "привет-123!!!"); // для проверки
        if (!isOk)
            return BadRequest(new { UpdateMessage = message });


        // вернуть Ok
        return Ok();

    } // DeleteEmployeeAsync


    // 12. по GET-запросу вернуть клиенту данные о коллекции записей
    // об услугах для заданного сотрудника из БД в JSON-формате
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllServicesByEmployeeIdAsync([FromQuery] int id) {

        // если данных об идентификаторе сотрудника нет - вернуть некорректные данные
        // id = 0; // для проверки
        if (id <= 0)
            return BadRequest(new { EmployeeId = 0 });


        // получить отображаемые данные(DTO) об услугах
        // для заданного сотрудника
        var services = (await _dbService.GetAllServicesByEmployeeIdAsync(id))
            .Select(Service.ServiceToDto)
            .ToList();


        // вернуть данные в JSON-формате
        return new JsonResult(new { services });

    } // GetAllServicesByEmployeeIdAsync


    // 12. по GET-запросу вернуть клиенту данные о коллекции записей об услугах
    // для заданного сотрудника, сгруппированные по категориям услуг из БД в JSON-формате
    /*[HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllServicesByEmployeeIdGroupByCategoriesAsync(
        [FromQuery] int id) {

        // если данных об идентификаторе сотрудника нет - вернуть некорректные данные
        // id = 0; // для проверки
        if (id <= 0)
            return BadRequest(new { EmployeeId = 0 });


        // получить отображаемые данные(DTO) об услугах
        // для заданного сотрудника, сгруппированные по категориям услуг
        var displayServicesCategories = await _dbService
            .GetAllServicesByEmployeeIdGroupByCategoriesAsync(id);

        *//*var displayServicesCategories = (await _dbService.GetEmployeeByIdAsync(id))
            .EmployeesServices
            .Where(employeeService => employeeService.Deleted == null &&
                                      employeeService.Service.Deleted == null)
            .Select(employeeService => employeeService.Service)
            .GroupBy(service => service.ServicesCategory,
                (key, group) => new {
                    ServicesCategory = key,
                    Services = group.ToList()
                })
            .Where(group => group.ServicesCategory.Deleted == null)
            .Select(group => new DisplayServicesCategory(
                ServicesCategory.ServicesCategoryToDto(group.ServicesCategory),
                Service.ServicesToDto(group.Services)
                ))
            .ToList();*//*

        // вернуть данные в JSON-формате
        return new JsonResult(new { displayServicesCategories });

    } // GetAllServicesByEmployeeIdGroupByCategoriesAsync*/


    // метод копирования файла фотографии из временной папки в рабочую
    private void CopyPhoto(string fileDirectoryName, string fileName) {

        // путь к папке с фотографиями сотрудников
        var photosDirPath = Path.Combine(_environment.ContentRootPath,
            LoadService.APP_DATA, LoadService.EMPLOYEES, LoadService.PHOTOS);

        // путь к папке с временными фотографиями
        var tempDirectoryPath = Path.Combine(photosDirPath, fileDirectoryName);

        // полный путь к копируемому файлу
        var tempPath = Path.Combine(tempDirectoryPath, fileName);

        // полный путь к копии файла
        var copyPath = Path.Combine(photosDirPath, fileName);

        // скопировать файл с фотографией
        _loadService.CopyFile(tempPath, copyPath);

        // удалить временную папку со всеми временными фотографиями
        (bool isOkDeleteDir, string messageDeleteDir) =
            _loadService.DeleteDirectory(tempDirectoryPath);

    } // CopyPhoto


    // метод удаления файла со старой фотографией сотрудника
    private void DeleteOldPhoto(string employeeEdtAvatar) {

        // имя файла старой фотографии
        var items = employeeEdtAvatar.Split("/", StringSplitOptions.RemoveEmptyEntries);

        var oldFileName = items[items.Length - 1];


        // путь к папке с фотографиями сотрудников
        var photosDirPath = Path.Combine(_environment.ContentRootPath,
            LoadService.APP_DATA, LoadService.EMPLOYEES, LoadService.PHOTOS);

        // полный путь к удаляемому файлу
        var oldFileNamePath = Path.Combine(photosDirPath, oldFileName);

        // имя файла по умолчанию(логотипа или основного изображения компании)
        var defaultFileName = LoadService.DEFAULT_PHOTO;

        // удаление файла (если он не является файлом по умолчанию)
        if (oldFileName != defaultFileName)
            System.IO.File.Delete(oldFileNamePath);

    } // DeleteOldPhoto

} // class EmployeesController