using InteraCoop.Backend.UnitsOfWork.Interfaces;
using InteraCoop.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InteraCoop.Backend.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : GenericController<City>
    {
        public CitiesController(IGenericUnitOfWork<City> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
