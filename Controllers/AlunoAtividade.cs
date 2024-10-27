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
        public async Task<IActionResult> Index(int id)
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
        public async Task<IActionResult> Update(int id)
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
        public async Task<IActionResult> Update(int id, Dictionary<int, decimal?> notas)
        {
            // Recupera as atividades dos alunos com base na atividade especificada
            var alunoAtividades = await db.AlunoAtividades
                .Where(a => a.AtividadeId == id)
                .ToListAsync();

            if (!alunoAtividades.Any())
            {
                return NotFound();
            }

            foreach (var alunoAtividade in alunoAtividades)
            {
                // Verifica se há uma nota correspondente no dicionário 'notas'
                if (notas.TryGetValue(alunoAtividade.AlunoAtividadeId, out decimal? nota))
                {
                    // Atualiza a nota da atividade do aluno, garantindo que seja um valor decimal
                    alunoAtividade.Nota = nota ?? 0;

                    // Converte a nota em moedas com base no valor decimal
                    var aluno = await db.Usuarios.FindAsync(alunoAtividade.UsuarioId);
                    if (aluno is Aluno alunoAtual)
                    {
                        // Define a quantidade de moedas com base na nota
                        int valorEmMoeda = nota switch
                        {
                            >= 9m and <= 10m => 15,    // 15 moedas para notas entre 9 e 10
                            >= 6m and < 9m => 10,      // 10 moedas para notas entre 6 e 8.99
                            >= 1m and < 6m => 5,       // 5 moedas para notas entre 1 e 5.99
                            _ => 0                     // Nenhuma moeda para nota 0 ou menor
                        };

                        alunoAtual.Moeda += valorEmMoeda;
                    }
                }
            }

            // Salva todas as alterações no banco de dados
            await db.SaveChangesAsync();

            // Redireciona para a página de visualização da atividade
            return RedirectToAction("Update", new { id });
        }
    }
}
