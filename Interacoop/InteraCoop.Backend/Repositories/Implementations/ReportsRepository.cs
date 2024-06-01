using InteraCoop.Backend.Data;
using InteraCoop.Backend.Helpers;
using InteraCoop.Backend.Repositories.Interfaces;
using InteraCoop.Backend.UnitsOfWork.Implementations;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace InteraCoop.Backend.Repositories.Implementations
{
    public class ReportsRepository : GenericRepository<ReportDto>, IReportsRepository
    {
        private readonly DataContext _context;

        public ReportsRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public  async Task <ActionResponse<IEnumerable<ReportDto>>> GetInteractionsReportAsync(PaginationDTO pagination, int id)
        {
            var queryable = _context.Interactions
            .Where(x => x.UserId == id)
            .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.InteractionType.ToLower().Contains(pagination.Filter.ToLower()));
            }

            var reportsQuery = queryable
                .GroupBy(x => new { x.user.UserName, x.InteractionType })
                .Select(g => new ReportDto
                {
                    UserName = g.Key.UserName,
                    InteractionType = g.Key.InteractionType,
                    InteractionCount = g.Count()
                });

            var paginatedReports = await reportsQuery
                .OrderBy(r => r.UserName).ThenBy(r => r.InteractionType) 
                .Paginate(pagination) 
                .ToListAsync();

            return new ActionResponse<IEnumerable<ReportDto>>
            {
                WasSuccess = true,
                Result = paginatedReports
            };
        }
    }
}
