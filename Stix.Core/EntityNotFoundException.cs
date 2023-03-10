namespace Stix.Core
{
    public class EntityNotFoundException : ApplicationException
    {

        public EntityNotFoundException(string id)
        {
            EntityId = id;
        }

        public string EntityId { get; }
    }
}
