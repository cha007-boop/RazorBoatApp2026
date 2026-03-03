using System.Collections;
using System.Reflection;

public class FilterByProperty<T>
{
    public string? PropertyName { get; set; }
    public string? FilterCriteria { get; set; }
    public FilterByProperty(string? propertyName, string? filterCriteria)
    {
        PropertyName = propertyName;
        FilterCriteria = filterCriteria;
    }

    /// <summary>
    /// Method for checking if the item matches the filter criteria based on the specified property name. 
    /// If no property name is provided, it checks all string properties of the item for a match.
    /// </summary>
    /// <param name="item">The item being checked</param>
    /// <returns>True if the specified property (or any property if no property name was given) mathces with the given value, </returns>
    /// <exception cref="ArgumentException"></exception>
    public bool IsMatch(T item)
    {
        if (string.IsNullOrEmpty(FilterCriteria))
            return true; // No filter criteria means all items match

        if (!string.IsNullOrEmpty(PropertyName))
        {
            PropertyInfo? propertyInfo = typeof(T).GetProperty(PropertyName);
            if (propertyInfo == null)
                throw new ArgumentException($"Property '{PropertyName}' does not exist on type '{typeof(T).Name}'.");
            var value = propertyInfo.GetValue(item)?.ToString();

            return value != null && value.Contains(FilterCriteria, StringComparison.OrdinalIgnoreCase);
        }
        else
        {
            // If no property name is provided, check all string properties
            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                //if (property.PropertyType == typeof(string))
                {
                    var value = property.GetValue(item)?.ToString();
                    if (value != null && value.Contains(FilterCriteria, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
            }
            return false; // No match found in any string property
        }
    }
}