using Cervantes.Arquisoft.Domain.Entities.Common;

namespace Cervantes.Arquisoft.Domain.Entities
{
    public class Client : BaseDomainModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public int Dni { get; set; }

    }
}
