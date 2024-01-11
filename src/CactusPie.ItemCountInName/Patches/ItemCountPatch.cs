using System.Reflection;
using Aki.Reflection.Patching;
using CactusPie.ItemCountInName.Helpers;
using CactusPie.ItemCountInName.Services;
using EFT.InventoryLogic;

namespace CactusPie.ItemCountInName.Patches
{
    public sealed class ItemCountPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(ItemFactory).GetMethod(
                nameof(ItemFactory.BriefItemName),
                BindingFlags.Public | BindingFlags.Instance);
        }

        [PatchPostfix]
        public static void PatchPostfix(ref string __result, Item item, string defaultName)
        {
            if (!ItemCountConfiguration.Enabled.Value ||
                ItemCountPlugin.ItemCountManager.ItemCounts == null ||
                (ItemCountConfiguration.OnlyInRaid.Value && !GameHelper.IsInGame()) ||
                ItemCountPlugin.ItemCountManager.IsItemOnBlacklist(item)
                )
            {
                return;
            }

            if (ItemCountPlugin.ItemCountManager.ItemCounts.TryGetValue(item.TemplateId, out ItemCountData itemCountData))
            {
                __result = string.Format(ItemCountConfiguration.ItemNameFormat.Value, itemCountData.FoundInRaidCount, itemCountData.TotalCount, __result);
            }
            else
            {
                __result = string.Format(ItemCountConfiguration.ZeroItemsFormat.Value, __result);
            }
        }
    }
}