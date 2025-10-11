using Microsoft.AspNetCore.Mvc;
using System.Threading;
using InvoiceEF.Crud.Application.Services.Contracts;
using InvoiceEF.Crud.Domain;


namespace InvoiceEF.Crud.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController(IStudentService studentService) : ControllerBase
    {
        private readonly IStudentService _studentService = studentService;

        // GET: api/<StudentController> (GET ALL)
        [HttpGet]
        public async Task<IEnumerable<StudentTest>> Get(CancellationToken cancellationToken)
        {
            return await _studentService.GetAllAsync(cancellationToken);
        }

        // GET api/<StudentController>/5 (GET BY ID)
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetByIdAsync(id, cancellationToken);
            if (student == null)
                return NotFound();

            return Ok(student);
        }


        // POST api/<StudentController> (CREATE)
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StudentTest student, CancellationToken cancellationToken)
        {
            bool result = await _studentService.AddAsync(student, cancellationToken);

            if (result)
                return StatusCode(201); // 201 Created

            return BadRequest("Failed to create student.");
        }


        // PUT api/<StudentController>/5 (UPDATE)
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] StudentTest student, CancellationToken cancellationToken)
        {
            if (id != student.Id)
                return BadRequest("ID mismatch");

            bool updated = await _studentService.UpdateAsync(student, cancellationToken);

            if (updated)
                return Ok();
            else
                return NotFound();
        }


        // DELETE api/<StudentController>/5 (DELETE)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            bool result = await _studentService.DeleteAsync(id, cancellationToken);
            if (result)
                return Ok();  // 200 OK If successfully deleted
            else
                return NotFound();  // 404 If the student was not found
        }

    }
}
