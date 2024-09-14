using DesafioBackEndProject.Domain.Entities;

namespace DesafioBackEndProject.Application.Interfaces
{
    public interface IMotorcycleRepository
    {
        /// <summary>
        /// Retorna todas as motos cadastradas.
        /// </summary>
        /// <returns>Uma lista de todas as motos.</returns>
        Task<IEnumerable<Motorcycle>> GetAllAsync();

        /// <summary>
        /// Retorna uma moto específica pelo ID.
        /// </summary>
        /// <param name="id">O ID da moto.</param>
        /// <returns>A moto correspondente ou uma nova instância se não for encontrada.</returns>
        Task<Motorcycle?> GetByIdAsync(int id);

        /// <summary>
        /// Retorna uma moto específica pela placa do veículo.
        /// </summary>
        /// <param name="plate">A placa da moto.</param>
        /// <returns>A moto correspondente.</returns>
        Task<Motorcycle?> GetByPlateAsync(string plate);

        /// <summary>
        /// Adiciona uma nova moto ao banco de dados.
        /// </summary>
        /// <param name="moto">A moto a ser adicionada.</param>
        /// <returns>O ID da moto adicionada.</returns>
        Task<int> AddAsync(Motorcycle moto);

        /// <summary>
        /// Atualiza a placa de uma moto existente.
        /// </summary>
        /// <param name="id">O ID da moto.</param>
        /// <param name="newPlate">A nova placa a ser atribuída.</param>
        /// <returns>Tarefa assíncrona.</returns>
        Task UpdatePlateAsync(int id, string newPlate);

        /// <summary>
        /// Remove uma moto existente pelo ID.
        /// </summary>
        /// <param name="id">O ID da moto a ser removida.</param>
        /// <returns>Tarefa assíncrona.</returns>
        Task DeleteAsync(int id);

    }
}
