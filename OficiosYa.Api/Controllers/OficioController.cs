using Microsoft.AspNetCore.Mvc;
using OficiosYa.Application.Handlers.Oficios;
using OficiosYa.Domain.Entities;

namespace OficiosYa.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OficioController : ControllerBase
    {
        private readonly GetAllOficiosHandler _getAllHandler;
        private readonly CreateOficioHandler _createHandler;
        private readonly UpdateOficioHandler _updateHandler;
        private readonly DeleteOficioHandler _deleteHandler;

        public OficioController(
            GetAllOficiosHandler getAllHandler,
            CreateOficioHandler createHandler,
            UpdateOficioHandler updateHandler,
            DeleteOficioHandler deleteHandler)
        {
            _getAllHandler = getAllHandler;
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _deleteHandler = deleteHandler;
        }

        // GET api/oficio
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _getAllHandler.HandleAsync();
            return Ok(result);
        }

        // POST api/oficio
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOficioRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var oficio = await _createHandler.HandleAsync(request.Nombre, request.Descripcion);

            return CreatedAtAction(nameof(GetAll), new { id = oficio.Id }, oficio);
        }

        // PUT api/oficio/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateOficioRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _updateHandler.HandleAsync(id, request.Nombre, request.Descripcion);

            if (!updated)
                return NotFound(new { message = "Oficio no encontrado" });

            return NoContent();
        }

        // DELETE api/oficio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _deleteHandler.HandleAsync(id);

            if (!deleted)
                return NotFound(new { message = "Oficio no encontrado" });

            return NoContent();
        }
    }

    // -----------------------
    // Request Models
    // -----------------------

    public class CreateOficioRequest
    {
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
    }

    public class UpdateOficioRequest
    {
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
    }
}

