using System.Reflection;
using Aki.Reflection.Patching;
using CactusPie.ItemCountInName.Helpers;
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
            if (!ItemCountPlugin.Enabled.Value)
            {
                return;
            }

            if (ItemCountPlugin.ItemCountManager.ItemCounts == null)
            {
                return;
            }

            if (ItemCountPlugin.OnlyInRaid.Value && !GameHelper.IsInGame())
            {
                return;
            }

            if (ItemCountPlugin.ItemCountManager.ItemCounts.TryGetValue(item.TemplateId, out ItemCountData itemCountData))
            {
                __result = string.Format(ItemCountPlugin.ItemNameFormat.Value, itemCountData.FoundInRaidCount, itemCountData.TotalCount, __result);
            }
            else
            {
                __result = string.Format(ItemCountPlugin.ZeroItemsFormat.Value, __result);
            }
        }
    }
}