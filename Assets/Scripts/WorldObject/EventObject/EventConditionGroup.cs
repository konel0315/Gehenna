using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gehenna
{
    [Serializable]
    public class EventConditionGroup
    {
        public ConditionLogic Logic;
        
        [SerializeReference]
        [ShowIf("@Logic == ConditionLogic.AND || Logic == ConditionLogic.OR")]
        public List<IEventCondition> Conditions;

        [SerializeReference]
        [ShowIf("@Logic == ConditionLogic.NOT")]
        public IEventCondition Condition;
        
        public bool IsMet(EventObject eventObject)
        {
            switch (Logic)
            {
                case ConditionLogic.AND:
                {
                    if (Conditions == null || Conditions.Count == 0)
                    {
                        GehennaLogger.Log(this, LogType.Warning, "Conditions list is null or empty for AND logic.");
                        return false;
                    } 
                    
                    foreach (var condition in Conditions)
                    {
                        if (!condition.IsMet(eventObject))
                        {
                            return false;
                        }
                    }
                    return true;
                }

                case ConditionLogic.OR:
                {
                    if (Conditions == null || Conditions.Count == 0)
                    {
                        GehennaLogger.Log(this, LogType.Warning, "Conditions list is null or empty for AND logic.");
                        return false;
                    } 
                    
                    foreach (var condition in Conditions)
                    {
                        if (condition.IsMet(eventObject))
                        {
                            return true;
                        }
                    }

                    return false;
                }

                case ConditionLogic.NOT:
                {
                    if (Condition == null)
                    {
                        GehennaLogger.Log(this, LogType.Warning, "Condition is null for NOT logic.");
                        return false;
                    }
                    
                    return !Condition.IsMet(eventObject);
                }

                default:
                {
                    GehennaLogger.Log(this, LogType.Warning, $"Unknown ConditionLogic value: {Logic}");
                    return false; 
                }
            }
        }
    }
}