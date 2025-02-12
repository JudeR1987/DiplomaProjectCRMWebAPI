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
        $"{Guid.NewGuid().ToString().Substring(0, characters)}";

} // Utils