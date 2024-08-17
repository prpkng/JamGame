using Game.Player.States;
using Game.Systems;
using UnityEngine;
namespace Game.Player
{
#if UNITY_EDITOR
    using UnityEditor;

    [CustomEditor(typeof(PlayerStateMachine))]
    public class PlayerStateMachineEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var obj = (PlayerStateMachine) target;
            EditorGUILayout.LabelField("Current State", obj.machine?.currentStateName);
        }
    }
#endif

    public class PlayerStateMachine : MonoBehaviour
    {
        public StateMachine machine;

        private void Start()
        {
            machine = new StateMachineBuilder()
                    .WithState(new IdleState(), "idle", true)
                    .WithState(new WalkState(), "walk")
                    .WithState(new RunState(), "run")
                    .WithState(new DragState(), "drag")
                    .Build();
        }

        private void Update() => machine.currentState.Update();

        private void FixedUpdate() =>
            machine.currentState.FixedUpdate();

        private void LateUpdate() =>
            machine.currentState.LateUpdate();

    }
}