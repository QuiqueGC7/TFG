using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using TFG.Repositories;
using TFG.Services;

namespace TFG.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   [Authorize]
   public class EquipoController : ControllerBase
   {
    private static List<Equipo> Equipos = new List<Equipo>();

    private readonly IAuthService _authorizationService;

    private readonly IEquipoRepository _repository;

    public EquipoController(IEquipoRepository repository, IAuthService authorizationService)
        {
            _repository = repository;
            _authorizationService = authorizationService;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Equipo>>> GetEquipo()
        {
          //  var idUser = 1;//Convert.ToInt32(bankAccountQueryParameters.Number);
          //  if (!_authorizationService.HasAccessToResource(idUser, HttpContext.User))
          //  {return Forbid(); }

            var equipos = await _repository.GetAllAsync();
            return Ok(equipos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Equipo>> GetEquipo(int id)
        {
            var equipo = await _repository.GetByIdAsync(id);
            if (equipo == null)
            {
                return NotFound();
            }
            return Ok(equipo);
        }

        [HttpPost]
        public async Task<ActionResult<Equipo>> CreateEquipo(Equipo equipo)
        {
            await _repository.AddAsync(equipo);
            return CreatedAtAction(nameof(GetEquipo), new { id = equipo.EquipoId }, equipo);
        }

       [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEquipo(int id, Equipo updatedEquipo)
        {
            var existingEquipo = await _repository.GetByIdAsync(id);
            if (existingEquipo == null)
            {
                return NotFound();
            }

            // Actualizar el equipo existente
            existingEquipo.Nombre = updatedEquipo.Nombre;
            existingEquipo.CantidadMiembros = updatedEquipo.CantidadMiembros;
            existingEquipo.FechaFundacion = updatedEquipo.FechaFundacion;
            await _repository.UpdateAsync(existingEquipo);
            return NoContent();
        }

        ///Cambio necesario///
  
       [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]
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