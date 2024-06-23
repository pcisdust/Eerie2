using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.IntenseTPS.Scripts.Common;

namespace Assets.IntenseTPS.Scripts.Level
{
    public class Conversation : Interactive
    {
        [Space]
        public bool autoStart = false;
        public bool showUnavailableOptions = false;
        public List<ConversationOption> conversationOptions;
        public string talkerName = "";
        public string startOption = "start";
        public Transform camPosition;
        [Header("NPC modle")]
        public Transform npcModle;
        public string npcModleName;
        public int npcModleBase = 1;
        public int idleFaceAnimType = 0;
        public bool lookAtPlayer=true;
        public bool autoPlaceOnGround = true;
        public RuntimeAnimatorController animatorController;
        [Tooltip("0 hat, 1 face_decoration,2 earPhone, 3 vest, 4 female_top, 5 watch, 6 glove, 7 bottom, 8 legwear,9 shoes")]
        public List<string> allCharacterPartNames;
        [Header("hand item")]
        public GameObject handitem;
        public bool onRightHand = true;
        public Vector3 handItemPosition;
        public Vector3 handItemRotation;
        public bool closeHand = false;

        private List<int> _nextOptions;
        private int _curIndex = -1;
        private float _conversationTimer = 0f;
        private float _conversationDelay = 0f;
        private bool _conversationUpdate = false;
        private int _npcId=-1;
        private void Start()
        {
            canUes = true;
            if (npcModle)
            {
                _npcId = LevelManager.AddStandNpc(npcModle, npcModleName, npcModleBase, idleFaceAnimType, lookAtPlayer, animatorController, allCharacterPartNames.ToArray(), closeHand, autoPlaceOnGround);
                LevelManager.AddNpcHandItem(handitem, _npcId, handItemPosition, handItemRotation, onRightHand);
            }
        }
        public override void Use()
        {
            if (!canUes) return;
            else
            {
                if (_npcId >= 0)
                    LevelManager.StandNpcTalkStart(_npcId, true);
                LevelManager.SetConversation(this, camPosition,_npcId);
                SelectAOption(startOption);
                canUes = false;
            }
        } 
        public void ResetConversation()
        {
            if (_npcId >= 0) 
                LevelManager.StandNpcTalkStart(_npcId, false);
            canUes = true;
        }
        public void FixedUpdate()
        {
            if (_conversationUpdate)
            { 
                _conversationTimer += Time.fixedUnscaledDeltaTime; 
                if (_conversationTimer > _conversationDelay) { ShowNextOptions(_curIndex); _conversationUpdate = false; } 
            }
        }
        public void SelectAOption(string _name) 
        {
            int _index=-1;
            for(int i = 0; i < conversationOptions.Count; i++)
            { if (conversationOptions[i].optionName == _name) _index = i; }
            if (_index < 0) return;
            _curIndex = _index;
            LevelManager.SelectConversationOption(conversationOptions[_curIndex],talkerName, _npcId);
            _conversationDelay = 0.5f; _conversationTimer = 0f; _conversationUpdate = true;
        }
        public void ShowNextOptions(int _curOptions) 
        {
            _nextOptions = new();
            string[] _next = conversationOptions[_curOptions].nextOptions.ToArray();
            if (_next.Length != 0)
            {
                for (int i = 0; i < _next.Length; i++)
                {
                    for (int n = 0; n < conversationOptions.Count; n++)
                    {
                        if (conversationOptions[n].optionName == _next[i])
                        {
                            _nextOptions.Add(n);
                        }
                        else continue;
                    }
                }
            }
            else
            {
                LevelManager.CloseConversation(this);
                return;
            }

            string[] _code = new string[_nextOptions.Count];
            string[] _text = new string[_nextOptions.Count];
            for (int i = 0; i < _nextOptions.Count; i++)
            {
                _code[i] = conversationOptions[_nextOptions[i]].optionName;
                _text[i] = conversationOptions[_nextOptions[i]].optionText;
            }
            LevelManager.SetOption(_code, _text);
            RefreshOption();
        }
        public void RefreshOption() 
        {
            bool[] show = CheckOption();
            bool getOne = false;
            if (show.Length > 0)
            {
                for (int i = 0; i < show.Length; i++)
                {
                    if (show[i]) { getOne = true; break; }
                }
            }
            if (!getOne)
            {
                LevelManager.CloseConversation(this);
                return;
            }
            LevelManager.RefreshOption(show);
        }
        private bool[] CheckOption()
        {
            bool[] _nextShow = new bool[0];
            if (_nextOptions.Count > 0)
            {
                _nextShow = new bool[_nextOptions.Count];
                for (int i = 0; i < _nextShow.Length; i++)
                {
                    _nextShow[i] = false;
                }
                for (int i = 0; i < _nextOptions.Count; i++)
                {
                    if (LevelManager.CanShowOption(conversationOptions[_nextOptions[i]]))
                    {
                        _nextShow[i] = true;
                    }
                }
            }
            return _nextShow;
        }

        [Serializable]
        public class ConversationOption
        {
            public string optionName;
            public string optionText;
            public List<string> variableConditions;
            public int itemCountCondition = 0;
            public string itemNameCondition;
            public bool getItemByTag = false;
            public bool consumeItem = false;
            public List<string> nextOptions;
            public bool npcAct = false;
            public int npcFaceType = 0;
            public bool showSubtitle = false;
            public string subtitle = "";
            public AudioClip audioClip;
            public string levelAction="";
        }
    }
}
