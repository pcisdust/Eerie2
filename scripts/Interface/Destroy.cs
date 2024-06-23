using UnityEngine;

namespace Assets.IntenseTPS.Scripts.Common
{
    public class Destroy : MonoBehaviour
    {
        public float destroyTime = 2;
        public float destroyTimeRandomize = 0;
        public bool Disable = false;
        private float countToTime;

        private void Awake()
        {
            destroyTime += Random.value * destroyTimeRandomize;
        }

        private void FixedUpdate()
        {
            countToTime += Time.fixedDeltaTime;
            if (countToTime >= destroyTime)
            {
                countToTime = 0f;
                if (!Disable) Destroy(gameObject);
                else gameObject.SetActive(false);
            }
        }

        public void Rest(float rate=0.5f) 
        { countToTime = destroyTime*rate; }
    }
}