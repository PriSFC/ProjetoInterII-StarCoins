using StarCoins.Models;
using Microsoft.EntityFrameworkCore;

public class StarCoinsDatabase: DbContext {

    public StarCoinsDatabase(DbContextOptions options) : base(options) {}
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<AdministradorModel> Administradores { get; set; }
    public DbSet<Professor> Professores { get; set; }
    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Turma> Turmas { get; set; }
    public DbSet<Atividade> Atividades { get; set; }
    public DbSet<AlunoAtividade> AlunoAtividades { get; set; }
   
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<ProdutoPedido> ProdutoPedidos { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProdutoPedido>()
            .HasKey(pp => new { pp.ProdutoId, pp.PedidoId });
    }
    public DbSet<Pedido> Pedidos { get; set; }
}