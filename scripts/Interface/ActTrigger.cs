using UnityEngine;
using System.Collections.Generic;
namespace Assets.IntenseTPS.Scripts.Level
{
	public class ActTrigger : MonoBehaviour
	{
		public bool repeatable = false;
        [Space]
		public bool onlyForPlayer = false;
		public List<int> players;
		[Space]
		public bool onlyForOneAiPlanner = false;
		public string aiPlannerCode = "";
		[Space]
		public bool onlyForOneFaction = false;
		public Faction faction;
		[Header("vehicle")]
		public bool onlyForOneVehicle = false;
		public int vehicleCode = -1;
		[Header("action")]
		public string levelAct;

		private bool used = false;
		private float _timer = 0f;
		private bool _update = false;

		public void FixedUpdate()
		{
			if (_update)
			{
				_timer += Time.fixedUnscaledDeltaTime;
				if (_timer > 2f) { used = false; _update = false; }
			}
		}

		public void OnTriggerEnter(Collider collider)
		{
			TriggerOn(collider);
		}
		public void TriggerOn(Collider collider)
		{
			if ( used) return;
			bool act = LevelManager.Trigger(collider,this);
            if (act)
			{ 
				used = true; 
				if (repeatable) 
				{ _timer = 0f; _update = true; } 
			}
		}
	}
}
