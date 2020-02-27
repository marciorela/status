﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Status.Data;

namespace Status.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Status.Domain.Entities.LogChecked", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateTimeChecked")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Obs")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid>("PortId")
                        .HasColumnType("char(36)");

                    b.Property<int>("PortNumber")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("LogsChecked");
                });

            modelBuilder.Entity("Status.Domain.Entities.Porta", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("CheckInterval")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataAlt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataInc")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<Guid>("ServidorId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("ServidorId");

                    b.ToTable("PortasServidor");
                });

            modelBuilder.Entity("Status.Domain.Entities.Servidor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DataAlt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataInc")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Host")
                        .IsRequired()
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Servidores");
                });

            modelBuilder.Entity("Status.Domain.Entities.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DataAlt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataInc")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Status.Domain.Entities.Porta", b =>
                {
                    b.HasOne("Status.Domain.Entities.Servidor", "Servidor")
                        .WithMany("Portas")
                        .HasForeignKey("ServidorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Status.Domain.Entities.Servidor", b =>
                {
                    b.HasOne("Status.Domain.Entities.Usuario", "Usuario")
                        .WithMany("Servidores")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}