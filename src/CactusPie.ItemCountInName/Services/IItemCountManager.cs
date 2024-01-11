using System.Collections.Generic;
using EFT.InventoryLogic;

namespace CactusPie.ItemCountInName.Services
{
    public interface IItemCountManager
    {
        IReadOnlyDictionary<string, ItemCountData> ItemCounts { get; }

        void ReloadItemCounts();

        bool IsCountVisibleForItem(Item item);

        void SetMoneyCountVisibility(bool isCountVisible);

        void SetAmmoCountVisibility(bool isCountVisible);
    }
}