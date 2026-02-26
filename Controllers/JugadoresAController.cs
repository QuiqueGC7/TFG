using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using TFG.Repositories;
using TFG.Services;

namespace TFG.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class JugadoresAController : ControllerBase
   {
    private static List<JugadoresA> JugadoresAs = new List<JugadoresA>();

    private readonly IAuthService _authorizationService;

    private readonly IJugadoresARepository _repository;

    public JugadoresAController(IJugadoresARepository repository, IAuthService authorizationService)
        {
            _repository = repository;
            _authorizationService = authorizationService;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<JugadoresA>>> GetJugadores()
        {
          //  var idUser = 1;//Convert.ToInt32(bankAccountQueryParameters.Number);
          //  if (!_authorizationService.HasAccessToResource(idUser, HttpContext.User))
          //  {return Forbid(); }

            var jugadoresAs = await _repository.GetAllAsync();
            return Ok(jugadoresAs);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<JugadoresA>> GetJugadores(int id)
        {
            var jugadoresA = await _repository.GetByIdAsync(id);
            if (jugadoresA == null)
            {
                return NotFound();
            }
            return Ok(jugadoresA);
        }

        [HttpPost]
        public async Task<ActionResult<JugadoresA>> CreateJugadores(JugadoresA jugadoresA)
        {
            await _repository.AddAsync(jugadoresA);
            return CreatedAtAction(nameof(GetJugadores), new { id = jugadoresA.JugadorAId }, jugadoresA);
        }

       [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJugadores(int id, JugadoresA updatedJugadores)
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
            existingJugadores.PorLibres = updatedJugadores.PorLibres;
            existingJugadores.DosPts = updatedJugadores.DosPts;
            existingJugadores.TresPts = updatedJugadores.TresPts;

            await _repository.UpdateAsync(existingJugadores);
            return NoContent();
        }

        ///Cambio necesario///
  
       [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]
       public async Task<IActionResult> DeleteJugadores(int id)
       {
           var jugadoresA = await _repository.GetByIdAsync(id);
           if (jugadoresA == null)
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