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
public ActionResult Create(Usuario model)
{
    // Verifica se o perfil é "Aluno" para criar uma instância do tipo Aluno
    if (model.Perfil == "Aluno")
    {
        // Cria uma instância de Aluno e define as propriedades compartilhadas
        var aluno = new Aluno
        {
            UsuarioId = model.UsuarioId, // Presumindo que o ID seja gerado ou atribuído
            Nome = model.Nome,
            Login = model.Login,
            Email = model.Email,
            Senha = model.Senha,
            Perfil = model.Perfil,
            Moeda = 30 // Define as moedas iniciais para o aluno
        };

        db.Usuarios.Add(aluno); // Adiciona o objeto Aluno ao banco de dados
    }
    else
    {
        // Adiciona o objeto Usuario para perfis diferentes de "Aluno"
        db.Usuarios.Add(model);
    }

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
            HttpContext.Session.SetString("perfil", user.Perfil);
            
            return RedirectToAction("Index", "Home");
        }
    }
    
    // Método para verificar as informações do usuário logado
    public ActionResult Verificar() {
        // Obtém o ID do usuário da sessão
        var userId = HttpContext.Session.GetInt32("userId");

        if (userId == null) {
            return RedirectToAction("Login"); // Redireciona para Login se não houver usuário logado
        }

        // Busca o usuário no banco de dados
        var usuario = db.Usuarios.SingleOrDefault(u => u.UsuarioId == userId);

        if (usuario == null) {
            return NotFound(); // Retorna um erro se o usuário não for encontrado
        }

        return View(usuario); // Retorna a view com as informações do usuário
    }

    public ActionResult Logout() {
    // Limpa a sessão do usuário
    HttpContext.Session.Remove("userId");
    HttpContext.Session.Remove("userName");

    // Redireciona para a página de login
    return RedirectToAction("Login");
}
    
}