using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polimedica.Interface;
using Polimedica.Models;
using Polimedica.ViewModel;

namespace Polimedica.Controllers
{
    [Authorize]
    public class MarcaController : Controller
    {
        private readonly IMarcaRepository _marcaRepo;

        public MarcaController(IMarcaRepository marcaRepo)
        {
            _marcaRepo = marcaRepo;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Marca> marca = await _marcaRepo.GetAllAsync();
            return View(marca);
        }

        public IActionResult CreateMarca()
        {
            var response = new CreateMarcaViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMarca(CreateMarcaViewModel createMarcaVM)
        {
            var marca = await _marcaRepo.GetById(createMarcaVM.Id);
            if (marca != null)
            {
                TempData["Error"] = "This email address is alreadt in use";
                return View(createMarcaVM);
            }
            else
            {
                var marcaVM = new Marca()
                {
                    NomeMarca = createMarcaVM.NomeMarca,
                    DescricaoMarca = createMarcaVM.DescricaoMarca,
                    LogoImagem = createMarcaVM.LogoImagemMarca
                };
                _marcaRepo.Add(marcaVM);
                return RedirectToAction("Index");
            }
        }
    }
}
