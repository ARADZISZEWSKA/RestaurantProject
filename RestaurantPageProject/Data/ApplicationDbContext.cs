using Microsoft.EntityFrameworkCore;
using RestaurantPageProject.Models;

namespace RestaurantPageProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories  { get; set; } //tworzy tabele
        public DbSet<MenuItems> Menu { get; set; }
        public DbSet<Reservation> Reservations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(

                new Category
                {
                    Id = 1,
                    Name = "Wegańskie",
                    DisplayOrder = 1
                },
                new Category
                {
                    Id = 2,
                    Name = "Bezglutenowe",
                    DisplayOrder = 2
                },
                new Category
                {
                    Id = 3,
                    Name = "Bez cukru",
                    DisplayOrder = 3
                }
            );

            // MenuItems
            modelBuilder.Entity<MenuItems>().HasData(
                new MenuItems
                {
                    Id = 1,
                    Name = "Croissant z malinami",
                    Description = "Puszysty croissant wegański z nadzieniem z malin.",
                    Price = 22,
                    CategoryId = 1 // Wegańskie
                },
                new MenuItems
                {
                    Id = 2,
                    Name = "Croissant z serkiem i łososiem",
                    Description = "Delikatny croissant bezglutenowy z serkiem i wędzonym łososiem.",
                    Price = 20,
                    CategoryId = 2 // Bezglutenowe
                },
                new MenuItems
                {
                    Id = 3,
                    Name = "Bułeczka z pistacjami",
                    Description = "Klasyczna bułeczka bez dodatku cukru, obsypana chrupkimi pistacjami.",
                    Price = 18,
                    CategoryId = 3 // Bez cukru
                }
                );

        }

    }
}
