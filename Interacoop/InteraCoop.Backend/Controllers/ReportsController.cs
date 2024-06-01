using InteraCoop.Backend.Repositories.Implementations;
using InteraCoop.Backend.Repositories.Interfaces;
using InteraCoop.Backend.UnitsOfWork.Implementations;
using InteraCoop.Backend.UnitsOfWork.Interfaces;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InteraCoop.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : GenericController<ReportDto>
    {
        private readonly IReportsRepository _reportsRepository;

        public ReportsController(IGenericUnitOfWork<ReportDto> unitOfWork, IReportsRepository reportsRepository) : base(unitOfWork)
        {
            _reportsRepository = reportsRepository;
        }

        [HttpGet("interactionsReports")]   
        public async Task<IActionResult> GetInteractionsReport([FromQuery] PaginationDTO pagination)
        {
            var action = await _reportsRepository.GetInteractionsReportAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest(action);
        }

        [HttpGet("opportunitiesReports")]
        public async Task<IActionResult> GetOpportunitiesReport([FromQuery] PaginationDTO pagination)
        {
            var action = await _reportsRepository.GetOpportunitiesReportAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest(action);
        }
    }
}
