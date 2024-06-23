using UnityEngine;
using UnityEngine.AI;

namespace Assets.IntenseTPS.Scripts.Level
{
    public class VariableControllDoor : MonoBehaviour
    {
        public bool openWhenVariableIsTrue = true;
        public Transform door;
        public AudioSource sound;
        public Collider visionBlock;
        public ParticleSystem particle;
        public NavMeshObstacle meshObstacle;
        public MeshRenderer meshRenderer;
        public Material closeMaterial;
        public Material openMaterial;
        public Vector3 closePosition;
        public Vector3 openPosition;
        public Vector3 closeRot;
        public Vector3 openRot;

        private float _timer = 0f;
        private bool _update = false;
        private bool _open = false;
        private void FixedUpdate()
        {
            if (_update) 
            {
                _timer += Time.fixedDeltaTime;
                if (_open) 
                {
                    door.localPosition = Vector3.Lerp(closePosition,openPosition, _timer);
                    door.localEulerAngles = Vector3.Lerp(closeRot, openRot, _timer);
                }
                else 
                {
                    door.localPosition = Vector3.Lerp(openPosition,closePosition, _timer);
                    door.localEulerAngles = Vector3.Lerp(openRot, closeRot, _timer);
                }
                if (_timer > 1f) 
                {
                    _update = false;
                }
            }
        }
        public void SetOpen(bool _on,bool _start=false) 
        {
            if (!openWhenVariableIsTrue) _on = !_on;
            _open = _on;
            if (door)
            {
                if (_start)
                {
                    door.localPosition = _on ? openPosition : closePosition;
                    door.localEulerAngles = _on ? openRot : closeRot;
                }
                else
                {
                    if (sound) sound.Play();
                    _timer = 0f;
                    _update = true;
                }
            }
            if (particle)
            {
                if (_open) particle.Stop();
                else particle.Play();
            }
            if (visionBlock)
                visionBlock.enabled = !_open;
            if (meshObstacle)
                meshObstacle.enabled = !_open;
            if (meshRenderer)
            { meshRenderer.material = _open ? openMaterial : closeMaterial; }
        }
    }
}