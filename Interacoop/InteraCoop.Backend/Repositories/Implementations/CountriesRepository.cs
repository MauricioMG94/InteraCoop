using InteraCoop.Backend.Data;
using InteraCoop.Backend.Repositories.Interfaces;
using InteraCoop.Backend.UnitsOfWork.Implementations;
using InteraCoop.Shared.Entities;
using InteraCoop.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace InteraCoop.Backend.Repositories.Implementations
{
    public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
    {
        private readonly DataContext _context;

        public CountriesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<Country>> GetAsync(int id)
        {
            var country = await _context.Countries
                .Include(c => c.States!)
                .ThenInclude(s => s.Cities)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (country == null)
            {
                return new ActionResponse<Country>
                {
                    WasSuccess = false,
                    Message = "Pais no existe"
                };
            }
            return new ActionResponse<Country>
            {
                WasSuccess = true,
                Result = country
            };
        }

        public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync()
        {
            var countries = await _context.Countries
                .Include(x => x.States)
                .ToListAsync();
            return new ActionResponse<IEnumerable<Country>>
            {
                WasSuccess = true,
                Result = countries
            };
        }
    }
}