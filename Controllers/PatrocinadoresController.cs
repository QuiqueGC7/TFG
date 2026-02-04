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
   public class PatrocinadoresController : ControllerBase
   {
    private static List<Patrocinadores> Patrocinadoress = new List<Patrocinadores>();

    private readonly IAuthService _authorizationService;

    private readonly IPatrocinadoresRepository _repository;

    public PatrocinadoresController(IPatrocinadoresRepository repository, IAuthService authorizationService)
        {
            _repository = repository;
            _authorizationService = authorizationService;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Patrocinadores>>> GetPatrocinador()
        {
          //  var idUser = 1;//Convert.ToInt32(bankAccountQueryParameters.Number);
          //  if (!_authorizationService.HasAccessToResource(idUser, HttpContext.User))
          //  {return Forbid(); }

            var patrocinadores = await _repository.GetAllAsync();
            return Ok(patrocinadores);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Patrocinadores>> GetPatrocinador(int id)
        {
            var patrocinadores = await _repository.GetByIdAsync(id);
            if (patrocinadores == null)
            {
                return NotFound();
            }
            return Ok(patrocinadores);
        }

        [HttpPost]
        public async Task<ActionResult<Patrocinadores>> CreatePatrocinador(Patrocinadores patrocinadores)
        {
            await _repository.AddAsync(patrocinadores);
            return CreatedAtAction(nameof(GetPatrocinador), new { id = patrocinadores.PatrocinadorId }, patrocinadores);
        }

       [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatrocinador(int id, Patrocinadores updatedPatrocinadores)
        {
            var existingPatrocinadores = await _repository.GetByIdAsync(id);
            if (existingPatrocinadores == null)
            {
                return NotFound();
            }

            // Actualizar el bebida existente
            existingPatrocinadores.Nombre = updatedPatrocinadores.Nombre;
            existingPatrocinadores.CantidadAportada = updatedPatrocinadores.CantidadAportada;
            existingPatrocinadores.Email = updatedPatrocinadores.Email;
            existingPatrocinadores.Telefono = updatedPatrocinadores.Telefono;
            await _repository.UpdateAsync(existingPatrocinadores);
            return NoContent();
        }

        ///Cambio necesario///
  
       [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]
       public async Task<IActionResult> DeletePatrocinadores(int id)
       {
           var patrocinadores = await _repository.GetByIdAsync(id);
           if (patrocinadores == null)
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