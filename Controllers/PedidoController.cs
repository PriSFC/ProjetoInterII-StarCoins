using Microsoft.AspNetCore.Mvc;
using StarCoins.Models;

namespace StarCoins.Controllers; // colocar no plural, serve para armazenar

// http://localhost:????/pedido
public class PedidoController : Controller {

    private readonly StarCoinsDatabase db;

    public PedidoController(StarCoinsDatabase db) {
        this.db = db;
    }

    public ActionResult Read() {
        
        return View(db.Pedidos.ToList()); // ~ SELECT * FROM Pedidos
    }

//

}