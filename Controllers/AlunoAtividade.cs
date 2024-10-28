using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarCoins.Models;
using System.Linq;
using System.Threading.Tasks;

namespace StarCoins.Controllers
{
    public class AlunoAtividadeController : Controller
    {
        private readonly StarCoinsDatabase db;

        public AlunoAtividadeController(StarCoinsDatabase db)
        {
            this.db = db;
        }

        // Lista a atividade específica com os alunos vinculados
        [HttpGet]
        public async Task<IActionResult> Read(int id)
        {
            var atividadeDetalhes = await db.AlunoAtividades
                .Include(a => a.Aluno) // Inclui os detalhes dos alunos
                .Include(a => a.Atividade) // Inclui os detalhes da atividade
                .Where(a => a.AtividadeId == id)
                .ToListAsync();

            if (atividadeDetalhes == null || !atividadeDetalhes.Any())
            {
                return NotFound();
            }

            return View(atividadeDetalhes);
        }

        // Página de atualização para salvar notas dos alunos
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // Recupera a atividade específica com os alunos vinculados
            var atividadeDetalhes = await db.AlunoAtividades
                .Include(a => a.Aluno)
                .Include(a => a.Atividade)
                .Where(a => a.AtividadeId == id)
                .ToListAsync();

            if (atividadeDetalhes == null || !atividadeDetalhes.Any())
            {
                return NotFound();
            }

            return View(atividadeDetalhes); // Exibe a view de atualização com os detalhes da atividade
        }

        // Atualiza as notas dos alunos para uma atividade específica
        [HttpPost]
        public async Task<IActionResult> SalvarNotas(int atividadeId, Dictionary<int, AlunoAtividade> alunoAtividades)
        {
            // Recupera as atividades dos alunos com base na atividade especificada
            var alunoAtividadesExistentes = await db.AlunoAtividades
                .Where(a => a.AtividadeId == atividadeId)
                .ToListAsync();

            if (!alunoAtividadesExistentes.Any())
            {
                return NotFound();
            }

            foreach (var alunoAtividade in alunoAtividadesExistentes)
            {
                if (alunoAtividades.TryGetValue(alunoAtividade.AlunoAtividadeId, out var alunoAtividadeAtualizada))
                {
                    // Atualiza a nota da atividade do aluno
                    alunoAtividade.Nota = alunoAtividadeAtualizada.Nota;

                    // Converte a nota em moedas com base no valor decimal
                    var aluno = await db.Usuarios.FindAsync(alunoAtividade.UsuarioId);
                    if (aluno is Aluno alunoAtual)
                    {
                        int valorEmMoeda = CalcularMoedas(alunoAtividade.Nota);
                        alunoAtual.Moeda += valorEmMoeda;
                        db.Usuarios.Update(alunoAtual); // Certifique-se de que a alteração é rastreada
                    }
                }
            }

            // Salva todas as alterações no banco de dados
            await db.SaveChangesAsync();

            // Redireciona para a página de visualização da atividade
            return RedirectToAction("Read", new { id = atividadeId });
        }

        private int CalcularMoedas(decimal? nota)
        {
            return nota switch
            {
                >= 9m and <= 10m => 15,
                >= 6m and < 9m => 10,
                >= 1m and < 6m => 5,
                _ => 0
            };
        }

    }
}
