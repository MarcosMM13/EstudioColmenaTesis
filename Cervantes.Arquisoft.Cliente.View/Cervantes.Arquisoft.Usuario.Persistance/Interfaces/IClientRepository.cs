using Cervantes.Arquisoft.Application.DTOs;

namespace Cervantes.Arquisoft.Data.Interfaces
{
    public interface IClientRepository
    {
        Task Create(ClientDTO clientDTO);
        Task Delete(int dni);
        Task<bool> Exist(int dni);
        Task<IEnumerable<ClientDTO>> GetAll();
        Task<ClientDTO> GetById(int dni);
        Task Update(ClientDTO clientDTO);
    }
}