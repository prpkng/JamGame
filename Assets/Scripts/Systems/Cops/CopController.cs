using System.Collections;
using Game.Systems.Path;
using UnityEngine;

namespace Game.Systems.Cops
{
    using Cysharp.Threading.Tasks;
    using Game.Systems.Path;
    public class CopController : MonoBehaviour
    {

        public enum MovementType { ToHouse, AroundHouse, BackToVan }
        public enum CopBehavior { FrontDoor, Perimeter }

        [SerializeField] private float movementSpeed = 3f;
        [SerializeField] private float rotateSmoothSecs = .1f;

        [SerializeField] private float intervalDelay = .75f;
        [SerializeField] private float goBackDelay = .5f;

        public MovementType currentMovement;
        public CopBehavior behavior;

        private Path path;
        private Vector3 vanLocation;
        private Vector3 frontDoorLocation;

        private float followPathCounter = 0f;

        private bool moveBackwards = false;


        private async void Start()
        {
            while (true)
            {
                await UniTask.WaitForFixedUpdate();

                if (gameObject == null) return;

                switch (currentMovement)
                {
                    case MovementType.ToHouse:

                        var remaining = frontDoorLocation - transform.position;

                        var direction = remaining.normalized;

                        var movementVector = direction * Mathf.Min(movementSpeed * Time.fixedDeltaTime, remaining.magnitude);

                        transform.position += movementVector;

                        if (remaining.sqrMagnitude <= Mathf.Epsilon)
                        {
                            currentMovement = MovementType.AroundHouse;
                            await UniTask.WaitForSeconds(intervalDelay);
                        }

                        followPathCounter = 0;
                        break;
                    case MovementType.AroundHouse:
                        switch (behavior)
                        {
                            case CopBehavior.FrontDoor:
                                break;
                            case CopBehavior.Perimeter:

                                if (moveBackwards)
                                    followPathCounter -= movementSpeed * Time.fixedDeltaTime;
                                else
                                    followPathCounter += movementSpeed * Time.fixedDeltaTime;

                                if (followPathCounter > path.TotalDistance || followPathCounter < 0)
                                {
                                    await UniTask.WaitForSeconds(goBackDelay);
                                    moveBackwards = !moveBackwards;
                                }





                                transform.position = path.EvaluateByDistance(followPathCounter);
                                break;
                        }
                        break;
                    case MovementType.BackToVan:
                        break;
                }
            }
        }

        private Vector3 _lastPos;

        private void FixedUpdate()
        {

            var velocity = transform.position - _lastPos;
            var dir = velocity.normalized;
            if (dir.sqrMagnitude > Mathf.Epsilon)
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.fixedDeltaTime / rotateSmoothSecs);
            _lastPos = transform.position;
        }

        public void SetUp(Vector3 vanLocation, Vector3 frontDoorLocation, Path path = null)
        {
            this.path = path;
            this.vanLocation = vanLocation;
            this.frontDoorLocation = frontDoorLocation;
        }

    }
}