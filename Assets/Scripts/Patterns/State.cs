using UnityEngine;

namespace BugTower.Patterns
{
    public abstract class State
    {
        public virtual void EntryAction() { }
        public virtual void UpdateAction() { }
        public virtual void FixedUpdateAction() { }
        public virtual void ExitAction() { }
        public virtual void Trigger(Collider2D collision) { }
    }
}