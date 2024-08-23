using Archivium.Domain.Entities.Fields;
using Archivium.Service.Configurations;

namespace Archivium.Service.Services.Fields;

public interface IFieldService
{
    ValueTask<Field> CreateAsync(Field field);
    ValueTask<Field> UpdateAsync(long id, Field field);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Field> GetByIdAsync(long id);
    ValueTask<IEnumerable<Field>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
