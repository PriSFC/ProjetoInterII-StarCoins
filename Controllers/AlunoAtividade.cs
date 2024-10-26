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

        public async Task<IActionResult> Index()
        {
            var alunoAtividades = await db.AlunoAtividades
                .Include(aa => aa.Atividade)
                .Include(aa => aa.Aluno)
                .ToListAsync();

            return View(alunoAtividades);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var alunoAtividade = await db.AlunoAtividades.FindAsync(id);
            if (alunoAtividade == null)
            {
                return NotFound();
            }

            return View(alunoAtividade);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AlunoAtividade model)
        {
            if (id != model.AlunoAtividadeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                db.Update(model);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
