using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarCoins.Models;

namespace StarCoins.Controllers; // colocar no plural, serve para armazenar

// http://localhost:????/pedido
public class PedidoController : Controller {

    private readonly StarCoinsDatabase db;

    public PedidoController(StarCoinsDatabase db) {
        this.db = db;
    }

    public async Task<IActionResult> Create(int pedidoId)
    {
        var pedido = await db.Pedidos
            .Include(p => p.Produto)
            .Include(p => p.Usuario)
            .FirstOrDefaultAsync(p => p.PedidoId == pedidoId);

        if (pedido == null)
            return NotFound();

        return View(pedido); // Renderiza a view `Create.cshtml` com o pedido
    }

    public async Task<IActionResult> Read()
    {
    // Obtém o ID do usuário logado da sessão
    var userId = HttpContext.Session.GetInt32("userId");
    if (userId == null)
    {
        // Se o usuário não estiver logado, redireciona para a página de login
        return RedirectToAction("Login", "Usuario");
    }

    // Filtra os pedidos para mostrar apenas aqueles que pertencem ao usuário logado
    var pedidosUsuario = await db.Pedidos
                                .Where(p => p.UsuarioId == userId)
                                .ToListAsync();

    return View(pedidosUsuario);
    }


    [HttpGet]
    public async Task<IActionResult> Verificar(int id)
    {
        var userId = HttpContext.Session.GetInt32("userId");
        if (userId == null)
        {
            return RedirectToAction("Login", "Usuario");
        }

        // Carrega o pedido, incluindo as informações de Produto e Usuario
        var pedido = await db.Pedidos
            .Include(p => p.Produto)  // Carrega o Produto relacionado
            .Include(p => p.Usuario)  // Carrega o Usuario relacionado
            .SingleOrDefaultAsync(p => p.PedidoId == id && p.UsuarioId == userId);

        if (pedido == null)
        {
            return RedirectToAction("Read"); // Redireciona para a lista de pedidos
        }

        return View("Verificar", pedido);
    }




}