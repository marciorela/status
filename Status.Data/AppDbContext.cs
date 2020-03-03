using Microsoft.EntityFrameworkCore;
using MR.Config;
using Status.Domain.Entities;
using System;

namespace Status.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Porta> PortasServidor { get; set; }
        public DbSet<Servidor> Servidores { get; set; }
        public DbSet<LogChecked> LogsChecked { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseMySql(Config.Read("ConnectionStrings:DbMySql"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogChecked>()
                .HasIndex(l => new { l.DateTimeChecked, l.PortId });
        }
    }
}
