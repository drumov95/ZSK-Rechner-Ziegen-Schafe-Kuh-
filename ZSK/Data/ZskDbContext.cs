using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.VisualBasic;
using ZSK.Models;

namespace ZSK.Data
{
    public class ZskDbContext : DbContext
    {
        public ZskDbContext(DbContextOptions<ZskDbContext> options) : base(options) { }

        public DbSet<ZSK.Models.Conversion> conversions { get; set; }
        public DbSet<AnimalRate> animalRates { get; set; }


        // Hier habe ich geübt, wie man Beziuhngen anpassen kann und DeleteBehavior zwischen Entitys implementieren kann
        // Man darf nur einmal Instanz von Classe AnimalRate anlegen. (IsUnique)

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ZSK.Models.Conversion>()
                .HasOne(c => c.AnimalRate)
                .WithMany(a => a.Conversions)
                .HasForeignKey(c => c.AnimalRateId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
