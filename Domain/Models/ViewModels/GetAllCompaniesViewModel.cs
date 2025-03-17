using Domain.Models.Dto;

namespace Domain.Models.ViewModels;

// выводимая часть коллекции и информация о пагинации
public class GetAllCompaniesViewModel(
    List<CompanyDto> companies, PageViewModel pageViewModel)
{
    // выводимая коллекция - что выводить
    public List<CompanyDto> Companies { get; } = companies;


    // информация о пагинации - как выводить
    public PageViewModel PageViewModel { get; } = pageViewModel;

} // class GetAllCompaniesViewModel