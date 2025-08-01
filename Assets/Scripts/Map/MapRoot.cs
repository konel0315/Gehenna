using Sirenix.OdinInspector;
using UnityEngine;

namespace Gehenna
{
    public class MapRoot : MonoBehaviour
    {
        [SerializeField] private MapEntrance[] entrances;
        [SerializeField] private MapExit[] exits;
    }
}