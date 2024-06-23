using System.Collections.Generic;
using UnityEngine;

namespace Assets.IntenseTPS.Scripts.Level
{
    public class VariableController : MonoBehaviour
    {
        public List<string> conditions;
        public bool onlyTrunOffOnLevelStart = false;
        public bool trigger = false;
        public bool initialization = false;
        public List<string> directlyChangeVariables;
        public bool canDelete=true;

        [Header("only for player item builder")]
        public List<int> playerGenerate;
        [HideInInspector]
        public bool activated = false;
        private bool inActive = false;
        private float convTimer = 0f;
        private Conversation waitConversation;
        public void BuildItem(int playerId = -1)
        {
            bool _canDelete = canDelete;
            if (trigger|| initialization) _canDelete = false;
            if (directlyChangeVariables.Count > 0) 
            {
                for (int i = 0; i < directlyChangeVariables.Count; i++)
                {
                    LevelManager.SetVariable(directlyChangeVariables[i]);
                }
            }
            for (int i = 0; i < transform.childCount; i++)
            {
                var _bu = transform.GetChild(i).GetComponent<ItemBuilder>();
                if (_bu)
                {
                    LevelManager.BuildItem(_bu,_canDelete ? gameObject.name : LevelManager.noOwnerTag, playerId, playerGenerate.ToArray());
                }
                var _di = transform.GetChild(i).GetComponent<Eerie2.DialogueSequence>();
                if (_di) 
                {
                    LevelManager.StartDialogue(_di);
                }

            }
        }

        private void FixedUpdate()
        {
            if (waitConversation)
            {
                convTimer += Time.fixedUnscaledDeltaTime;
                if (convTimer > 1f)
                {
                    waitConversation.Use();
                    waitConversation = null;
                }
            }
        }
        public void SetState(bool _on,bool _start=false)
        {
            if (inActive != _on || _start)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    var _co = transform.GetChild(i).GetComponent<Conversation>();
                    if (_co)
                    {
                        if (_co.autoStart)
                        {
                            if (_on)
                            {
                                convTimer = 0f;
                                waitConversation = _co;
                            }
                            else
                            {
                                if (waitConversation == _co) 
                                { convTimer = 0f; waitConversation = null; }
                                LevelManager.CloseConversation(_co);
                            }
                        }
                    }

                    var _door = transform.GetChild(i).GetComponent<VariableControllDoor>();
                    if (_door)
                    {
                        _door.SetOpen(_on,_start);
                    }
                    else 
                    { 
                        transform.GetChild(i).gameObject.SetActive(_on);
                    }
                }
                inActive = _on;
            }
        }
    }
}