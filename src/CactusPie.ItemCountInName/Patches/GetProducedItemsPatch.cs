using System.Reflection;
using System.Threading.Tasks;
using Aki.Reflection.Patching;
using CactusPie.ItemCountInName.Helpers;

namespace CactusPie.ItemCountInName.Patches
{
    public sealed class GetProducedItemsPatch<T> : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(T)
                .GetMethod("GetProducedItems", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        [PatchPostfix]
        public static void PatchPostfix()
        {
            // We only want to reload the count when OnlyInRaid is checked
            // Additionally, we don't want to do reload the count when we are in raid, as
            // stash content will never change during raid
            if (!ItemCountPlugin.OnlyInRaid.Value && !GameHelper.IsInGame())
            {
                ItemCountPlugin.ItemCountManager.ReloadItemCounts();

                // Upon retrieving the crafted item it won't appear in the stash immediately, so we have
                // to run it again with a delay
                Task.Delay(100).ContinueWith(_ => ItemCountPlugin.ItemCountManager.ReloadItemCounts());
            }
        }
    }
}