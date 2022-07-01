
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ristorante.ProvaMVC.Core.Entities;

namespace Ristorante.ProvaMVC.Core.RepositoryEF.Configuration
{
    internal class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menu");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.HasMany(x => x.Piatti).WithOne(x => x.Menu).HasForeignKey(x => x.IdMenu);
        }
    }
}