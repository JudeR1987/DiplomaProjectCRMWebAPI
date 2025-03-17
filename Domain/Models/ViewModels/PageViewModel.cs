namespace Domain.Models.ViewModels;

// класс, хранящий информацию о пагинации
public class PageViewModel(int pageNumber, int count, int pageSize)
{
    // номер текущей страницы
    public int PageNumber { get; } = pageNumber;

    // количество элементов коллекции на одной странице
    public int PageSize { get; } = pageSize;


    // общее количество страниц
    // (count - количество элементов коллекции)
    public int TotalPages => (int)Math.Ceiling(count / (double)PageSize);


    // наличие страниц "ДО" текущей
    public bool HasPreviousPage => PageNumber > 1;


    // наличие страниц "ПОСЛЕ" текущей
    public bool HasNextPage => PageNumber < TotalPages;

} // class PageViewModel