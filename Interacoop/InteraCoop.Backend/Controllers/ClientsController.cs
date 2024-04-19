using InteraCoop.Backend.UnitsOfWork.Interfaces;
using InteraCoop.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InteraCoop.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : GenericController<Client>
    {
        public ClientsController(IGenericUnitOfWork<Client> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
