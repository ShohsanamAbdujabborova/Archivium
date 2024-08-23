namespace Archivium.WebApi.Models.FieldValues;

public class FieldValueCreateModel
{
    public string Value { get; set; }
    public long ItemId { get; set; }
    public long FieldId { get; set; }
}
