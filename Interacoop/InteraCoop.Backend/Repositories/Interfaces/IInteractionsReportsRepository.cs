using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Responses;

namespace InteraCoop.Backend.Repositories.Interfaces
{
    public interface IInteractionsReportsRepository
    {
        Task<ActionResponse<IEnumerable<ReportDto>>> GetInteractionsReportAsync(PaginationDTO paginationDTO);
        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

    }
}
