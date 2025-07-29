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
                List<Categoria> categoriaL = new List<Categoria>();
                List<DetalheMarcaViewModel> marcaL = new List<DetalheMarcaViewModel>();

                var todasMarcas = await _marcaRepository.GetAllAsync();
                var marcasProduto = await _marcaProdutoRepository.getByProdutoId(id);

                if (marcasProduto != null)
                {
                    foreach (var item in marcasProduto)
                    {
                        marcaL.Add(new DetalheMarcaViewModel
                        {
                            MarcaId = item.MarcaId,
                            NomeMarca = todasMarcas.FirstOrDefault(m => m.Id == item.MarcaId)?.NomeMarca // Busca o nome da marca pelo ID
                        });
                    }
                    ViewBag.marcaDetalheVb = marcaL;
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
                return View(produto);
            }
            return NotFound();
        }

        [Authorize]
        public async Task<IActionResult> CreateProdutoAsync()  //  ***********  PRIMEIRO CREATE
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
         * Na primeira parte, nós criamos o produto e salvamos as Url's das imagens.
         * Na segunda parte nós criamos as relações entre Produto e Marca
         * Na terceira parte nós criamos as relações entre Produto e Categoria
        */
        [Authorize(Roles = "Admin,Gerente,Vendedor")]
        [HttpPost]
        public async Task<IActionResult> CreateProduto(CreateProdutoViewModel produtoVM)  //  ***********  SEGUNDO CREATE
        {

            if (ModelState.IsValid)
            {
                // Primeira parte: Criação do produto
                var produto = new Produto
                {
                    NomeProduto = produtoVM.NomeProduto,
                    DescricaoProduto = produtoVM.DescricaoProduto,
                    Preco = (Decimal)produtoVM.Preco,
                    Ativo = produtoVM.Ativo,
                    Imagem1 = produtoVM.Imagem1 != null ? SalvaPhoto(produtoVM.Imagem1) : "", // Verifica se a imagem é diferente da original
                    Imagem2 = produtoVM.Imagem2 != null ? SalvaPhoto(produtoVM.Imagem2) : "", // Verifica se a imagem é diferente da original
                    Imagem3 = produtoVM.Imagem3 != null ? SalvaPhoto(produtoVM.Imagem3) : "", // Verifica se a imagem é diferente da original
                    Imagem4 = produtoVM.Imagem4 != null ? SalvaPhoto(produtoVM.Imagem4) : "", // Verifica se a imagem é diferente da original
                    Imagem5 = produtoVM.Imagem5 != null ? SalvaPhoto(produtoVM.Imagem5) : "", // Verifica se a imagem é diferente da original

                    DataAdicionado = DateOnly.FromDateTime(DateTime.Now),
                };
                await _produtoRepository.Add(produto); // Salva as alterações no BD para ProdutosBd

                // segunda parte: Criação das relações entre Produto e Marca
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

                        await _marcaProdutoRepository.Add(marcaId); // Salva as alterações no BD para MarcaProdutoBd
                        i++;
                    }
                    else
                    {
                        ModelState.AddModelError("MarcaId", "Marca não pode ser nulo ou zero.");
                        return View(produtoVM); // Retorna a view com o erro
                    }
                }

                // Terceira parte: Criação das relações entre Produto e Categoria
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

                        await _categoriaProdutoRepository.Add(categoriaId); // Salva as alterações no BD para CategoriaProdutoBd
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
            return RedirectToAction("Index", "Produto");
        }

        /* Método que chama a edição do produto e carrega os valores em seus devidos campos
         * Na primeira parte nós carregamos os dados simples do produto e as imagens
         * Na segunda parte nós carregamos as marcas selecionadas para o produto
         * Na terceira parte nós carregamos as categorias selecionadas para o produto
         */

        [Authorize(Roles = "Admin,Gerente,Vendedor")]
        public async Task<IActionResult> EditarProduto(int id)  //  ***********  PRIMEIRO EDIT
        {
            if (id != 0)
            {
                Produto produto = await _produtoRepository.GetById(id); // Carrega o objeto produto pelo ID
                if (produto == null)
                {
                    return NotFound();
                }

                var ProdutoEdit = new EditProdutoViewModel
                {
                    NomeProduto = produto.NomeProduto,
                    DescricaoProduto = produto.DescricaoProduto,
                    Preco = (Decimal)produto.Preco,
                    Ativo = produto.Ativo,

                    Imagem1 = produto.Imagem1,
                    Imagem2 = produto.Imagem2,
                    Imagem3 = produto.Imagem3,
                    Imagem4 = produto.Imagem4,
                    Imagem5 = produto.Imagem5
                };

                //Segunda parte - Carrega as marcas selecionadas para o produto
                List<EditProdutMarcaViewModel> marcaL = new List<EditProdutMarcaViewModel>(); //Cria a lista de marcas
                var marcaProduto = await _marcaProdutoRepository.getByProdutoId(id); // Busca todas as marcas associadas ao produto
                var marca = await _marcaRepository.GetAllAsync(); // Busca todas as marcas cadastradas no banco de dados

                if(marca != null)
                {
                    //Carrega as marcas
                    foreach (var item in marca) //Para cada marca cadastrada
                    {
                        var Omarca = marcaProduto.FirstOrDefault(m => m.MarcaId == item.Id); // Busca se esta cadastrada no produto 
                        if (Omarca != null)
                        {
                            marcaL.Add(new EditProdutMarcaViewModel
                            {
                                MarcaId = item.Id,
                                MarcaNome = item.NomeMarca,
                                IsChecked = true, // Marca está cadastrada no produto
                                ProdutoId = id
                            });
                        }
                        else // Marca não está cadastrada no produto
                        {
                            marcaL.Add(new EditProdutMarcaViewModel
                            {
                                MarcaId = item.Id,
                                MarcaNome = item.NomeMarca,
                                IsChecked = false, // Marca não está cadastrada no produto
                                ProdutoId = id
                            });
                        }
                    }
                    ViewBag.marcaVb = marcaL;
                }

                //Terceira parte - Carrega as categorias selecionadas para o produto
                List<EditProdutCategoriaViewModel> categoriaL = new List<EditProdutCategoriaViewModel>(); //Cria a lista de categorias
                var categoriaProduto = await _categoriaProdutoRepository.GetByProdutoId(id); // Busca todas as categorias associadas ao produto
                var categoria = await _categoriaRepository.GetAllAsync(); // Busca todas as categorias cadastradas no banco de dados

                if (categoria != null)
                {
                    //Carrega as categorias
                    foreach (var item in categoria) //Para cada categoria cadastrada
                    {
                        var Ocategoria = categoriaProduto.FirstOrDefault(c => c.CategoriaId == item.Id); // Busca se esta cadastrada no produto 
                        if (Ocategoria != null)
                        {
                            categoriaL.Add(new EditProdutCategoriaViewModel
                            {
                                CategoriaId = item.Id,
                                CategoriaNome = item.NomeCategoria,
                                IsChecked = true, // Categoria está cadastrada no produto
                                ProdutoId = id
                            });
                        }
                        else // Categoria não está cadastrada no produto
                        {
                            categoriaL.Add(new EditProdutCategoriaViewModel
                            {
                                CategoriaId = item.Id,
                                CategoriaNome = item.NomeCategoria,
                                IsChecked = false, // Categoria não está cadastrada no produto
                                ProdutoId = id
                            });
                        }
                    }
                    ViewBag.categoriaVb = categoriaL;
                }
                return View(ProdutoEdit);
            }
            return NotFound();
        }

        /* Método que atuializa os dados do produto.
         * Na primeira parte nós verificamos se o modelo é válido
         * Na segunda parte atualizamos os dados do produto
         * Na terceira parte atualizamos as fotos
         * Na quarta parte atualizamos as marcas
         * Na quinta parte atualizamos as categorias
         */

        [Authorize(Roles = "Admin,Gerente,Vendedor")]
        [HttpPost]
        public async Task<IActionResult> EditarProduto(EditProdutoViewModel produtoVM, int id)  //  ***********  SEGUNDO EDIT
        {
            // Verifica se o ViewModel é valido
            if (ModelState.IsValid)
            {
                Produto produto = await _produtoRepository.GetById(id);
                if (produto == null)
                {
                    return NotFound();
                }
                // Segunda parte => Atualiza os dados do produto
                produto.NomeProduto = produtoVM.NomeProduto;
                produto.DescricaoProduto = produtoVM.DescricaoProduto;
                produto.Preco = (Decimal)produtoVM.Preco;
                produto.Ativo = produtoVM.Ativo ? true: false ;
                // Terceira Parte =>Atualiza as imagens se novas forem enviadas
                if (produtoVM.IImagem1 != null)
                {
                    await DeletePhoto(produto.Imagem1);
                    produto.Imagem1 = SalvaPhoto(produtoVM.IImagem1);
                }
                if (produtoVM.IImagem2 != null)
                {
                    await DeletePhoto(produto.Imagem2);
                    produto.Imagem2 = SalvaPhoto(produtoVM.IImagem2);
                }
                if (produtoVM.IImagem3 != null)
                {
                    await DeletePhoto(produto.Imagem3);
                    produto.Imagem3 = SalvaPhoto(produtoVM.IImagem3);
                }
                if (produtoVM.IImagem4 != null)
                {
                    await DeletePhoto(produto.Imagem4);
                    produto.Imagem4 = SalvaPhoto(produtoVM.IImagem4);
                }
                if (produtoVM.IImagem5 != null)
                {
                    await DeletePhoto(produto.Imagem5);
                    produto.Imagem5 = SalvaPhoto(produtoVM.IImagem5);
                }

                var marcaProduto = await _marcaProdutoRepository.getByProdutoId(id); // Busca todas as marcas associadas ao produto
                int i = 0;
                if(produtoVM.MarcaId != null)
                {
                    //Deleta todas as associações de categorias do produto
                    foreach (var item in marcaProduto)
                    {
                        var deletaMarca = await _marcaProdutoRepository.DeleteByProdutoId(item.ProdutoId);
                    }
                    // Adiciona as novas marcas
                    while (i < produtoVM.MarcaId.Length)
                    {

                        if (produtoVM.MarcaId[i].ToString() != "" && produtoVM.MarcaId[i] != null)
                        {
                            var marcaProd = new MarcaProduto
                            {
                                MarcaId = produtoVM.MarcaId[i],
                                ProdutoId = produto.Id
                            };

                            await _marcaProdutoRepository.Add(marcaProd); // Salva as alterações no BD para MarcaProdutoBd
                            i++;
                        }
                        else
                        {
                            ModelState.AddModelError("MarcaId", "Marca não pode ser nulo ou zero.");
                            return View(produtoVM); // Retorna a view com o erro
                        }
                    }
                }

                int y = 0;
                if (produtoVM.CategoriaId != null)
                {
                    var categoriaProduto = await _categoriaProdutoRepository.GetByProdutoId(id); // Busca todas as categorias associadas ao produto
                    // Deleta todas associações de marcas do produto
                    foreach (var item in categoriaProduto)
                    {
                        var deletaCategoria = await _categoriaProdutoRepository.DeleteByProdutoId(item.ProdutoId);
                    }
                    while (y < produtoVM.CategoriaId.Length)
                    {
                        if (produtoVM.CategoriaId[y].ToString() != "" && produtoVM.CategoriaId[y] != null)
                        {
                            var categoriaId = new CategoriaProduto
                            {
                                CategoriaId = produtoVM.CategoriaId[y],
                                ProdutoId = id
                            };
                            await _categoriaProdutoRepository.Add(categoriaId); // Adiciona as novas categorias
                            y++;
                        }
                        else
                        {
                            ModelState.AddModelError("CategoriaId", "Categoria não pode ser nulo ou zero.");
                            return View(produtoVM); // Retorna a view com o erro
                        }
                    }
                }
                await _produtoRepository.Update(produto);

                return RedirectToAction("Detalhe", "Produto", new {id = id });
            }

            return RedirectToAction("Index", "Produto");

        }


        public string SalvaPhoto(IFormFile photo)
        {
            var envia = photo != null ? _photoService.AddPhotoAsync(photo) : null;
            if (envia != null)
            {
                return envia.Result.Url.ToString(); // Retorna a URL da imagem enviada
            }
            else
            {
                return null; // Retorna nulo se não houver imagem
            }
        }

        public async Task<string> DeletePhoto(string publicId)
        {
            if (!string.IsNullOrEmpty(publicId))
            {
                var result = await _photoService.DeletePhotoAsync(publicId);
                return result.Result == "ok" ? "Imagem deletada com sucesso." : "Erro ao deletar imagem.";
            }
            return "PublicId inválido.";
        }
    }
}
