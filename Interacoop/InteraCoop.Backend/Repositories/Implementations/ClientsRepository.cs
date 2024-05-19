using InteraCoop.Backend.Data;
using InteraCoop.Backend.Helpers;
using InteraCoop.Backend.Repositories.Interfaces;
using InteraCoop.Backend.UnitsOfWork.Implementations;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using InteraCoop.Shared.Enums;
using InteraCoop.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace InteraCoop.Backend.Repositories.Implementations
{
    public class ClientsRepository : GenericRepository<Client>, IClientsRepository
    {
        private readonly DataContext _context;
        public ClientsRepository(DataContext context) : base(context)
        {
            _context = context; 
        }

        public async Task<IEnumerable<Client>> GetComboAsync()
        {
            return await _context.Clients
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public override async Task<ActionResponse<IEnumerable<Client>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.Clients.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<Client>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(x => x.Name)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.Clients.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling(count / pagination.RecordsNumber);
            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = totalPages
            };
        }

        public override async Task<ActionResponse<Client>> GetAsync(int id)
        {
            var client = await _context.Clients
                .FirstOrDefaultAsync(x => x.Id == id);

            if (client == null)
            {
                return new ActionResponse<Client>
                {
                    WasSuccess = false,
                    Message = "Cliente no existe"
                };
            }

            return new ActionResponse<Client>
            {
                WasSuccess = true,
                Result = client
            };
        }

        public async Task<ActionResponse<Client>> AddFullAsync(ClientDto clientDto)
        {
            try
            {
                var newClient = new Client
                {
                    Name = clientDto.Name,
                    DocumentType = DocumentType.CC,
                    Document = clientDto.Document,
                    Address = clientDto.Address,
                    Telephone = clientDto.Telephone,
                    AuditUpdate = DateTime.Today,
                    AuditUser = "Admin"
                };

                 _context.Add(newClient);
                await _context.SaveChangesAsync();
                return new ActionResponse<Client>
                {
                    WasSuccess = true,
                    Result = newClient
                };
            }
            catch (DbUpdateException)
            {
                return new ActionResponse<Client>
                {
                    WasSuccess = false,
                    Message = "Ya existe un cliente con el mismo nombre."
                };
            }
            catch (Exception exception)
            {
                return new ActionResponse<Client>
                {
                    WasSuccess = false,
                    Message = exception.Message
                };
            }
        }

        public async Task<ActionResponse<Client>> UpdateFullAsync(ClientDto clientDto)
        {
            try
            {
                var client = await _context.Clients
                   /* .Include(x => x.ProductCategories!)
                    .ThenInclude(x => x.Category)*/
                    .FirstOrDefaultAsync(x => x.Id == clientDto.Id);
                if (client == null)
                {
                    return new ActionResponse<Client>
                    {
                        WasSuccess = false,
                        Message = "Cliente no existe"
                    };
                }

                    client.Name = clientDto.Name;
                    client.DocumentType = DocumentType.CC;
                    client.Document = clientDto.Document;
                    client.Address = clientDto.Address;
                    client.Telephone = clientDto.Telephone;
                    client.AuditUpdate = DateTime.Today;
                    client.AuditUser = "Admin";


                _context.Update(client);
                await _context.SaveChangesAsync();
                return new ActionResponse<Client>
                {
                    WasSuccess = true,
                    Result = client
                };
            }
            catch (DbUpdateException)
            {
                return new ActionResponse<Client>
                {
                    WasSuccess = false,
                    Message = "Ya existe un cliente con el mismo nombre."
                };
            }
            catch (Exception exception)
            {
                return new ActionResponse<Client>
                {
                    WasSuccess = false,
                    Message = exception.Message
                };
            }
        }
    }
}
