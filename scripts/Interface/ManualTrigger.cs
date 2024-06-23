
using UnityEngine;

namespace Assets.IntenseTPS.Scripts.Level
{
    public class ManualTrigger : Interactive
    {
        [Space]
        public bool onlyForOneFaction = false;
        public Faction faction;
        [Space]
        public int itemCountCondition = 0;
        public string itemNameCondition;
        public bool consumeItem;
        public bool getItemByTag=false;
        [Header("action")]
        public string levelAct;

        private float _timer = 0f;
        private bool _update = false;
        public void Awake()
        {
            canUes = true;
        }
        public void FixedUpdate()
        {
            if (_update)
            {
                _timer += Time.fixedUnscaledDeltaTime;
                if (_timer > 2f) { canUes=true; _update = false; }
            }
        }
        public override void Use()
        {
            if (!canUes) return;
            else
            {
                canUes = false;
                bool act = LevelManager.ManualTrigger(this);
                if(!act)
                {
                    _timer = 0f; _update = true;
                }
            }
        }
    }
}