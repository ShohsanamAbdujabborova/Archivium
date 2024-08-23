using Archivium.Domain.Commons;
using Archivium.Domain.Entities.Categories;
using Archivium.Domain.Entities.Commons;
using Archivium.Domain.Entities.Fields;
using Archivium.Domain.Entities.Items;
using Archivium.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivium.Domain.Entities.Collections;

public class Collection : Auditable
{
    public string Name { get; set; }
    public string Description { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
    public long CategoryId { get; set; }
    public Category Category { get; set; }
    public long? AssetId { get; set; }
    public Asset Asset { get; set; }
    public IEnumerable<Item> Items { get; set; }
    public IEnumerable<Field> Fields { get; set; }
}