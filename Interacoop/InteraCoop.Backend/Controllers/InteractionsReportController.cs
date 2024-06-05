using InteraCoop.Backend.Repositories.Implementations;
using InteraCoop.Backend.Repositories.Interfaces;
using InteraCoop.Backend.UnitsOfWork.Interfaces;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InteraCoop.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InteractionsReportController : GenericController<ReportDto>
    {
        private readonly IInteractionsReportsRepository _reportsRepository;

        public InteractionsReportController(IGenericUnitOfWork<ReportDto> unitOfWork, IInteractionsReportsRepository reportsRepository) : base(unitOfWork)
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

        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _reportsRepository.GetTotalPagesAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

    }
}
