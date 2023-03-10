namespace Stix.Core;

public class EntityBase
{
    public EntityBase(string id)
    {
        this.id = id;
    }

    // ReSharper disable once InconsistentNaming
    public string id { get; set; }
}