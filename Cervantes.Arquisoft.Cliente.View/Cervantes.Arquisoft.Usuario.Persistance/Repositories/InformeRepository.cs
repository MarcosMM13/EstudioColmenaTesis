using Cervantes.Arquisoft.Application.DTOs;
using Cervantes.Arquisoft.Data.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Cervantes.Arquisoft.Data.Repositories
{
    public class InformeRepository : IinformeRepository
    {

        private readonly string connectionString;

        public InformeRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<IEnumerable<InformeDTO>> GetAll()
        {
            using var conn = new SqlConnection(connectionString);
            return await conn.QueryAsync<InformeDTO>(@"SELECT * FROM Clients");
        }

        public async Task Create(InformeDTO informeDTO)
        {
            using var conn = new SqlConnection(connectionString);
            var id = await conn.QuerySingleAsync<int>("INSERT INTO Informes (Descripcion) VALUES (@Descripcion); SELECT SCOPE_IDENTITY();", informeDTO);

            informeDTO.Id = id;
        }
    }
}
