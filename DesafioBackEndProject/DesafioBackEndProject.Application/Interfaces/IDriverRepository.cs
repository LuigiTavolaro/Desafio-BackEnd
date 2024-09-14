using DesafioBackEndProject.Domain.Entities;

namespace DesafioBackEndProject.Application.Interfaces
{
    public interface IDriverRepository
    {
        /// <summary>
        /// Adiciona uma nova moto ao banco de dados.
        /// </summary>
        /// <param name="moto">A moto a ser adicionada.</param>
        /// <returns>O ID da moto adicionada.</returns>
        Task<int> AddAsync(Driver driver);

        /// <summary>
        /// Busca por Chave composta (CNPJ e CNH) 
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        Task<bool> GetByCompositeKey(Driver driver);

        /// <summary>
        /// Atualiza a placa de uma moto existente.
        /// </summary>
        /// <param name="id">O ID da moto.</param>
        /// <param name="newPlate">A nova placa a ser atribuída.</param>
        /// <returns>Tarefa assíncrona.</returns>
        Task UpdateCnhPictureAsync(int id, string newCnh);
    }
}
