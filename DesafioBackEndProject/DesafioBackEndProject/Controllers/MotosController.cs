using DesafioBackEndProject.Application.DTOs;
using DesafioBackEndProject.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesafioBackEndProject.Controllers
{
    [ApiController]
    [Route("motos")]
    public class MotosController : ControllerBase
    {
        private readonly IMotorcycleService _motorcycleService;

        public MotosController(IMotorcycleService motoService)
        {
            _motorcycleService = motoService;
        }

        /// <summary>
        /// Consulta todas as motos existentes.
        /// </summary>
        /// <returns>Uma lista de todas as motos cadastradas.</returns>
        [HttpGet]
        public async Task<IActionResult> GetMotos()
        {
            var motos = await _motorcycleService.GetAllAsync();
            return Ok(motos);
        }
        /// <summary>
        /// Consulta uma moto existente pelo ID.
        /// </summary>
        /// <param name="id">O ID da moto a ser consultada.</param>
        /// <returns>A moto correspondente ao ID.</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMotoById(int id)
        {
            var moto = await _motorcycleService.GetByIdAsync(id);
            if (moto is null)
                return NotFound();
            return Ok(moto);
        }
        /// <summary>
        /// Consulta uma moto existente pela placa.
        /// </summary>
        /// <param name="plate">A placa da moto a ser consultada.</param>
        /// <returns>A moto correspondente a placa.</returns>
        [HttpGet("{placa}")]
        public async Task<IActionResult> GetMotoByPlate(string placa)
        {
            var moto = await _motorcycleService.GetByPlateAsync(placa);
            if (moto is null)
                return NotFound();
            return Ok(moto);
        }

        /// <summary>
        /// Cadastra uma nova moto.
        /// </summary>
        /// <param name="motoDto">Os dados da moto a ser cadastrada.</param>
        /// <returns>O ID da moto cadastrada.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateMoto([FromBody] MotorcycleCreateDto motoDto)
        {
            if (motoDto == null)
                return BadRequest("Dados da moto não fornecidos.");

            var id = await _motorcycleService.AddAsync(motoDto);

            if (id == 0)
                return BadRequest("Moto já cadastrada.");

            return CreatedAtAction(nameof(GetMotoById), new { id }, motoDto);
        }

        /// <summary>
        /// Modifica a placa de uma moto existente.
        /// </summary>
        /// <param name="id">O ID da moto a ser atualizada.</param>
        /// <param name="newPlate">A nova placa da moto.</param>
        /// <returns>Status da operação.</returns>
        [HttpPut("{id}/placa")]
        public async Task<IActionResult> UpdatePlate(int id, [FromBody] string newPlate)
        {
            if (string.IsNullOrWhiteSpace(newPlate))
                return BadRequest("Nova placa não fornecida.");

            await _motorcycleService.UpdatePlateAsync(id, newPlate);
            return NoContent();
        }

        /// <summary>
        /// Remove uma moto existente pelo ID.
        /// </summary>
        /// <param name="id">O ID da moto a ser removida.</param>
        /// <returns>Status da operação.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMoto(int id)
        {
            var moto = await _motorcycleService.GetByIdAsync(id);
            if (moto == null)
                return NotFound();

            await _motorcycleService.DeleteAsync(id);
            return NoContent();
        }
    }
}
