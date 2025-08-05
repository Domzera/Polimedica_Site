using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polimedica.Interface;
using Polimedica.Models;
using Polimedica.ViewModel;

namespace Polimedica.Controllers
{
    public class BannerController : Controller
    {
        private readonly IPhotoService _photoService;
        private readonly IBannerRepository _banner;

        public BannerController(
            IPhotoService photoService,
            IBannerRepository banner)
        {
            _photoService = photoService;
            _banner = banner;
        }

        [Authorize(Roles = "Admin,Gerente,Vendedor")]
        public async Task<IActionResult> Index()  //  PRIMEIRO EDIT - NA VERDADE
        {
            Banner banner = await _banner.GetBanner(1); // Carrega o bannerd
            if (banner == null)
            {
                var bannerVM = new BannerViewModel
                {
                    ImageUrl1 = "Coloque seu primeiro Banner",
                    ImageUrl2 = "Coloque seu primeiro Banner",
                    ImageUrl3 = "Coloque seu primeiro Banner"
                };
                return View(bannerVM); // Retorna as frases dizendo para colocar banners
            }
            else
            {
                var bannerVM = new BannerViewModel
                {
                    ImageUrl1 = banner.Banner1 == null ? "Carregue um Banner" : banner.Banner1,
                    ImageUrl2 = banner.Banner2 == null ? "Carregue um Banner" : banner.Banner2,
                    ImageUrl3 = banner.Banner3 == null ? "Carregue um Banner" : banner.Banner3
                };

                return View(bannerVM);
            }
        }


        [Authorize(Roles = "Admin,Gerente,Vendedor")]
        [HttpPost]
        public async Task<IActionResult> Index(BannerViewModel bannerVM)
        {
            if (ModelState.IsValid)
            {
                Banner banner = await _banner.GetBanner(1);

                if (banner == null)
                {
                    banner = new Banner
                    {
                        Banner1 = SalvaBanner(bannerVM.IImageUrl1),
                        Banner2 = SalvaBanner(bannerVM.IImageUrl2),
                        Banner3 = SalvaBanner(bannerVM.IImageUrl3)
                    };
                    await _banner.Add(banner);
                    return RedirectToAction("Index", "Produto");
                }
                else
                {
                     if(banner != null)
                    {
                        if(bannerVM.IImageUrl1 != null)
                        {
                            banner.Banner1 = SalvaBanner(bannerVM.IImageUrl1);
                        }
                        if(bannerVM.IImageUrl2 != null)
                        {
                            banner.Banner2 = SalvaBanner(bannerVM.IImageUrl2);
                        }
                        if(bannerVM.IImageUrl3 != null)
                        {
                            banner.Banner3 = SalvaBanner(bannerVM.IImageUrl3);
                        }
                    }
                     await _banner.Update(banner);
                    return RedirectToAction("Index", "Produto");
                }
            }
            return View(bannerVM);
        }

        public string SalvaBanner(IFormFile banner)
        {
            var envia = banner != null ? _photoService.AddBannerAsync(banner) : null;
            if (envia != null)
            {
                return envia.Result.Url.ToString(); // Retorna a URL da imagem enviada
            }
            else
            {
                return "Vazio"; // Retorna nulo se não houver imagem
            }
        }
    }
}

