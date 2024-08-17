using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Systems.Path
{
    [System.Serializable]
    public class Path
    {
        public enum WrapType
        {
            None,
            Wrap,
            PingPong
        }
        [SerializeField] private List<Vector3> m_Points;
        public List<Vector3> Points => m_Points;
        private readonly List<float> cumulativeDistances;
        private float totalDistance;

        public Path(params Vector3[] points)
        {
            m_Points = points.ToList();
            cumulativeDistances = new List<float>();
            totalDistance = 0f;
            CalculateDistances();
        }

        public void CalculateDistances()
        {
            cumulativeDistances.Clear();
            totalDistance = 0f;

            if (Points.Count < 2)
            {
                return;
            }

            cumulativeDistances.Add(0f);  // Start at the first point

            for (int i = 1; i < Points.Count; i++)
            {
                float distance = Vector3.Distance(Points[i - 1], Points[i]);
                totalDistance += distance;
                cumulativeDistances.Add(totalDistance);
            }
        }

        public void UpdatePoints(List<Vector3> points)
        {
            m_Points = points;
            CalculateDistances();
        }

        public Vector3 EvaluateByDistance(float distance, WrapType wrap = WrapType.Wrap)
        {
            if (Points.Count < 2)
            {
                Debug.LogError("Path must contain at least 2 points.");
                return Vector3.zero;
            }

            // Clamp distance to valid range
            if (distance < 0)
            {
                switch (wrap)
                {
                    case WrapType.None:
                        return Points[0];
                    case WrapType.Wrap:
                        distance = totalDistance - (Mathf.Abs(distance) % totalDistance);
                        break;
                    case WrapType.PingPong:
                        distance = Mathf.PingPong(distance, totalDistance);
                        break;
                }

            }

            if (distance >= totalDistance)
            {
                switch (wrap)
                {
                    case WrapType.None:
                        return Points[^1];
                    case WrapType.Wrap:
                        distance = distance % totalDistance;
                        break;
                    case WrapType.PingPong:
                        distance = Mathf.PingPong(distance, totalDistance);
                        break;
                }
            }

            // Binary search for the segment that contains the distance
            int segmentIndex = cumulativeDistances.BinarySearch(distance);
            if (segmentIndex < 0)
            {
                segmentIndex = ~segmentIndex - 1;  // Get the segment before the closest point
            }

            // Interpolate between the points at segmentIndex and segmentIndex + 1
            float segmentT = Mathf.InverseLerp(
                cumulativeDistances[segmentIndex],
                cumulativeDistances[segmentIndex + 1],
                distance);

            return Vector3.Lerp(Points[segmentIndex], Points[segmentIndex + 1], segmentT);
        }

        public float TotalDistance => totalDistance;
    }
}