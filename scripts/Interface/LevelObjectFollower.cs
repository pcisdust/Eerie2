using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.IntenseTPS.Scripts.Level
{
    public class LevelObjectFollower : MonoBehaviour
    {
        public Transform follow;
        public Vector3 followRotate;
        public Vector3 followOffset;
        [Space]
        public Rigidbody physicalbody;
        public Collider physicalCollider;
        [Space]
        public bool needSave = false;
        public string saveCode = "follower0";
        [Space]
        public List<ObjectFollowerState> followerStates;
        private int curState = -1;
        public void SetFollow(Transform _transform) 
        {
            follow = _transform;
            if (physicalbody)
            {
                physicalbody.isKinematic = follow;
                physicalbody.useGravity = !follow;
            }
            if (physicalCollider)
            {
                physicalCollider.enabled = !follow;
            }
        }

        private void LateUpdate()
        {
            if (follow) 
            {
                transform.SetPositionAndRotation(follow.TransformPoint(followOffset), follow.rotation*Quaternion.Euler(followRotate));
            }
        }
        public void ChangeStates(int _state = -1)
        {
            if (_state == curState) return;
            curState = _state;
            if (_state < 0)
            {
                SetFollow(null);
            }
        }
    }
    [Serializable]
    public class ObjectFollowerState
    {
        public List<string> conditions;
        public int playerId;
    }
}