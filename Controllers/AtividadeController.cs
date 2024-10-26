using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarCoins.Models;
using System.Linq;
using System.Threading.Tasks;

namespace StarCoins.Controllers
{
    public class AtividadeController : Controller
    {
        private readonly StarCoinsDatabase db; // banco de dados

        public AtividadeController(StarCoinsDatabase db)
        {
            this.db = db;
        }

        public ActionResult Read()
        {
            return View(db.Atividades.ToList()); // ~ SELECT * FROM Atividades
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Atividade model)
        {
            if (ModelState.IsValid)
            {
                // Adicionar a nova atividade
                db.Atividades.Add(model); // ~INSERT INTO Atividades VALUES (model.Nome...)
                await db.SaveChangesAsync(); // ~commit

                // Recuperar todos os alunos
                var alunos = await db.Usuarios.OfType<Aluno>().ToListAsync();

                // Criar registros de AlunoAtividade para cada aluno
                foreach (var aluno in alunos)
                {
                    var alunoAtividade = new AlunoAtividade
                    {
                        AtividadeId = model.AtividadeId,
                        UsuarioId = aluno.UsuarioId,
                        Nota = null // Inicialmente sem nota
                    };

                    db.AlunoAtividades.Add(alunoAtividade);
                }

                await db.SaveChangesAsync(); // Commit final para salvar as associações

                return RedirectToAction("Read");
            }

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            // Realizar a exclusão
            var atividade = db.Atividades.Single(e => e.AtividadeId == id); // SELECT * FROM Atividades WHERE atividadeId = id
            db.Atividades.Remove(atividade); // DELETE FROM Atividades WHERE AtividadeId = id
            db.SaveChanges(); // commit

            // Redirecionar para a página de read novamente
            return RedirectToAction("Read");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            Atividade atividade = db.Atividades.Single(e => e.AtividadeId == id);

            return View(atividade);
        }

        [HttpPost]
        public ActionResult Update(int id, Atividade model)
        {
            var atividade = db.Atividades.Single(e => e.AtividadeId == id);

            atividade.Nome = model.Nome;
            atividade.Descricao = model.Descricao;
            atividade.DataEntrega = model.DataEntrega;
            atividade.Moeda = model.Moeda;

            db.SaveChanges();

            return RedirectToAction("Read");
        }
    }
}
