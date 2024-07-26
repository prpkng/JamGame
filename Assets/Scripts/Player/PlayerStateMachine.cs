using Game.Player.States;
using Game.Systems;
using UnityEngine;
namespace Game.Player
{
#if UNITY_EDITOR
using UnityEditor;

    [CustomEditor(typeof(PlayerStateMachine))]
    public class PlayerStateMachineEditor: Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var obj = (PlayerStateMachine)target;
            EditorGUILayout.LabelField("Current State", obj.machine?.currentStateName);
        }
    }
#endif

    public class PlayerStateMachine : MonoBehaviour
    {
        public StateMachine machine;

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