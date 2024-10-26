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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurações para Usuario e Aluno
        modelBuilder.Entity<Usuario>().HasKey(u => u.UsuarioId);
        modelBuilder.Entity<Aluno>().HasBaseType<Usuario>();

        // Configurações para AlunoAtividade
        modelBuilder.Entity<AlunoAtividade>()
            .HasOne(aa => aa.Atividade)
            .WithMany()
            .HasForeignKey(aa => aa.AtividadeId);

        modelBuilder.Entity<AlunoAtividade>()
            .HasOne(aa => aa.Aluno)
            .WithMany()
            .HasForeignKey(aa => aa.UsuarioId);

        // Configurações para ProdutoPedido
        modelBuilder.Entity<ProdutoPedido>()
            .HasKey(pp => new { pp.ProdutoId, pp.PedidoId });

        modelBuilder.Entity<ProdutoPedido>()
            .HasOne(pp => pp.Produto)
            .WithMany(p => p.ProdutoPedidos)
            .HasForeignKey(pp => pp.ProdutoId);

        modelBuilder.Entity<ProdutoPedido>()
            .HasOne(pp => pp.Pedido)
           .WithMany(p => p.ProdutoPedidos)
            .HasForeignKey(pp => pp.PedidoId);

        base.OnModelCreating(modelBuilder);
    }
   
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<ProdutoPedido> ProdutoPedidos { get; set; }
   
    public DbSet<Pedido> Pedidos { get; set; }
}