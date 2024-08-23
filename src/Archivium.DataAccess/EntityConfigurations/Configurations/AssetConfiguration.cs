﻿using Archivium.DataAccess.EntityConfigurations.Commons;
using Archivium.Domain.Entities.Commons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Archivium.DataAccess.EntityConfigurations.Configurations;

public class AssetConfiguration : IEntityConfiguration
{
    public void Configure(ModelBuilder modelBuilder)
    { }

    public void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asset>().HasData(
            new Asset() { Id = 1, Name = "Picture", Path = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.vecteezy.com%2Ffree-photos%2Fpicture&psig=AOvVaw3XaUHI9Jhqr2Yekc8F0A7X&ust=1715337265006000&source=images&cd=vfe&opi=89978449&ved=0CBIQjRxqFwoTCNjGn7WvgIYDFQAAAAAdAAAAABAE", CreatedAt = DateTime.UtcNow },
            new Asset() { Id = 2, Name = "Picture2", Path = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.vecteezy.com%2Ffree-photos%2Fpicture&psig=AOvVaw3XaUHI9Jhqr2Yekc8F0A7X&ust=1715337265006000&source=images&cd=vfe&opi=89978449&ved=0CBIQjRxqFwoTCNjGn7WvgIYDFQAAAAAdAAAAABAE", CreatedAt = DateTime.UtcNow });
    }
}