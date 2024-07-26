using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Game.Systems
{
    public class StateMachineBuilder
    {
        private List<(State, string)> states = new();
        private int defaultState = 0;
        public StateMachineBuilder WithState(State state, string name, bool @default = false)
        {
            if (@default)
                defaultState = states.Count;
            states.Add((state, name));
            return this;
        }

        public StateMachine Build()
        {
            var machine = new StateMachine()
            {
                states = states.ToArray(),
                currentState = states[defaultState].Item1,
                currentStateName = states[defaultState].Item2
            };
            foreach (var (state, _) in states)
                state.machine = machine;

            machine.currentState.Entered(new object[] { });
            return machine;
        }
    }

    public class StateMachine
    {
        internal (State state, string name)[] states;
        internal State currentState;

        public string currentStateName;

        public void Switch(string name, object[] args = null)
        {
            foreach (var (state, _name) in states)
            {
                if (name == _name)
                {
                    currentState.Exited();
                    currentStateName = name;
                    currentState = state;
                    currentState.Entered(args ?? (new object[] { }));
                    return;
                }
            }

            Debug.LogError($"No state found with name: {name}");
        }

    }


    public abstract class State
    {
        public StateMachine machine;
        public abstract void Entered(object[] args);
        public abstract void Exited();
        public abstract void Update();
        public abstract void FixedUpdate();
        public abstract void LateUpdate();
    }
}