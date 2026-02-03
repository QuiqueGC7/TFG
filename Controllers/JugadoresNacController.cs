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
   public class JugadoresNacController : ControllerBase
   {
    private static List<JugadoresNac> JugadoresNacs = new List<JugadoresNac>();

    private readonly IAuthService _authorizationService;

    private readonly IJugadoresNacRepository _repository;

    public JugadoresNacController(IJugadoresNacRepository repository, IAuthService authorizationService)
        {
            _repository = repository;
            _authorizationService = authorizationService;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<JugadoresNac>>> GetJugadores()
        {
          //  var idUser = 1;//Convert.ToInt32(bankAccountQueryParameters.Number);
          //  if (!_authorizationService.HasAccessToResource(idUser, HttpContext.User))
          //  {return Forbid(); }

            var jugadoresNacs = await _repository.GetAllAsync();
            return Ok(jugadoresNacs);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<JugadoresNac>> GetJugadores(int id)
        {
            var jugadoresNac = await _repository.GetByIdAsync(id);
            if (jugadoresNac == null)
            {
                return NotFound();
            }
            return Ok(jugadoresNac);
        }

        [HttpPost]
        public async Task<ActionResult<JugadoresNac>> CreateJugadores(JugadoresNac jugadoresNac)
        {
            await _repository.AddAsync(jugadoresNac);
            return CreatedAtAction(nameof(GetJugadores), new { id = jugadoresNac.JugadorNacId }, jugadoresNac);
        }

       [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJugadores(int id, JugadoresNac updatedJugadores)
        {
            var existingJugadores = await _repository.GetByIdAsync(id);
            if (existingJugadores == null)
            {
                return NotFound();
            }

            // Actualizar el bebida existente
            existingJugadores.Nombre = updatedJugadores.Nombre;
            existingJugadores.Dorsal = updatedJugadores.Dorsal;
            existingJugadores.Posicion = updatedJugadores.Posicion;
            existingJugadores.Equipo = updatedJugadores.Equipo;
            existingJugadores.Puntos = updatedJugadores.Puntos;
            existingJugadores.Libres = updatedJugadores.Libres;
            existingJugadores.Valoracion = updatedJugadores.Valoracion;
            existingJugadores.Rebotes = updatedJugadores.Rebotes;
            existingJugadores.Asistencias = updatedJugadores.Asistencias;
            existingJugadores.por2Pts = updatedJugadores.por2Pts;
            existingJugadores.por3Pts = updatedJugadores.por3Pts;

            await _repository.UpdateAsync(existingJugadores);
            return NoContent();
        }

        ///Cambio necesario///
  
       [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]
       public async Task<IActionResult> DeleteJugadores(int id)
       {
           var jugadoresNac = await _repository.GetByIdAsync(id);
           if (jugadoresNac == null)
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