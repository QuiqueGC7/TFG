using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using TFG.Models;
using TFG.Repositories;
using TFG.Services;

namespace TFG.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class EquipoController : ControllerBase
   {
    private static List<Equipos> Equipos = new List<Equipos>();

    private readonly IAuthService _authorizationService;

    private readonly IEquipoRepository _repository;

    public EquipoController(IEquipoRepository repository, IAuthService authorizationService)
        {
            _repository = repository;
            _authorizationService = authorizationService;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Equipos>>> GetEquipo()
        {
          //  var idUser = 1;//Convert.ToInt32(bankAccountQueryParameters.Number);
          //  if (!_authorizationService.HasAccessToResource(idUser, HttpContext.User))
          //  {return Forbid(); }

            var equipos = await _repository.GetAllAsync();
            return Ok(equipos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Equipos>> GetEquipo(int id)
        {
            var equipo = await _repository.GetByIdAsync(id);
            if (equipo == null)
            {
                return NotFound();
            }
            return Ok(equipo);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Equipos>> CreateEquipo(Equipos equipo)
        {
            await _repository.AddAsync(equipo);
            return CreatedAtAction(nameof(GetEquipo), new { id = equipo.IdEquipo }, equipo);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEquipo(int id, Equipos updatedEquipo)
        {
            var existingEquipo = await _repository.GetByIdAsync(id);
            if (existingEquipo == null)
            {
                return NotFound();
            }

            // Actualizar el equipo existente
            existingEquipo.Nombre = updatedEquipo.Nombre;
            existingEquipo.Victorias = updatedEquipo.Victorias;
            existingEquipo.Derrotas = updatedEquipo.Derrotas;
            await _repository.UpdateAsync(existingEquipo);
            return NoContent();
        }


  
       [Authorize]
       [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteEquipo(int id)
       {
           var equipo = await _repository.GetByIdAsync(id);
           if (equipo == null)
           {
               return NotFound();
           }
           await _repository.DeleteAsync(id);
           return NoContent();
       }

        [HttpPost("inicializar")]
        public async Task<IActionResult> InicializarDatos()
        {
            await _repository.InicializarDatosAsync();
            return Ok("Datos inicializados correctamente.");
        }

   }
}