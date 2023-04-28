using Cervantes.Arquisoft.Application.DTOs;
using Cervantes.Arquisoft.Data.Interfaces;
using Cervantes.Arquisoft.Data.Services;
using Cervantes.Arquisoft.View.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cervantes.Arquisoft.View.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientRepository clientRepository;
        private readonly IUserService userService;

        public ClientController(IClientRepository _clientRepository, IUserService _userService)
        {
            clientRepository = _clientRepository;
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

        #endregion
        #region Create

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClientViewModel clientViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(clientViewModel);
            }

            var UserID = userService.GetUserId();

            var clientDTO = new ClientDTO
            {
                Id = clientViewModel.Id,
                Name = clientViewModel.Name,
                Surname = clientViewModel.Surname,
                Email = clientViewModel.Email,
                Phone = clientViewModel.Phone,
                Address = clientViewModel.Address,
                City = clientViewModel.City,
                Country = clientViewModel.Country,
                ZipCode = clientViewModel.ZipCode,
                Dni = int.Parse(clientViewModel.Dni)
            };
            var AlreadyExist = await clientRepository.Exist(clientDTO.Dni);

            if (AlreadyExist)
            {
                ModelState.AddModelError(nameof(clientDTO.Dni), $"El DNI {clientDTO.Dni} ya existe.");
                return View(clientViewModel);
            }

            await clientRepository.Create(clientDTO);
            return RedirectToAction("Index");
        }

        #endregion
        #region Edit

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var UserID = userService.GetUserId();

            var clientDTO = await clientRepository.GetById(id);

            var clientViewModel = new ClientViewModel
            {
                Id = clientDTO.Id,
                Name = clientDTO.Name,
                Surname = clientDTO.Surname,
                Email = clientDTO.Email,
                Phone = clientDTO.Phone,
                Address = clientDTO.Address,
                City = clientDTO.City,
                Country = clientDTO.Country,
                ZipCode = clientDTO.ZipCode,
                Dni = clientDTO.Dni.ToString(),
            };

            if (clientViewModel is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(clientViewModel);

        }

        [HttpPost]
        public async Task<ActionResult> Edit(ClientViewModel clientViewModel)
        {
            var UserID = userService.GetUserId();

            var clientDTO = new ClientDTO
            {
                Id = clientViewModel.Id,
                Name = clientViewModel.Name,
                Surname = clientViewModel.Surname,
                Email = clientViewModel.Email,
                Phone = clientViewModel.Phone,
                Address = clientViewModel.Address,
                City = clientViewModel.City,
                Country = clientViewModel.Country,
                ZipCode = clientViewModel.ZipCode,
                Dni = int.Parse(clientViewModel.Dni),
                Password = clientViewModel.Dni
            };

            var clienteExiste = await clientRepository.Exist(clientViewModel.Id);

            if (clienteExiste)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await clientRepository.Update(clientDTO);
            return RedirectToAction("Index");
        }

        #endregion
        #region Delete

        public async Task<ActionResult> Delete(int id)
        {
            var usuarioID = userService.GetUserId();
            var clientDTO = await clientRepository.GetById(id);

            if (clientDTO == null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            var clientViewModel = new ClientViewModel
            {
                Id = clientDTO.Id,
                Name = clientDTO.Name,
                Surname = clientDTO.Surname,
                Email = clientDTO.Email,
                Phone = clientDTO.Phone,
                Address = clientDTO.Address,
                City = clientDTO.City,
                Country = clientDTO.Country,
                ZipCode = clientDTO.ZipCode,
                Dni = clientDTO.Dni.ToString(),
            };

            return View(clientViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteId(int id)
        {
            var usuarioID = userService.GetUserId();

            var clientDTO = await clientRepository.GetById(id);

            if (clientDTO == null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            await clientRepository.Delete(id);
            return RedirectToAction("Index");
        }

        #endregion

        public IActionResult NotFound()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AlreadyExistCheck(int dni)
        {
            var alreadyExistCheck = await clientRepository.Exist(dni);

            if (alreadyExistCheck)
            {
                return Json($"El DNI {dni} ya existe");
            }

            return Json(true);
        }



    }
}
