using Archivium.DataAccess.Contexts;
using Archivium.DataAccess.Repositories;
using Archivium.Domain.Entities.Categories;
using Archivium.Domain.Entities.Collections;
using Archivium.Domain.Entities.Commets;
using Archivium.Domain.Entities.Commons;
using Archivium.Domain.Entities.Fields;
using Archivium.Domain.Entities.Items;
using Archivium.Domain.Entities.Likes;
using Archivium.Domain.Entities.Tags;
using Archivium.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivium.DataAccess.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext context;
    public IRepository<User> Users { get; }
    public IRepository<Asset> Assets { get; }
    public IRepository<Category> Categories { get; set; }
    public IRepository<Collection> Collections { get; set; }
    public IRepository<Comment> Comments { get; set; }
    public IRepository<Field> Fields { get; set; }
    public IRepository<FieldValue> FieldValues { get; set; }
    public IRepository<Item> Items { get; set; }
    public IRepository<ItemTag> ItemTags { get; set; }
    public IRepository<Like> Likes { get; set; }
    public IRepository<Tag> Tags { get; set; }

    private IDbContextTransaction transaction;

    public UnitOfWork(AppDbContext context)
    {
        this.context = context;
        Users = new Repository<User>(this.context);
        Assets = new Repository<Asset>(this.context);
        Categories = new Repository<Category>(this.context);
        Collections = new Repository<Collection>(this.context);
        Comments = new Repository<Comment>(this.context);
        Fields = new Repository<Field>(this.context);
        FieldValues = new Repository<FieldValue>(this.context);
        Tags = new Repository<Tag>(this.context);
        Likes = new Repository<Like>(this.context);
        Items = new Repository<Item>(this.context);
        ItemTags = new Repository<ItemTag>(this.context);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async ValueTask<bool> SaveAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public async ValueTask BeginTransactionAsync()
    {
        transaction = await context.Database.BeginTransactionAsync();
    }

    public async ValueTask CommitTransactionAsync()
    {
        await transaction.CommitAsync();
    }
}

