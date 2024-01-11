using System.Collections.Generic;
using System.Linq;
using Comfort.Common;
using EFT.InventoryLogic;

namespace CactusPie.ItemCountInName.Services
{
    public class ItemCountManager : IItemCountManager
    {
        public IReadOnlyDictionary<string, ItemCountData> ItemCounts { get; private set; }

        private readonly HashSet<string> _parentIdBlackList = new HashSet<string>();

        public void ReloadItemCounts()
        {
            IEnumerable<Item> inventoryItems = Singleton<HideoutClass>.Instance.AllStashItems;

            if (inventoryItems != null)
            {
                ItemCounts = (
                    from item in inventoryItems
                    where !IsItemOnBlacklist(item)
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

        public bool IsItemOnBlacklist(Item item)
        {
            ItemTemplate itemTemplate = item.Template;

            while (itemTemplate != null)
            {
                if (_parentIdBlackList.Contains(itemTemplate._id))
                {
                    return true;
                }

                if (string.IsNullOrEmpty(itemTemplate._parent))
                {
                    break;
                }

                itemTemplate = itemTemplate.Parent;
            }

            return false;
        }

        public void SetParentIdBlacklist(bool isBlacklisted, string parentId)
        {
            if (isBlacklisted)
            {
                _parentIdBlackList.Add(parentId);
            }
            else
            {
                _parentIdBlackList.Remove(parentId);
            }
        }
    }
}