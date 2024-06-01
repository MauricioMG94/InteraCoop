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

        public ReportsController(IGenericUnitOfWork<ReportDto> unitOfWork) : base(unitOfWork)
        {
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReport(PaginationDTO pagination, int id)
        {
            var action = await _reportsRepository.GetInteractionsReportAsync(pagination, id);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest(action);
        }
    }
}
