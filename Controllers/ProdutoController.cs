using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using StarCoins.Models;

namespace StarCoins.Controllers
{
    // http://localhost:????/produto
    public class ProdutoController : Controller
    {
        private readonly StarCoinsDatabase db;

        public ProdutoController(StarCoinsDatabase db)
        {
            this.db = db;
        }

        // Produtos para o Adm
        public ActionResult Read()
        {
            return View(db.Produtos.ToList()); // ~ SELECT * FROM Produtos
        }

        // Método de Comprar para o Aluno - Lista de Produtos
        public ActionResult Comprar()
        {
            return View(db.Produtos.ToList()); // ~ SELECT * FROM Produtos
        }
        
        [HttpGet]
        public IActionResult CreateFisico()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateDigital()
        {
            return View();
        }

        // Criar produtos físicos
        [HttpPost]
        public ActionResult CreateFisico(ProdutoFisico model)
        {
            if (ModelState.IsValid){
                db.Produtos.Add(model); // ~INSERT INTO Produtos VALUES (model.Nome...)
                db.SaveChanges(); // ~commit
                return RedirectToAction("Read");
            }
            return View(model);
        }

        // Criar produtos digitais
        [HttpPost]
        public ActionResult CreateDigital(ProdutoDigital model)
        {
            if (ModelState.IsValid){
                db.Produtos.Add(model); // ~INSERT INTO Produtos VALUES (model.Nome...)
                db.SaveChanges(); // ~commit
                return RedirectToAction("Read");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var produto = db.Produtos.Single(e => e.ProdutoId == id); // SELECT * FROM Produtos WHERE ProdutoId = id
            db.Produtos.Remove(produto); // DELETE FROM Produtos WHERE ProdutoId = id
            db.SaveChanges(); // commit
            return RedirectToAction("Read");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            // Obtém o produto pelo ID
            Produto produto = db.Produtos.Single(e => e.ProdutoId == id);

            // Verifica o tipo do produto e retorna a view apropriada
            if (produto is ProdutoFisico)
            {
                return View("UpdateFisico", produto); // Retorna a view para produto físico
            }
            else if (produto is ProdutoDigital)
            {
                return View("UpdateDigital", produto); // Retorna a view para produto digital
            }

            // Se não for nenhum dos tipos, pode-se redirecionar ou lançar uma exceção
            return NotFound(); 
        }

        [HttpPost]
        public ActionResult UpdateDigital(int id, ProdutoDigital model)
        {
            if (ModelState.IsValid){
                var produto = db.Produtos.Single(e => e.ProdutoId == id);

                produto.Nome = model.Nome;
                produto.Descricao = model.Descricao;
                produto.Moeda = model.Moeda;
                produto.Quantidade = model.Quantidade;
                produto.Status = model.Status;

                if (produto is ProdutoDigital digital)
                digital.TamanhoArquivo = model.TamanhoArquivo;

                db.SaveChanges();
                return RedirectToAction("Read");
            }
            // Passa o id para a view para que possa ser usado no formulário
            ViewBag.Id = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateFisico(int id, ProdutoFisico model)
        {
            if (ModelState.IsValid){
                var produto = db.Produtos.Single(e => e.ProdutoId == id);

                produto.Nome = model.Nome;
                produto.Descricao = model.Descricao;
                produto.Moeda = model.Moeda;
                produto.Quantidade = model.Quantidade;
                produto.Status = model.Status;

                if (produto is ProdutoFisico fisico)
                fisico.Peso = model.Peso;

                db.SaveChanges();
                return RedirectToAction("Read");
            }
            // Passa o id para a view para que possa ser usado no formulário
            ViewBag.Id = id;
            return View(model);
        }

        // Detalhe do Produto para Aluno
        [HttpGet]
        public IActionResult ExibirDetalhes(int id)
        {
            // Buscar o produto no banco de dados pelo ID
            var produto = db.Produtos.Single(e => e.ProdutoId == id);

            if (produto == null)
            {
                return NotFound();
            }

            // Verificar o tipo de produto e chamar ExibirDetalhes
            if (produto is ProdutoDigital produtoDigital)
            {
                produtoDigital.ExibirDetalhes();
                return View("DetalhesProdutoDigital", produtoDigital); // Redireciona para a View do Produto Digital
            }
            else if (produto is ProdutoFisico produtoFisico)
            {
                produtoFisico.ExibirDetalhes();
                return View("DetalhesProdutoFisico", produtoFisico); // Redireciona para a View do Produto Físico
            }

            return NotFound("Tipo de produto desconhecido.");
        }

        [HttpGet]
        public IActionResult ConfirmarCompra(int id)
        {
            Produto produto = db.Produtos.Single(e => e.ProdutoId == id);
            if (produto is ProdutoFisico)
            {
                return View("ConfirmarCompra", produto); // Retorna a view para produto físico espeçifico
            }
            else if (produto is ProdutoDigital)
            {
                return View("ConfirmarCompra", produto); // Retorna a view para produto digital específico
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> PedidoCreate(int id)
        {
            // Verifica se o usuário está logado
            var userId = HttpContext.Session.GetInt32("userId");
            if (userId == null)
                return RedirectToAction("Login", "Usuario");

            // Busca o produto pelo id do produto
            var produto = await db.Produtos.FindAsync(id);
            // Busca o aluno pelo id do usuário
            var usuario = await db.Usuarios.FindAsync(userId);

            // Verifica se o produto e o aluno foram encontrados
            if (produto == null || usuario == null)
                return NotFound();

            // Verifica se o usuário é do tipo Aluno
            if (usuario is not Aluno aluno)
                return BadRequest("Apenas alunos podem realizar essa compra.");

            // Verifica se o aluno tem moedas suficientes para realizar a compra
            if (aluno.Moeda < produto.Moeda)
                return View("SaldoInsuficiente", produto);

            // Cria o pedido com as informações fornecidas
            var pedido = new Pedido
            {
                UsuarioId = aluno.UsuarioId,
                ProdutoId = produto.ProdutoId,
                DataPedido = DateOnly.FromDateTime(DateTime.Now),
                Moeda = produto.Moeda, // Define o valor da moeda do produto no pedido
                Ticket = Guid.NewGuid().ToString(), // Gera um ticket único para o pedido
                Status = "1" // Status "1" para indicar "em andamento"
            };

            // Atualiza o saldo de moedas do aluno
            aluno.Moeda -= produto.Moeda;
            db.Usuarios.Update(aluno);

            // Atualiza a quantidade de produtos
            produto.Quantidade -= 1;
            db.Produtos.Update(produto);

            // Adiciona o pedido ao banco de dados e salva as alterações
            db.Pedidos.Add(pedido);
            await db.SaveChangesAsync();

            // Redireciona para a página de verificação do pedido, passando o PedidoId
            return RedirectToAction("Create", "Pedido", new { pedidoId = pedido.PedidoId });
        }

    }
    
}
