using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InvoiceEF.Crud.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("exception")]
        public IActionResult ThrowException()
        {
            throw new Exception("¡Excepción de prueba!");
        }

        /*
        [HttpGet("notfound")]
        public IActionResult NotFoundTest()
        {
            throw new NotFoundException("Cliente no encontrado");
        }

        [HttpGet("validation")]
        public IActionResult ValidationTest()
        {
            throw new ValidationException(new List<Error> { new(1001, "Campo requerido") });
        }
        */
    }
}
