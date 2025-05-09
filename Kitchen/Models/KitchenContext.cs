using Microsoft.EntityFrameworkCore;

namespace Kitchen.Models
{
    public class KitchenContext : DbContext
    {
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Chef> Chefs { get; set; }
        public KitchenContext()
        {
            
        }
        public KitchenContext(DbContextOptions option) : base(option)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Kitchen;Integrated Security=True;Encrypt=False");
        }
    }
}
