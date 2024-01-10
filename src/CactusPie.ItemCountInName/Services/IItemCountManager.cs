using System.Collections.Generic;

namespace CactusPie.ItemCountInName.Services
{
    public interface IItemCountManager
    {
        IReadOnlyDictionary<string, ItemCountData> ItemCounts { get; }

        void ReloadItemCounts();
    }
}