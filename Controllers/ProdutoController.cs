using Microsoft.AspNetCore.Mvc;
using StarCoins.Models;

namespace StarCoins.Controllers; // colocar no plural, serve para armazenar

// http://localhost:????/produto
public class ProdutoController : Controller {

    private readonly StarCoinsDatabase db;

    public ProdutoController(StarCoinsDatabase db) {
        this.db = db;
    }

    public ActionResult Read() {
        
        return View(db.Produtos.ToList()); // ~ SELECT * FROM Produtos
    }

    public ActionResult AlunoView() {
        
        return View(db.Produtos.ToList()); // ~ SELECT * FROM Produtos
    }

    [HttpGet]
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Produto model) {

        db.Produtos.Add(model); // ~INSERT INTO Produtos VALUES (model.Nome...)
        db.SaveChanges(); // ~commit
        return RedirectToAction("Read");

    }

    public ActionResult Delete(int id){
        // realizar a exclusão
        var produto = db.Produtos.Single(e => e.ProdutoId == id); // SELECT * FROM Produtos WHERE produtoId = id
        db.Produtos.Remove(produto); // DELETE FROM Produtos WHERE ProdutoId = id
        // db.Entry<Produto>(produto).State = Microsoft.EntityFrameworkCore.EntityState.Deleted; // código igual ao de cima
        db.SaveChanges(); // commit
        


        // redirecionar para a página de read novamente
        return RedirectToAction("Read");
    }

    [HttpGet]
    public ActionResult Update(int id)
    {
        Produto produto = db.Produtos.Single(e => e.ProdutoId == id);

        return View(produto);
    }

    [HttpPost]
    public ActionResult Update(int id, Produto model) {

        var produto = db.Produtos.Single(e => e.ProdutoId == id);

        produto.Nome = model.Nome;
        produto.Descricao = model.Descricao;
        produto.Moeda = model.Moeda;
        produto.Quantidade = model.Quantidade;
        produto.Status = model.Status;
        
        db.SaveChanges();

        return RedirectToAction("Read");

    }

    [HttpPost] //FAZER
    public ActionResult Comprar(int id, Produto model) {

        var produto = db.Produtos.Single(e => e.ProdutoId == id);

        produto.Nome = model.Nome;
        produto.Descricao = model.Descricao;
        produto.Moeda = model.Moeda;
        produto.Quantidade = model.Quantidade;
        produto.Status = model.Status;
        
        db.SaveChanges();

        return RedirectToAction("Read");

    }

    
}