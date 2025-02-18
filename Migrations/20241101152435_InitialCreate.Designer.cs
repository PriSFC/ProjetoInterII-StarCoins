﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace StarCoins.Migrations
{
    [DbContext(typeof(StarCoinsDatabase))]
    [Migration("20241101152435_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StarCoins.Models.AlunoAtividade", b =>
                {
                    b.Property<int>("AlunoAtividadeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AlunoAtividadeId"));

                    b.Property<int>("AtividadeId")
                        .HasColumnType("int");

                    b.Property<DateOnly>("DataRealizacao")
                        .HasColumnType("date");

                    b.Property<decimal?>("Nota")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("AlunoAtividadeId");

                    b.HasIndex("AtividadeId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("AlunoAtividades");
                });

            modelBuilder.Entity("StarCoins.Models.Atividade", b =>
                {
                    b.Property<int>("AtividadeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AtividadeId"));

                    b.Property<DateOnly>("DataEntrega")
                        .HasColumnType("date");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Moeda")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AtividadeId");

                    b.ToTable("Atividades");
                });

            modelBuilder.Entity("StarCoins.Models.Pedido", b =>
                {
                    b.Property<int>("PedidoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PedidoId"));

                    b.Property<DateOnly>("DataPedido")
                        .HasColumnType("date");

                    b.Property<int>("Moeda")
                        .HasColumnType("int");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ticket")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("PedidoId");

                    b.HasIndex("ProdutoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("StarCoins.Models.Produto", b =>
                {
                    b.Property<int>("ProdutoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProdutoId"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Moeda")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("TipoProduto")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.HasKey("ProdutoId");

                    b.ToTable("Produtos");

                    b.HasDiscriminator<string>("TipoProduto").HasValue("Produto");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("StarCoins.Models.Turma", b =>
                {
                    b.Property<int>("TurmaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TurmaId"));

                    b.Property<string>("Ano")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TurmaId");

                    b.ToTable("Turmas");
                });

            modelBuilder.Entity("StarCoins.Models.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UsuarioId"));

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Perfil")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UsuarioId");

                    b.ToTable("Usuarios");

                    b.HasDiscriminator().HasValue("Usuario");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("StarCoins.Models.ProdutoDigital", b =>
                {
                    b.HasBaseType("StarCoins.Models.Produto");

                    b.Property<float>("TamanhoArquivo")
                        .HasColumnType("real");

                    b.HasDiscriminator().HasValue("Digital");
                });

            modelBuilder.Entity("StarCoins.Models.ProdutoFisico", b =>
                {
                    b.HasBaseType("StarCoins.Models.Produto");

                    b.Property<float>("Peso")
                        .HasColumnType("real");

                    b.HasDiscriminator().HasValue("Fisico");
                });

            modelBuilder.Entity("StarCoins.Models.AdministradorModel", b =>
                {
                    b.HasBaseType("StarCoins.Models.Usuario");

                    b.HasDiscriminator().HasValue("AdministradorModel");
                });

            modelBuilder.Entity("StarCoins.Models.Aluno", b =>
                {
                    b.HasBaseType("StarCoins.Models.Usuario");

                    b.Property<int>("Moeda")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Aluno");
                });

            modelBuilder.Entity("StarCoins.Models.Professor", b =>
                {
                    b.HasBaseType("StarCoins.Models.Usuario");

                    b.HasDiscriminator().HasValue("Professor");
                });

            modelBuilder.Entity("StarCoins.Models.AlunoAtividade", b =>
                {
                    b.HasOne("StarCoins.Models.Atividade", "Atividade")
                        .WithMany()
                        .HasForeignKey("AtividadeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StarCoins.Models.Aluno", "Aluno")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Aluno");

                    b.Navigation("Atividade");
                });

            modelBuilder.Entity("StarCoins.Models.Pedido", b =>
                {
                    b.HasOne("StarCoins.Models.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StarCoins.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Produto");

                    b.Navigation("Usuario");
                });
#pragma warning restore 612, 618
        }
    }
}
