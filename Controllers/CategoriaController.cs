using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polimedica.ViewModel;
using Polimedica.Interface;
using Polimedica.Models;

namespace Polimedica.Controllers
{
    [Authorize]
    public class CategoriaController : Controller
    {
        private readonly ICategoriaRepository _catere;

        public CategoriaController(ICategoriaRepository catere)
        {
            _catere = catere;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Categoria> categoria = await _catere.GetAllAsync();
            return View(categoria);
        }

        public IActionResult CreateCategoria()
        {
            var response = new CreateCategoriaViewModel();
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategoria(CreateCategoriaViewModel createCategoriaVM)
        {
            var categoria = await _catere.GetById(createCategoriaVM.Id);
            if (categoria != null)
            {
                TempData["Error"] = "This email address is alreadt in use";
                return View(createCategoriaVM);
            }
            else
            {
                var categoriaVm = new Categoria()
                {
                    NomeCategoria = createCategoriaVM.Nome,
                    DescricaoCategoria = createCategoriaVM.Descricao
                };
                _catere.Add(categoriaVm);
                return RedirectToAction("Index");
            }

        }
    }
}
