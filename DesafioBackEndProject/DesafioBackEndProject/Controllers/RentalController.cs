using DesafioBackEndProject.Application.DTOs;
using DesafioBackEndProject.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesafioBackEndProject.Controllers
{
    [ApiController]
    [Route("locacao")]
    public class RentalController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }
        /// <summary>
        /// Consulta uma moto existente pelo ID.
        /// </summary>
        /// <param name="id">O ID da moto a ser consultada.</param>
        /// <returns>A moto correspondente ao ID.</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRentalById(int id)
        {
            var rental = await _rentalService.GetByIdAsync(id).ConfigureAwait(false);
            if (rental is null)
                return NotFound();
            return Ok(rental);
        }
        

        /// <summary>
        /// Cadastra uma nova moto.
        /// </summary>
        /// <param name="motoDto">Os dados da moto a ser cadastrada.</param>
        /// <returns>O ID da moto cadastrada.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateRental([FromBody] RentalCreateDto rentalDto)
        {
            if (rentalDto == null)
                return BadRequest("Dados da moto não fornecidos.");

            var id = await _rentalService.AddAsync(rentalDto);

            if (id == 0)
                return BadRequest("Moto já cadastrada.");
            return Created("Sucesso", id);
           // return CreatedAtAction(nameof(GetMotoById), new { id }, motoDto);
        }

        /// <summary>
        /// Cadastra uma nova moto.
        /// </summary>
        /// <param name="motoDto">Os dados da moto a ser cadastrada.</param>
        /// <returns>O ID da moto cadastrada.</returns>
        [HttpPut("{id:int}/devolucao")]
        public async Task<IActionResult> CalculateRentalReturnPrice([FromRoute] int id,[FromBody]DateTime dataDevoluacao)
        {
            //if (rentalDto == null)
            //    return BadRequest("Dados da moto não fornecidos.");

            var rentalPrice = await _rentalService.CalculateRentalReturnPrice(id, dataDevoluacao);

            //if (id == 0)
            //    return BadRequest("Moto já cadastrada.");
            return Ok(rentalPrice);
            // return CreatedAtAction(nameof(GetMotoById), new { id }, motoDto);
        }

    }
}
