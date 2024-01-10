using System.Collections.Generic;
using System.Linq;
using Comfort.Common;
using EFT.InventoryLogic;

namespace CactusPie.ItemCountInName.Services
{
    public class ItemCountManager : IItemCountManager
    {
        public IReadOnlyDictionary<string, ItemCountData> ItemCounts { get; private set; }

        public void ReloadItemCounts()
        {
            IEnumerable<Item> inventoryItems = Singleton<HideoutClass>.Instance.AllStashItems;

            if (inventoryItems != null)
            {
                ItemCounts = (
                    from item in inventoryItems
                    group item by item.Template._id
                    into groupedItem
                    select new ItemCountData
                    {
                        Template = groupedItem.First().Template,
                        TotalCount = groupedItem.Select(x => x.StackObjectsCount).Sum(),
                        FoundInRaidCount = groupedItem.Where(x => x.MarkedAsSpawnedInSession).Select(x => x.StackObjectsCount).Sum(),
                    }
                ).ToDictionary(itemCountData => itemCountData.Template._id, itemCountData => itemCountData);
            }
        }
    }
}