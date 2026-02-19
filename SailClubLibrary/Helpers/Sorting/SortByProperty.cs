public class SortByProperty
{

    public IOrderedEnumerable<T> Sort<T>(IEnumerable<T> items,string propertyName, string sortOrder)
    {
        if (string.IsNullOrEmpty(propertyName))
            throw new ArgumentException("Property name cannot be null or empty.");
        var propertyInfo = typeof(T).GetProperty(propertyName);
        if (propertyInfo == null)
            throw new ArgumentException($"Property '{propertyName}' does not exist on type '{typeof(T).Name}'.");

        return sortOrder == "asc"
            ? items.OrderBy(item => propertyInfo.GetValue(item))
            : items.OrderByDescending(item => propertyInfo.GetValue(item));
    }
}

