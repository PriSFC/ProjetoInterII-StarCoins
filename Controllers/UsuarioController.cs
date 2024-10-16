using Microsoft.AspNetCore.Mvc;
using StarCoins.Models;

namespace StarCoins.Controllers;

// http://localhost:????/usuario
public class UsuarioController : Controller {

    private readonly StarCoinsDatabase db;

    public UsuarioController(StarCoinsDatabase db) {
        this.db = db;
    }

    public ActionResult Read() {
        
        return View(db.Usuarios.ToList()); // ~ SELECT * FROM Usuarios
    }

    [HttpGet]
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Usuario model) {

        db.Usuarios.Add(model); // ~INSERT INTO Usuarios VALUES (model.Nome...)
        db.SaveChanges(); // ~commit
        return RedirectToAction("Read");

    }

    public ActionResult Delete(int id){
        // realizar a exclusão
        var usuario = db.Usuarios.Single(e => e.UsuarioId == id); // SELECT * FROM Usuarios WHERE UsuarioId = id
        db.Usuarios.Remove(usuario); // DELETE FROM Usuarios WHERE UsuarioId = id
        // db.Entry<Usuario>(usuario).State = Microsoft.EntityFrameworkCore.EntityState.Deleted; // código igual ao de cima
        db.SaveChanges(); // commit
        


        // redirecionar para a página de read novamente
        return RedirectToAction("Read");
    }

    [HttpGet]
    public ActionResult Update(int id)
    {
        Usuario usuario = db.Usuarios.Single(e => e.UsuarioId == id);

        return View(usuario);
    }

    [HttpPost]
    public ActionResult Update(int id, Usuario model) {

        var usuario = db.Usuarios.Single(e => e.UsuarioId == id);

        usuario.Nome = model.Nome;
        usuario.Perfil = model.Perfil;
        usuario.Login = model.Login;
        usuario.Senha = model.Senha;
        
        db.SaveChanges();

        return RedirectToAction("Read");

    }

    public ActionResult Login() {
        return View();
    }

    [HttpPost]
    public ActionResult Login(UsuarioViewModel model)
    {
        var user = db.Usuarios.SingleOrDefault(e => e.Email == model.Email && e.Senha == model.Senha);

        if(user == null) 
        {
            ViewBag.Autenticado = false;
            return View(model);
        }
        else 
        {
            // login (sessão)
            HttpContext.Session.SetInt32("userId", user.UsuarioId);
            HttpContext.Session.SetString("userName", user.Nome);
            
            return RedirectToAction("Index", "Home");
        }
    }

    
}