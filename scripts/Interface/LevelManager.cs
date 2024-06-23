using System.Collections.Generic;
using UnityEngine;

namespace Assets.IntenseTPS.Scripts.Level
{
    public class LevelManager : MonoBehaviour
    {
        public readonly static string[] players = new string[] { "player0", "player1", "player2", "player3", "player4", "player5", "player6", "player7",
            "player8", "player9", "player10", "player11", "player12", "player13", "player14", "player15","player16","player17", "player18","player19",
            "player20", "player21","player22","player23" };//must
        public static readonly string noOwnerTag = "null";//must
        public static readonly string defPlayerData = "player0/1.00/1.00/0/0/null/0/1/1";//must
        public static readonly string playerIdReplace = "#";//must
        public static void LoadMapOver()//must
        {
        }
        public static bool ConditionLevelAction(TriggerListOption _item)//must
        {
            return true;
        }

        public static void SetVariable(string _code)//must
        {

        } 

        public static bool SaveGameForNextMap(string targetMap, string targetEntrance) //must
        {
            return true;
        } 

        public static bool Trigger(Collider collider, ActTrigger _trigger)//must
        {
            return true;
        }

        public static bool ManualTrigger(ManualTrigger _trigger)//must
        {
            return true;
        }

        public static void StartDialogue(Eerie2.DialogueSequence _dialogueSequence)//must
        {

        }

        public static void LoadCampaignInfo(string infoCode, bool haveInfo) //must
        { }

        public static void BuildItem(ItemBuilder builder, string owner, int player, int[] players)//must
        {  

        }

        public static int AddStandNpc(Transform _base, string npcModleName, int npcModleBase, int idleFaceAnimType, bool lookAtPlayer, RuntimeAnimatorController animatorController, string[] allCharacterPartNames, bool closeHand, bool autoPlaceOnGround)//must
        {
            return 1;
        }

        public static void AddNpcHandItem(GameObject handitem,int _id,Vector3 _pos, Vector3 _rot, bool onRightHand=true)//must
        {
        }

        public static void SetConversation(Conversation _conversation,Transform camPosition, int _npc)//must
        {
        }
        public static void CloseConversation(Conversation _conversation) //must;
        { 
        }
        public static void RefreshOption(bool[] _can) //must
        {
        }
        public static void SetOption(string[] _code, string[] _text) //must
        {
        }

        public static void StandNpcTalkStart(int _id,bool _start = false) //must
        {
        }

        public static void SelectConversationOption(Conversation.ConversationOption option,string _talkName,int _id) //must
        {
        }

        public static bool CanShowOption(Conversation.ConversationOption option) //must
        {
            return true;
        }

        public static void LootBoxUse(int _id)//must
        {
        }

        public static int LootBoxStart(Transform _base, string _showName, bool _needSerializeToFile, string _inventoryFileName, bool _transaction, string _transactionName)//must
        {
            return 1;
        }
        public static void LevelSound(Transform _list, LevelSoundType _type) //must
        {
        }
    }
}