using DesafioBackEndProject.Application.DTOs;
using DesafioBackEndProject.Application.Services;
using DesafioBackEndProject.Domain.Common;
using DesafioBackEndProject.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioBackEndProject.Controllers
{
    [ApiController]
    [Authorize(Policy = "DriverPolicy")]
    [Route("locacao")]
    public class RentalController : ControllerBase
    {
        private readonly IRentalService _rentalService;
        private readonly NotificationHandler _notificationHandler;
        public RentalController(IRentalService rentalService, NotificationHandler notificationHandler)
        {
            _rentalService = rentalService;
            _notificationHandler = notificationHandler;
        }

        /// <summary>
        /// Consulta uma locação existente pelo ID.
        /// </summary>
        /// <param name="id">O ID da locação a ser consultada.</param>
        /// <returns>A locação correspondente ao ID.</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RentalReadDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRentalById(int id)
        {
            var rental = await _rentalService.GetByIdAsync(id).ConfigureAwait(false);
            if (rental is null)
                return NotFound();
            return Ok(rental);
        }


        /// <summary>
        /// Cadastra uma nova locação.
        /// </summary>
        /// <param name="rentalDto">Os dados da locação a ser cadastrada.</param>
        /// <returns>O ID da locação cadastrada.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateRental([FromBody] RentalCreateDto rentalDto)
        {
            if (rentalDto == null)
                return BadRequest("Dados da moto não fornecidos.");

            var id = await _rentalService.AddAsync(rentalDto).ConfigureAwait(false);


            if (!_notificationHandler.HasNotifications())
            {
                return Created("Sucesso", id);
            }

            var notifications = _notificationHandler.GetNotifications();
            return BadRequest(notifications);
        }

        /// <summary>
        /// Calcula o preço de devolução de uma locação.
        /// </summary>
        /// <param name="id">O ID da locação.</param>
        /// <param name="dataDevolucao">A data de devolução.</param>
        /// <returns>O preço calculado para a devolução.</returns>
        [HttpPut("{id:int}/devolucao")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(decimal))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CalculateRentalReturnPrice([FromRoute] int id, [FromBody] DateTime dataDevoluacao)
        {
            
            var rentalPrice = await _rentalService.CalculateRentalReturnPrice(id, dataDevoluacao);
            if (rentalPrice is null)
                return NotFound();

            return Ok(rentalPrice);
           
        }

    }
}
