
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ristorante.ProvaMVC.Core.Entities;

namespace Ristorante.ProvaMVC.Core.RepositoryEF.Configuration
{
    internal class UtenteConfiguration : IEntityTypeConfiguration<Utente>
    {
        public void Configure(EntityTypeBuilder<Utente> builder)
        {

            builder.ToTable("Utente");
            builder.HasKey(x => x.Id);
            builder.HasIndex(u => u.Username).IsUnique();
            builder.Property(u => u.Username).IsRequired();
            builder.Property(u => u.Password).IsRequired();
            builder.Property(u => u.Role).IsRequired();

            builder.HasData(
               new Utente
               {
                   Id = 1,
                   Username = "renata@mail.it",
                   Password="1234",
                   Role = Role.Ristoratore
               },
               new Utente
               {
                   Id = 2,
                   Username = "mario@mail.it",                   
                   Password = "12345",
                   Role = Role.Utente
               });
        }
    }
}