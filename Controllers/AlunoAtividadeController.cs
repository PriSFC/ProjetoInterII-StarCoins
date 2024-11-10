using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarCoins.Models;

namespace StarCoins.Controllers
{
    public class AlunoAtividadeController : Controller
    {
        private readonly StarCoinsDatabase db;

        // Construtor que injeta a instância do banco de dados
        public AlunoAtividadeController(StarCoinsDatabase db)
        {
            this.db = db;
        }

        // Lista a atividade específica com os alunos vinculados
        [HttpGet]
        public async Task<IActionResult> Read(int id)
        {
            // Consulta os detalhes da atividade específica incluindo os dados dos alunos e da atividade
            var atividadeDetalhes = await db.AlunoAtividades
                .Include(a => a.Aluno) // Inclui os detalhes dos alunos vinculados
                .Include(a => a.Atividade) // Inclui os detalhes da atividade
                .Where(a => a.AtividadeId == id)
                .ToListAsync();

            // Verifica se a atividade foi encontrada e se possui registros de alunos
            if (atividadeDetalhes == null || !atividadeDetalhes.Any())
            {
                return NotFound(); // Retorna um erro 404 se a atividade não for encontrada
            }

            // Retorna a view com os detalhes da atividade e dos alunos vinculados
            return View(atividadeDetalhes);
        }

        // Página de atualização para salvar notas dos alunos
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            // Recupera a atividade específica junto com os alunos vinculados para edição única
            var atividadeDetalhes = await db.AlunoAtividades
                .Include(a => a.Aluno) // Inclui os detalhes dos alunos
                .Include(a => a.Atividade) // Inclui os detalhes da atividade
                .Where(a => a.AtividadeId == id)
                .ToListAsync();

            // Verifica se a atividade foi encontrada e se possui alunos vinculados
            if (atividadeDetalhes == null || !atividadeDetalhes.Any())
            {
                return NotFound(); // Retorna um erro 404 se não houver registros encontrados
            }

            // Retorna a view de atualização com os detalhes da atividade e dos alunos vinculados
            return View(atividadeDetalhes);
        }

        // Atualiza as notas dos alunos para uma atividade específica
        [HttpPost]
        public async Task<IActionResult> SalvarNotas(int atividadeId, Dictionary<int, AlunoAtividade> alunoAtividades)
        {
            // Recupera as atividades dos alunos com base na atividade especificada
            var alunoAtividadesExistentes = await db.AlunoAtividades
                .Where(a => a.AtividadeId == atividadeId)
                .ToListAsync();

            // Verifica se há registros existentes de atividades dos alunos
            if (!alunoAtividadesExistentes.Any())
            {
                return NotFound(); // Retorna um erro 404 se não houver registros encontrados
            }

            // Para cada registro de aluno na atividade, tenta atualizar a nota
            foreach (var alunoAtividade in alunoAtividadesExistentes)
            {
                if (alunoAtividades.TryGetValue(alunoAtividade.AlunoAtividadeId, out var alunoAtividadeAtualizada))
                {
                    // Atualiza a nota da atividade do aluno
                    alunoAtividade.Nota = alunoAtividadeAtualizada.Nota;

                    // Recupera o aluno para atualizar suas moedas
                    var aluno = await db.Usuarios.FindAsync(alunoAtividade.UsuarioId);
                    if (aluno is Aluno alunoAtual)
                    {
                        // Calcula o valor em moedas com base na nota e atualiza a quantidade de moedas do aluno
                        int valorEmMoeda = CalcularMoedas(alunoAtividade.Nota);
                        alunoAtual.Moeda += valorEmMoeda;
                        db.Usuarios.Update(alunoAtual); // Atualiza o aluno no contexto de banco de dados
                    }
                }
            }

            // Salva todas as alterações no banco de dados de forma assíncrona
            await db.SaveChangesAsync();

            // Redireciona para a página de visualização da atividade específica
            return RedirectToAction("Read", new { id = atividadeId });
        }

        // Método auxiliar para calcular o número de moedas com base na nota
        private int CalcularMoedas(decimal? nota)
        {
            return nota switch
            {
                >= 9m and <= 10m => 15, // Notas de 9 a 10 geram 15 moedas
                >= 6m and < 9m => 10,   // Notas de 6 a 8,9 geram 10 moedas
                >= 1m and < 6m => 5,    // Notas de 1 a 5,9 geram 5 moedas
                _ => 0                  // Notas abaixo de 1 geram 0 moedas
            };
        }

        // Lista todas as atividades e notas do aluno
        [HttpGet]
        public async Task<IActionResult> AlunoRead() // Utilizado para o Usuário Aluno conseguir ver suas atividades e 
        //respectivas notas
        {
            var alunoId = HttpContext.Session.GetInt32("userId"); // Certifique-se de que o userId é armazenado na sessão
            var atividadesAluno = await db.AlunoAtividades
                .Include(a => a.Atividade)
                .Where(a => a.UsuarioId == alunoId) // Filtra apenas as atividades do aluno logado
                .Select(a => new 
                {
                    a.Atividade,
                    Nota = a.Nota.HasValue ? a.Nota.Value.ToString() : "Nota não atribuída"
                })
                .ToListAsync();

            if (atividadesAluno == null || !atividadesAluno.Any())
            {
                return NotFound();
            }

            return View(atividadesAluno);
        }

    }

}
