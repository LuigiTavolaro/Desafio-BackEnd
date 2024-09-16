using DesafioBackEndProject.Application.DTOs;
using DesafioBackEndProject.Application.Services;
using DesafioBackEndProject.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioBackEndProject.Controllers
{
    [ApiController]
    [Authorize(Policy = "DriverPolicy")]
    [Route("entregadores")]
    public class DriversController : ControllerBase
    {
        private readonly IDriverService _driverService;
        private readonly NotificationHandler _notificationHandler;

        public DriversController(IDriverService driverService, NotificationHandler notificationHandler)
        {
            _driverService = driverService;
            _notificationHandler = notificationHandler;
        }


        /// <summary>
        /// Cadastra um novo entregador.
        /// </summary>
        /// <param name="driverDto">Os dados do entregador a ser cadastrada.</param>
        /// <returns>O ID cadastrado.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DriverCreateDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateDriver([FromBody] DriverCreateDto driverDto)
        {
            if (driverDto == null)
                return BadRequest("Dados do entregador não fornecidos.");

            var id = await _driverService.AddAsync(driverDto);

            if (id == null)
                return BadRequest("Entregador já cadastrado.");

            return Created("Sucesso",null);

        }

        /// <summary>
        /// Modifica a foto da carteira de motorista.
        /// </summary>
        /// <param name="id">O ID do entregador.</param>
        /// <param name="newCnh">A nova imagem em formato bit64 da moto.</param>
        /// <returns>Status da operação.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCnhPicture(int id, [FromBody] string newCnh)
        {
            if (string.IsNullOrWhiteSpace(newCnh))
                return BadRequest("Nova CNH não fornecida.");

            await _driverService.UpdateCnhPictureAsync(id, newCnh);
            return Accepted();
        }
    }
}
