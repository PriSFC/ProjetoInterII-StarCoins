using StarCoins.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace StarCoins.Controllers {
    public class HomeController : Controller {

        public IActionResult Index() {
            
            HomeModel home = new HomeModel();

            home.Nome = HttpContext.Session.GetString("userName");
            home.Perfil = HttpContext.Session.GetString("perfil");

            return View(home);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }

}

