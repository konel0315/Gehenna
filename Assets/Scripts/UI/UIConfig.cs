using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gehenna
{
    [CreateAssetMenu(fileName = "UIConfig", menuName = "Gehenna/Config/UIConfig")]
    public class UIConfig : BaseConfig
    {
        [SerializeField] private Dictionary<UILayerType, GameObject> canvasMap;
        
        public override void Initialize()
        {
            GehennaLogger.Log(this, LogType.Success, "Initialize");
        }

        public GameObject GetCanvasPrefab(UILayerType layer)
        {
            return canvasMap.TryGetValue(layer, out var prefab) ? prefab : null; 
        } 

        public (UILayerType, GameObject)[] GetCanvasPrefabs()
        {
            (UILayerType, GameObject)[] result = new (UILayerType, GameObject)[canvasMap.Count];
            
            int index = 0;
            foreach (var kvp in canvasMap)
                result[index++] = (kvp.Key, kvp.Value);

            return result;
        }
    }
}