using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gehenna
{
    [Serializable]
    public class BattleEndConditionSet
    {
        [SerializeReference] private List<IBattleEndCondition> victoryConditions;
        [SerializeReference] private List<IBattleEndCondition> defeatConditions;
        
        public BattleEndType Evaluate()
        {
            if (defeatConditions.Any(c => c.IsSatisfied()))
                return BattleEndType.Defeat;

            if (victoryConditions.All(c => c.IsSatisfied()))
                return BattleEndType.Victory;

            return BattleEndType.None;
        }
    }
}