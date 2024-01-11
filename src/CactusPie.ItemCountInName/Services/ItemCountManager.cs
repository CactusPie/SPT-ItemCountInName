using System.Collections.Generic;
using System.Linq;
using Comfort.Common;
using EFT.HandBook;
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
                    where !_parentIdBlackList.Contains(item.Template._parent)
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

        public bool IsCountVisibleForItem(Item item)
        {
            bool result = !_parentIdBlackList.Contains(item.Template._parent);
            return result;
        }

        public void SetMoneyCountVisibility(bool isCountVisible)
        {
            SetParentIdVisibility(isCountVisible, "543be5dd4bdc2deb348b4569");
        }

        public void SetAmmoCountVisibility(bool isCountVisible)
        {
            SetParentIdVisibility(isCountVisible, "5485a8684bdc2da71d8b4567");
        }

        private void SetParentIdVisibility(bool isCountVisible, string parentId)
        {
            if (isCountVisible)
            {
                _parentIdBlackList.Remove(parentId);
            }
            else
            {
                _parentIdBlackList.Add(parentId);
            }
        }
    }
}