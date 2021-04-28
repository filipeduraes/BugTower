using UnityEngine;

namespace BugTower.Patterns
{
    public abstract class StateMachine : MonoBehaviour
    {
        protected State currentState;

        public virtual void TransitionToState(State state)
        {
            currentState?.ExitAction();
            currentState = state;
            currentState.EntryAction();
        }
    }
}
