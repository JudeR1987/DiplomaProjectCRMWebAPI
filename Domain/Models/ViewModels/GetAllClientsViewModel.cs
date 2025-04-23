using Domain.Models.Dto;

namespace Domain.Models.ViewModels;

// выводимая часть коллекции и информация о пагинации
public class GetAllClientsViewModel(
    List<ClientDto> clients, PageViewModel pageViewModel)
{
    // выводимая коллекция - что выводить
    public List<ClientDto> Clients { get; } = clients;


    // информация о пагинации - как выводить
    public PageViewModel PageViewModel { get; } = pageViewModel;

} // class GetAllClientsViewModel