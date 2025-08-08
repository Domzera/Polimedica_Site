using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polimedica.ViewModel;
using Polimedica.Interface;
using Polimedica.Models;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace Polimedica.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ICategoriaRepository _catere;
        private readonly ICategoriaProdutoRepository _categoriaProduto;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IPromocaoRepository _promocaoRepository;
        private readonly IBannerRepository _banner;

        public CategoriaController(
            ICategoriaRepository catere,
            ICategoriaProdutoRepository categoriaProduto,
            IProdutoRepository produtoRepository,
            IPromocaoRepository promocaoRepository,
            IBannerRepository banner)
        {
            _catere = catere;
            _categoriaProduto = categoriaProduto;
            _produtoRepository = produtoRepository;
            _promocaoRepository = promocaoRepository;
            _banner = banner;
        }

        public async Task<IActionResult> Index()
        {
            Banner banner = await _banner.GetBanner(1);
            if (banner != null)
            {
                ViewBag.Banner1 = banner.Banner1 == null ? "/Image/LogoNav.png" : banner.Banner1;
                ViewBag.Banner2 = banner.Banner2 == null ? "/Image/LogoNav.png" : banner.Banner2;
                ViewBag.Banner3 = banner.Banner3 == null ? "/Image/LogoNav.png" : banner.Banner3;
            }
            else
            {
                // Caminho padrão para o Logo da Polimeidca
                ViewBag.Banner1 = "/Image/LogoNav.png";
                ViewBag.Banner2 = "/Image/LogoNav.png";
                ViewBag.Banner3 = "/Image/LogoNav.png";
            }
            IEnumerable<Categoria> categoria = await _catere.GetAllAsync();
            return View(categoria);
        }

        [Authorize(Roles = "Admin,Gerente,Vendedor\"")]
        public IActionResult CreateCategoria()
        {
            var response = new CreateCategoriaViewModel();
            return View(response);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Gerente,Vendedor\"")]
        public async Task<IActionResult> CreateCategoria(CreateCategoriaViewModel createCategoriaVM)
        {
            if (!ModelState.IsValid)
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
                if (categoriaVm.NomeCategoria != null || categoriaVm.NomeCategoria != ""
                    &&
                    categoriaVm.DescricaoCategoria != null || categoriaVm.DescricaoCategoria != "")
                {
                    _catere.Add(categoriaVm);
                    return RedirectToAction("Index");
                }
                else { return RedirectToAction("CreateCategoria"); }
            }

        }

        public async Task<IActionResult> Produtos(int id)
        {

            List<ProdutoCategoriaViewModel> produtoList = new List<ProdutoCategoriaViewModel>();
            var categoria = await _categoriaProduto.GetProdutoByCategoria(id);

            if (categoria != null)
            {
                foreach(var item in categoria)
                {
                    var buscaProduto = _produtoRepository.GetById(id).Result;
                    var buscaPromocao = _promocaoRepository.GetByProdutoId(id).Result;
                    produtoList.Add(new ProdutoCategoriaViewModel
                    {
                        ProdutoId = id,
                        NomeProduto = buscaProduto.NomeProduto,
                        DescricaoProduto = buscaProduto.DescricaoProduto,
                        Preco = buscaProduto.Preco,
                        Imagem1 = buscaProduto.Imagem1,
                        Imagem2 = buscaProduto.Imagem2,
                        Imagem3 = buscaProduto.Imagem3,
                        Imagem4 = buscaProduto.Imagem4,
                        Imagem5 = buscaProduto.Imagem5,
                        Ativo = buscaProduto.Ativo,
                        Promocao = await verificaPromocao(buscaPromocao.ProdutoID),
                        PrecoPromocional = (float?)buscaPromocao.Preco,
                        DataFinalPromocao = buscaPromocao.DataFinal                        
                    });
                }
                return View(produtoList);
            }else
            {
                return RedirectToAction("Index");
            }
        }

        private async Task<bool> verificaPromocao(int id)
        {
            var testaPromocao = _promocaoRepository.GetByProdutoId(id).Result;
            if(testaPromocao != null)
            {
                return true;
            }
            return false;
        }
    }
}
