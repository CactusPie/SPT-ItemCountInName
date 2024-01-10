using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using CactusPie.ItemCountInName.Configuration;
using CactusPie.ItemCountInName.Patches;
using CactusPie.ItemCountInName.Services;
using JetBrains.Annotations;

namespace CactusPie.ItemCountInName
{
    [BepInPlugin("com.cactuspie.itemcountinname", "CactusPie.ItemCountInName", "1.0.0")]
    public sealed class ItemCountPlugin : BaseUnityPlugin
    {
        internal static ManualLogSource PluginLogger { get; private set; }

        internal static ConfigEntry<bool> Enabled { get; set; }

        internal static ConfigEntry<string> ItemNameFormat { get; private set; }

        internal static ConfigEntry<string> ZeroItemsFormat { get; private set; }

        internal static IItemCountManager ItemCountManager { get; private set; }

        [UsedImplicitly]
        internal void Start()
        {
            PluginLogger = Logger;
            PluginLogger.LogInfo("ItemCountInName loaded");

            const string configSection = "Item count settings";

            Enabled = Config.Bind
            (
                configSection,
                "Mod enabled",
                true,
                new ConfigDescription
                (
                    "Whether or not the mod is enabled",
                    null,
                    new ConfigurationManagerAttributes { Order = 100 }
                )
            );

            ItemNameFormat = Config.Bind(
                configSection,
                "Item name format",
                "[{0}/{1}] {2}",
                new ConfigDescription(
                    "How the item name should be formatted. {0} - found in raid count, {1} - total count, {2} item name",
                    null,
                    new ConfigurationManagerAttributes { Order = 90 })
            );

            ZeroItemsFormat = Config.Bind(
                configSection,
                "Format for no items",
                "[0] {0}",
                new ConfigDescription(
                    "Used when there are no matching items in stash. {0} - item name",
                    null,
                    new ConfigurationManagerAttributes { Order = 80 })
            );

            ItemCountManager = new ItemCountManager();

            new GameStartPatch().Enable();
            new ItemCountPatch().Enable();
        }
    }
}