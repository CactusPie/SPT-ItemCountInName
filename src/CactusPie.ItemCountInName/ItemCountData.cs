﻿using EFT.InventoryLogic;

namespace CactusPie.ItemCountInName
{
    public sealed class ItemCountData
    {
        public ItemTemplate Template { get; set; }

        public int TotalCount { get; set; }

        public int FoundInRaidCount { get; set; }
    }
}