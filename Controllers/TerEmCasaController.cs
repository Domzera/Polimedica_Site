using Microsoft.AspNetCore.Mvc;
using Polimedica.Interface;
using Polimedica.Models;
using Polimedica.ViewModel;

namespace Polimedica.Controllers
{
    public class TerEmCasaController : Controller
    {
        private readonly ITerEmCasaRepository _terEmCasa;
        private readonly IProdutoRepository _produto;
        private readonly IBannerRepository _banner;
        private readonly IPromocaoRepository _promocao;

        public TerEmCasaController(
            ITerEmCasaRepository terEmCasa,
            IProdutoRepository produto,
            IBannerRepository banner,
            IPromocaoRepository promocao)
        {
            _terEmCasa = terEmCasa;
            _produto = produto;
            _banner = banner;
            _promocao = promocao;
            // Constructor logic can be added here if needed
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

            IEnumerable<TerEmCasa> terEmCasaList = await _terEmCasa.GetAll();

            if (terEmCasaList != null)
            {
                List<TerEmCasaViewModel> terEmCasaListVM = new List<TerEmCasaViewModel>();


                foreach (var item in terEmCasaList)
                {
                    var produto = await _produto.GetById(item.ProdutoId);
                    terEmCasaListVM.Add(new TerEmCasaViewModel
                    {
                        ProdutoId = item.ProdutoId,
                        NomeProduto = produto.NomeProduto,
                        DescricaoProduto = produto.DescricaoProduto,
                        Preco = produto.Preco,
                        Imagem1 = produto.Imagem1,
                        Imagem2 = produto.Imagem2,
                        Imagem3 = produto.Imagem3,
                        Imagem4 = produto.Imagem4,
                        Imagem5 = produto.Imagem5,
                        Ativo = produto.Ativo
                    });
                }
                return View(terEmCasaListVM);
            }
            return RedirectToAction("Index", "Produto");
        }   
    }
}