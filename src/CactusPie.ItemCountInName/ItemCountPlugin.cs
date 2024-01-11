using BepInEx;
using BepInEx.Logging;
using CactusPie.ItemCountInName.Patches;
using CactusPie.ItemCountInName.Services;
using EFT.Hideout;
using JetBrains.Annotations;

namespace CactusPie.ItemCountInName
{
    [BepInPlugin("com.cactuspie.itemcountinname", "CactusPie.ItemCountInName", "1.0.0")]
    public sealed class ItemCountPlugin : BaseUnityPlugin
    {
        internal static ManualLogSource PluginLogger { get; private set; }

        internal static IItemCountManager ItemCountManager { get; private set; }

        [UsedImplicitly]
        internal void Start()
        {
            PluginLogger = Logger;

            ItemCountManager = new ItemCountManager();
            ItemCountConfiguration.BindConfiguration(Config, ItemCountManager);

            new GetProducedItemsPatch<FarmingView>().Enable();
            new GetProducedItemsPatch<PermanentProductionView>().Enable();
            new GetProducedItemsPatch<ProduceView>().Enable();
            new GetProducedItemsPatch<ScavCaseView>().Enable();
            new ConfirmPurchasePatch().Enable();
            new ConfirmSellPatch().Enable();
            new GameStartPatch().Enable();
            new ItemCountPatch().Enable();
            new InventoryShowPatch().Enable();

        }
    }
}