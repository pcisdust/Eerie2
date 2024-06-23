using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.IntenseTPS.Scripts.Level;

namespace Assets.IntenseTPS.Scripts.AI
{
    [Serializable]
    public class PlannerState
    {
        public string note = "state";
        public PatrolRoute patrolRoute=null;
        public List<string> conditions;
        public List<PatrolRoute> patrolRoutes;
        public List<int> variableCondition;
        public bool needFollowOnePlayer = false;
        public int followPlayer = 0;
        public bool alert=false;
        public bool defense=true;
    }
    public enum EventType 
    {
        AllDown, OneDown, AfterAlert
    }
    public class AiPlanner : MonoBehaviour
    {
        public Faction faction;
        public List<int> takeOverPlayers;
        public string controlVariable;
        public List<PlannerState> plannerStates;
        [Tooltip("Members below(on) this number will not be harmed")]
        public int muteNumber = -1;
        [Space]
        public AiPlanner eventRef;
        [Header("all down event")]
        public bool haveEventAfterAllDown = false;
        public TriggerListOption eventAfterAllDown;
        [Header("one down event")]
        public bool haveEventAfterOneDown = false;
        public TriggerListOption eventAfterOneDown;
        [Header("stealth event")]
        public bool haveEventAfterAlert = false;
        public TriggerListOption eventAfterAlert;

        private bool _start = false;
        public void StatUp()
        {
            if (_start) return;
            _start = true;
            if (plannerStates.Count == 0)
            {
                plannerStates.Add(new PlannerState());
            }
            if (plannerStates.Count !=0)
            {
                for (int i = 0; i < plannerStates.Count; i++)
                {
                    if (plannerStates[i].patrolRoutes.Count != 0)
                    {
                        foreach (var _item in plannerStates[i].patrolRoutes)
                        {
                            _item.StartUp();
                        }
                    }
                }
            }
        }
        public void TriggerAction(EventType _type) 
        {
            if (eventRef) 
            {
                eventRef.TriggerAction(_type);return;
            }
            bool _get = false;
            TriggerListOption _act = new();
            if (_type == EventType.AllDown)
            {
                _get = haveEventAfterAllDown;
                _act = eventAfterAllDown;
            }
            else if (_type == EventType.OneDown)
            {
                _get = haveEventAfterOneDown;
                _act = eventAfterOneDown;
            }
            else if (_type == EventType.AfterAlert)
            {
                _get = haveEventAfterAlert;
                _act = eventAfterAlert;
            }
            if (_get)
                LevelManager.ConditionLevelAction(_act);
        }
    }
}
