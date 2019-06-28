using UnityEngine;

namespace odt.util
{
    public abstract class State
    {
        protected Transform transform;

        protected State(Transform transform)
        {
            this.transform = transform;
        }

        public virtual void OnEnterState(State previousState = null) { }
        public virtual void OnUpdateState() { }
        public virtual void OnFixedUpdateState() { }
        public virtual void OnLeaveState() { }
    }
}
