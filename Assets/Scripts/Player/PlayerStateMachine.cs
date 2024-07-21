using Game.Player.States;
using Game.Systems;
using UnityEngine;
namespace Game.Player
{
    public class PlayerStateMachine : MonoBehaviour
    {
        private StateMachine machine;

        private void Awake()
        {
            machine = new StateMachineBuilder()
                    .WithState(new IdleState(), "idle", true)
                    .WithState(new WalkState(), "walk")
                    .Build();
        }

        private void Update()
        {
            machine.currentState.Update();
            print(machine.currentStateName);
        }

        private void FixedUpdate() =>
            machine.currentState.FixedUpdate();

        private void LateUpdate() =>
            machine.currentState.LateUpdate();

    }
}