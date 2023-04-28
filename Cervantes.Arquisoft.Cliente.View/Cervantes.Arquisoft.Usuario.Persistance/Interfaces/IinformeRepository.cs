using Cervantes.Arquisoft.Application.DTOs;

namespace Cervantes.Arquisoft.Data.Interfaces
{
    public interface IinformeRepository
    {
        Task Create(InformeDTO informeDTO);
        Task<IEnumerable<InformeDTO>> GetAll();
    }
}