using Microsoft.AspNetCore.Mvc;
using StarCoins.Models;

namespace StarCoins.Controllers; 

public class AtividadeController : Controller {

    private readonly StarCoinsDatabase db; // banco de dados

    public AtividadeController(StarCoinsDatabase db) {
        this.db = db;
    }

    public ActionResult Read() {
        
        return View(db.Atividades.ToList()); // ~ SELECT * FROM Atividades
    }

    [HttpGet]
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Atividade model) {

        db.Atividades.Add(model); // ~INSERT INTO Atividades VALUES (model.Nome...)
        db.SaveChanges(); // ~commit
        return RedirectToAction("Read");

    }

    public ActionResult Delete(int id){
        // realizar a exclusão
        var atividade = db.Atividades.Single(e => e.AtividadeId == id); // SELECT * FROM Atividades WHERE atividadeId = id
        db.Atividades.Remove(atividade); // DELETE FROM Atividades WHERE AtividadeId = id
        // db.Entry<Atividade>(atividade).State = Microsoft.EntityFrameworkCore.EntityState.Deleted; // código igual ao de cima
        db.SaveChanges(); // commit
        


        // redirecionar para a página de read novamente
        return RedirectToAction("Read");
    }

    [HttpGet]
    public ActionResult Update(int id)
    {
        Atividade atividade = db.Atividades.Single(e => e.AtividadeId == id);

        return View(atividade);
    }

    [HttpPost]
    public ActionResult Update(int id, Atividade model) {

        var atividade = db.Atividades.Single(e => e.AtividadeId == id);

        atividade.Nome = model.Nome;
        atividade.Descricao = model.Descricao;
        atividade.DataEntrega = model.DataEntrega;
        atividade.Moeda = model.Moeda;
        
        db.SaveChanges();

        return RedirectToAction("Read");

    }

    
}