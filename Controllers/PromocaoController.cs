using Microsoft.AspNetCore.Mvc;
using Polimedica.Interface;
using Polimedica.Models;
using Polimedica.ViewModel;

namespace Polimedica.Controllers
{
    public class PromocaoController : Controller
    {
        private readonly IPromocaoRepository _promocao;
        private readonly IProdutoRepository _produto;
        private readonly IBannerRepository _banner;

        public PromocaoController(
            IPromocaoRepository promocao,
            IProdutoRepository produto,
            IBannerRepository banner)
        {
            _promocao = promocao;
            _produto = produto;
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

            IEnumerable<Promocao> promocao = await _promocao.GetAll();
            List<PromocaoViewModel> promocoesVM = new List<PromocaoViewModel>();

            foreach (var item in promocao)
            {
                if(item.ProdutoID != 0)
                {
                    Produto produto = await _produto.GetById(item.ProdutoID);
                    
                    var promocaoVM = new PromocaoViewModel
                    {
                        ProdutiId = produto.Id,
                        NomeProduto = produto.NomeProduto,
                        DescricaoProduto = produto.DescricaoProduto,
                        Imagem1 = produto.Imagem1,
                        Imagem2 = produto.Imagem2,
                        Imagem3 = produto.Imagem3,
                        Imagem4 = produto.Imagem4,
                        Imagem5 = produto.Imagem5,
                        Ativo = produto.Ativo,
                        PrecoPromocional = (float)item.Preco,
                        DataInicioPromocao = item.Datainicio,
                        DataFinalPromocao = item.DataFinal,
                    };

                    promocoesVM.Add(promocaoVM);
                }
            }

            return View(promocoesVM);
        }
    }
}
