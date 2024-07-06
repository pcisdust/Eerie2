using System;
using System.Collections.Generic;
using UnityEngine;
using Assets.IntenseTPS.Scripts.AI;
using Assets.IntenseTPS.Scripts.Common;


public enum Faction { Friendly, Hostile, ThirdParty, Neutral }

namespace Assets.IntenseTPS.Scripts.Level
{
    public class LevelLogic : MonoBehaviour
    {
        public string logicName = "";
        public bool haveInfo = true;
        public string infoCode = "";
        [Header("ui")]
        public CanvasGroup levelUiGroup;
        public List<LevelUi> levelUiElements;
        [HideInInspector]
        public List<LevelAction> levelActions;
        [HideInInspector]
        public List<TriggerListOption> startAction;
        [HideInInspector]
        public List<TriggerListOption> updateAction;
        [HideInInspector]
        public List<TimerTrigger> timerTriggers;
        [HideInInspector]
        public List<PlayerEvent> playerRemove;
        [HideInInspector]
        public List<PlayerEvent> playerDead;
        [HideInInspector]
        public List<GearState> gearStates;
        [Space]
        public List<Transform> landMarks;
        [Header("auto get")]
        public List<LevelActionList> levelActionLists;
        public List<SceneEntrance> sceneEntrances;
        public List<VariableController> variableControllers;
        public List<AiPlanner> aiGenerators;
        public List<LevelControlArea> controlAreas;
        public List<LevelObjectFollower> objectFollowers;
        [Space]
        public Transform cam;
        public void Start()
        {
           LevelManager.LoadMapOver();
        }
        public void StartUp() 
        {
            levelActions = new();
            startAction = new();
            updateAction = new();
            timerTriggers = new();
            playerRemove = new();
            playerDead = new();
            gearStates = new();
            LevelManager.LoadCampaignInfo(infoCode, haveInfo);
            foreach (var item in levelActionLists)
            {
                item.Init();
                levelActions.AddRange(item.levelActions);
                startAction.AddRange(item.startAction);
                updateAction.AddRange(item.updateAction);
                timerTriggers.AddRange(item.timerTriggers);
                playerRemove.AddRange(item.playerRemove);
                playerDead.AddRange(item.playerDead);
                gearStates.AddRange(item.gearStates);
            }
        }
        public void AutoGet()
        {
            Transform variable = transform;
            aiGenerators = new();
            variableControllers = new();
            sceneEntrances = new();
            levelActionLists = new();
            controlAreas = new();
            objectFollowers = new();
            levelUiElements = new();
            for (int i = 0; i < variable.childCount; i++)
            {
                var _t = variable.GetChild(i);

                var _ai = _t.GetComponentsInChildren<AiPlanner>(true);
                if (_ai.Length != 0)
                {
                    foreach (var _aiItem in _ai)
                    {
                        #region temp
                        if (_aiItem.plannerStates.Count != 0) 
                        {

                            for (int n = 0; n < _aiItem.plannerStates.Count; n++) 
                            {
                                if (_aiItem.plannerStates[n].patrolRoute) 
                                {
                                    _aiItem.plannerStates[n].note = _aiItem.plannerStates[n].patrolRoute.name;
                                    _aiItem.plannerStates[n].patrolRoutes = new()
                                    {
                                        _aiItem.plannerStates[n].patrolRoute
                                    };
                                }
                                if (_aiItem.plannerStates[n].variableCondition.Count > 0)
                                {
                                    _aiItem.plannerStates[n].conditionVariable = _aiItem.controlVariable;
                                }
                                else
                                {
                                    _aiItem.plannerStates[n].conditionVariable = "";
                                }
                            }
                        }
                        #endregion
                        aiGenerators.Add(_aiItem);
                    }

       
                }

                var _co = _t.GetComponentsInChildren<VariableController>(true);
                if (_co.Length != 0)
                {
                    foreach (var _coItem in _co)
                    { variableControllers.Add(_coItem); }
                }

                var _en = _t.GetComponentsInChildren<SceneEntrance>(true);
                if (_en.Length != 0)
                {
                    foreach (var _enItem in _en)
                    { sceneEntrances.Add(_enItem); }
                }

                var _ac = _t.GetComponentsInChildren<LevelActionList>(true);
                if (_ac.Length != 0)
                {
                    foreach (var _acItem in _ac)
                    { levelActionLists.Add(_acItem); }
                }

                var _ca = _t.GetComponentsInChildren<LevelControlArea>(true);
                if (_ca.Length != 0) 
                {
                    foreach (var _caItem in _ca)
                    { controlAreas.Add(_caItem); }
                }

                var _fo = _t.GetComponentsInChildren<LevelObjectFollower>(true);
                if (_fo.Length != 0) 
                {
                    foreach(var _foItem in _fo)
                    { objectFollowers.Add(_foItem); }
                }

                var _ui = _t.GetComponentsInChildren<LevelUi>(true);
                if (_ui.Length != 0)
                {
                    foreach (var _uiItem in _ui)
                    { levelUiElements.Add(_uiItem); }
                }
            }
            Debug.Log("auto get ok");
        }
    }
    [Serializable]
    public class LevelAction
    {
        public string code;
        [Header("variables")]
        public List<string> changeVariables;
        public List<string> variableControllersTrigger;
        public List<string> infomationToAdd;
        public List<string> infomationToDelete;
        public bool autoSave = false;
        public bool needShowSubtitle = false;
        public string text;
        public string talkName;
        public AudioClip audioClip;
        public bool isBgm = false;
        public bool needMoveToNextScene = false;
        public bool movePlayerToNextScene = true;
        public string targetScene;
        public string targetEntrance;
    }
    [Serializable]
    public class TriggerListOption
    {
        public string levelAction;
        public List<string> variableConditions;
    }
    [Serializable]
    public class TimerTrigger
    {
        public string variable;
        public List<int> values;
        public List<string> levelActions;
        public int playerId = -1;
    }
    [Serializable]
    public class TransactionDB
    {
        public string currencyName;
        public List<BuyInfo> buyPrice;
        public List<TransactionListDB> priceLists;
    }
    [Serializable]
    public class TransactionListDB
    {
        public string invFileName;
        public string shopText;
        public List<int> priceIndex;
        public List<int> price;
    }
    [Serializable]
    public class BuyInfo
    {
        public bool conditionIsTag = false;
        public string itemCondition;
        public int itemPrice = 0;
    }
    [Serializable]
    public class PlayerEvent
    {
        public string levelAction;
        public List<string> conditions;
        public List<int> players;
    }
    [Serializable]
    public class GearState
    {
        public string invFileName;
        public bool forPlayer = true;
        public List<int> players;
        public List<string> aiPlanners;
        public List<string> conditions;
    }
}