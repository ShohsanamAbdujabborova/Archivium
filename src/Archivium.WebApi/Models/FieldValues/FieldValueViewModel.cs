namespace Archivium.WebApi.Models.FieldValues;

public class FieldValueViewModel
{
    public long Id { get; set; }
    public string Value { get; set; }
    public long ItemId { get; set; }
    public long FieldId { get; set; }
}
