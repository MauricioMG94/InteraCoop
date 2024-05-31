using InteraCoop.Backend.Data;
using InteraCoop.Backend.Helpers;
using InteraCoop.Backend.Repositories.Interfaces;
using InteraCoop.Backend.UnitsOfWork.Implementations;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using InteraCoop.Shared.Responses;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InteraCoop.Backend.Repositories.Implementations
{
    public class InteractionsRepository : GenericRepository<Interaction>, IInteractionsRepository
    {
        private readonly DataContext _context;

        public InteractionsRepository(DataContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<ActionResponse<Interaction>> GetAsync(int id)
        {
            var interaction = await _context.Interactions
                .Include(x => x.Client!)
                .Include(x => x.User!)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (interaction == null)
            {
                return new ActionResponse<Interaction>
                {
                    WasSuccess = false,
                    Message = "La Interacción no existe"
                };
            }
            return new ActionResponse<Interaction>
            {
                WasSuccess = true,
                Result = interaction
            };
        }

        public async override Task<ActionResponse<IEnumerable<Interaction>>> GetAsync()
        {
            var interactions = await _context.Interactions
                .OrderBy(c => c.InteractionType)
                .ToListAsync();
            return new ActionResponse<IEnumerable<Interaction>>
            {
                WasSuccess = true,
                Result = interactions
            };
        }
        public override async Task<ActionResponse<IEnumerable<Interaction>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.Interactions
                .Include(x => x.Client)
                .Include(x => x.User)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.InteractionType.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<Interaction>>
            {
                WasSuccess = true,
                Result = await queryable.OrderBy(x => x.InteractionCreationDate).Paginate(pagination).ToListAsync()
            };
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.Interactions.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.InteractionType.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling(count / pagination.RecordsNumber);
            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = totalPages
            };
        }

        public async Task<ActionResponse<Interaction>> AddAsync(InteractionDto interactionDto)
        {
            try
            {
                var client = await _context.Clients.FirstOrDefaultAsync(x => x.Id == interactionDto.ClientId);
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == interactionDto.UserDocument);

                var newInteraction = new Interaction
                {
                    InteractionType = interactionDto.InteractionType,
                    InteractionCreationDate = interactionDto.InteractionCreationDate,
                    StartDate = interactionDto.StartDate,
                    EndDate = interactionDto.EndDate,
                    Address = interactionDto.Address,
                    ObservationsInteraction = interactionDto.ObservationsInteraction,
                    Office = interactionDto.Office,
                    AuditDate = interactionDto.AuditDate,
                    UserDocument = interactionDto.UserDocument,
                    User = user,
                    ClientId = interactionDto.ClientId,
                    Client = client,
                };
                _context.Add(newInteraction);
                await _context.SaveChangesAsync();
                return new ActionResponse<Interaction>
                {
                    WasSuccess = true,
                    Result = newInteraction,
                };
            }
            catch (DbUpdateException)
            {
                return new ActionResponse<Interaction>
                {
                    WasSuccess = false,
                    Message = "Ya existe una interacción con el mismo Id."
                };
            }
            catch (Exception exception)
            {
                return new ActionResponse<Interaction>
                {
                    WasSuccess = false,
                    Message = exception.Message,
                };
            }
        }

        public async Task<ActionResponse<Interaction>> UpdateAsync(InteractionDto interactionDto)
        {
            try
            {
                var client = await _context.Clients.FirstOrDefaultAsync(x => x.Id == interactionDto.ClientId);
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == interactionDto.UserDocument);

                var interaction = await _context.Interactions
                    .Include(x => x.Client)
                    .FirstOrDefaultAsync(x => x.Id == interactionDto.Id);
                if (interaction == null)
                {
                    return new ActionResponse<Interaction>
                    {
                        WasSuccess = false,
                        Message = "La Interacción no existe"
                    };
                }
                interaction.InteractionType = interactionDto.InteractionType;
                interaction.InteractionCreationDate = interactionDto.InteractionCreationDate;
                interaction.StartDate = interactionDto.StartDate;
                interaction.EndDate = interactionDto.EndDate;
                interaction.Address = interactionDto.Address;
                interaction.ObservationsInteraction = interactionDto.ObservationsInteraction;
                interaction.Office = interactionDto.Office;
                interaction.AuditDate = interactionDto.AuditDate;
                interaction.UserDocument = interactionDto.UserDocument;
                interaction.User = user;
                interaction.ClientId = interactionDto.ClientId;
                interaction.Client = client;
                _context.Update(interaction);
                await _context.SaveChangesAsync();
                return new ActionResponse<Interaction>
                {
                    WasSuccess = true,
                    Result = interaction
                };
            }
            catch (DbUpdateException)
            {
                return new ActionResponse<Interaction>
                {
                    WasSuccess = false,
                    Message = "Ya existe una interación con el mismo id"
                };
            }
            catch (Exception exception)
            {
                return new ActionResponse<Interaction>
                {
                    WasSuccess = false,
                    Message = exception.Message
                };
            }
        }

        public override async Task<ActionResponse<Interaction>> DeleteAsync(int id)
        {
            var interaction = await _context.Interactions.FirstOrDefaultAsync(x => x.Id == id);
            if (interaction == null)
            {
                return new ActionResponse<Interaction>
                {
                    WasSuccess = false,
                    Message = "Interación no encontrada"
                };
            }
            try
            {
                _context.Interactions.Remove(interaction);
                await _context.SaveChangesAsync();
                return new ActionResponse<Interaction>
                {
                    WasSuccess = true,
                };

            }
            catch
            {
                return new ActionResponse<Interaction>
                {
                    WasSuccess = false,
                    Message = "No se puede borrar la interacción, porque tiene registros relacionados"
                };
            }
        }
    }

}
