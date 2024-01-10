using System.Linq;
using System.Reflection;
using Aki.Reflection.Patching;
using CactusPie.ItemCountInName.Helpers;
using EFT.UI;

namespace CactusPie.ItemCountInName.Patches
{
    public sealed class InventoryShowPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(InventoryScreen)
                .GetMethods()
                .FirstOrDefault(x => x.Name == "Show" && x.GetParameters().Length == 1);
        }

        [PatchPrefix]
        public static void PatchPrefix()
        {
            // We only want to reload the count when OnlyInRaid is checked
            // Additionally, we don't want to do reload the count when we are in raid, as
            // stash content will never change during raid
            if (!ItemCountPlugin.OnlyInRaid.Value && !GameHelper.IsInGame())
            {
                ItemCountPlugin.ItemCountManager.ReloadItemCounts();
            }
        }
    }
}