using System;
using JetBrains.Annotations;
using UnityEngine;

namespace CactusPie.ItemCountInName
{
    public sealed class ItemCountInNameController : MonoBehaviour
    {
        [UsedImplicitly]
        public void Start()
        {
            try
            {
                ItemCountPlugin.ItemCountManager.ReloadItemCounts();
            }
            catch (Exception e)
            {
                ItemCountPlugin.PluginLogger.LogError($"Exception {e.GetType()} occured. Message: {e.Message}. StackTrace: {e.StackTrace}");
            }
        }

        [UsedImplicitly]
        public void OnDestroy()
        {
            try
            {
                Destroy(this);
            }
            catch (Exception e)
            {
                ItemCountPlugin.PluginLogger.LogError($"Exception {e.GetType()} occured. Message: {e.Message}. StackTrace: {e.StackTrace}");
            }
        }
    }
}