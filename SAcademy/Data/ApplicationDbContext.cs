using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SAcademy.Models;

namespace SAcademy.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Menu> Menus { get; set; }
        public DbSet<Header> Headers { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<FormationPage> FormationPages { get; set; }
        public DbSet<InscriptionPage> InscriptionPages { get; set; }
        public DbSet<Formation> Formations { get; set; }
        public DbSet<Mode> Modes { get; set; }
        public DbSet<FType> FTypes { get; set; }
        public DbSet<Ville> Villes { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Footer> Footers { get; set; }
        public DbSet<Offre> Offres { get; set; }
    }
}