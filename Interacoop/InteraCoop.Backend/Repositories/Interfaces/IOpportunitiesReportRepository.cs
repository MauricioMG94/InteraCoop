using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Responses;

namespace InteraCoop.Backend.Repositories.Interfaces
{
    public interface IOpportunitiesReportRepository
    {
        Task<ActionResponse<IEnumerable<ReportDto>>> GetOpportunitiesReportAsync(PaginationDTO paginationDTO);
        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);
    }
}
