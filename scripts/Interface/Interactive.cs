using UnityEngine;
namespace Assets.IntenseTPS.Scripts.Level
{
    public class Interactive : MonoBehaviour
    {
        public string tipTextCode = "def_interactive_tip";
        public bool canUes = true;
        public virtual bool CanUse()
        {
            return canUes;
        }
        public virtual void Use()
        {
            if (!canUes) return;
        }
    }
}