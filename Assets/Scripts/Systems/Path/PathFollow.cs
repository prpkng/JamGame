using System.Collections;
using UnityEngine;

namespace Game.Systems.Path
{
    public class PathFollow : MonoBehaviour
    {

        [SerializeField] private PathContainer pathContainer;

        [SerializeField] private float dist;

        void Update()
        {
            transform.position = pathContainer.path.EvaluateByDistance(dist);
        }
    }
}