using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
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
        private readonly ICategoriaProdutoRepository _categoriaProdutoRepository;
        private readonly IPhotoService _photoService;

        public ProdutoController(
            IProdutoRepository produtoRepository,
            ICategoriaRepository categoriaRepository,
            IMarcaRepository marcaRepository,
            IMarcaProdutoRepository marcaProdutoRepository,
            ICategoriaProdutoRepository categoriaProdutoRepository,
            IPhotoService photoService
            )
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
            _marcaRepository = marcaRepository;
            _marcaProdutoRepository = marcaProdutoRepository;
            _categoriaProdutoRepository = categoriaProdutoRepository;
            _photoService = photoService;
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
                return View(produto);
            }
            return NotFound();
        }

        [Authorize]
        public async Task<IActionResult> CreateProdutoAsync()
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


        /* A função de criar produtos é extensa, então vamos explica-a parte po parte para melhor compreensão e manutenção.
         * Na primeira parte, nós faemos o Upload das imagens. O upload é feito um a um;
         * Na segunda parte, nós criamos o produto e salvamos as Url's das imagens.
         * Na terceira parte nós criamos as relações entre Produto e Marca
         * Na quarta parte nós criamos as relações entre Produto e Categoria
        */
        [Authorize(Roles = "Admin,Gerente,Vendedor")]
        [HttpPost]
        public async Task<IActionResult> CreateProduto(CreateProdutoViewModel produtoVM)
        {

            if (ModelState.IsValid)
            {

                //Primeira parte: Upload das imagens
                var photo1 = produtoVM.Imagem1 != null ? await _photoService.AddPhotoAsync(produtoVM.Imagem1) : null;
                var photo2 = produtoVM.Imagem2 != null ? await _photoService.AddPhotoAsync(produtoVM.Imagem2) : null;
                var photo3 = produtoVM.Imagem3 != null ? await _photoService.AddPhotoAsync(produtoVM.Imagem3) : null;
                var photo4 = produtoVM.Imagem4 != null ? await _photoService.AddPhotoAsync(produtoVM.Imagem4) : null;
                var photo5 = produtoVM.Imagem5 != null ? await _photoService.AddPhotoAsync(produtoVM.Imagem5) : null;

                // Segunda parte: Criação do produto
                var produto = new Produto
                {
                    NomeProduto = produtoVM.NomeProduto,
                    DescricaoProduto = produtoVM.DescricaoProduto,
                    Preco = (long)produtoVM.Preco,
                    Ativo = produtoVM.Ativo,
                    Imagem1 = photo1 != null ? photo1.Url.ToString() : null, //Salva as url's das imagens
                    Imagem2 = photo2 != null ? photo2.Url.ToString() : null,
                    Imagem3 = photo3 != null ? photo3.Url.ToString() : null,
                    Imagem4 = photo4 != null ? photo4.Url.ToString() : null,
                    Imagem5 = photo5 != null ? photo5.Url.ToString() : null,
                    DataAdicionado = DateOnly.FromDateTime(DateTime.Now),
                };
                _produtoRepository.Add(produto); // Salva as alterações no BD para ProdutosBd

                // Terceira parte: Criação das relações entre Produto e Marca
                int i = 0;
                while (i < produtoVM.MarcaId.Length)
                {

                    if (produtoVM.MarcaId[i].ToString() != "" && produtoVM.MarcaId[i] != null)
                    {
                        var marcaId = new MarcaProduto
                        {
                            MarcaId = produtoVM.MarcaId[i],
                            ProdutoId = produto.Id
                        };

                        _marcaProdutoRepository.Add(marcaId); // Salva as alterações no BD para MarcaProdutoBd
                        i++;
                    }
                    else
                    {
                        ModelState.AddModelError("MarcaId", "Marca não pode ser nulo ou zero.");
                        return View(produtoVM); // Retorna a view com o erro
                    }
                }

                // Quarta parte: Criação das relações entre Produto e Categoria
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

                        _categoriaProdutoRepository.Add(categoriaId); // Salva as alterações no BD para CategoriaProdutoBd
                        y++;
                    }
                    else
                    {
                        ModelState.AddModelError("CategoriaId", "Categoria não pode ser nulo ou zero.");
                        return View(produtoVM); // Retorna a view com o erro
                    }
                }

                return RedirectToAction("Index", "Produto");
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

        /* Método que chama a edição do produto e carrega os valores em seus devidos campos
         * Na primeira parte nós carregamos os dados simples do produto e as imagens
         * Na segunda parte nós carregamos as marcas selecionadas para o produto
         * Na terceira parte nós carregamos as categorias selecionadas para o produto
         */
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditarProduto(int id)
        {
            if (id != 0)
            {
                Produto produto = await _produtoRepository.GetById(id); // Carrega o objeto produto pelo ID
                if (produto == null)
                {
                    return NotFound();
                }

                List<EditProdutCategoriaViewModel> categoriaL = new List<EditProdutCategoriaViewModel>(); //Cria a lista de categorias
                List<EditProdutMarcaViewModel> marcaL = new List<EditProdutMarcaViewModel>(); //Cria a lista de marcas

                var resultMarca = await _marcaProdutoRepository.getByProdutoId(id); // Carrega as marcas selecionadas para o produto
                var resultCategoria = await _categoriaProdutoRepository.GetByProdutoId(id); // Carrega as categorias selecionadas para o produto

                //Primeira parte - Carrega os item da marca para poderem ser vistas na pagina
                var ProdutoEdit = new EditProdutoViewModel
                {
                    NomeProduto = produto.NomeProduto,
                    DescricaoProduto = produto.DescricaoProduto,
                    Preco = (long)produto.Preco,
                    Ativo = produto.Ativo,
                    Imagem1 = produto.Imagem1 != null ? new FormFile(new MemoryStream(), 0, 0, null, produto.Imagem1) : null,
                    Imagem2 = produto.Imagem2 != null ? new FormFile(new MemoryStream(), 0, 0, null, produto.Imagem2) : null,
                    Imagem3 = produto.Imagem3 != null ? new FormFile(new MemoryStream(), 0, 0, null, produto.Imagem3) : null,
                    Imagem4 = produto.Imagem4 != null ? new FormFile(new MemoryStream(), 0, 0, null, produto.Imagem4) : null,
                    Imagem5 = produto.Imagem5 != null ? new FormFile(new MemoryStream(), 0, 0, null, produto.Imagem5) : null,
                };

                //Segunda parte - Carrega as marcas selecionadas para o produto
                var resultMar = await _marcaRepository.GetAllAsync();
                if (resultMar.IsNullOrEmpty())
                {
                    foreach (var item in resultMar)
                    {
                        if(resultMarca.Any(m => m.MarcaId == item.Id)) // Verifica se a marca está selecionada
                        {
                            marcaL.Add(new EditProdutMarcaViewModel
                            {
                                MarcaId = item.Id,
                                IsChecked = true // Marca está selecionada
                            });
                        }
                        else // Marca não está selecionada
                        {
                            marcaL.Add(new EditProdutMarcaViewModel
                            {
                                MarcaId = item.Id,
                                IsChecked = false // Marca não está selecionada
                            });
                        }
                    }
                    ViewBag.marcaVb = marcaL;
                }

                //Terceira parte - Carrega as categorias selecionadas para o produto
                var resultCat = await _categoriaRepository.GetAllAsync();
                if (!resultCat.IsNullOrEmpty())
                {
                    foreach (var item in resultCat)
                    {
                        if(resultCategoria.Any(c => c.CategoriaId == item.Id)) // Verifica se a categoria está selecionada 
                        {
                            categoriaL.Add(new EditProdutCategoriaViewModel
                            {
                                CategoriaId = item.Id,
                                IsChecked = true // Categoria está selecionada
                            });
                        }
                        else // Categoria não está selecionada
                        {
                            categoriaL.Add(new EditProdutCategoriaViewModel
                            {
                                CategoriaId = item.Id,
                                IsChecked = false // Categoria não está selecionada
                            });
                        }
                    }
                    ViewBag.categoriaVb = categoriaL;
                }
            }
            return View(new EditProdutoViewModel());
        }
    }
}
