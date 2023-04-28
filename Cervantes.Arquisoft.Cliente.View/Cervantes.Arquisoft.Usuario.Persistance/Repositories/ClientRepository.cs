using Cervantes.Arquisoft.Application.DTOs;
using Cervantes.Arquisoft.Data.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Cervantes.Arquisoft.Data.Repositories
{
    public class ClientRepository : IClientRepository

    {
        private readonly string connectionString;

        public ClientRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task Create(ClientDTO clientDTO)
        {
            using var conn = new SqlConnection(connectionString);
            clientDTO.Password = clientDTO.Dni.ToString();
            var id = await conn.QuerySingleAsync<int>("INSERT INTO Clients (Name, Surname, Email, Phone, Address, City, Country, ZipCode, Dni, Password) VALUES (@Name, @Surname, @Email, @Phone, @Address, @City, @Country, @ZipCode, @Dni, @Password); SELECT SCOPE_IDENTITY();", clientDTO);

            clientDTO.Id = id;
        }

        public async Task<bool> Exist(int dni)
        {
            using var conn = new SqlConnection(connectionString);
            var result = await conn.QueryFirstOrDefaultAsync<int>(@"SELECT 1 FROM Clients WHERE Dni = @Dni", new { dni });
            return result == 1;
        }

        public async Task<IEnumerable<ClientDTO>> GetAll()
        {
            using var conn = new SqlConnection(connectionString);
            return await conn.QueryAsync<ClientDTO>(@"SELECT * FROM Clients");
        }

        public async Task Update(ClientDTO clientDTO)
        {
            using var conn = new SqlConnection(connectionString);
            await conn.ExecuteAsync(@"UPDATE Clients SET Name = @Name, Surname = @Surname, Email = @Email, Phone = @Phone,
                                                         Address = @Address, City = @City, Country = @Country, ZipCode = @ZipCode,
                                                         Dni = @Dni WHERE Id = @Id", clientDTO);
        }

        public async Task Delete(int id)
        {
            using var conn = new SqlConnection(connectionString);
            await conn.ExecuteAsync(@"DELETE FROM Clients WHERE id = @id", new { id });
        }

        public async Task<ClientDTO> GetById(int id)
        {
            using var conn = new SqlConnection(connectionString);
            return await conn.QueryFirstOrDefaultAsync<ClientDTO>(@"SELECT * FROM Clients WHERE id = @id", new { id });
        }


    }
}
