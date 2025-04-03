using Application.Interfaces;
using Application.Services;
using Domain.Models.Dto;
using Domain.Models.Entities;
using Domain.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaProjectCRMWebAPI.Controllers;

// CompaniesController - передаёт данные таблицы "КОМПАНИИ" в JSON-формате
[ApiController]
[Route("api/{controller}/{action}")]
public class CompaniesController(
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

    // количество элементов на странице
    private readonly int _pageSize = 6;

    // количество элементов на странице "бизнес"
    private readonly int _pageSizeBusiness = 3;


    // 1. по GET-запросу вернуть клиенту данные
    // о коллекции записей о компаниях из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page) {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();

        // если данных о запрашиваемой странице нет - вернуть некорректные данные
        // page = 0; // для проверки
        if (page <= 0)
            return BadRequest(new { page });

        // все записи таблицы
        var source = await _dbService.GetAllCompaniesAsync();

        // часть коллекции для заданной страницы
        var items = source
            .Skip((page - 1) * _pageSize)
            .Take(_pageSize)
            .Select(Company.CompanyToDto)
            .ToList();

        // данные для пагинации
        var pageViewModel = new PageViewModel(page, source.Count, _pageSize);
        //var pageViewModel = new PageViewModel(3, 43, _pageSize);

        // данные для вывода части коллекции
        var viewModel = new GetAllCompaniesViewModel(items, pageViewModel);

        // вернуть данные в JSON-формате
        return new JsonResult(viewModel);

    } // GetAllAsync


    // 2. по GET-запросу вернуть клиенту данные о коллекции записей
    // о компаниях из БД для заданного пользователя в JSON-формате
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllByUserIdAsync(
        [FromQuery] int id, [FromQuery] int page) {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();

        // если данных о пользователе нет - вернуть некорректные данные
        // id = 0; // для проверки
        if (id <= 0)
            return BadRequest(new { UserId = 0 });

        // если данных о запрашиваемой странице нет - вернуть некорректные данные
        // page = 0; // для проверки
        if (page <= 0)
            return BadRequest(new { page });

        // все записи о компаниях заданного пользователя
        var source = await _dbService.GetAllCompaniesByUserIdAsync(id);

        // часть коллекции для заданной страницы
        var items = source
            .Skip((page - 1) * _pageSizeBusiness)
            .Take(_pageSizeBusiness)
            .Select(Company.CompanyToDto)
            .ToList();

        // данные для пагинации
        var pageViewModel = new PageViewModel(page, source.Count, _pageSizeBusiness);

        // данные для вывода части коллекции
        var viewModel = new GetAllCompaniesViewModel(items, pageViewModel);

        // вернуть данные в JSON-формате
        return new JsonResult(viewModel);

    } // GetAllByUserIdAsync


    // 3. по GET-запросу вернуть клиенту данные о коллекции записей
    // о компаниях(включая удалённые) из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllWithDeletedAsync() {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();

        // все записи таблицы
        var source = (await _dbService.GetAllCompaniesWithDeletedAsync())
            .Select(Company.CompanyToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllWithDeletedAsync


    // 4. по GET-запросу вернуть клиенту данные о коллекции
    // удалённых записей о компаниях из БД в JSON-формате
    [HttpGet]
    public async Task<IActionResult> GetAllDeletedAsync() {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();

        // все записи таблицы
        var source = (await _dbService.GetAllDeletedCompaniesAsync())
            .Select(Company.CompanyToDto)
            .ToList();

        // вернуть данные в JSON-формате
        return new JsonResult(source);

    } // GetAllDeletedAsync


    // 5. по GET-запросу вернуть клиенту данные о записи о компании
    // по её идентификатору из БД в JSON-формате
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetByIdAsync([FromQuery] int id) {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();


        // если данных об идентификаторе компании нет - вернуть некорректные данные
        // id = 0; // для проверки
        if (id <= 0)
            return BadRequest(new { CompanyId = 0 });


        // поиск записи о компании по Id
        var company = await _dbService.GetCompanyByIdAsync(id);


        // если компания не найдена(Id=0) - вернуть сообщение
        // об ошибке 401(НЕ АВТОРИЗОВАН)
        // company.Id = 0; // для проверки
        if (company.Id == 0)
            return Unauthorized(new { CompanyId = id });


        // получить отображаемые данные о компании(DTO)
        var displayCompany = Company.CompanyToDto(company);

        // вернуть данные в JSON-формате
        return new JsonResult(new { Company = displayCompany });

    } // GetByIdAsync


    // 6. по GET-запросу вернуть клиенту данные для формы
    // создания/изменения данных о компании из БД в JSON-формате
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetCompanyFormParamsAsync() {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();


        // все записи таблицы "СТРАНЫ" из БД
        var allCountries = (await _dbService.GetAllCountriesAsync())
            .Select(Country.CountryToDto)
            .OrderBy(country => country.Name)
            .ToList();

        // все записи таблицы "ГОРОДА" из БД
        var allCities = (await _dbService.GetAllCitiesAsync())
            .Select(City.CityToDto)
            .OrderBy(city => city.Name)
            .ToList();

        // все записи таблицы "УЛИЦЫ" из БД
        var allStreets = (await _dbService.GetAllStreetsAsync())
            .Select(Street.StreetToDto)
            .OrderBy(street => street.Name)
            .ToList();


        // вернуть данные в JSON-формате
        return new JsonResult(new { allCountries, allCities, allStreets });

    } // GetCompanyFormParamsAsync


    // 7. по PUT-запросу получить новые данные о компании для создания
    // новой записи в таблице БД, вернуть клиенту Ok или сообщение об ошибке
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> CreateCompanyAsync([FromBody] CompanyDto company) {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();
        //var temp = Request;


        // если данных о компании нет - вернуть некорректные данные
        //if (true) // для проверки
        if (company == null || company.Id < 0)
            return BadRequest(new { CompanyId = 0 });


        // данные для создания новой записи в БД о компании:

        // 1. данные о пользователе-владельце
        var userOwnerId = company.UserOwnerId;


        // 2. название компании
        var companyName = company.Name;


        // 3. данные об адресе

        // преобразования данных о квартире
        int? flat = company.Address.Flat == 0 ? null : company.Address.Flat;

        // --- проверка, потом удалить
        var allCountries = await _dbService.GetAllCountriesAsync();
        // ---

        // 3.1 получить данные о стране расположения компании
        var country = await _dbService
            .GetCountryByIdAsync(company.Address.City.Country.Id);

        // если запись о стране не найдена - вернуть некорректные данные
        // country.Id = 0; // для проверки
        if (country.Id <= 0)
            return BadRequest(new { CountryId = 0 });


        // 3.2 получить данные о городе расположения компании
        var cityId = company.Address.City.Id;

        // --- проверка, потом удалить
        var allCities = await _dbService.GetAllCitiesAsync();
        // ---

        // если cityId=0 - добавление записи в БД о новом городе
        var city = new City();
        if (cityId == 0) {

            // создание новой записи о городе
            city = new City(company.Address.City.Name, country.Id, null);

            // добавление записи в БД
            (bool isOkCreateCity, string messageCreateCity) =
                await _dbService.CreateCityAsync(city);

            // если при добавлении была ошибка - передать ошибку
            /*(bool isOkCreateCity, string messageCreateCity) =
                (false, "привет-123!!!"); // для проверки*/
            if (!isOkCreateCity)
                return BadRequest(new { CreateMessage = messageCreateCity });

        } else {
            // иначе - получить запись по Id из БД
            city = await _dbService.GetCityByIdAsync(cityId);

            // если запись о городе не найдена или в городе данные о стране
            // не соответствуют данным найденной записи о стране - вернуть
            // некорректные данные
            //city.Id = 0; // для проверки
            if (city.Id == 0 || city.CountryId != country.Id)
                return BadRequest(new { CityId = 0 });

        } // if

        // --- проверка, потом удалить
        var allCountries2 = await _dbService.GetAllCountriesAsync();
        var allCities2 = await _dbService.GetAllCitiesAsync();
        var x = 999; // для точки останова
        // ---


        // 3.3 получить данные об улице расположения компании
        var streetId = company.Address.Street.Id;

        // --- проверка, потом удалить
        var allStreets = await _dbService.GetAllStreetsAsync();
        // ---

        // если streetId=0 - добавление записи в БД о новой улице
        var street = new Street();
        if (streetId == 0) {

            // создание новой записи об улице
            street = new Street(company.Address.Street.Name, null);

            // добавление записи в БД
            (bool isOkCreateStreet, string messageCreateStreet) =
                await _dbService.CreateStreetAsync(street);

            // если при добавлении была ошибка - передать ошибку
            /*(bool isOkCreateStreet, string messageCreateStreet) =
                (false, "привет-123!!!"); // для проверки*/
            if (!isOkCreateStreet)
                return BadRequest(new { CreateMessage = messageCreateStreet });

        } else {
            // иначе - получить запись по Id из БД
            street = await _dbService.GetStreetByIdAsync(streetId);

            // если запись об улице не найдена - вернуть некорректные данные
            // street.Id = 0; // для проверки
            if (street.Id == 0)
                return BadRequest(new { StreetId = 0 });

        } // if

        // --- проверка, потом удалить
        var allStreets2 = await _dbService.GetAllStreetsAsync();
        var x2 = 999; // для точки останова
        // ---


        // 3.4 получить данные об адресе расположения компании

        // --- проверка, потом удалить
        var allAddresses = await _dbService.GetAllAddressesAsync();
        // ---

        // получить запись об адресе по всем полученным параметрам
        var address = await _dbService.GetAddressByParamsAsync(
            city.Id, street.Id, company.Address.Building, flat
        );

        // если address.Id=0 - добавление записи в БД о новом адресе
        if (address.Id == 0) {

            // создание новой записи об адресе
            address = new Address(
                city.Id, street.Id, company.Address.Building, flat, null
            );

            // добавление записи в БД
            (bool isOkCreateAddress, string messageCreateAddress) =
                await _dbService.CreateAddressAsync(address);

            // если при добавлении была ошибка - передать ошибку
            /*(bool isOkCreateAddress, string messageCreateAddress) =
                (false, "привет-123!!!"); // для проверки*/
            if (!isOkCreateAddress)
                return BadRequest(new { CreateMessage = messageCreateAddress });

        } // if


        // --- проверка, потом удалить
        var allAddresses2 = await _dbService.GetAllAddressesAsync();
        // ---


        // 4. телефон компании
        var companyPhone = company.Phone;


        // 5. описание компании
        var description = company.Description == "" ? null : company.Description;


        // 6. путь к файлу изображения логотипа компании
        var companyLogo = company.Logo;

        // получить имена файла и папки его расположения
        // http://localhost:5297/download/getimage/companies/logos/logo.ico
        // http://localhost:5297/download/getTempImage/companies/logos/tempLogo_1_0_kokokok/logo_test_1_69...png
        var items = companyLogo.Split("/", StringSplitOptions.RemoveEmptyEntries);

        var fileName = items[items.Length - 1];

        var fileDirectoryName = items[items.Length - 2];

        // если имя папки соответствует имени временной папки -
        // изображение нужно скопировать из временной папки в рабочую
        var tempDirName = LoadService.TEMP_LOGO;
        if (fileDirectoryName.StartsWith(tempDirName)) {

            // копировать файл из временной папки в рабочую
            CopyImage("logo", fileDirectoryName, fileName);

            // установить новое значение пути изображения логотипа компании
            companyLogo = _loadService.GetPathToCompanyImage("logo", fileName);

        } // if


        // 7. путь к файлу основного изображения компании
        var companyTitleImage = company.TitleImage;

        // получить имена файла и папки его расположения
        // http://localhost:5297/download/getimage/companies/images/company.jpg
        // http://localhost:5297/download/getTempImage/companies/images/tempImage_1_1/company001_123.png
        items = companyTitleImage.Split("/", StringSplitOptions.RemoveEmptyEntries);

        fileName = items[items.Length - 1];

        fileDirectoryName = items[items.Length - 2];

        // если имя папки соответствует имени временной папки -
        // изображение нужно скопировать из временной папки в рабочую
        tempDirName = LoadService.TEMP_IMAGE;
        if (fileDirectoryName.StartsWith(tempDirName)) {

            // копировать файл из временной папки в рабочую
            CopyImage("image", fileDirectoryName, fileName);

            // установить новое значение пути изображения компании
            companyTitleImage = _loadService.GetPathToCompanyImage("image", fileName);

        } // if


        // 8. график работы компании
        var schedule = company.Schedule;


        // 9. сайт компании
        var companySite = company.Site;


        // 10. дата и время удаления записи о компании
        DateTime? deleted = null;


        // создание новой записи о компании
        var newCompany = new Company(
            userOwnerId, companyName, address.Id, companyPhone, description,
            companyLogo, companyTitleImage, schedule, companySite, deleted
        );

        // добавление записи в БД
        (bool isOk, string message) =
            await _dbService.CreateCompanyAsync(newCompany);

        // если при добавлении была ошибка - передать ошибку
        // (bool isOk, string message) = (false, "привет-123!!!"); // для проверки
        if (!isOk)
            return BadRequest(new { CreateMessage = message });


        // вернуть Ok
        return Ok();

    } // CreateCompanyAsync


    // 8. по POST-запросу получить данные о компании для изменения
    // записи в таблице БД, вернуть клиенту Ok или сообщение об ошибке
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> EditCompanyAsync([FromBody] CompanyDto company) {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();
        // var temp = Request;

        // если данных о компании нет - вернуть некорректные данные
        // if (true) // для проверки
        if (company == null || company.Id <= 0)
            return BadRequest(new { CompanyId = 0 });


        // данные для изменения записи в БД о компании:
        // (меняем данные и ссылки при наличии изменений)

        // получить изменяемую запись о компании по Id
        var companyEdt = await _dbService.GetCompanyByIdAsync(company.Id);

        // если компания не найдена(Id=0) - вернуть сообщение
        // об ошибке 401(НЕ АВТОРИЗОВАН)
        // companyEdt.Id = 0; // для проверки
        if (companyEdt.Id == 0)
            return Unauthorized(new { CompanyId = company.Id });


        // 1. данные о пользователе-владельце
        // companyEdt.UserOwnerId = 10; // для проверки
        if (companyEdt.UserOwnerId != company.UserOwnerId) {
            
            // получить запись в БД о пользователе по Id
            var user = await _dbService.GetUserByIdAsync(company.UserOwnerId);

            // если данных о пользователе нет - вернуть некорректные данные
            // user.Id = 0; // для проверки
            if (user.Id == 0)
                return BadRequest(new { UserId = 0 });


            // изменить данные
            companyEdt.UserOwnerId = user.Id;

            // изменить ссылку
            companyEdt.UserOwner = user;

        } // if


        // 2. название компании
        if (companyEdt.Name != company.Name)
            companyEdt.Name = company.Name;


        // 3. адрес компании
        
        // преобразования данных о квартире
        int? flat = company.Address.Flat == 0 ? null : company.Address.Flat;

        // если какой-либо из параметров адреса изменён, то требуется
        // провести изменения соответствующих параметров адреса
        if (companyEdt.Address.CityId   != company.Address.City.Id ||
            companyEdt.Address.StreetId != company.Address.Street.Id ||
            companyEdt.Address.Building != company.Address.Building ||
            companyEdt.Address.Flat     != flat) {

            // --- проверка, потом удалить
            var allCountries = await _dbService.GetAllCountriesAsync();
            // ---

            // 3.1 получить данные о стране расположения компании
            var country = await _dbService
                .GetCountryByIdAsync(company.Address.City.Country.Id);

            // если запись о стране не найдена - вернуть некорректные данные
            // country.Id = 0; // для проверки
            if (country.Id == 0)
                return BadRequest(new { CountryId = 0 });


            // 3.2 получить данные о городе расположения компании
            var cityId = company.Address.City.Id;

            // --- проверка, потом удалить
            var allCities = await _dbService.GetAllCitiesAsync();
            // ---

            // если cityId=0 - добавление записи в БД о новом городе
            var city = new City();
            if (cityId == 0) {

                // создание новой записи о городе
                city = new City(company.Address.City.Name, country.Id, null);

                // добавление записи в БД
                (bool isOkCreateCity, string messageCreateCity) =
                    await _dbService.CreateCityAsync(city);

                // если при добавлении была ошибка - передать ошибку
                /*(bool isOkCreateCity, string messageCreateCity) =
                    (false, "привет-123!!!"); // для проверки*/
                if (!isOkCreateCity)
                    return BadRequest(new { CreateMessage = messageCreateCity });

            } else {
                // иначе - получить запись по Id из БД
                city = await _dbService.GetCityByIdAsync(cityId);

                // если запись о городе не найдена или в городе данные о стране
                // не соответствуют данным найденной записи о стране - вернуть
                // некорректные данные
                // city.Id = 0; // для проверки
                if (city.Id == 0 || city.CountryId != country.Id)
                    return BadRequest(new { CityId = 0 });

            } // if

            // --- проверка, потом удалить
            var allCountries2 = await _dbService.GetAllCountriesAsync();
            var allCities2 = await _dbService.GetAllCitiesAsync();
            var x = 999; // для точки останова
            // ---


            // 3.3 получить данные об улице расположения компании
            var streetId = company.Address.Street.Id;

            // --- проверка, потом удалить
            var allStreets = await _dbService.GetAllStreetsAsync();
            // ---

            // если streetId=0 - добавление записи в БД о новой улице
            var street = new Street();
            if (streetId == 0) {

                // создание новой записи об улице
                street = new Street(company.Address.Street.Name, null);

                // добавление записи в БД
                (bool isOkCreateStreet, string messageCreateStreet) =
                    await _dbService.CreateStreetAsync(street);

                // если при добавлении была ошибка - передать ошибку
                /*(bool isOkCreateStreet, string messageCreateStreet) =
                    (false, "привет-123!!!"); // для проверки*/
                if (!isOkCreateStreet)
                    return BadRequest(new { CreateMessage = messageCreateStreet });

            } else {
                // иначе - получить запись по Id из БД
                street = await _dbService.GetStreetByIdAsync(streetId);

                // если запись об улице не найдена - вернуть некорректные данные
                // street.Id = 0; // для проверки
                if (street.Id == 0)
                    return BadRequest(new { StreetId = 0 });

            } // if

            // --- проверка, потом удалить
            var allStreets2 = await _dbService.GetAllStreetsAsync();
            var x2 = 999; // для точки останова
            // ---


            // 3.4 получить данные об адресе расположения компании

            // --- проверка, потом удалить
            var allAddresses = await _dbService.GetAllAddressesAsync();
            // ---

            // получить запись об адресе по всем полученным параметрам
            var address = await _dbService.GetAddressByParamsAsync(
                city.Id, street.Id, company.Address.Building, flat
            );

            // если address.Id=0 - добавление записи в БД о новом адресе
            if (address.Id == 0) {

                // создание новой записи об адресе
                address = new Address(
                    city.Id, street.Id, company.Address.Building, flat, null
                );

                // добавление записи в БД
                (bool isOkCreateAddress, string messageCreateAddress) =
                    await _dbService.CreateAddressAsync(address);

                // если при добавлении была ошибка - передать ошибку
                /*(bool isOkCreateAddress, string messageCreateAddress) =
                    (false, "привет-123!!!"); // для проверки*/
                if (!isOkCreateAddress)
                    return BadRequest(new { CreateMessage = messageCreateAddress });

            } // if

            // --- проверка, потом удалить
            var allAddresses2 = await _dbService.GetAllAddressesAsync();
            // ---


            // изменить данные
            companyEdt.AddressId = address.Id;

            // изменить ссылку на запись в БД
            companyEdt.Address = address;

        } // if


        // 4. телефон компании
        if (companyEdt.Phone != company.Phone)
            companyEdt.Phone = company.Phone;


        // 5. описание компании
        var companyDescription =
            company.Description == "" ? null : company.Description;
        
        if (companyEdt.Description != companyDescription)
            companyEdt.Description = companyDescription;
        
        
        // 6. путь к файлу изображения логотипа компании
        if (companyEdt.Logo != company.Logo) {

            // получить имена файла и папки его расположения
            // http://localhost:5297/download/getTempImage/companies/logos/tempLogo_1_1/logo_test_1_12...png
            var items = company.Logo.Split("/", StringSplitOptions.RemoveEmptyEntries);

            var fileName = items[items.Length - 1];

            var fileDirectoryName = items[items.Length - 2];


            // если файл находится не во временной папке,
            // вернуть объект и сообщение об ошибке
            var tempDirName = LoadService.TEMP_LOGO;
            // if (fileDirectoryName.StartsWith(tempDirName)) // для проверки
            if (!fileDirectoryName.StartsWith(tempDirName))
                return BadRequest(new { company.Logo });


            // копировать файл из временной папки в рабочую
            CopyImage("logo", fileDirectoryName, fileName);

            // удалить файл со старым изображением логотипа компании
            DeleteOldImage("logo", companyEdt.Logo);


            // установить новое значение пути изображения логотипа компании
            companyEdt.Logo = _loadService.GetPathToCompanyImage("logo", fileName);

        } // if


        // 7. путь к файлу основного изображения компании
        if (companyEdt.TitleImage != company.TitleImage) {

            // получить имена файла и папки его расположения
            // http://localhost:5297/download/getTempImage/companies/images/tempImage_1_1/company001_123.png
            var items = company.TitleImage.Split("/", StringSplitOptions.RemoveEmptyEntries);

            var fileName = items[items.Length - 1];

            var fileDirectoryName = items[items.Length - 2];


            // если файл находится не во временной папке,
            // вернуть объект и сообщение об ошибке
            var tempDirName = LoadService.TEMP_IMAGE;
            // if (fileDirectoryName.StartsWith(tempDirName)) // для проверки
            if (!fileDirectoryName.StartsWith(tempDirName))
                return BadRequest(new { Image = company.TitleImage });


            // копировать файл из временной папки в рабочую
            CopyImage("image", fileDirectoryName, fileName);

            // удалить файл со старым изображением логотипа компании
            DeleteOldImage("image", companyEdt.TitleImage);


            // установить новое значение пути изображения логотипа компании
            companyEdt.TitleImage = _loadService.GetPathToCompanyImage("image", fileName);

        } // if
        
        
        // 8. график работы компании
        if (companyEdt.Schedule != company.Schedule)
            companyEdt.Schedule = company.Schedule;


        // 9. сайт компании
        if (companyEdt.Site != company.Site)
            companyEdt.Site = company.Site;


        // изменение записи в БД
        (bool isOk, string message) =
            await _dbService.UpdateCompanyAsync(companyEdt);

        // если при изменении была ошибка - передать ошибку
        // (bool isOk, string message) = (false, "привет-123!!!"); // для проверки
        if (!isOk)
            return BadRequest(new { UpdateMessage = message });


        // вернуть Ok
        return Ok();

    } // EditCompanyAsync


    // метод копирования файла изображения из временной папки в рабочую
    private void CopyImage(string imageType, string fileDirectoryName, string fileName) {

        // путь к папке с изображениями
        var imagesDirPath = Path.Combine(_environment.ContentRootPath,
            LoadService.APP_DATA, LoadService.COMPANIES,
            imageType == "logo" ? LoadService.LOGOS : LoadService.IMAGES);

        // путь к папке с временными изображениями
        var tempDirectoryPath = Path.Combine(imagesDirPath, fileDirectoryName);

        // полный путь к копируемому файлу
        var tempPath = Path.Combine(tempDirectoryPath, fileName);

        // полный путь к копии файла
        var copyPath = Path.Combine(imagesDirPath, fileName);

        // скопировать файл с изображением
        _loadService.CopyFile(tempPath, copyPath);

        // удалить временную папку со всеми временными изображениями
        (bool isOkDeleteDir, string messageDeleteDir) =
            _loadService.DeleteDirectory(tempDirectoryPath);

    } // CopyImage


    // метод удаления файла со старым изображением компании
    private void DeleteOldImage(string imageType, string companyEdtImage) {
        
        // имя файла старого изображения
        var items = companyEdtImage.Split("/", StringSplitOptions.RemoveEmptyEntries);

        var oldFileName = items[items.Length - 1];


        // путь к папке с изображениями
        var imagesDirPath = Path.Combine(_environment.ContentRootPath,
            LoadService.APP_DATA, LoadService.COMPANIES,
            imageType == "logo" ? LoadService.LOGOS : LoadService.IMAGES);

        // полный путь к удаляемому файлу
        var oldFileNamePath = Path.Combine(imagesDirPath, oldFileName);

        // имя файла по умолчанию(логотипа или основного изображения компании)
        var defaultFileName = imageType == "logo"
            ? LoadService.DEFAULT_LOGO
            : LoadService.DEFAULT_COMPANY;

        // удаление файла (если он не является файлом по умолчанию)
        if (oldFileName != defaultFileName)
            System.IO.File.Delete(oldFileNamePath);

    } // DeleteOldImage


    // 9. по DELETE-запросу удалить временную папку
    // со всеми временными изображениями компании
    [HttpDelete]
    [Authorize]
    public IActionResult DeleteTempCompanyImages(
        [FromQuery] int userId, [FromQuery] int id, [FromQuery] string imageType) {

        // имитация временной задержки
        // Task.Delay(1_500).Wait();
        //var temp = Request;

        // id -> companyId

        // если данных о пользователе нет - вернуть некорректные данные
        //userId = 0; // для проверки
        if (userId <= 0)
            return BadRequest(new { UserId = 0 });

        // если данные о компании неверные - вернуть некорректные данные
        // (id=0 - режим создания компании, иначе - режим изменения данных)
        // id = -1; // для проверки
        if (id < 0)
            return BadRequest(new { CompanyId = 0 });

        // если данных о типе изображения нет - вернуть некорректные данные
        //imageType = ""; // для проверки
        if (string.IsNullOrEmpty(imageType))
            return BadRequest(new { ImageType = "" });


        // для разных типов изображений используем разные папки
        // (для логотипа и основного изображения компании)
        var currentDir = imageType == "logo"
            ? LoadService.LOGOS
            : LoadService.IMAGES;

        // получить путь к папке с временными изображениями
        var tempDirectoryPath = Path.Combine(_environment.ContentRootPath,
            LoadService.APP_DATA, LoadService.COMPANIES, currentDir);

        // получить все папки с временными изображениями для удаления
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
            if ((items[0] == LoadService.TEMP_LOGO ||
                items[0] == LoadService.TEMP_IMAGE) &&
                itemUserId == userId) {

                // удалить временную папку со всеми временными изображениями
                (bool isOk, string message) = _loadService.DeleteDirectory(dir);

                // если при удалении была ошибка - передать ошибку
                //if (true)
                if (!isOk)
                    return BadRequest(new { DeleteMessage = message });

            } // if

        } // foreach dir


        // вернуть Ok
        return Ok();

    } // DeleteTempCompanyImages

} // class CompaniesController