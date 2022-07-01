
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ristorante.ProvaMVC.Core.Entities;

namespace Ristorante.ProvaMVC.Core.RepositoryEF.Configuration
{
    internal class PiattoConfiguration : IEntityTypeConfiguration<Piatto>
    {
        public void Configure(EntityTypeBuilder<Piatto> builder)
        {
            builder.ToTable("Piatto");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Descrizione).IsRequired();
            builder.Property(x => x.Tipologia).IsRequired();
            builder.Property(x => x.Prezzo).IsRequired();
            builder.HasOne(x => x.Menu).WithMany(x => x.Piatti).HasForeignKey(x => x.IdMenu);
        }
    }
}