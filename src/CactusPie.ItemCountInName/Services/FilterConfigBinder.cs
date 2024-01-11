using System.Collections.Generic;
using BepInEx.Configuration;
using CactusPie.ItemCountInName.Configuration;
using CactusPie.ItemCountInName.Services.Data;

namespace CactusPie.ItemCountInName.Services
{
    public static class FilterConfigBinder
    {
        public static List<ConfigEntry<bool>> BindFilterCheckboxes(
            ConfigFile configFile,
            int baseIndex,
            string configSection,
            IItemCountManager itemCountManager,
            IReadOnlyCollection<FilterCheckboxData> filterCheckboxes)
        {
            var configEntries = new List<ConfigEntry<bool>>(filterCheckboxes.Count);

            foreach (FilterCheckboxData filterCheckbox in filterCheckboxes)
            {
                ConfigEntry<bool> configEntry = configFile.Bind
                (
                    configSection,
                    $"HideCountFor{filterCheckbox.CategoryName.Replace(" ", "")}",
                    filterCheckbox.DefaultValue,
                    new ConfigDescription
                    (
                        "Whether or not the count should be hidden on this type of items",
                        null,
                        new ConfigurationManagerAttributes
                        {
                            Order = baseIndex--,
                            DispName = filterCheckbox.CategoryName,
                        }
                    )
                );

                itemCountManager.SetParentIdBlacklist(configEntry.Value, filterCheckbox.ParentId);

                configEntry.SettingChanged += (sender, args) =>
                {
                    itemCountManager.SetParentIdBlacklist(configEntry.Value, filterCheckbox.ParentId);
                };

                configEntries.Add(configEntry);
            }

            return configEntries;
        }
    }
}