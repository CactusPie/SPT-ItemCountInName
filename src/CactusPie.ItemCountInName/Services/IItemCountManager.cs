using System.Collections.Generic;
using EFT.InventoryLogic;

namespace CactusPie.ItemCountInName.Services
{
    public interface IItemCountManager
    {
        IReadOnlyDictionary<string, ItemCountData> ItemCounts { get; }

        void ReloadItemCounts();

        bool IsItemOnBlacklist(Item item);

        void SetParentIdBlacklist(bool isBlacklisted, string parentId);
    }
}