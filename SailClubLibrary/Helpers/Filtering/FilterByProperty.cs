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
                //if (property.PropertyType == typeof(string) || property.PropertyType != typeof(IEnumerable))
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