﻿using Comfort.Common;
using EFT;

namespace CactusPie.ItemCountInName.Helpers
{
    public static class GameHelper
    {
        public static bool IsInGame()
        {
            GameWorld gameWorld = Singleton<GameWorld>.Instance;

            bool isInGame = gameWorld != null && gameWorld.MainPlayer != null && gameWorld.MainPlayer.Location != "hideout";

            return isInGame;
        }
    }
}