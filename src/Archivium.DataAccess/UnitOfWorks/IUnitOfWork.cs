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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivium.DataAccess.UnitOfWorks;

public interface IUnitOfWork : IDisposable
{
    IRepository<User> Users { get; }
    IRepository<Asset> Assets { get; }
    IRepository<Category> Categories { get; set; }
    IRepository<Collection> Collections { get; set; }
    IRepository<Comment> Comments { get; set; }
    IRepository<Field> Fields { get; set; }
    IRepository<FieldValue> FieldValues { get; set; }
    IRepository<Item> Items { get; set; }
    IRepository<ItemTag> ItemTags { get; set; }
    IRepository<Like> Likes { get; set; }
    IRepository<Tag> Tags { get; set; }
    ValueTask<bool> SaveAsync();
    ValueTask BeginTransactionAsync();
    ValueTask CommitTransactionAsync();
}