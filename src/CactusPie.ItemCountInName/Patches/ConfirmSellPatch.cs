using System;
using System.Reflection;
using Aki.Reflection.Patching;
using Aki.Reflection.Utils;
using CactusPie.ItemCountInName.Helpers;
using CactusPie.ItemCountInName.Services;

namespace CactusPie.ItemCountInName.Patches
{
    public sealed class ConfirmSellPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            foreach (Type eftType in PatchConstants.EftTypes)
            {
                if (!eftType.Name.StartsWith("Class"))
                {
                    continue;
                }

                MethodInfo purchaseMethod = eftType.GetMethod("ConfirmSell");

                if (purchaseMethod == null)
                {
                    continue;
                }

                return purchaseMethod;
            }

            return null;
        }

        [PatchPrefix]
        public static void PatchPrefix()
        {
            ItemCountPlugin.PluginLogger.LogError("PurchaseD!!!!");

            // We only want to reload the count when OnlyInRaid is checked
            // Additionally, we don't want to do reload the count when we are in raid, as
            // stash content will never change during raid
            if (!ItemCountConfiguration.OnlyInRaid.Value && !GameHelper.IsInGame())
            {
                ItemCountPlugin.ItemCountManager.ReloadItemCounts();
            }
        }
    }
}