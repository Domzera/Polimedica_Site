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

        public ProdutoController(
            IProdutoRepository produtoRepository,
            ICategoriaRepository categoriaRepository,
            IMarcaRepository marcaRepository)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
            _marcaRepository = marcaRepository;
        }
        public async Task<IActionResult> IndexAsync()
        {
            IEnumerable<Produto> produtos = await _produtoRepository.GetAll();
            return View(produtos);
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

                    //MarcaId = 1,
                    //CategoriaId = 1
                };
                
                _produtoRepository.Add(produto);
                
                //foreach (var marcaId in produtoVM.Marca)
                //{
                //    var prodMar = new ProductMarca
                //    {
                //        //MarcaId = int.Parse(produtoVM.Marca)
                //    };
                //}
                //foreach (var categoriaId in produtoVM.Categoria)
                //{
                //    var prodCat = new ProductCategoria
                //    {
                //        //MarcaId = int.Parse(produtoVM.Marca)
                //    };
                //}
                return View("Index");
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
