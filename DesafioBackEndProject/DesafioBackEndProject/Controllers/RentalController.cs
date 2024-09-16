using DesafioBackEndProject.Application.DTOs;
using DesafioBackEndProject.Application.Services;
using DesafioBackEndProject.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace DesafioBackEndProject.Controllers
{
    [ApiController]
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

            var id = await _rentalService.AddAsync(rentalDto).ConfigureAwait(false);


            if (!_notificationHandler.HasNotifications())
            {
                return Created("Sucesso", id);
            }

            var notifications = _notificationHandler.GetNotifications();
            return BadRequest(notifications);
        }

        /// <summary>
        /// Cadastra uma nova moto.
        /// </summary>
        /// <param name="motoDto">Os dados da moto a ser cadastrada.</param>
        /// <returns>O ID da moto cadastrada.</returns>
        [HttpPut("{id:int}/devolucao")]
        public async Task<IActionResult> CalculateRentalReturnPrice([FromRoute] int id, [FromBody] DateTime dataDevoluacao)
        {
            
            var rentalPrice = await _rentalService.CalculateRentalReturnPrice(id, dataDevoluacao);


            return Ok(rentalPrice);
           
        }

    }
}
