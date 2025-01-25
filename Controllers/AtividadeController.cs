using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarCoins.Models;

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

        // Criar nova atividade e já fazer a associação com todos os alunos registrados no banco de dados
        [HttpPost]
        public async Task<ActionResult> Create(Atividade model)
        {
            // Verifica se os dados do modelo são válidos de acordo com as regras de validação definidas
            if (ModelState.IsValid)
            {
                // Adiciona a nova atividade ao contexto do banco de dados
                db.Atividades.Add(model);

                // Salva a nova atividade no banco de dados de forma assíncrona
                await db.SaveChangesAsync();

                // Verifica se o ID da atividade foi definido corretamente após a gravação no banco
                if (model.AtividadeId <= 0)
                {
                    // Adiciona um erro ao estado do modelo indicando falha na criação da atividade
                    ModelState.AddModelError("", "Atividade não foi criada corretamente.");
                    return View(model); // Retorna à view com o erro para o usuário corrigir
                }

                // Recupera todos os usuários que possuem o perfil "Aluno" da base de dados
                var alunos = await db.Usuarios
                                    .Where(u => u.Perfil == "Aluno")
                                    .ToListAsync();

                // Verifica se há alunos cadastrados para serem associados à nova atividade
                if (alunos == null || alunos.Count == 0)
                {
                    // Adiciona um erro ao estado do modelo indicando que nenhum aluno foi encontrado
                    ModelState.AddModelError("", "Nenhum aluno encontrado para associar a esta atividade.");
                    return View(model); // Retorna à view com o erro para o usuário corrigir
                }

                // Para cada aluno encontrado, cria um novo registro de AlunoAtividade associando-o à nova atividade
                foreach (var aluno in alunos)
                {
                    var alunoAtividade = new AlunoAtividade
                    {
                        AtividadeId = model.AtividadeId, // Define o ID da atividade recém-criada
                        UsuarioId = aluno.UsuarioId,     // Define o ID do aluno
                        Nota = null                      // Inicialmente, a nota é definida como nula
                    };

                    // Adiciona o novo registro de AlunoAtividade ao contexto do banco de dados
                    db.AlunoAtividades.Add(alunoAtividade);
                }

                // Salva as associações entre a atividade e os alunos no banco de dados de forma assíncrona
                await db.SaveChangesAsync();

                // Redireciona o usuário para a ação "Read", que possivelmente exibe a lista de atividades criadas
                return RedirectToAction("Read");
            }
            
            // Caso os dados do modelo não sejam válidos, retorna à view com o modelo para que o usuário corrija os erros
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
            if (ModelState.IsValid){
                var atividade = db.Atividades.Single(e => e.AtividadeId == id);

                atividade.Nome = model.Nome;
                atividade.Descricao = model.Descricao;
                atividade.DataEntrega = model.DataEntrega;
                atividade.Moeda = model.Moeda;

                db.SaveChanges();

                return RedirectToAction("Read");
            }
            // Passa o id para a view para que possa ser usado no formulário
            ViewBag.Id = id;
            return View(model);
        }
    }
}
