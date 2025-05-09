using Microsoft.EntityFrameworkCore;

namespace Kitchen.Models
{
    public class KitchenContext : DbContext
    {
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Chef> Chefs { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public KitchenContext()
        {
            
        }
        public KitchenContext(DbContextOptions option) : base(option)
        {
            
        }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Order> Orders { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-KRVT3AP;Initial Catalog=Kitchen;Integrated Security=True;Encrypt=False");
        }
    }
}
