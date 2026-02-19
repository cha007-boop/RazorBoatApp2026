public class FilterFunctions
{
    public static List<T> FilterByProperty<T>(List<T> items, string propertyName, string filterCriteria)
    {
        var filter = new FilterByProperty<T>(propertyName, filterCriteria);
        return items.Where(item => filter.IsMatch(item)).ToList();
    }

    public static List<T> FilterByMultipleProperties<T>(List<T> items, Dictionary<string, string> filters)
    {
        var filteredItems = items.AsEnumerable();
        foreach (var filter in filters)
        {
            var propertyName = filter.Key;
            var filterCriteria = filter.Value;
            var filterByProperty = new FilterByProperty<T>(propertyName, filterCriteria);
            filteredItems = filteredItems.Where(item => filterByProperty.IsMatch(item));
        }
        return filteredItems.ToList();
    }

    public static List<T> FilterByFunc<T>(List<T> items, Func<T, bool> filterFunc)
    {
        return items.Where(filterFunc).ToList();
    }

    // FilterByFuncMultiple allows for multiple filter functions to be applied in sequence
    public static List<T> FilterByFuncMultiple<T>(List<T> items, List<Func<T, bool>> filterFuncs)
    {
        var filteredItems = items.AsEnumerable();
        foreach (var filterFunc in filterFuncs)
        {
            filteredItems = filteredItems.Where(filterFunc);
        }
        return filteredItems.ToList();
    }
}