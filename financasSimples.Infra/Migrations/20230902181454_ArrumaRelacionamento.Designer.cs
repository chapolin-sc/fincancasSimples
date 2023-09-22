﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using financasSimples.Infra.Context;

#nullable disable

namespace financasSimples.Infra.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230902181454_ArrumaRelacionamento")]
    partial class ArrumaRelacionamento
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("financasSimples.Domain.Entity.ItensVendas", b =>
                {
                    b.Property<int>("IdItensVendas")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("IdProduto")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("IdVenda")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<decimal>("IvCusto")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal>("IvPreco")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("IvQuantidade")
                        .HasColumnType("int");

                    b.HasKey("IdItensVendas");

                    b.HasIndex("IdProduto")
                        .IsUnique();

                    b.HasIndex("IdVenda");

                    b.ToTable("ItensVenda");
                });

            modelBuilder.Entity("financasSimples.Domain.Entity.Produtos", b =>
                {
                    b.Property<int>("IdProduto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("DescricaoProduto")
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("ImagemProduto")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("MarcaProduto")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("NomeProduto")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("VolumeProduto")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.HasKey("IdProduto");

                    b.ToTable("Produto");
                });

            modelBuilder.Entity("financasSimples.Domain.Entity.Vendas", b =>
                {
                    b.Property<int>("IdVenda")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateOnly>("DataVenda")
                        .HasColumnType("date");

                    b.HasKey("IdVenda");

                    b.ToTable("Venda");
                });

            modelBuilder.Entity("financasSimples.Domain.Entity.ItensVendas", b =>
                {
                    b.HasOne("financasSimples.Domain.Entity.Produtos", "Produto")
                        .WithOne("ItensVendas")
                        .HasForeignKey("financasSimples.Domain.Entity.ItensVendas", "IdProduto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("financasSimples.Domain.Entity.Vendas", "Venda")
                        .WithMany("ItensVendas")
                        .HasForeignKey("IdVenda")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Produto");

                    b.Navigation("Venda");
                });

            modelBuilder.Entity("financasSimples.Domain.Entity.Produtos", b =>
                {
                    b.Navigation("ItensVendas");
                });

            modelBuilder.Entity("financasSimples.Domain.Entity.Vendas", b =>
                {
                    b.Navigation("ItensVendas");
                });
#pragma warning restore 612, 618
        }
    }
}
