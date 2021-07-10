using Microsoft.EntityFrameworkCore;
using webDotNet.Models;

namespace dotnetcoreWebapp.Database
{
    public class AplicationDB : DbContext
    {
        public DbSet<Pagamento> Pagamentos {get; set;}

        public AplicationDB(DbContextOptions<AplicationDB> options) : base(options){

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Pagamento>().Property(p => p.Descriacao).HasMaxLength(500);
        }
    }
}