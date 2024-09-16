using DesafioBackEndProject.Application.DTOs;
using DesafioBackEndProject.Application.Services;
using DesafioBackEndProject.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace DesafioBackEndProject.Controllers
{
    [ApiController]
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
        /// Modifica a placa de uma moto existente.
        /// </summary>
        /// <param name="id">O ID da moto a ser atualizada.</param>
        /// <param name="newPlate">A nova placa da moto.</param>
        /// <returns>Status da operação.</returns>
        [HttpPut("{id}/placa")]
        public async Task<IActionResult> UpdatePlate(int id, [FromBody] string newCnh)
        {
            if (string.IsNullOrWhiteSpace(newCnh))
                return BadRequest("Nova CNH não fornecida.");

            await _driverService.UpdateCnhPictureAsync(id, newCnh);
            return NoContent();
        }
    }
}
