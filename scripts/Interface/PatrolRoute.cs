
using System.Collections.Generic;
using UnityEngine;

namespace Assets.IntenseTPS.Scripts.AI
{

    public class PatrolRoute : MonoBehaviour
    {
#if UNITY_EDITOR
        public bool _fold;
#endif
        public bool fast = false;
        public float randomRadius = 2f;
        public List<Vector3> patrolPositions;
        public Transform follow;
        private int numPoints;
        private Quaternion[] rotations;
        private Vector3[] points;
        private float[] distances;
        public bool patrolLoop = false;

        public Quaternion[] PatrolRotations { get { return rotations; } }
        //this being here will save GC allocs
        public float Length { get; private set; }
        private int p1n;
        private int p2n;
        private float i;
        private bool start = false;
        private float updateTimer = 0;
        public void FixedUpdate()
        {
            if (follow) 
            {
                updateTimer += Time.fixedUnscaledDeltaTime;
                if (updateTimer > 2f)
                {
                    if (patrolPositions.Count > 0)
                    {
                        patrolPositions[^1] = follow.position;
                    }
                    else 
                    {
                        patrolPositions.Add(follow.position); 
                    }
                    if (patrolPositions.Count > 1)
                    {
                        CachePositionsAndDistances();
                    }
                    numPoints = patrolPositions.Count;
                    updateTimer = 0f;
                }
            }
        }
        public void StartUp()
        {
            if (start) return;
            start = true;
            if (patrolPositions.Count > 1)
            {
                CachePositionsAndDistances();
            }
            numPoints = patrolPositions.Count;
        }
        public RoutePoint GetRoutePoint(float dist)
        {
            // position and direction
            Vector3 p1 = GetRoutePosition(dist);
            Vector3 p2 = GetRoutePosition(dist + 0.1f);
            Vector3 delta = p2 - p1;
            return new RoutePoint(p1, delta.normalized);
        }
        public Vector3 GetRoutePosition(float dist)
        {
            int point = 0;

            if (Length == 0)
            {
                Length = distances[distances.Length - 1];
            }

            dist = Mathf.Repeat(dist, Length);

            while (distances[point] < dist)
            {
                ++point;
            }

            // get nearest two points, ensuring points wrap-around start & end of circuit
            p1n = ((point - 1) + numPoints) % numPoints;
            p2n = point;
            // found point numbers, now find interpolation value between the two middle points
            i = Mathf.InverseLerp(distances[p1n], distances[p2n], dist);
            // simple linear lerp between the two points:
            p1n = ((point - 1) + numPoints) % numPoints;
            p2n = point;
            return Vector3.Lerp(points[p1n], points[p2n], i);
        }
        private void CachePositionsAndDistances()
        {
            // transfer the position of each point and distances between points to arrays for
            // speed of lookup at runtime
            rotations = new Quaternion[patrolPositions.Count + 1];
            points = new Vector3[patrolPositions.Count + 1];
            distances = new float[patrolPositions.Count + 1];

            float accumulateDistance = 0;
            for (int i = 0; i < points.Length; ++i)
            {
                var t1 = patrolPositions[(i) % patrolPositions.Count];
                var t2 = patrolPositions[(i + 1) % patrolPositions.Count];
                if (t1 != null && t2 != null)
                {
                    Vector3 p1 = t1;
                    Vector3 p2 = t2;
                    rotations[i] = Quaternion.LookRotation((p2-p1),Vector3.up);
                    points[i] = patrolPositions[i % patrolPositions.Count];
                    distances[i] = accumulateDistance;
                    accumulateDistance += (p1 - p2).magnitude;
                }
            }
        }
        public struct RoutePoint
        {
            public Vector3 position;
            public Vector3 direction;


            public RoutePoint(Vector3 position, Vector3 direction)
            {
                this.position = position;
                this.direction = direction;
            }
        }
    }
}