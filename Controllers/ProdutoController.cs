using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Polimedica.Interface;
using Polimedica.Models;
using Polimedica.ViewModel;

namespace Polimedica.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMarcaRepository _marcaRepository;
        private readonly IMarcaProdutoRepository _marcaProdutoRepository;

        public ProdutoController(
            IProdutoRepository produtoRepository,
            ICategoriaRepository categoriaRepository,
            IMarcaRepository marcaRepository,
            IMarcaProdutoRepository marcaProdutoRepository)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
            _marcaRepository = marcaRepository;
            _marcaProdutoRepository = marcaProdutoRepository;
        }

        // Começa aqui
        public async Task<IActionResult> IndexAsync()
        {
            IEnumerable<Produto> produtos = await _produtoRepository.GetAll();
            return View(produtos);
        }
        public async Task<IActionResult> Detalhe(int id)
        {
            if (id != 0)
            {
                Produto produto = await _produtoRepository.GetById(id);
                return View(new ProdutoDetalheViewModel());
            }
            return NotFound();
        }

        [Authorize]
        public async Task<IActionResult> CreateProdutoAsync()
        {
            List<Categoria> categoriaL = new List<Categoria>();
            List<Marca> marcaL = new List<Marca>();

            var resultMar = await _marcaRepository.GetAllAsync();
            if (resultMar != null) {
                foreach (var item in resultMar) {
                    marcaL.Add(item);
                }
                ViewBag.marcaVb = marcaL;
            }

            var resultCat = await _categoriaRepository.GetAllAsync();
            if (!resultCat.IsNullOrEmpty())
            {
                foreach (var item in resultCat)
                {
                    categoriaL.Add(item);
                }
                ViewBag.categoriaVb = categoriaL;
            }
            return View(new CreateProdutoViewModel());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateProduto(CreateProdutoViewModel produtoVM)
        {
            //Carregando as fotos
            if (ModelState.IsValid)
            {
                var produto = new Produto
                {
                    NomeProduto = produtoVM.NomeProduto,
                    DescricaoProduto = produtoVM.DescricaoProduto,
                    Preco = (long)produtoVM.Preco,
                    Imagem1 = produtoVM.Imagem1,
                    Imagem2 = produtoVM.Imagem2,
                    Imagem3 = produtoVM.Imagem3,
                    Imagem4 = produtoVM.Imagem4,
                    Imagem5 = produtoVM.Imagem5,
                    Ativo = produtoVM.Ativo,
                    //MarcaId = produtoVM.MarcaId,
                    //CategoriaId = produtoVM.CategoriaId,
                    DataAdicionado = DateOnly.FromDateTime(DateTime.Now),
                };
                _produtoRepository.Add(produto);

                int i=0;
                while (i < produtoVM.MarcaId.Length)
                {
                    
                    if (produtoVM.MarcaId[i].ToString() != "" && produtoVM.MarcaId[i] != null)
                    {
                        var marcaId = new MarcaProduto
                        {
                            MarcaId = produtoVM.MarcaId[i],
                            ProdutoId = produto.Id
                        };

                        _marcaProdutoRepository.Add(marcaId);
                        i++;
                    }
                    else
                    {
                        ModelState.AddModelError("MarcaId", "Marca não pode ser nulo ou zero.");
                        return View(produtoVM); // Retorna a view com o erro
                    }
                }

                int y = 0;
                while (y < produtoVM.CategoriaId.Length)
                {

                    if (produtoVM.CategoriaId[y].ToString() != "" && produtoVM.CategoriaId[y] != null)
                    {
                        var categoriaId = new CategoriaProduto
                        {
                            CategoriaId = produtoVM.CategoriaId[y],
                            ProdutoId = produto.Id
                        };
                        y++;
                    }
                    else
                    {
                        ModelState.AddModelError("CategoriaId", "Categoria não pode ser nulo ou zero.");
                        return View(produtoVM); // Retorna a view com o erro
                    }
                }

                return RedirectToAction("Index", "Produto"); //View();
            }
            else
            {
                List<Categoria> categoriaL = new List<Categoria>();
                List<Marca> marcaL = new List<Marca>();

                var resultMar = await _marcaRepository.GetAllAsync();
                if (resultMar != null)
                {
                    foreach (var item in resultMar)
                    {
                        marcaL.Add(item);
                    }
                    ViewBag.marcaVb = marcaL;
                }

                var resultCat = await _categoriaRepository.GetAllAsync();
                if (!resultCat.IsNullOrEmpty())
                {
                    foreach (var item in resultCat)
                    {
                        categoriaL.Add(item);
                    }
                    ViewBag.categoriaVb = categoriaL;
                }
                return View(new CreateProdutoViewModel());
            }
        }
    }
}
