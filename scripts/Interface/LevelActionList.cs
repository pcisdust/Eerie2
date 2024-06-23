using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.IntenseTPS.Scripts.Level
{
    public class LevelActionList : MonoBehaviour
    {
        public List<LevelAction> levelActions;
        public List<TriggerListOption> startAction;
        public List<TriggerListOption> updateAction;
        public List<TimerTrigger> timerTriggers;
        [Space]
        public bool duplicateTimerForPlayers = false;
        public List<int> timerTriggerPlayers;
        [Header("player events")]
        public List<PlayerEvent> playerRemove;
        public List<PlayerEvent> playerDead;
        [Space]
        public List<GearState> gearStates;
        private bool init = false;
        public void Init() 
        {
            if (init) return;
            init = true;
            if (!duplicateTimerForPlayers)
            {
                for (int i = 0; i < timerTriggers.Count; i++)
                {
                    timerTriggers[i].playerId = -1;
                }
            }
            else
            {
                if (timerTriggerPlayers.Count == 0)
                {
                    for (int i = 0; i < LevelManager.players.Length; i++)
                    {
                        timerTriggerPlayers.Add(i);
                    }
                }
                var _list = timerTriggers.ToArray();
                timerTriggers = new();
                for (int i = 0; i < _list.Length; i++)
                {
                    if (_list[i].variable.Contains(LevelManager.playerIdReplace))
                    {
                        for (int n = 0; n < timerTriggerPlayers.Count; n++)
                        {
                            TimerTrigger _newTimer = new();
                            _newTimer.values = _list[i].values;
                            _newTimer.levelActions = _list[i].levelActions;
                            _newTimer.playerId = timerTriggerPlayers[n];
                            _newTimer.variable= _list[i].variable.Replace(LevelManager.playerIdReplace, timerTriggerPlayers[n].ToString());
                            timerTriggers.Add(_newTimer);
                        }
                    }
                    else 
                    {
                        _list[i].playerId = -1;
                        timerTriggers.Add(_list[i]);
                    }
                }
            }
        }
    }
}