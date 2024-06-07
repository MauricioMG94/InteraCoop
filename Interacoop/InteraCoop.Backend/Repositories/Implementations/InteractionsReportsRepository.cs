using InteraCoop.Backend.Data;
using InteraCoop.Backend.Helpers;
using InteraCoop.Backend.Repositories.Interfaces;
using InteraCoop.Backend.UnitsOfWork.Implementations;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace InteraCoop.Backend.Repositories.Implementations
{
    public class InteractionsReportsRepository : GenericRepository<ReportDto>, IInteractionsReportsRepository
    {
        private readonly DataContext _context;

        public InteractionsReportsRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ActionResponse<IEnumerable<ReportDto>>> GetInteractionsReportAsync(PaginationDTO pagination)
        {
            var queryable = _context.Interactions.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.InteractionType.ToLower().Contains(pagination.Filter.ToLower()));
            }

            var reportsQuery = queryable
                .GroupBy(x => new { x.User.UserName, FullName = x.User.FirstName + " " + x.User.LastName, x.InteractionType })
                .Select(g => new ReportDto
                {
                    UserName = g.Key.UserName,
                    FullName = g.Key.FullName,
                    Type = g.Key.InteractionType,
                    TypeCount = g.Count()
                });

            var paginatedReports = await reportsQuery
                .OrderBy(r => r.FullName).ThenBy(r => r.Type)
                .Paginate(pagination)
                .ToListAsync();

            return new ActionResponse<IEnumerable<ReportDto>>
            {
                WasSuccess = true,
                Result = paginatedReports
            };
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.Interactions.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.InteractionType.ToLower().Contains(pagination.Filter.ToLower()));
            }

            var reportsQuery = queryable
                .GroupBy(x => new { x.User.UserName, FullName = x.User.FirstName + " " + x.User.LastName, x.InteractionType })
                .Select(g => new ReportDto
                {
                    UserName = g.Key.UserName,
                    FullName = g.Key.FullName,
                    Type = g.Key.InteractionType,
                    TypeCount = g.Count()
                });

            double totalReports = await reportsQuery.CountAsync();
            int totalPages = (int)Math.Ceiling(totalReports / pagination.RecordsNumber);
            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = totalPages
            };

        }

    }
}
