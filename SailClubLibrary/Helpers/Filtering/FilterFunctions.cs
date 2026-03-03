public class FilterFunctions
{
    /// <summary>
    /// Filters a collection of items based on a specified property and filter criteria.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items">The collection to filter</param>
    /// <param name="propertyName">The property name that is checked</param>
    /// <param name="filterCriteria">What the value of the property is being compared with</param>
    /// <returns>A collection containing only the items for which the specified property has the specified value</returns>
    public static List<T> FilterByProperty<T>(IEnumerable<T> items, string propertyName, string filterCriteria)
    {
        var filter = new FilterByProperty<T>(propertyName, filterCriteria);
        return FilterByFunc<T>(items, filter.IsMatch).ToList();
        //return items.Where(item => filter.IsMatch(item)).ToList();
    }

    /// <summary>
    /// Filters a collection of items based on multiple property filters. 
    /// Each filter is represented as a key-value pair in the dictionary, 
    /// where the key is the property name and the value is the filter criteria. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items">The collection to filter</param>
    /// <param name="filters">Dictionary with keys corresponding to property names and values for filter criteria</param>
    /// <returns>A collection containing only the items that match all the specified filters.</returns>
    public static IEnumerable<T> FilterByMultipleProperties<T>(IEnumerable<T> items, Dictionary<string, string> filters)
    {
        var filteredItems = items;

        filteredItems = filteredItems.Where(item => filters.All(filter =>
        {
            var propertyName = filter.Key;
            var filterCriteria = filter.Value;
            var filterByProperty = new FilterByProperty<T>(propertyName, filterCriteria);
            return filterByProperty.IsMatch(item);
        }));

        #region Alternative using the FilterByFuncMultiple method
        //filteredItems = FilterByFuncMultiple<T>(filteredItems, filters.Select<KeyValuePair<string, string>, Func<T, bool>>(filter =>
        //{
        //    var propertyName = filter.Key;
        //    var filterCriteria = filter.Value;
        //    var filterByProperty = new FilterByProperty<T>(propertyName, filterCriteria);
        //    return filterByProperty.IsMatch;
        //}));
        #endregion
        return filteredItems;
    }

    /// <summary>
    /// Filters a collection of items based on a provided filter function.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items">The collection to filter</param>
    /// <param name="filterFunc">The function to filter by</param>
    /// <returns>A collection containing only the items that match the filter</returns>
    public static IEnumerable<T> FilterByFunc<T>(IEnumerable<T> items, Func<T, bool> filterFunc)
    {
        return items.Where(filterFunc);
    }

    /// <summary>
    /// Filters a collection of items based on multiple filter functions.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items">The collection to filter</param>
    /// <param name="filterFuncs">The collection of functions to filter by</param>
    /// <returns>A collection containing only the items that match all the specified filters</returns>
    public static IEnumerable<T> FilterByFuncMultiple<T>(IEnumerable<T> items, IEnumerable<Func<T, bool>> filterFuncs)
    {
        var filteredItems = items;

        filteredItems = filteredItems.Where(item => filterFuncs.All(func => func(item)));

        return filteredItems;
    }
}