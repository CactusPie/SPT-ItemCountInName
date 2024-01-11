using System.Collections.Generic;
using EFT.InventoryLogic;

namespace CactusPie.ItemCountInName.Services
{
    public interface IItemCountManager
    {
        IReadOnlyDictionary<string, ItemCountData> ItemCounts { get; }

        void ReloadItemCounts();

        bool IsCountVisibleForItem(Item item);

        void SetParentIdVisibility(bool isCountVisible, string parentId);
    }
}