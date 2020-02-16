using Microsoft.EntityFrameworkCore;
using MR.Config;
using Status.Domain.Entities;
using System;

namespace Status.Data
{
    public class AppContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Porta> PortasServidor { get; set; }
        public DbSet<Servidor> Servidores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseMySql(Config.Read("ConnectionStrings:DbMySql"));
        }
    }
}
