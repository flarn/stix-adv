namespace Stix.Core;

public static class IdGenerator
{
    public static string Generate<T>()
    {
        ReadOnlySpan<char> typeName = typeof(T).Name;

        if (MemoryExtensions.EndsWith(typeName, "Entity", StringComparison.Ordinal) && typeName.Length > 5)
        {
            Span<char> lower = stackalloc char[typeName.Length - 6];
            MemoryExtensions.ToLowerInvariant(typeName[..^6], lower);

            return $"{lower}--{Guid.NewGuid()}";
        }

        //could be spanifed
        return $"{typeof(T).Name.ToLowerInvariant()}--{Guid.NewGuid()}";
    }
}