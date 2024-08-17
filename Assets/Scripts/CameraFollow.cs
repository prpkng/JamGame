using System.Collections;
using UnityEngine;

namespace Game
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;

        [SerializeField] private float followSpeed;
        void FixedUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, target.position, followSpeed * Time.fixedDeltaTime);
        }
    }
}