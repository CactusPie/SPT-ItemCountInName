namespace CactusPie.ItemCountInName.Services.Data
{
    public sealed class FilterCheckboxData
    {
        public string ParentId { get; }

        public string CategoryName { get; }

        public bool DefaultValue { get; }

        public FilterCheckboxData(string parentId, string categoryName, bool defaultValue)
        {
            ParentId = parentId;
            CategoryName = categoryName;
            DefaultValue = defaultValue;
        }
    }
}