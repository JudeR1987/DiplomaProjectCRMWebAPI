namespace Domain.Models.Infrastructure;

// вспомогательные объекты и методы
public static class Utils
{
    // объект для получения случайных чисел
    public static Random Random = new Random();


    // генератор случайных чисел
    // формирование случайных целых чисел в диапазоне от lo до hi
    public static int GetRandom(int lo, int hi) => Random.Next(lo, hi + 1);


    // формирование случайных вещественных чисел в диапазоне от lo до hi
    public static double GetRandom(double lo, double hi)
        => lo + (hi - lo) * Random.NextDouble();


    // формирование строки из n-ого количества случайных символов
    public static string GetRandomString(int characters = 8) =>
        Guid.NewGuid().ToString().Substring(0, characters);


    // формирование случайного прошедшего даты и времени
    public static DateTime GetRandomDateTime() {

        // случайное количество дней
        var randomDays = GetRandom(1, 4);
        
        // параметры даты с вычетом полученного количества дней
        var year = DateTime.Now.AddDays(-randomDays).Year;
        var month = DateTime.Now.AddDays(-randomDays).Month;
        var day = DateTime.Now.AddDays(-randomDays).Day;

        // дата
        var date = new DateOnly(year, month, day);

        // случайное время
        var time = new TimeOnly(GetRandom(0, 23), GetRandom(0, 59), GetRandom(0, 59));

        return new DateTime(date, time);

    } // GetRandomDateTime
    
    /*public static DateTime GetRandomDateTime() =>
        new DateTime(
            new DateOnly(
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.AddDays(-GetRandom(1, 3)).Day
            ),
            new TimeOnly(GetRandom(0, 23), GetRandom(0, 59), GetRandom(0, 59))
        );*/


    // вернуть строку с датой в формате yyyy-mm-dd
    public static string DateToYYYYMMDD(DateTime date, string separator = "-") =>
        string.Join(
            separator,
            date.Date.ToString().Substring(0, 10).Split(".").Reverse()
        );


    // получить дату из строки формата "yyyy-mm-dd"
    public static DateTime StringToDate(string dateString) {

        // если получена строка формата "2024-07-02T08:46:37.808Z", то требуется обрезка
        // если получена строка формата "2024-07-02", то обрезка не требуется
        if (string.IsNullOrWhiteSpace(dateString)) return new DateTime();
        if (dateString.Length > 10) dateString = dateString.Substring(0, 10);

        var year = int.Parse(dateString.Split("-")[0]);
        var month = int.Parse(dateString.Split("-")[1]);
        var day = int.Parse(dateString.Split("-")[2]);

        return new DateTime(year, month, day);

    } // StringToDate


    // получить время из строки формата "HH:mm"
    public static TimeOnly StringToTime(string timeString) {

        // если получена пустая строка, вернуть время по умолчанию
        if (string.IsNullOrWhiteSpace(timeString)) return new TimeOnly(0, 0);

        var hours = int.Parse(timeString.Split(":")[0]);
        var minutes = int.Parse(timeString.Split(":")[1]);

        return new TimeOnly(hours, minutes);

    } // StringToTime


    // получить дату из строки формата "yyyy-mm-dd" и строки формата "HH:mm"
    public static DateTime StringToDateTime(string dateString, string timeString) {

        // если получена строка формата "2024-07-02T08:46:37.808Z", то требуется обрезка
        // если получена строка формата "2024-07-02", то обрезка не требуется
        if (string.IsNullOrWhiteSpace(dateString)) return new DateTime();
        if (dateString.Length > 10) dateString = dateString.Substring(0, 10);

        var year = int.Parse(dateString.Split("-")[0]);
        var month = int.Parse(dateString.Split("-")[1]);
        var day = int.Parse(dateString.Split("-")[2]);

        var hours = int.Parse(timeString.Split(":")[0]);
        var minutes = int.Parse(timeString.Split(":")[1]);

        return new DateTime(year, month, day, hours, minutes, 0);

    } // StringToDateTime

} // Utils