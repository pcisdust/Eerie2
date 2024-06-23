using System.Collections.Generic;
using UnityEngine;

namespace Assets.IntenseTPS.Scripts.Level
{
    public class SceneEntrance : Interactive
	{
		public string targetMap;
		public string targetEntrance;
		public List<string> canReachMap;
        [Range(0,10)]
		public float characterEntranceRange=1f;
		[Range(0, 10)]
		public float characterEntranceOffset=0.25f;
		public override void Use()
		{
			if (!canUes) return;
			else
			{
				if(LevelManager.SaveGameForNextMap(targetMap, targetEntrance))
				{ canUes = false; }
			}
		}
		public Vector3 GetStartPosition() 
		{
			return Entrance() + new Vector3(Random.Range(-characterEntranceRange, characterEntranceRange),0, Random.Range(-characterEntranceRange, characterEntranceRange));
		}
        public void OnDrawGizmosSelected()
        {
			Gizmos.DrawSphere(Entrance(), characterEntranceRange);
		}
		private Vector3 Entrance() 
		{
			return transform.position + transform.forward * (characterEntranceRange + characterEntranceOffset);
		}

	}
}
