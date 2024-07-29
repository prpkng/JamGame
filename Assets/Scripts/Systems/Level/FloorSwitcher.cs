namespace Game.Systems.Level
{
    using Game.Player;
    using Game.Systems.Common;
    using UnityEngine;

    public class FloorSwitcher : MonoBehaviour
    {
        [SerializeField] private CollisionMessenger collisionMessenger;

        private void OnEnable() =>
            collisionMessenger.EnteredHitbox += SwitchFloor;

        public int floor;
        public void SwitchFloor(Collider _)
        {
            FloorsManager.Instance.ChangeFloor(floor);
        }
    }
}