using InteraCoop.Backend.Repositories.Interfaces;
using InteraCoop.Backend.UnitsOfWork.Interfaces;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using InteraCoop.Shared.Responses;

namespace InteraCoop.Backend.UnitsOfWork.Implementations
{
    public class ClientsUnitOfWork : GenericUnitOfWork<Client>, IClientsUnitOfWork
    {
        private readonly IClientsRepository _clientsRepository;
        public ClientsUnitOfWork(IGenericRepository<Client> repository, IClientsRepository clientsRepository) : base(repository)
        {
            _clientsRepository = clientsRepository;
        }

        async Task<ActionResponse<IEnumerable<Client>>> IClientsUnitOfWork.GetAsync(PaginationDTO pagination) => await _clientsRepository.GetAsync(pagination);

        async Task<ActionResponse<int>> IClientsUnitOfWork.GetTotalPagesAsync(PaginationDTO pagination) => await _clientsRepository.GetTotalPagesAsync(pagination);
    }
}
