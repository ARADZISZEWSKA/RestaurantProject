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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(

                 new Category
                 {
                     Id = 1,
                     Name = "Croissanty słodkie",
                     DisplayOrder = 1
                 },
                 new Category
                 {
                     Id = 2,
                     Name = "Croissanty słone",
                     DisplayOrder = 2
                 },
                 new Category
                 {
                     Id = 3,
                     Name = "Bułeczki",
                     DisplayOrder = 3
                 }
 );

            modelBuilder.Entity<MenuItems>().HasData(

                new MenuItems
                {
                    Id = 1,
                    Name = "Croissanty słodkie z mascarpone i malinami",
                    Description = "Puszyste croissanty nadziewane kremowym mascarpone i świeżymi malinami.",
                    Price = 20
                },
                new MenuItems
                {
                    Id = 2,
                    Name = "Croissanty słone z serkiem i łososiem",
                    Description = "Delikatne croissanty z serkiem i wędzonym łososiem.",
                    Price = 18
                },
                new MenuItems
                {
                    Id = 3,
                    Name = "Bułeczka z pistacjami",
                    Description = "Klasyczna bułeczka obsypana chrupkimi pistacjami.",
                    Price = 15
                }
            );

        }

    }
}
