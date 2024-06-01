﻿using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Responses;

namespace InteraCoop.Backend.Repositories.Interfaces
{
    public interface IReportsRepository
    {
        Task<ActionResponse<IEnumerable<ReportDto>>> GetInteractionsReportAsync(PaginationDTO paginationDTO, int id);
    }
}
