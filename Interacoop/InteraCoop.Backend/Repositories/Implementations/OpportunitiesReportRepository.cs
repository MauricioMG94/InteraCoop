using InteraCoop.Backend.Data;
using InteraCoop.Backend.Helpers;
using InteraCoop.Backend.Repositories.Interfaces;
using InteraCoop.Backend.UnitsOfWork.Implementations;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace InteraCoop.Backend.Repositories.Implementations
{
    public class OpportunitiesReportRepository : GenericRepository<ReportDto>, IOpportunitiesReportRepository
    {
        private readonly DataContext _context;
    

        public OpportunitiesReportRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ActionResponse<IEnumerable<ReportDto>>> GetOpportunitiesReportAsync(PaginationDTO pagination)
        {
            var queryable = _context.Opportunities.Include(o => o.Interaction).ThenInclude(i => i.User).AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Status.ToLower().Contains(pagination.Filter.ToLower()));
            }

            var reportsQuery = queryable
                .GroupBy(x => new { x.Interaction.User.UserName, FullName = x.Interaction.User.FirstName + " " + x.Interaction.User.LastName, x.Status })
                .Select(g => new ReportDto
                {
                    UserName = g.Key.UserName,
                    FullName = g.Key.FullName,
                    Type = g.Key.Status,
                    TypeCount = g.Count()
                });

            var paginatedReports = await reportsQuery
                .OrderBy(r => r.UserName).ThenBy(r => r.Type)
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

            var queryable = _context.Opportunities.Include(o => o.Interaction).ThenInclude(i => i.User).AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Status.ToLower().Contains(pagination.Filter.ToLower()));
            }

            var reportsQuery = queryable
                .GroupBy(x => new { x.Interaction.User.UserName, FullName = x.Interaction.User.FirstName + " " + x.Interaction.User.LastName, x.Status })
                .Select(g => new ReportDto
                {
                    UserName = g.Key.UserName,
                    FullName = g.Key.FullName,
                    Type = g.Key.Status,
                    TypeCount = g.Count()
                });

            var totalReports = await reportsQuery.CountAsync();

            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = totalReports
            };

        }
    }
}
