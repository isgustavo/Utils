using UnityEngine;

namespace odt.util.thirdperson
{
    public class ControllableActorIdleState : State
    {
        private Animator animator;

        public ControllableActorIdleState(Transform transform) : base(transform)
        {
            animator = transform.GetComponentInChildren<Animator>();
        }

        public override void OnEnterState(State previousState = null)
        {
            base.OnEnterState(previousState);

            animator.SetFloat("Forward", 0f, 0.1f, Time.deltaTime);
            animator.SetFloat("Turn", 0f, 0.1f, Time.deltaTime);
        }
    }
}
