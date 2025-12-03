using System;
using System.Collections.Generic;
using Katchau_Back.Models;
using Microsoft.EntityFrameworkCore;

namespace Katchau_Back.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Carrinho> Carrinhos { get; set; }

    public virtual DbSet<Produto> Produtos { get; set; }

    public virtual DbSet<Regra> Regras { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:ConexaoPadrao");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carrinho>(entity =>
        {
            entity.HasKey(e => e.id_carrinho).HasName("PK__Carrinho__248DFAAE539BEBE7");

            entity.ToTable("Carrinho");

            entity.HasOne(d => d.id_produtoNavigation).WithMany(p => p.Carrinhos)
                .HasForeignKey(d => d.id_produto)
                .HasConstraintName("FK_Carrinho_Produto");

            entity.HasOne(d => d.id_usuarioNavigation).WithMany(p => p.Carrinhos)
                .HasForeignKey(d => d.id_usuario)
                .HasConstraintName("FK_Carrinho_Usuario");
        });

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.HasKey(e => e.id_produto).HasName("PK__Produto__BA38A6B8554F13DB");

            entity.ToTable("Produto");

            entity.Property(e => e.Categoria)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nome).HasMaxLength(255);
            entity.Property(e => e.Qt_Clique).HasDefaultValue(0);
        });

        modelBuilder.Entity<Regra>(entity =>
        {
            entity.HasKey(e => e.id_regra).HasName("PK__Regra__46D174CE041FA386");

            entity.ToTable("Regra");

            entity.Property(e => e.Nome).HasMaxLength(255);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.id_usuario).HasName("PK__Usuario__4E3E04AD4BD9E379");

            entity.ToTable("Usuario");

            entity.Property(e => e.Bairro).HasMaxLength(255);
            entity.Property(e => e.CPF).HasMaxLength(20);
            entity.Property(e => e.Cidade).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Estado).HasMaxLength(2);
            entity.Property(e => e.Nome).HasMaxLength(255);
            entity.Property(e => e.NumeroCasa)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Rua).HasMaxLength(255);
            entity.Property(e => e.Senha)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Telefone).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
