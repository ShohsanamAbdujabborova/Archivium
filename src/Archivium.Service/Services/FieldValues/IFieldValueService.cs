using Archivium.Domain.Entities.Fields;
using Archivium.Service.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivium.Service.Services.FieldValues;
public interface IFieldValueService
{
    ValueTask<FieldValue> CreateAsync(FieldValue fieldValue);
    ValueTask<FieldValue> UpdateAsync(long id, FieldValue fieldValue);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<FieldValue> GetByIdAsync(long id);
    ValueTask<IEnumerable<FieldValue>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}


