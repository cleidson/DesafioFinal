using DesafioFinal.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DesafioFinal.Infrastructure.Data
{
    
    public class DesafioFinalContext : DbContext
    {
        public DesafioFinalContext(DbContextOptions<DesafioFinalContext> options)
            : base(options) { }

        public DbSet<Cliente> Cliente { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aplica automaticamente todas as configurações do Fluent API
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DesafioFinalContext).Assembly);
        }
    }
}
