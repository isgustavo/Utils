using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace odt.util.thirdperson
{
    public class ControllableActorMovingState : State
    {
        readonly float movingTurnSpeed = 360;
        readonly float stationaryTurnSpeed = 180;
        readonly float groundCheckDistance = 0.2f;

        private Transform cameraTranform;

        private Animator animator;

        private Vector3 groundNormal;

        private bool isGrounded;

        private float turnAmount;
        private float forwardAmount;

        public ControllableActorMovingState(Transform transform) : base(transform)
        {
            cameraTranform = Camera.main.transform;
            animator = transform.GetComponentInChildren<Animator>();
        }

        public override void OnUpdateState()
        {
            base.OnUpdateState();

            Vector3 input = ReadInput();

            Move(input);
        }

        private Vector3 ReadInput()
        {
            float horizontal = CustomInput.Instance.GetAxis(Axis.HORIZONTAL);
            float vertical = CustomInput.Instance.GetAxis(Axis.VERTICAL);

            Vector3 camForward = Vector3.Scale(cameraTranform.forward, new Vector3(1, 0, 1)).normalized;
            return vertical * camForward +horizontal * cameraTranform.right;
        }

        private void Move(Vector3 input)
        {
            input.Normalize();
            input = transform.InverseTransformDirection(input);
            CheckGround(transform.position);
            input = Vector3.ProjectOnPlane(input, groundNormal);
            turnAmount = Mathf.Atan2(input.x, input.z);
            forwardAmount = input.z;

            ApplyExtraTurnRotation(turnAmount, forwardAmount);

            UpdateAnimator(turnAmount, forwardAmount);

        }

        void UpdateAnimator(float turn, float forward)
        {
            animator.SetFloat("Forward", forward, 0.1f, Time.deltaTime);
            animator.SetFloat("Turn", turn, 0.1f, Time.deltaTime);
        }

        void ApplyExtraTurnRotation(float turn, float forward)
        {
            // help the character turn faster (this is in addition to root rotation in the animation)
            float turnSpeed = Mathf.Lerp(stationaryTurnSpeed, movingTurnSpeed, forward);
            transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
        }

        void CheckGround(Vector3 position)
        {
            isGrounded = false;
            groundNormal = Vector3.up;
            animator.applyRootMotion = false;

            RaycastHit hitInfo;
            if (Physics.Raycast(position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, groundCheckDistance))
            {
                Debug.Log("GROUND");
                groundNormal = hitInfo.normal;
                isGrounded = true;
                animator.applyRootMotion = true;
            }
        }
    }
}

