using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using StarCoins.Models;
using System.Linq;

namespace StarCoins.Controllers
{
    // http://localhost:????/produto
    public class ProdutoController : Controller
    {
        private readonly StarCoinsDatabase db;

        public ProdutoController(StarCoinsDatabase db)
        {
            this.db = db;
        }

        public ActionResult Read()
        {
            return View(db.Produtos.ToList()); // ~ SELECT * FROM Produtos
        }

        public ActionResult Comprar()
        {
            return View(db.Produtos.ToList()); // ~ SELECT * FROM Produtos
        }

        [HttpGet]
        public IActionResult CreateFisico()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateDigital()
        {
            return View();
        }

        // Criar produtos físicos
        [HttpPost]
        public ActionResult CreateFisico(ProdutoFisico model)
        {
            db.Produtos.Add(model); // ~INSERT INTO Produtos VALUES (model.Nome...)
            db.SaveChanges(); // ~commit
            return RedirectToAction("Read");
        }

        // Criar produtos digitais
        [HttpPost]
        public ActionResult CreateDigital(ProdutoDigital model)
        {
            db.Produtos.Add(model); // ~INSERT INTO Produtos VALUES (model.Nome...)
            db.SaveChanges(); // ~commit
            return RedirectToAction("Read");
        }

        public ActionResult Delete(int id)
        {
            var produto = db.Produtos.Single(e => e.ProdutoId == id); // SELECT * FROM Produtos WHERE ProdutoId = id
            db.Produtos.Remove(produto); // DELETE FROM Produtos WHERE ProdutoId = id
            db.SaveChanges(); // commit
            return RedirectToAction("Read");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            // Obtém o produto pelo ID
            Produto produto = db.Produtos.Single(e => e.ProdutoId == id);

            // Verifica o tipo do produto e retorna a view apropriada
            if (produto is ProdutoFisico)
            {
                return View("UpdateFisico", produto); // Retorna a view para produto físico
            }
            else if (produto is ProdutoDigital)
            {
                return View("UpdateDigital", produto); // Retorna a view para produto digital
            }

            // Se não for nenhum dos tipos, pode-se redirecionar ou lançar uma exceção
            return NotFound(); // Ou outra lógica apropriada
        }

        [HttpPost]
        public ActionResult Update(int id, Produto model)
        {
            var produto = db.Produtos.Single(e => e.ProdutoId == id);

            produto.Nome = model.Nome;
            produto.Descricao = model.Descricao;
            produto.Moeda = model.Moeda;
            produto.Quantidade = model.Quantidade;
            produto.Status = model.Status;

            // Se o produto for do tipo Fisico, atualiza propriedades específicas
            if (produto is ProdutoFisico fisico)
            {
                fisico.Peso = ((ProdutoFisico)model).Peso;
            }
            // Se o produto for do tipo Digital, atualiza propriedades específicas
            else if (produto is ProdutoDigital digital)
            {
                digital.TamanhoArquivo = ((ProdutoDigital)model).TamanhoArquivo;
            }

            db.SaveChanges();
            return RedirectToAction("Read");
        }

        [HttpGet]
        public IActionResult ConfirmarComprar(int id)
        {
            Produto produto = db.Produtos.Single(e => e.ProdutoId == id);
            if (produto is ProdutoFisico)
            {
                return View("ConfirmarCompra", produto); // Retorna a view para produto físico
            }
            else if (produto is ProdutoDigital)
            {
                return View("ConfirmarCompra", produto); // Retorna a view para produto digital
            }
            return NotFound();
        }

        //[HttpPost]
        //public IActionResult AceitaComprar(int id)
        //{

    }
}
