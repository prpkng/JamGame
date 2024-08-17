using System.Collections;
using Game.Systems.Path;
using Game.Systems.Sound;
using UnityEngine;

namespace Game.Systems.Cops
{
    public class CopsManager : MonoBehaviour
    {
        [SerializeField] private Transform frontDoorLocation;
        [SerializeField] private Transform vanLocation;

        [SerializeField] private PathContainer firstPath;
        [SerializeField] private PathContainer secondPath;


        [Header("References")]
        [SerializeField] private GameObject copPrefab;

        private bool didCopsArrived = false;

        private void Awake()
        {
            Instance = this;
        }



        private void WhenCopsCalled()
        {
            if (didCopsArrived) return;

            Debug.Log("Cops Arrived!");
            didCopsArrived = true;

            BGMManager.SetTension(true);

            var behaviors = new CopController.CopBehavior[] {
                CopController.CopBehavior.FrontDoor,
                CopController.CopBehavior.Perimeter,
                CopController.CopBehavior.Perimeter
            };
            var paths = new Path.Path[] {
                null,
                firstPath.path,
                secondPath.path
            };
            var dests = new Vector3[] {
                frontDoorLocation.position,
                firstPath.path.EvaluateByDistance(0),
                secondPath.path.EvaluateByDistance(0)
            };


            for (int i = 0; i < 3; i++)
            {
                var cop = Instantiate(copPrefab);
                cop.transform.position = vanLocation.position;
                Debug.Assert(cop.TryGetComponent<CopController>(out var c));
                c.behavior = behaviors[i];
                c.SetUp(vanLocation.position, dests[i], paths[i]);
            }

        }


        // Static Area
        public static CopsManager Instance;
        public static void CallCops()
        {
            Instance.WhenCopsCalled();
        }
    }
}