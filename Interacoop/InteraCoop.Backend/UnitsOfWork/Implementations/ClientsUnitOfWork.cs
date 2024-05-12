using InteraCoop.Backend.Repositories.Implementations;
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

        public async Task<ActionResponse<Client>> AddFullAsync(ClientDto clientDto) => await _clientsRepository.AddFullAsync(clientDto);

        public async Task<ActionResponse<Client>> UpdateFullAsync(ClientDto clientDto) => await _clientsRepository.UpdateFullAsync(clientDto);
        public override async Task<ActionResponse<Client>> GetAsync(int id) => await _clientsRepository.GetAsync(id);

        async Task<ActionResponse<IEnumerable<Client>>> IClientsUnitOfWork.GetAsync(PaginationDTO pagination) => await _clientsRepository.GetAsync(pagination);

        async Task<ActionResponse<int>> IClientsUnitOfWork.GetTotalPagesAsync(PaginationDTO pagination) => await _clientsRepository.GetTotalPagesAsync(pagination);

    }
}
