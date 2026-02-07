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
   public class StaffController : ControllerBase
   {
    private static List<Staff> Staffs = new List<Staff>();

    private readonly IAuthService _authorizationService;

    private readonly IStaffRepository _repository;

    public StaffController(IStaffRepository repository, IAuthService authorizationService)
        {
            _repository = repository;
            _authorizationService = authorizationService;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Staff>>> GetStaff()
        {
          //  var idUser = 1;//Convert.ToInt32(bankAccountQueryParameters.Number);
          //  if (!_authorizationService.HasAccessToResource(idUser, HttpContext.User))
          //  {return Forbid(); }

            var staff = await _repository.GetAllAsync();
            return Ok(staff);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Staff>> GetStaff(int id)
        {
            var staff = await _repository.GetByIdAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            return Ok(staff);
        }

        [HttpPost]
        public async Task<ActionResult<Staff>> CreateStaff(Staff staff)
        {
            await _repository.AddAsync(staff);
            return CreatedAtAction(nameof(GetStaff), new { id = staff.IdStaff }, staff);
        }

       [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStaff(int id, Staff updatedStaff)
        {
            var existingStaff = await _repository.GetByIdAsync(id);
            if (existingStaff == null)
            {
                return NotFound();
            }

            // Actualizar el staff existente
            existingStaff.Nombre = updatedStaff.Nombre;
            existingStaff.Puesto = updatedStaff.Puesto;
            await _repository.UpdateAsync(existingStaff);
            return NoContent();
        }

        ///Cambio necesario///
  
       [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]
       public async Task<IActionResult> DeleteStaff(int id)
       {
           var staff = await _repository.GetByIdAsync(id);
           if (staff == null)
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