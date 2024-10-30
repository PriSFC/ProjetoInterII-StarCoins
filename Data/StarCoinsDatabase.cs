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
   
    public DbSet<Pedido> Pedidos { get; set; }

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
            .WithMany() // Se necessário, especifique o nome da coleção
            .HasForeignKey(aa => aa.UsuarioId) // Explicitamente o nome da chave estrangeira
            .HasPrincipalKey(a => a.UsuarioId); // Garante que o relacionamento seja baseado no campo correto

        //Configurações para Produto (Físico e Digital)
        modelBuilder.Entity<Produto>()
            .HasDiscriminator<string>("TipoProduto")
            .HasValue<ProdutoFisico>("Fisico")
            .HasValue<ProdutoDigital>("Digital");

        base.OnModelCreating(modelBuilder);
    }
   
    
}