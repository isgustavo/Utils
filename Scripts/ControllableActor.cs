using System.Collections.Generic;
using UnityEngine;

namespace odt.util.thirdperson
{
    public class ControllableActor : Actor
    {
        public static int TURN = Animator.StringToHash("Turn");
        public static int FORWARD = Animator.StringToHash("Forward");

        private void Awake()
        {
            Dictionary<string, State> states = new Dictionary<string, State>
            {
                { nameof(ControllableActorIdleState), new ControllableActorIdleState(transform)},
                { nameof(ControllableActorMovingState), new ControllableActorMovingState(transform)}
            };

            stateMachine = new StateMachine(states, nameof(ControllableActorIdleState));
        }

        protected void Update()
        {
            if(CustomInput.Instance.HasHorizontalOrVerticalInput())
            {
                stateMachine.ChangeState(nameof(ControllableActorMovingState));
            }
            stateMachine?.OnUpdate();
        }

        protected void FixedUpdate()
        {
            stateMachine?.OnFixedUpdate();
        }
    }
}
