using Cervantes.Arquisoft.Data.Interfaces;
using Cervantes.Arquisoft.Data.Services;
using Cervantes.Arquisoft.View.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cervantes.Arquisoft.View.Controllers
{
    public class InformeController : Controller
    {

        private readonly IinformeRepository informeRepository;
        private readonly IUserService userService;

        public InformeController(IinformeRepository _informeRepository, IUserService _userService)
        {
            informeRepository = _informeRepository;
            userService = _userService;
        }
        #region Read
        public async Task<IActionResult> Index()
        {
            var clientDTO = await clientRepository.GetAll();

            IEnumerable<ClientViewModel> clientViewModels = clientDTO.Select(clientDto => new ClientViewModel
            {
                Id = clientDto.Id,
                Name = clientDto.Name,
                Surname = clientDto.Surname,
                Email = clientDto.Email,
                Phone = clientDto.Phone,
                Address = clientDto.Address,
                City = clientDto.City,
                Country = clientDto.Country,
                ZipCode = clientDto.ZipCode,
                Dni = clientDto.Dni.ToString()
            });

            return View(clientViewModels);
        }
    }
}
