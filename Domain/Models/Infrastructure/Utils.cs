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
    public static DateTime GetRandomDateTime() =>
        new DateTime(
            new DateOnly(
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.AddDays(-GetRandom(1, 3)).Day
            ),
            new TimeOnly(GetRandom(0, 23), GetRandom(0, 59), GetRandom(0, 59))
        );

} // Utils