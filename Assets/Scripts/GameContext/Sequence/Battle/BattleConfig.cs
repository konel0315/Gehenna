using System.Collections.Generic;
using UnityEngine;

namespace Gehenna
{
    [CreateAssetMenu(fileName = "BattleData", menuName = "Gehenna/Sequence/Battle")]
    public class BattleConfig : SequenceData
    {
        public string ID;
        public BattleEndConditionSet EndConditionSet;
        public List<BattleSpawnInfo> SpawnInfos;
    }
}