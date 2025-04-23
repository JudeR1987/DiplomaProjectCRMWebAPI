using Domain.Models.Dto;

namespace Domain.Models.ViewModels;

// выводимая часть коллекции и информация о пагинации
public class GetAllRecordsViewModel(
    List<RecordDto> records, PageViewModel pageViewModel)
{
    // выводимая коллекция - что выводить
    public List<RecordDto> Records { get; } = records;


    // информация о пагинации - как выводить
    public PageViewModel PageViewModel { get; } = pageViewModel;

} // class GetAllRecordsViewModel