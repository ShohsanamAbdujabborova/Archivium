using Microsoft.EntityFrameworkCore;

namespace Archivium.DataAccess.EntityConfigurations.Commons;

public interface IEntityConfiguration
{
    void Configure(ModelBuilder modelBuilder);
    void SeedData(ModelBuilder modelBuilder);
}