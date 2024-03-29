using System.Reflection;
using Aki.Reflection.Patching;
using Comfort.Common;
using EFT;

namespace CactusPie.ItemCountInName.Patches
{
    public sealed class GameStartPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(GameWorld).GetMethod("OnGameStarted", BindingFlags.Public | BindingFlags.Instance);
        }

        [PatchPostfix]
        public static void PatchPostFix()
        {
            GameWorld gameWorld = Singleton<GameWorld>.Instance;

            if (gameWorld == null)
            {
                return;
            }

            gameWorld.gameObject.AddComponent<ItemCountInNameController>();
        }
    }
}