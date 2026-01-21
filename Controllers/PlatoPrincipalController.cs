using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.Services;

namespace RestauranteAPI.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class PlatoPrincipalController : ControllerBase
   {
    private static List<PlatoPrincipal> platos = new List<PlatoPrincipal>();

    private readonly IPlatoPrincipalService _platoPrincipalService;

    public PlatoPrincipalController(IPlatoPrincipalService platoPrincipalService)
        {
            _platoPrincipalService = platoPrincipalService;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<PlatoPrincipal>>> GetPlatos()
        {
            var platos = await _platoPrincipalService.GetAllAsync();
            return Ok(platos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PlatoPrincipal>> GetPlato(int id)
        {
            var plato = await _platoPrincipalService.GetByIdAsync(id);
            if (plato == null)
            {
                return NotFound();
            }
            return Ok(plato);
        }

        [HttpPost]
        public async Task<ActionResult<PlatoPrincipal>> CreatePlato(PlatoPrincipal plato)
        {
            await _platoPrincipalService.AddAsync(plato);
            return CreatedAtAction(nameof(GetPlato), new { id = plato.Id }, plato);
        }

       [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlato(int id, PlatoPrincipal updatedPlato)
        {
            var existingPlato = await _platoPrincipalService.GetByIdAsync(id);
            if (existingPlato == null)
            {
                return NotFound();
            }

            // Actualizar el plato existente
            existingPlato.Nombre = updatedPlato.Nombre;
            existingPlato.Precio = updatedPlato.Precio;
            existingPlato.Ingredientes = updatedPlato.Ingredientes;

            await _platoPrincipalService.UpdateAsync(existingPlato);
            return NoContent();
        }
  
       [HttpDelete("{id}")]
       [Authorize]
       public async Task<IActionResult> DeletePlato(int id)
       {
           var plato = await _platoPrincipalService.GetByIdAsync(id);
           if (plato == null)
           {
               return NotFound();
           }
           await _platoPrincipalService.DeleteAsync(id);
           return NoContent();
       }

        [HttpPost("inicializar")]
        public async Task<IActionResult> InicializarDatos()
        {
            await _platoPrincipalService.InicializarDatosAsync();
            return Ok("Datos inicializados correctamente.");
        }

   }
}