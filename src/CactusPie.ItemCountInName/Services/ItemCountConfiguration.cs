using System.Collections.Generic;
using BepInEx.Configuration;
using CactusPie.ItemCountInName.Configuration;
using CactusPie.ItemCountInName.Services.Data;

namespace CactusPie.ItemCountInName.Services
{
    public static class ItemCountConfiguration
    {
        internal static ConfigEntry<bool> Enabled { get; set; }

        internal static ConfigEntry<bool> OnlyInRaid { get; set; }

        internal static ConfigEntry<bool> ApplyToMoney { get; set; }

        internal static ConfigEntry<bool> ApplyToAmmo { get; set; }

        internal static ConfigEntry<string> ItemNameFormat { get; private set; }

        internal static ConfigEntry<string> ZeroItemsFormat { get; private set; }

        public static List<ConfigEntry<bool>> FiltersConfig { get; set; }

        public static void BindConfiguration(ConfigFile configFile, IItemCountManager itemCountManager)
        {
            const string configSection = "Item count settings";

            Enabled = configFile.Bind
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

            OnlyInRaid = configFile.Bind
            (
                configSection,
                "Only in raid",
                true,
                new ConfigDescription
                (
                    "Whether or not the mod is should only work during a raid",
                    null,
                    new ConfigurationManagerAttributes { Order = 90 }
                )
            );

            ItemNameFormat = configFile.Bind(
                configSection,
                "Item name format",
                "[{0}/{1}] {2}",
                new ConfigDescription(
                    "How the item name should be formatted. {0} - found in raid count, {1} - total count, {2} item name",
                    null,
                    new ConfigurationManagerAttributes { Order = 80 })
            );

            ZeroItemsFormat = configFile.Bind(
                configSection,
                "Format for no items",
                "[0] {0}",
                new ConfigDescription(
                    "Used when there are no matching items in stash. {0} - item name",
                    null,
                    new ConfigurationManagerAttributes { Order = 70 })
            );

            FiltersConfig = FilterConfigBinder.BindFilterCheckboxes(
                configFile,
                1000,
                "Item type blacklist",
                itemCountManager,
                new[]
                {
                    new FilterCheckboxData("543be5dd4bdc2deb348b4569", "Money", true),
                    new FilterCheckboxData("5485a8684bdc2da71d8b4567", "Ammo", true),
                    new FilterCheckboxData("5448eb774bdc2d0a728b4567", "Barter Items", false),
                    new FilterCheckboxData("543be5f84bdc2dd4348b456a", "Equipment", false),
                    new FilterCheckboxData("5422acb9af1c889c16000029", "Weapons", false),
                    new FilterCheckboxData("566162e44bdc2d3f298b4573", "Mods", false),
                    new FilterCheckboxData("543be5e94bdc2df1348b4568", "Keys", false),
                    new FilterCheckboxData("543be5664bdc2dd4348b4569", "Medication", false),
                    new FilterCheckboxData("543be6674bdc2df1348b4569", "Food And Drinks", false),
                    new FilterCheckboxData("543be6564bdc2df4348b4568", "Grenades", false),
                });

            OnlyInRaid.SettingChanged += (sender, args) => itemCountManager.ReloadItemCounts();
        }
    }
}